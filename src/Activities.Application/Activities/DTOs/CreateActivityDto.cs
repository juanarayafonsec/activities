using System.ComponentModel.DataAnnotations;

namespace Activities.Application.Activities.DTOs;
public class CreateActivityDto
{
    [Required]
    public required string Title { get; set; }
    
    [Required]
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
    public double Latitude { get; set; }
    
    [Required]
    public double Longitude { get; set; }
}

