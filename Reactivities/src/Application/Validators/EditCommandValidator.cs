using Application.Commands;
using FluentValidation;

namespace Application.Validators;
public class EditCommandValidator : AbstractValidator<EditActivityCommand>
{
    public EditCommandValidator()
    {
        RuleFor(x => x.Activity).SetValidator(new EditActivityValidator());
    }
}

public class EditActivityValidator : ActivityValidator
{
    public EditActivityValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Activity Id must not be empty.");
    }
}
