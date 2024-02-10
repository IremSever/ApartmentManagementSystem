using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace ApartmentManagementSystem.API.Models
{
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, Guid>
    {
    }
}
