using System.ComponentModel.DataAnnotations;

namespace Activities.Application.Validators;

public class LatitudeRangeAttribute() : ValidationAttribute("Latitude must be between -90 and 90.")
{
    private readonly double _min = -90;
    private readonly double _max = 90;

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
