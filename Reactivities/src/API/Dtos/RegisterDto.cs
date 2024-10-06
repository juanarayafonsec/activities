using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public record RegisterDto
{
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    public string DisplayName { get; set; }
    public string Username { get; set; }
};