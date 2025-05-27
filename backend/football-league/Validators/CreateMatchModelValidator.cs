using FluentValidation;
using football_league.Data.Models.DTOs;

namespace football_league.Validators;

public class CreateMatchModelValidator : AbstractValidator<CreateMatchModel>
{
    public CreateMatchModelValidator()
    {
        RuleFor(x => x.HomeTeamId)
            .NotEmpty().WithMessage("Home team is required");

        RuleFor(x => x.AwayTeamId)
            .NotEmpty().WithMessage("Away team is required");

        RuleFor(x => x)
            .Must(x => x.HomeTeamId != x.AwayTeamId)
            .WithMessage("A team cannot play against itself.");

        RuleFor(x => x.PlayedAt)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Match date cannot be in the future.");
    }
}