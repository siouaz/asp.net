using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MediatR;
using OeuilDeSauron.Data.Identity;
using OeuilDeSauron.Data.Infrastructure.Configuration;
using OeuilDeSauron.Data.Items;
using OeuilDeSauron.Models;
using Models;


namespace OeuilDeSauron.Data
{
    public class MonitoringContext : IdentityDbContext
    {
        public MonitoringContext(DbContextOptions<MonitoringContext> options) : base(options)
        {
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ApiHealth> ApiHealths { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ItemRelation> ItemRelations { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

    }
}



