using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PricingConfigService.Models;
using System.Runtime.CompilerServices;

namespace PricingConfigService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingConfigController : ControllerBase
    {
        private readonly PricingConfigDbContext _context;

        public PricingConfigController(PricingConfigDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PricingConfig>> GetPricingConfig()
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
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { code = "400", message = e.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<PricingConfig>> EditPricingConfig(PricingConfig pricingConfig)
        {
            try
            {
                _context.PricingConfigs.Update(pricingConfig);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { code = "400", message = e.Message });
            }
        }

        [HttpDelete("{pricingConfigId:int}")]
        public async Task<ActionResult<PricingConfig>> DeletePricingConfig(int pricingConfigId)
        {
            try
            {
                var pricingConfig = _context.PricingConfigs.Find(pricingConfigId);
                if (pricingConfig != null)
                {
                    _context.PricingConfigs.Remove(pricingConfig);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { code = "400", message = e.Message });
            }
        }
    }
}
