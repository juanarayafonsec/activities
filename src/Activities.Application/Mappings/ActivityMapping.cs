using Activities.Application.Activities.DTOs;
using Activities.Domain.Entity;

namespace Activities.Application.Mappings;

public static class ActivityMapping
{
    public static Activity Map(this CreateActivityDto createActivity)
    {
        return new Activity
        {
            Title = createActivity.Title,
            Category = createActivity.Category,
            Date = createActivity.Date,
            Description = createActivity.Description,
            Venue = createActivity.Venue,
            City = createActivity.City,
            Longitude = createActivity?.Longitude ?? 0,
            Latitude = createActivity?.Latitude ?? 0,
            IsCancelled = false
        };
    }

    public static void Map(this Activity activity, EditActivityDto editActivity)
    {
        activity.Title = editActivity.Title;
        activity.Category = editActivity.Category;
        activity.Date = editActivity.Date; 
        activity.Description = editActivity.Description;
        activity.Venue = editActivity.Venue;
        activity.City = editActivity.City;
        activity.Longitude = editActivity?.Longitude ?? 0;
        activity.Latitude = editActivity?.Latitude ?? 0;
        activity.IsCancelled = editActivity.IsCancelled;
    }
}