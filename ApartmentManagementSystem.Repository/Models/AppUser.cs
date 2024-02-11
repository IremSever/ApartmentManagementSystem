using ApartmentManagementSystem.Repository.Models.ManyToMany;
using Microsoft.AspNetCore.Identity;

namespace ApartmentManagementSystem.Repository.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public Guid ResidentId { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string IdentityNumber { get; set; } = default!;
        public string PaymentMethod { get; set; } = default!;
        public virtual ICollection<Payment> Payments { get; set; } = default!;
        public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
        public virtual ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
        public ICollection<ResidentOwner> ResidentOwners { get; } = default!;
    }
}
