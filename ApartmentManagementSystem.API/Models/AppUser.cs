using Microsoft.AspNetCore.Identity;

namespace ApartmentManagementSystem.API.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public int IdentityNumber {  get; set; }
    }
}
