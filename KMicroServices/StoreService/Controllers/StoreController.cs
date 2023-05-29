using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreService.Models;

namespace StoreService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly StoreDbContext _context;
        public StoreController(StoreDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Store>> GetStores()
        {
            return _context.Stores;
        }

        [HttpGet("{storeid:int}")]
        public async Task<ActionResult<Store>> GetStoreById(int storeId)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store != null)
            {
                return store;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{storeName}")]
        public async Task<ActionResult<Store>> GetStoreByName(string storeName)
        {
            var store = _context.Stores.Where(x => x.StoreDescription.Contains(storeName)).FirstOrDefault();
            if (store != null)
            {
                return store;
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public async Task<ActionResult<Store>> AddStore(Store store)
        {
            var storeCode = store.StoreCode;
            try
            {
                var extStore = _context.Stores.FirstOrDefault(x => x.StoreCode == storeCode);
                if (extStore == null)
                {
                    _context.Stores.Add(store);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                else
                {
                    return new BadRequestObjectResult(new { code = "400", message = "Store Code must be unique" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut]
        public async Task<ActionResult<Store>> EditStore(Store store)
        {
            try
            {
                _context.Stores.Update(store);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{storeId:int}")]
        public async Task<ActionResult<Store>> DeleteStore(int storeId)
        {
            var store = _context.Stores.Find(storeId);
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
