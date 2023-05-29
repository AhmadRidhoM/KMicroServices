using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;
using ProductService.Models;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;
        public ProductController(ProductDbContext dbContext) 
        {
            _context = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var product = _context.Products;
            return product;
        }

        [HttpGet("{productid:int}")]
        public async Task<ActionResult<Product>> GetProductByID(int productid)
        {
            var product = await _context.Products.FindAsync(productid);
            if (product != null)
            {
                return product;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{productName}")]
        public async Task<ActionResult<Product>> GetProductByName(string productName)
        {
            var product = _context.Products.Where(x => x.ProductDescription.Contains(productName)).FirstOrDefault();
            if (product != null)
            {
                return product;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            var productCode = product.ProductCode;
            try
            {
                var extProduct = _context.Products.Where(x => x.ProductCode == productCode).FirstOrDefaultAsync();
                if (extProduct.Result == null)
                {
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                else
                {
                    return new BadRequestObjectResult(new { code = "400", message = "product code must be unique" });
                }


                }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPut]
        public async Task<ActionResult<Product>> EditProduct(Product product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{productid:int}")]
        public async Task<ActionResult<Product>> DeleteProduct(int productid)
        {
            try
            {
                var product = await _context.Products.FindAsync(productid);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
