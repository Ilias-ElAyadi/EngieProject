using EngieGemChallengeApi.Domain.Models.V1;

namespace EngieGemChallengeApi.Core.Component
{
    public interface IProductionEnergieComponent
    {
        List<ProductionEnergieResponse> DetermineProductionEnergie(ProductionEnergieRequest request);
    }
}
