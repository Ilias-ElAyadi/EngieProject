using EngieGemChallengeApi.Models.V1;
using FluentValidation;

namespace EngieGemChallengeApi.Models.Validator
{
    public class PowerPlantValidator : AbstractValidator<PowerPlant>
    {
        public PowerPlantValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type cannot be empty.");

            RuleFor(x => x.Efficiency)
                .InclusiveBetween(0, 1).WithMessage("Efficiency must be between 0 and 1.");

            RuleFor(x => x.Pmin)
                .GreaterThanOrEqualTo(0).WithMessage("Pmin must be greater than or equal to 0.");

            RuleFor(x => x.Pmax)
                .GreaterThanOrEqualTo(x => x.Pmin).WithMessage("Pmax must be greater than or equal to Pmin.");
        }
    }
}
