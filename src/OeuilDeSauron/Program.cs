using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.Console;
using Hangfire.Console.Extensions;
using Hangfire.Dashboard;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using Microsoft.EntityFrameworkCore;
using siwar;
using siwar.Data.Identity;
using siwar.Data.Infrastructure;
using siwar.Domain;
using siwar.Domain.Extensions;
using siwar.Domain.Interfaces;
using siwar.Filters;
using siwar.Hosting;
using siwar.Infrastructure;
using siwar.Services;
using siwar.Telemetry;
using System.Net;
using siwar.Data;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var supportedCultures = new List<CultureInfo> { new("fr-FR") };
var dbConnectionString = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<MonitoringContext>(options => options.UseSqlServer(dbConnectionString, builder => builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));


// Logging
builder.Host.UseSerilog((context, configuration) =>
    configuration.Enrich.WithMachineName()
        .ReadFrom.Configuration(context.Configuration));

// Application Insights
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddApplicationInsightsTelemetryProcessor<ExcludeRequestTelemetryProcessor>();

// Mini Profiler
builder.Services.AddMiniProfiler(options =>
    {
        options.RouteBasePath = "/profiler";
        options.ResultsAuthorize = request => request.HttpContext.User.Identity is not null && request.HttpContext.User.Identity.IsAuthenticated && request.HttpContext.User.IsAdmin();
        options.ResultsListAuthorize = request => request.HttpContext.User.Identity is not null && request.HttpContext.User.Identity.IsAuthenticated && request.HttpContext.User.IsAdmin();
    })
    .AddEntityFramework();

if (builder.Environment.IsDevelopment())
{
    // Developer Page
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

// Health Checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<MonitoringContext>(dbConnectionString)
    .AddSqlServer(dbConnectionString, name: "Azure SQL Database - siwar",
        tags: new[] { "Azure", "SQL" })
    .AddSqlServer(dbConnectionString, name: "Azure SQL Database - Jobs",
        tags: new[] { "Azure", "SQL", "Hangfire" })
    .AddAzureBlobStorage(dbConnectionString, "documents", name: "Azure Storage",
        tags: new[] { "Azure", "Storage" })
    .AddHangfire(options => options.MaximumJobsFailed = 1, "Hangfire", tags: new[] { "Hangfire" });

// Identity
builder.Services.AddIdentity<User, Role>(options => options.User.RequireUniqueEmail = true)
    .AddEntityFrameworkStores<MonitoringContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.User.RequireUniqueEmail = builder.Environment.IsProduction();
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
});

// Token duration
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
    options.TokenLifespan = TimeSpan.FromHours(1));

// Application Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "Authentication";
    options.ExpireTimeSpan = TimeSpan.FromDays(10);
});

if (!builder.Environment.IsDevelopment())
{
    // Data Protection
    builder.Services.AddDataProtection()
        .PersistKeysToAzureBlobStorage(dbConnectionString,
            builder.Configuration.GetValue<string>("DataProtection:Container"),
            builder.Configuration.GetValue<string>("DataProtection:Blob"))
        .SetApplicationName("siwar");
}

// Cache
builder.Services.AddDistributedMemoryCache();

// Authentication
builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.Authority = string.Empty;
        options.RequireHttpsMetadata = !builder.Environment.IsEnvironment("Development");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Authentication:Jwt:Key"))),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.ConfigureApplicationCookie(options =>
    options.Events.OnRedirectToAccessDenied =
        options.Events.OnRedirectToLogin = c =>
        {
            c.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.FromResult<object>(null);
        });

// Authorization
builder.Services.AddAuthorization();

// GDPR
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.ConsentCookie.Name = "Consent";
    options.CheckConsentNeeded = _ => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

// Antiforgery
builder.Services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
// Hangfire
builder.Services.AddHangfire(config =>
        config.UseConsole()
            .UseSqlServerStorage(dbConnectionString))
    .AddHangfireConsoleExtensions();
builder.Services.AddHangfireServer(options => options.WorkerCount = 1);

// Localization
builder.Services.AddLocalization();

// Hosted Services
builder.Services.AddHostedService<HangfireService>();

// Domains
builder.Services.AddDomain(builder.Configuration, builder.Environment);

// Current User Service
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();

// Mail
builder.Services.AddMailing(builder.Environment);

// Fluent Validation
builder.Services.AddFluentValidationClientsideAdapters();

// Mvc
builder.Services.AddMvc()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

var app = builder.Build();

// Security Headers
app.UseSecurityHeaders();

if (app.Environment.IsDevelopment())
{
    // Exception Handling
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    // Strict Transport Security
    app.UseHsts();
}

// Static Files
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = context =>
        context.Context.Response.Headers.Append("Cache-Control", "public,max-age=31536000")
});

// GDPR
app.UseCookiePolicy();

// Routing
app.UseRouting();

// Authentication
app.UseAuthentication();
app.UseAuthorization();

// Localization
app.UseRequestLocalization(options =>
{
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.SetDefaultCulture("fr-FR");
});

// Response Caching
app.UseResponseCaching();

// Mini Profiler
app.UseMiniProfiler();

// Mvc
app.MapHealthChecks("/health",
        new HealthCheckOptions { Predicate = _ => true, ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
app.MapControllerRoute("default", "{controller=App}/{action=Index}");
app.MapHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = app.Environment.IsProduction()
            ? new[] { new HangfireAuthorizationFilter() }
            : Array.Empty<IDashboardAuthorizationFilter>(),
    IsReadOnlyFunc = context =>
        app.Environment.IsProduction() && context.GetHttpContext().User.IsInRole("Administrator")
});
app.MapFallbackToController("Index", "App");

app.Run();
