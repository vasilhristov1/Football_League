using FluentValidation;
using football_league.Models.DTOs;

namespace football_league.Validators;

public class UpdateTeamModelValidator : AbstractValidator<UpdateTeamModel>
{
    public UpdateTeamModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Team name is required")
            .MaximumLength(100);

        RuleFor(x => x.LogoUrl)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrWhiteSpace(x.LogoUrl))
            .WithMessage("LogoUrl must be a valid URL.");
    }
}