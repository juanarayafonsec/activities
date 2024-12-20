using System.Collections;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUser : IdentityUser
{
    public string DisplayName { get; set; }
    public string Bio { get; set; }
    public ICollection<ActivityAttendee> Activities { get; set; }
    public ICollection<Photo> Photos { get; set; }
}