using ApartmentManagementSystem.API.DTOs;
using ApartmentManagementSystem.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ApartmentManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentitiesController(IdentityService identityService, TokenService tokenService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateRequestDto request)
        {
            var response = await identityService.CreateUser(request);
            if(response.AnyError)
                return BadRequest(response);

            return Created("", response);
        }

        public async Task<IActionResult> CreateToken(TokenCreateRequestDto request)
        {
            var response = await tokenService.Create(request);
            
            if(response.AnyError)
                return BadRequest(response);

            return Ok(response);
        }

        //Role Assignment
        public async Task<IActionResult> CreateRole(RoleCreateRequestDto request)
        {
            var response = await identityService.CreateRole(request);

            if (response.AnyError)
                return BadRequest(response);

            return Created("", response);
        }
    }
}
