using API.Dtos;
using API.Services;
using Asp.Versioning;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/v{apiVersion:apiVersion}/[controller]")]
[ApiVersion(1)]
public class AccountController(UserManager<AppUser> userManager, TokenService tokenService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);

        if (user is null) return Unauthorized();

        return await userManager.CheckPasswordAsync(user, loginDto.Password)
            ? new UserDto
            {
                DisplayName = user.DisplayName, Image = null, Token = tokenService.GenerateToken(user),
                Username = user.UserName
            }
            : Unauthorized();
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
            return BadRequest("Username is taken");

        var user = new AppUser
        {
            UserName = registerDto.Username,
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email
        };

        var result = await userManager.CreateAsync(user, registerDto.Password);

        return result.Succeeded
            ? new UserDto
            {
                DisplayName = user.DisplayName,
                Image = null,
                Token = tokenService.GenerateToken(user),
                Username = user.UserName
            }
            : BadRequest("Problem registering user");
    }
}