using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MediatR;
using siwar.Data.Identity;
using siwar.Data.Infrastructure.Configuration;
using siwar.Data.Items;
using siwar.Models;


namespace siwar.Data
{
    public class MonitoringContext : DbContext
    {
        public MonitoringContext(DbContextOptions<MonitoringContext> options) : base(options)
        {
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<IdentityUserLogin<string>> UserLogins { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ItemRelation> ItemRelations { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

    }
}



