using Application.Dtos;

namespace Application.Mappings;

public static class AttendeeMapper
{
    public static List<AttendeeDto> Map(this ICollection<ActivityAttendee> activityAttendees) =>
        activityAttendees.Where(a => a.AppUser is not null)
            .Select(x => new AttendeeDto
            {
                Username = x.AppUser?.UserName, DisplayName = x.AppUser?.DisplayName,
                Image = x.AppUser?.Photos?.FirstOrDefault(p => p.IsMain)?.Url
            }).ToList();
}