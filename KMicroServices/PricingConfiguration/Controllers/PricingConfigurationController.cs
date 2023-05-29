using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PricingConfiguration.Models;

namespace PricingConfiguration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingConfigurationController : ControllerBase
    {
        private readonly OrderServiceDbContext _context;

        public PricingConfigurationController(OrderServiceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PricingConfig>> GetPricingConfigs()
        {
            return _context.PricingConfigs;
        }

        [HttpGet("{pricingConfigId:int}")]
        public async Task<ActionResult<PricingConfig>> GetPricingConfigById(int pricingConfigId)
        {
            var pricingConfig = await _context.PricingConfigs.FindAsync(pricingConfigId);
            if (pricingConfig != null)
            {
                return pricingConfig;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<PricingConfig>> AddPricingConfig(PricingConfig pricingConfig)
        {
            try
            {
                _context.PricingConfigs.Add(pricingConfig);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
