﻿using Application.Dtos;
using FluentValidation;

namespace Application.Validators;
public class ActivityDtoValidator : AbstractValidator<EditActivityDto>
{
    public ActivityDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title must not be empty.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description must not be empty.");
        RuleFor(x => x.Date).NotEmpty().WithMessage("Date must not be empty.");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category must not be empty.");
        RuleFor(x => x.City).NotEmpty().WithMessage("City must not be empty.");
        RuleFor(x => x.Venue).NotEmpty().WithMessage("Venue must not be empty.");
    }
}
