using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using ApartmentManagementSystem.Repository.Models;

namespace ApartmentManagementSystem.Service.Requirements
{
    public class ResidentOwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, AppUser>
    {
        private readonly UserManager<AppUser> _userManager;

        public ResidentOwnerAuthorizationHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                             OperationAuthorizationRequirement requirement,
                                                             AppUser resident)
        {
            if (context.User == null || resident == null)
            {
                return;
            }

            // Get the current user
            var currentUser = await _userManager.GetUserAsync(context.User);

            if (currentUser != null && await _userManager.IsInRoleAsync(currentUser, "ResidentOwner"))
            {
                if (resident.ResidentId == currentUser.Id)
                {
                    context.Succeed(requirement); 
                }
            }
        }
    }
}
