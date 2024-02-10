using Microsoft.AspNetCore.Identity;
using ApartmentManagementSystem.API.Models;
using ApartmentManagementSystem.API.DTOs;
using ApartmentManagementSystem.API.DTOs.Shared;
using System.Security.Claims;

namespace ApartmentManagementSystem.API.Services
{
    public class IdentityService(
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        SignInManager<AppUser> signInManager)
    {
        public UserManager<AppUser> UserManager { get; set; } = userManager;
        public RoleManager<AppRole> RoleManager { get; set; } = roleManager;

        public SignInManager<AppUser> SignInManager { get; set; } = signInManager;

        public async Task<ResponseDto<Guid>> CreateUser(UserCreateRequestDto request)
        {
            var appUser = new AppUser
            {
                UserName = request.UserName,
                Email = request.UserEmail
            };

            var result = await userManager.CreateAsync(appUser, request.Password);

            if (!result.Succeeded)
            {
                var errorList = result.Errors.Select(x => x.Description).ToList();
                return ResponseDto<Guid>.Fail(errorList);
            }

            return ResponseDto<Guid>.Success(appUser.Id);
        }
    }
}
