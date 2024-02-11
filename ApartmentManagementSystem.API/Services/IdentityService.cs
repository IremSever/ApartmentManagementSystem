using Microsoft.AspNetCore.Identity;
using ApartmentManagementSystem.API.Models;
using ApartmentManagementSystem.API.DTOs;
using ApartmentManagementSystem.API.DTOs.Shared;
using System.Security.Claims;
using Microsoft.VisualBasic;

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

            return ResponseDto<Guid
                >.Success(appUser.Id);
        }

        //Create Role
        public async Task<ResponseDto<string>> CreateRole(RoleCreateRequestDto request)
        {
            var appRole = new AppRole
            {
                Name = request.RoleName
            };


            //Check Role
            var hasRole = await roleManager.RoleExistsAsync(appRole.Name);
            IdentityResult? roleCreateResult = null;
            if (!hasRole)
                roleCreateResult = await roleManager.CreateAsync(appRole);

            if (roleCreateResult is not null && roleCreateResult.Succeeded)
            {
                var errorList = roleCreateResult.Errors.Select(x => x.Description).ToList();
                return ResponseDto<string>.Fail(errorList);
            }

            //Find User
            var hasUser = await userManager.FindByIdAsync(request.UserId);
            if(hasUser is null)
                return ResponseDto<String>.Fail("User not found!");

            //Role Assigment
            var roleAssignResult = await userManager.AddToRoleAsync(hasUser, appRole.Name);
            if (!roleAssignResult.Succeeded)
            {
                var errorList = roleAssignResult.Errors.Select(x => x.Description).ToList();
                return ResponseDto<string>.Fail(errorList);
            }
           
            return ResponseDto<string>.Success(string.Empty);
        }
    } 
}
