using MarketingService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketingAreaController : ControllerBase
    {
        private readonly MarketingDbContext _context;

        public MarketingAreaController(MarketingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Marketing>> GetAllMarketingArea()
        {
            return _context.Marketings;
        }

        [HttpGet("{marketingAreaId:int}")]
        public async Task<ActionResult<Marketing>> GetMarketingAreaById(int marketingAreaId)
        {
            var marketing = await _context.Marketings.FindAsync(marketingAreaId);
            if (marketing != null)
            {
                return marketing;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{areaDescription}")]
        public async Task<ActionResult<Marketing>> GetMarketingAreaByName(string areaDescription)
        {
            var marketing = _context.Marketings.Where(x => x.MarketingAreaDescription.Contains(areaDescription)).FirstOrDefault();
            if (marketing != null)
            {
                return marketing;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Marketing>> AddMarketing(Marketing marketing)
        {
            var marketingAreaCode = marketing.MarketingAreaCode;
            try
            {
                var extMarketing = _context.Marketings.Where(x => x.MarketingAreaCode == marketingAreaCode).FirstOrDefault();
                if (extMarketing == null)
                {
                    _context.Marketings.Add(marketing);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                else
                {
                    return new BadRequestObjectResult(new { code = "400", message = "Area code must be unique" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Marketing>> EditMarketing(Marketing marketing)
        {
            try
            {
                _context.Marketings.Update(marketing);
                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{marketingAreaId:int}")]
        public async Task<ActionResult<Marketing>> DeleteMarketing(int marketingAreaId)
        {
            var marketing = await _context.Marketings.FindAsync(marketingAreaId);
            if (marketing != null)
            {
                _context.Marketings.Remove(marketing);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
