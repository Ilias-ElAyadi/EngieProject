using AutoMapper;
using EngieGemChallengeApi.Models.V1;
using BDL = EngieGemChallengeApi.Domain.Models.V1;

namespace EngieGemChallengeApi.Mapper
{
    /// <summary>
    /// Mapper API-to-Domain model mapper.
    /// </summary>
    public class ProductionEnergieMapper : IProductionEnergieMapper
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductionEnergieMapper"/> class.
        /// </summary>
        public ProductionEnergieMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<InnerMapper>();
            });

            _mapper = config.CreateMapper();
        }

        /// <summary>
        /// Map API model to Domain model.
        /// </summary>
        public BDL.ProductionEnergieRequest MapProductionPlanRequest(ProductionEnergieRequest productionEnergieRequest)
        {
            return _mapper.Map<ProductionEnergieRequest, BDL.ProductionEnergieRequest>(productionEnergieRequest);
        }

        /// <summary>
        /// Map Domain model to API model.
        /// </summary>
        public ProductionEnergieResponse MapProductionPlanResponse(BDL.ProductionEnergieResponse productionEnergieResponse)
        {
            return _mapper.Map<BDL.ProductionEnergieResponse, ProductionEnergieResponse>(productionEnergieResponse);
        }

        /// <inheritdoc />
        private class InnerMapper : Profile
        {
            public InnerMapper()
            {
                ConfigureMapping(this);
            }

            protected void ConfigureMapping(IProfileExpression configuration)
            {
                #region Models -> Domain Models
                configuration.CreateMap<ProductionEnergieRequest, BDL.ProductionEnergieRequest>().ReverseMap();
                configuration.CreateMap<ProductionEnergieResponse, BDL.ProductionEnergieResponse>().ReverseMap();

                configuration.CreateMap<Fuel, BDL.Fuel>().ReverseMap();
                configuration.CreateMap<Load, BDL.Load>().ReverseMap();
                configuration.CreateMap<PowerPlant, BDL.PowerPlant>().ReverseMap();
                #endregion
            }
        }
    }
}
