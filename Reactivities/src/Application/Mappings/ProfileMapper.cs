using Application.Profiles;

namespace Application.Mappings;

public static class ProfileMapper
{
    public static List<Profile> Map(this ICollection<ActivityAttendee> activityAttendees) =>
        activityAttendees.Where(a => a.AppUser is not null)
            .Select(x => new Profile { Username = x.AppUser?.UserName, DisplayName = x.AppUser?.DisplayName }).ToList();
}