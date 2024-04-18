using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MediatR;
using siwar.Data.Identity;
using siwar.Data.Infrastructure.Configuration;
using siwar.Data.Items;

namespace siwar.Data.Infrastructure;

/// <summary>
/// siwar database context.
/// </summary>
public class siwarContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>, IUnitOfWork
{
    public const string Schema = "dbo";

    private readonly IMediator _mediator;

    #region DbSet

    public DbSet<Item> Items { get; set; }

    public DbSet<List> Lists { get; set; }

    public DbSet<ItemRelation> ItemRelations { get; set; }

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="siwarContext" /> class.
    /// </summary>
    /// <param name="options">Creation options. Useful when using InMemory driver for testing.</param>
    /// <param name="mediator"><see cref="IMediator"/> instance.</param>
    public siwarContext(DbContextOptions<siwarContext> options, IMediator mediator) : base(options) =>
        _mediator = mediator;

    #region Configuration

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Schema
        builder.HasDefaultSchema(Schema);

        base.OnModelCreating(builder);

        // Configuration
        builder.ApplyConfiguration(new ItemConfiguration());
        builder.ApplyConfiguration(new ListConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
        builder.ApplyConfiguration(new ItemRelationConfiguration());

        // Identity
        builder.Entity<User>()
            .ToTable("Users");

        builder.Entity<Role>()
            .ToTable("Roles");

        builder.Entity<IdentityUserLogin<string>>()
            .ToTable("UserLogins");

        builder.Entity<IdentityUserClaim<string>>()
            .ToTable("UserClaims");

        builder.Entity<UserRole>()
            .ToTable("UserRoles");

        builder.Entity<IdentityRoleClaim<string>>()
            .ToTable("RoleClaims");

        builder.Entity<IdentityUserToken<string>>()
            .ToTable("UserTokens");
    }

    #endregion

    #region IUnitOfWork

    /// <inheritdoc />
    async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
    {
        // Dispatch domain events
        await _mediator.DispatchEvents(this);
        // Commit changes
        return await base.SaveChangesAsync(cancellationToken);
    }

    #endregion
}
