using ApartmentManagementSystem.Repository.Models;
using ApartmentManagementSystem.Service.DTOs;
using ApartmentManagementSystem.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly TokenService _tokenService;

    public AuthController(UserManager<AppUser> userManager, TokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(TokenCreateRequestDto request)
    {
        var response = await _tokenService.Create(request);

        if (response.AnyError)
            return BadRequest(response);

        return Ok(response);
    }
}

