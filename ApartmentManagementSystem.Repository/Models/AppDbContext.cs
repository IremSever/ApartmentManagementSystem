using ApartmentManagementSystem.Repository.Models.ManyToMany;
using Microsoft.EntityFrameworkCore;

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
                .HasOne(a => a.Residents)
                .WithMany(u => u.Apartments)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Resident)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.ResidentId)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Apartments)
                .WithMany(a => a.Payments)
                .HasForeignKey(p => p.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ResidentOwner>()
               .HasMany(ro => ro.Tenants)
               .WithMany(t => t.ResidentOwners)
               .UsingEntity(join => join.ToTable("ResidentOwnerTenants"));
  

            base.OnModelCreating(modelBuilder);
        }
    }
}
