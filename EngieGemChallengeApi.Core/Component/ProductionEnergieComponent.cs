
using EngieGemChallengeApi.Domain.Models.V1;

namespace EngieGemChallengeApi.Core.Component
{
    public class ProductionEnergieComponent : IProductionEnergieComponent
    {
        public List<ProductionEnergieResponse> DetermineProductionEnergie(ProductionEnergieRequest request)
        {
            var powerPlantCosts = CalculatePowerPlantCosts(request);
            var sortedPowerPlants = SortPowerPlantsByCost(powerPlantCosts);
            var productionEnergieResponseList = GenerateResponses(sortedPowerPlants, request);
            var finalResponses = AddZeroProductionPlants(productionEnergieResponseList, request);

            return finalResponses;
        }

        private List<(PowerPlant Plant, double Cost)> CalculatePowerPlantCosts(ProductionEnergieRequest request)
        {
            var ok = "";
            var powerPlantCosts = new List<(PowerPlant Plant, double Cost)>();

            foreach (var plant in request.PowerPlants)
            {
                double cost;
                if (plant.Type == "gasfired")
                {
                    cost = request.Fuels.Gas / plant.Efficiency;
                }
                else if (plant.Type == "turbojet")
                {
                    cost = request.Fuels.Kerosine / plant.Efficiency;
                }
                else if (plant.Type == "windturbine")
                {
                    cost = 0;
                }
                else
                {
                    cost = double.MaxValue;
                }

                powerPlantCosts.Add((Plant: plant, Cost: cost));
            }

            return powerPlantCosts;
        }

        private List<(PowerPlant Plant, double Cost)> SortPowerPlantsByCost(List<(PowerPlant Plant, double Cost)> powerPlantCosts)
        {
            return powerPlantCosts
                .OrderBy(p => p.Cost)
                .ToList();
        }

        private List<ProductionEnergieResponse> GenerateResponses(List<(PowerPlant Plant, double Cost)> sortedPowerPlants, ProductionEnergieRequest request)
        {
            var responses = new List<ProductionEnergieResponse>();
            double remainingLoad = request.Load;

            foreach (var (plant, cost) in sortedPowerPlants)
            {
                if (remainingLoad <= 0)
                {
                    break;
                }

                double energie;

                if (plant.Type == "windturbine")
                {
                    energie = request.Fuels.Wind > 0 ? Math.Min(plant.Pmax * (request.Fuels.Wind / 100.0), remainingLoad) : 0;
                }
                else
                {
                    energie = Math.Min(plant.Pmax, remainingLoad);
                }

                energie = Math.Max(energie, plant.Pmin);

                energie = Math.Round(energie, 1);

                responses.Add(new ProductionEnergieResponse
                {
                    Name = plant.Name,
                    Power = energie
                });

                remainingLoad -= energie;
            }

            if (Math.Abs(remainingLoad) > 0.1)
            {
                throw new InvalidOperationException("Impossible de satisfaire la charge demandée avec les centrales disponibles.");
            }

            return responses;
        }

        private List<ProductionEnergieResponse> AddZeroProductionPlants(List<ProductionEnergieResponse> responses, ProductionEnergieRequest request)
        {
            var existingNames = responses.Select(r => r.Name).ToHashSet();

            var zeroProductionPlants = request.PowerPlants
                .Where(p => !existingNames.Contains(p.Name))
                .Select(p => new ProductionEnergieResponse
                {
                    Name = p.Name,
                    Power = 0.0
                })
                .ToList();

            responses.AddRange(zeroProductionPlants);

            return responses;
        }
    }
}
