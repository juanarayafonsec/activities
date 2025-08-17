using Activities.Application.Validators;
using System.ComponentModel.DataAnnotations;

namespace Activities.Application.Activities.DTOs;
public class BaseActivityDto
{
    [Required]
    [MaxLength(100, ErrorMessage = "Title mus not exceed 100 characters")]
    public string? Title { get; set; }

    [Required]
    [DateGreaterThanNow]
    public DateTime Date { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public string? Category { get; set; }

    [Required]
    public string? City { get; set; }

    [Required]
    public string? Venue { get; set; }

    [Required]
    [Range(-90, 90)]
    public double? Latitude { get; set; }

    [Required]
    [Range(-180,180)]
    public double? Longitude { get; set; }
}
