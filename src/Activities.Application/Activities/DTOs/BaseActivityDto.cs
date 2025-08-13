using Activities.Application.Validators;
using System.ComponentModel.DataAnnotations;

namespace Activities.Application.Activities.DTOs;
public class BaseActivityDto
{
    [Required]
    [MaxLength(100, ErrorMessage = "Title mus not exceed 100 characters")]
    public required string Title { get; set; }

    [Required]
    [DateGreaterThanNow]
    public DateTime Date { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required string Category { get; set; }

    [Required]
    public required string City { get; set; }

    [Required]
    public required string Venue { get; set; }

    [Required]
    [LatitudeRangeAttribute]
    public double? Latitude { get; set; }

    [Required]
    [LongitudeRangeAttribute]
    public double? Longitude { get; set; }
}
