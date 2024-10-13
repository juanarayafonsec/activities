using Application.Dtos;
using Application.Profiles;

namespace Application.Mappings;

public static class ActivityMapper
{
    public static Activity Map(this EditActivityDto editActivity)
        => new()
        {
            Id = editActivity.Id,
            Category = editActivity.Category,
            City = editActivity.City,
            Date = editActivity.Date,
            Description = editActivity.Description,
            Title = editActivity.Title,
            Venue = editActivity.Venue
        };

    public static Activity Map(this CreateActivityDto activity)
        => new()
        {
            Category = activity.Category,
            City = activity.City,
            Date = activity.Date,
            Description = activity.Description,
            Title = activity.Title,
            Venue = activity.Venue
        };

    public static void Map(this Activity target, Activity source)
    {
        target.Category = source.Category;
        target.City = source.City;
        target.Date = source.Date;
        target.Description = source.Description;
        target.Title = source.Title;
        target.Venue = source.Venue;
    }

    public static List<ActivityDto> ToActivityDto(this List<Activity> activities)
        => activities.Select(x => new ActivityDto
        {
            Id = x.Id, Category = x.Category, City = x.City, Date = x.Date, Description = x.Description,
            Title = x.Title, Venue = x.Venue, IsCancelled = x.IsCancelled,
            HostUsername = x.Attendees.FirstOrDefault(h => h.IsHost)?.AppUser.UserName, 
            Profiles = x.Attendees.Map()
        }).ToList();
    
    public static ActivityDto ToActivityDto(this Activity activity)
        =>new()
        {
            Id = activity.Id, Category = activity.Category, City = activity.City, Date = activity.Date, Description = activity.Description,
            Title = activity.Title, Venue = activity.Venue, IsCancelled = activity.IsCancelled,
            HostUsername = activity.Attendees.FirstOrDefault(h => h.IsHost)?.AppUser.UserName,
            Profiles = activity.Attendees.Map()
        };
}