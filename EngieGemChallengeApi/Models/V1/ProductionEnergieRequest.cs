namespace EngieGemChallengeApi.Models.V1
{
    public class ProductionEnergieRequest
    {
        public double Load { get; set; }
        public Fuel Fuels { get; set; }
        public List<PowerPlant> PowerPlants { get; set; }
    }
}
