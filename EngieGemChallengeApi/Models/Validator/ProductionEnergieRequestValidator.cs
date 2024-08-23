using EngieGemChallengeApi.Models.V1;
using FluentValidation;

namespace EngieGemChallengeApi.Models.Validator
{
    public class ProductionEnergieRequestValidator : AbstractValidator<ProductionEnergieRequest>
    {
        public ProductionEnergieRequestValidator()
        {
            RuleFor(x => x.Load)
                .GreaterThan(0).WithMessage("Load must be greater than 0.");

            RuleFor(x => x.Fuels)
                .NotNull().WithMessage("Fuels cannot be null.");

            RuleFor(x => x.Fuels.Gas)
                .GreaterThanOrEqualTo(0).WithMessage("Gas must be greater than or equal to 0.");

            RuleFor(x => x.Fuels.Kerosine)
                .GreaterThanOrEqualTo(0).WithMessage("Kerosine must be greater than or equal to 0.");

            RuleFor(x => x.Fuels.Co2)
                .GreaterThanOrEqualTo(0).WithMessage("Co2 must be greater than or equal to 0.");

            RuleFor(x => x.Fuels.Wind)
                .GreaterThanOrEqualTo(0).WithMessage("Wind must be greater than or equal to 0.");

            RuleForEach(x => x.PowerPlants)
                .SetValidator(new PowerPlantValidator());
        }
    }
}
