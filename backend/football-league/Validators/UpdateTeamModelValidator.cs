using FluentValidation;
using football_league.Data.Models.DTOs;

namespace football_league.Validators;

public class UpdateTeamModelValidator : AbstractValidator<UpdateTeamModel>
{
    public UpdateTeamModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Team name is required")
            .MaximumLength(100);
    }
}