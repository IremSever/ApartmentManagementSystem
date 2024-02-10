using ApartmentManagementSystem.API.DTOs;
using ApartmentManagementSystem.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ApartmentManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentitiesController(IdentityService identityService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateRequestDto request)
        {
            var response = await identityService.CreateUser(request);
            if(response.AnyError)
            {
                return BadRequest(response);
            }

            return Created("", response);
        }
    }
}
