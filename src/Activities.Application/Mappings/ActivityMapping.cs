

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
            Longitude = createActivity.Longitude,
            Latitude = createActivity.Latitude,
            IsCancelled = false
        };
    }
}