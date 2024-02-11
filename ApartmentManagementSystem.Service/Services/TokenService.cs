using ApartmentManagementSystem.Service.DTOs;
using ApartmentManagementSystem.Repository.Models;
using ApartmentManagementSystem.Service.DTOs.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ApartmentManagementSystem.Service.Services
{
    public class TokenService(IConfiguration configuration, UserManager<AppUser> userManager)
    {
        public async Task<ResponseDto<TokenCreateResponseDto>> Create(TokenCreateRequestDto request)
        {
            var hasUser = await userManager.FindByNameAsync(request.UserName);

            if (hasUser == null || !await userManager.CheckPasswordAsync(hasUser, request.Password))
                return ResponseDto<TokenCreateResponseDto>.Fail("Username or password is wrong!");

            var signatureKey = configuration.GetSection("TokenOptions")!["SignatureKey"]!;
            var expireAsHour = configuration.GetSection("TokenOptions")!["Expire"]!;
            var issuer = configuration.GetSection("TokenOptions")!["Issuer"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signatureKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claimList = new List<Claim>();
            var userIdAsClaim = new Claim(ClaimTypes.NameIdentifier, hasUser.Id.ToString());
            var userNameAsClaim = new Claim(ClaimTypes.Name, hasUser.UserName!);
            var idAsClaim = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());

            var userClaims = await userManager.GetClaimsAsync(hasUser);
            foreach(var claim in userClaims)
            {
                claimList.Add(new Claim(claim.Type, claim.Value));
            }

            claimList.Add(userIdAsClaim);
            claimList.Add(userNameAsClaim);
            claimList.Add(idAsClaim);
            foreach (var role in await userManager.GetRolesAsync(hasUser))
            {
                claimList.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(Convert.ToDouble(expireAsHour)),
                signingCredentials: signingCredentials,
                claims: claimList,
                issuer: issuer);

            var responseDto = new TokenCreateResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };

            return ResponseDto<TokenCreateResponseDto>.Success(responseDto);
        }
        
    }
}
