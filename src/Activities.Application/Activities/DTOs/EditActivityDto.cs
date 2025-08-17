using System.ComponentModel.DataAnnotations;

namespace Activities.Application.Activities.DTOs;
public class EditActivityDto : BaseActivityDto
{
    [Required]
    public Guid? Id { get; set; }

    [Required]
    public bool? IsCancelled { get; set; }
}


