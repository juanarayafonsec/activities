using System.ComponentModel.DataAnnotations;

namespace Activities.Application.Validators;

public class LongitudeRangeAttribute() : ValidationAttribute("Longitude must be between -180 and 180.")
{
    private readonly double _min = -180;
    private readonly double _max = 180;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is double latitude)
        {
            if (latitude < _min || latitude > _max)
            {
                return new ValidationResult(ErrorMessage ?? $"Latitude must be between {_min} and {_max}.");
            }
            return ValidationResult.Success;
        }

        return new ValidationResult("Invalid latitude value.");
    }
}
