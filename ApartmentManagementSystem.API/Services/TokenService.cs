using ApartmentManagementSystem.API.DTOs;
using ApartmentManagementSystem.API.Models;
using ApartmentManagementSystem.API.DTOs.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApartmentManagementSystem.API.Services
{
    public class TokenService(IConfiguration configuration, UserManager<AppUser> userManager)
    {
        public async Task<ResponseDto<TokenCreateResponseDto>> Create(TokenCreateRequestDto request)
        {
            var hasUser = await userManager.FindByNameAsync(request.UserName);
            var checkPassword = await userManager.CheckPasswordAsync(hasUser!, request.Password);

            if (hasUser is null)
                return ResponseDto<TokenCreateResponseDto>.Fail("Username or password is wrong!");
            if (checkPassword == false)
                return ResponseDto<TokenCreateResponseDto>.Fail("Username or password is wrong");

            var signatureKey = configuration.GetSection("TokenOptions")!["SignatureKey"]!;
            var expireAsHour = configuration.GetSection("TokenOptions")!["Expire"]!;
            var issuer = configuration.GetSection("TokenOptions")!["Issuer"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signatureKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claimList = new List<Claim>();
            var userIdAsClaim = new Claim(ClaimTypes.NameIdentifier, hasUser.Id.ToString());
            var userNameAsClaim = new Claim(ClaimTypes.Name, hasUser.UserName!);
            var idAsClaim = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());

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
