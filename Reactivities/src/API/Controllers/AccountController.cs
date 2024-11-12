using System.Security.Claims;
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
        var user = await userManager.Users.Include(p => p.Photos)
            .FirstOrDefaultAsync(x => x.Email == loginDto.Email);

        if (user is null) return Unauthorized();

        return await userManager.CheckPasswordAsync(user, loginDto.Password)
            ? CreateUserObject(user)
            : Unauthorized();
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
            return ModelError("username", "Username is taken");
        if (await userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            return ModelError("email", "Email is taken");


        var user = new AppUser
        {
            UserName = registerDto.Username,
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email
        };

        var result = await userManager.CreateAsync(user, registerDto.Password);

        return result.Succeeded
            ? CreateUserObject(user)
            : BadRequest("Problem registering user");
    }

    [HttpGet]
    public async Task<ActionResult<UserDto>> GetUser()
    {
        var user = await userManager.Users.Include(p => p.Photos)
            .FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));
        return CreateUserObject(user);
    }

    private UserDto CreateUserObject(AppUser user)
        => new()
        {
            DisplayName = user.DisplayName,
            Image = user.Photos?.FirstOrDefault(p => p.IsMain)?.Url,
            Token = tokenService.GenerateToken(user),
            Username = user.UserName
        };
    
    private ActionResult ModelError(string key, string errorMessage)
    {
        ModelState.AddModelError(key, errorMessage);
        return ValidationProblem();
    }
}