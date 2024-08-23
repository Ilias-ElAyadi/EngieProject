using EngieGemChallengeApi.Models.V1;
using BDL = EngieGemChallengeApi.Domain.Models.V1;


namespace EngieGemChallengeApi.Mapper
{
    public interface IProductionEnergieMapper
    {

        BDL.ProductionEnergieRequest MapProductionPlanRequest(ProductionEnergieRequest productionEnergieRequest);
        ProductionEnergieResponse MapProductionPlanResponse(BDL.ProductionEnergieResponse productionEnergieResponse);

    }
}
