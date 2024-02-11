using Microsoft.AspNetCore.Identity;
using ApartmentManagementSystem.Repository.Models;
using ApartmentManagementSystem.Service.DTOs;
using ApartmentManagementSystem.Service.DTOs.Shared;
using System.Security.Claims;

namespace ApartmentManagementSystem.Service.Services
{
    public class IdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IdentityService(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<ResponseDto<Guid>> CreateUser(UserCreateRequestDto request)
        {
            var appUser = new AppUser
            {
                UserName = request.UserName,
                Email = request.UserEmail
            };

            var result = await _userManager.CreateAsync(appUser, request.Password);

            if (!result.Succeeded)
            {
                var errorList = result.Errors.Select(x => x.Description).ToList();
                return ResponseDto<Guid>.Fail(errorList);
            }

            if (request.IsResidentOwner)
            {
                var resultAsClaim = await _userManager.AddClaimAsync(appUser, new Claim("IsResidentOwner", "true"));
                if (!resultAsClaim.Succeeded)
                    return ResponseDto<Guid>.Fail(resultAsClaim.Errors.Select(x => x.Description).ToList());
            }

            return ResponseDto<Guid>.Success(appUser.Id);
        }

        //Create Role
        public async Task<ResponseDto<string>> CreateRole(RoleCreateRequestDto request)
        {
            var appRole = new AppRole
            {
                Name = request.RoleName
            };

            //Check Role
            var hasRole = await _roleManager.RoleExistsAsync(appRole.Name);
            IdentityResult? roleCreateResult = null;
            if (!hasRole)
                roleCreateResult = await _roleManager.CreateAsync(appRole);

            if (roleCreateResult is not null && roleCreateResult.Succeeded)
            {
                var errorList = roleCreateResult.Errors.Select(x => x.Description).ToList();
                return ResponseDto<string>.Fail(errorList);
            }

            //Find User
            var hasUser = await _userManager.FindByIdAsync(request.UserId);
            if (hasUser is null)
                return ResponseDto<String>.Fail("User not found!");

            //Role Assignment
            var roleAssignResult = await _userManager.AddToRoleAsync(hasUser, appRole.Name);
            if (!roleAssignResult.Succeeded)
            {
                var errorList = roleAssignResult.Errors.Select(x => x.Description).ToList();
                return ResponseDto<string>.Fail(errorList);
            }

            return ResponseDto<string>.Success(string.Empty);
        }
    }
}
