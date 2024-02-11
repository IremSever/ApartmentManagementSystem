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
            modelBuilder.Entity<Apartment>()
               .HasOne(a => a.Resident)
               .WithMany(u => u.Apartments)
               .HasForeignKey(a => a.ResidentId)
               .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Resident)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.ResidentId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Apartment)
                .WithMany(a => a.Payments)
                .HasForeignKey(p => p.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<ResidentOwner>()
                .HasMany(ro => ro.Tenants)
                .WithMany(t => t.ResidentOwners)
                .UsingEntity<Dictionary<string, object>>(
                    "ResidentOwnerTenants",
                    j => j.HasOne<Tenant>().WithMany().HasForeignKey("TenantId"),
                    j => j.HasOne<ResidentOwner>().WithMany().HasForeignKey("ResidentOwnerId"),
                    j =>
                    {
                        j.HasKey("TenantId", "ResidentOwnerId");
                        j.ToTable("ResidentOwnerTenants");
                    });

            base.OnModelCreating(modelBuilder);
        }
    }
}
