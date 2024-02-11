using Microsoft.AspNetCore.Identity;

namespace ApartmentManagementSystem.Repository.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public Guid ResidentId { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string IdentityNumber { get; set; } = default!;
        public string PaymentMethod { get; set; } = default!; // Credit Card/Cash
        public virtual ICollection<Payment> Payments { get; set; } = default!;
    }
}
