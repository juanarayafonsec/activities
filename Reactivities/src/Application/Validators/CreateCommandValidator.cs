using Application.Commands;
using FluentValidation;

namespace Application.Validators;
public class CreateCommandValidator : AbstractValidator<CreateActivityCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(x => x.Activity).SetValidator(new ActivityValidator());

    }
}
