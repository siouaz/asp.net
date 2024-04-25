using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MediatR;
using siwar.Data.Identity;
using siwar.Data.Infrastructure.Configuration;
using siwar.Data.Items;
using siwar.Domain.Models;


namespace siwar.Data
{
    public class MonitoringContext : DbContext
    {
        public MonitoringContext(DbContextOptions<MonitoringContext> options) : base(options)
        {
        }
        public DbSet<siwar.Domain.Models.Project> Projects { get; set; }

    }
}



