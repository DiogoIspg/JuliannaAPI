using JullianaDomainCore.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace JullianaDomainCore
{
    public class JullianaDbContext : DbContext, IDbContext
    {
        private readonly IConfiguration configuration;

        public DbSet<JewelrySaved> SavedJewelries { get; set; }
        public DbSet<JewelryOrder> JewelryOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JewelryOrder>().HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }

        public JullianaDbContext(DbContextOptions<JullianaDbContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("JullianaDb"));
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();
            var added = this.ChangeTracker.Entries()
                        .Where(t => t.State == EntityState.Added)
                        .Select(t => t.Entity)
                        .ToArray();

            foreach (var entity in added)
            {
                if (entity is EntityBase)
                {
                    var track = entity as EntityBase;
                    track.CreatedDate = DateTime.Now;
                    //TODO: when users are added track.CreatedBy = UserId;
                }
            }

            var modified = this.ChangeTracker.Entries()
                        .Where(t => t.State == EntityState.Modified)
                        .Select(t => t.Entity)
                        .ToArray();

            foreach (var entity in modified)
            {
                if (entity is EntityBase)
                {
                    var track = entity as EntityBase;
                    track.ModifiedDate = DateTime.Now;
                    //TODO: when users are added track.ModifiedBy = UserId;
                }
            }

            return base.SaveChanges();
        }
    }
}