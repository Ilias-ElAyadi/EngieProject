using EngieGemChallengeApi.Core.Component;
using EngieGemChallengeApi.Mapper;
using EngieGemChallengeApi.Models.V1;
using EngieGemChallengeApi.Models.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EngieGemChallengeApi.Controllers
{
    [ApiController]
    public class ProductionEnergieController : ControllerBase
    {
        private readonly IProductionEnergieComponent _productionEnergieComponent;
        private readonly IProductionEnergieMapper _mapper;
        private readonly IValidator<ProductionEnergieRequest> _validator;



        public ProductionEnergieController(IProductionEnergieComponent productionEnergieComponent, IProductionEnergieMapper mapper, IValidator<ProductionEnergieRequest> validator)
        {
            _productionEnergieComponent = productionEnergieComponent ?? throw new ArgumentNullException(nameof(productionEnergieComponent));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator)); ;

        }

        [HttpPost("productionplan")]
        public async Task<ActionResult<ProductionEnergieResponse>> PostProductionplan([FromBody] ProductionEnergieRequest request)
        {

            var validationResult =  await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new { error = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage).ToList())});
            }
            try
            {
                  var response = _productionEnergieComponent.DetermineProductionEnergie(_mapper.MapProductionPlanRequest(request));
                  return Ok(response);
            }
            catch (InvalidOperationException ex)
            {

                    return BadRequest(new { error = ex.Message });

            }
        }
    }

}
