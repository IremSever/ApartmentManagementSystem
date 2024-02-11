using ApartmentManagementSystem.Repository.Models.ManyToMany;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ApartmentManagementSystem.Repository.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ResidentOwner> ResidentOwners { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Bill>()
                .Property(b => b.Amount)
                .HasColumnType("decimal(18,2)");
            
            modelBuilder.Entity<ResidentOwner>()
                .HasMany(ro => ro.Tenant)
                .WithMany(t => t.ResidentOwner)
                .UsingEntity(j => j.ToTable("ResidentOwnerTenants"));
        }
    }
}
