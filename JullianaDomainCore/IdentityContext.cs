using JullianaApi.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace JullianaDomainCore
{
    public partial class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration configuration;

        public IdentityContext(DbContextOptions<IdentityContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("JullianaDb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<ApplicationUser>()
                .Property(au => au.Id)
                .HasMaxLength(36); // guid length

            modelBuilder
                .Entity<IdentityRole>()
                .Property(ir => ir.Id)
                .HasMaxLength(36);

            modelBuilder
                .Entity<IdentityUserLogin<string>>()
                .Property(iul => iul.LoginProvider)
                .HasMaxLength(128);

            modelBuilder
                .Entity<IdentityUserLogin<string>>()
                .Property(iul => iul.ProviderKey)
                .HasMaxLength(128);

            modelBuilder
                .Entity<IdentityUserToken<string>>()
                .Property(iul => iul.LoginProvider)
                .HasMaxLength(128);

            modelBuilder
                .Entity<IdentityUserToken<string>>()
                .Property(iul => iul.Name)
                .HasMaxLength(128);
        }
    }
}