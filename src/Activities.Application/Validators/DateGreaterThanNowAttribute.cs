using System.ComponentModel.DataAnnotations;

namespace Activities.Application.Validators;

public class DateGreaterThanNowAttribute() : ValidationAttribute("The date must be greater than the current date and time.")
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime date && date <= DateTime.Now)
        {
            return new ValidationResult(ErrorMessage ?? "The date must be greater than the current date and time.");
        }
        return ValidationResult.Success;
    }
}