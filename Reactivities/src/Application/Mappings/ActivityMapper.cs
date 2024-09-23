﻿using Application.Dtos;

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
}