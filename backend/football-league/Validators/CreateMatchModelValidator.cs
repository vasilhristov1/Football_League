using FluentValidation;
using football_league.Models.DTOs;

namespace football_league.Validators;

public class CreateMatchModelValidator : AbstractValidator<CreateMatchModel>
{
    public CreateMatchModelValidator()
    {
        RuleFor(x => x.HomeTeamName)
            .NotEmpty().WithMessage("Home team is required");

        RuleFor(x => x.AwayTeamName)
            .NotEmpty().WithMessage("Away team is required");

        RuleFor(x => x)
            .Must(x => x.HomeTeamName != x.AwayTeamName)
            .WithMessage("A team cannot play against itself.");

        RuleFor(x => x.PlayedAt)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Match date cannot be in the future.");
    }
}