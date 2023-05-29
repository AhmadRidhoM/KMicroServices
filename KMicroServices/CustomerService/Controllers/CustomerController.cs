using CustomerService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _context;
        public CustomerController(CustomerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            return _context.Customers;
        }

        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                return customer;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{customerName}")]
        public async Task<ActionResult<Customer>> GetCustomerByName(string customerName)
        {
            var customer = _context.Customers.Where(x => x.CustomerName.Contains(customerName)).FirstOrDefault();
            if (customer != null)
            {
                return customer;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            var customerCode = customer.CustomerNo;
            try
            {
                var extCustomer = _context.Customers.FirstOrDefault(x => x.CustomerNo == customerCode);
                if (extCustomer == null) 
                {
                    _context.Customers.Add(customer);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return new BadRequestObjectResult(new { code = "400", message = "Customer No must be unique" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Customer>> EditCustomer(Customer customer)
        {
            try
            {
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{customerId:int}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int customerId)
        {
            try
            {
                var customer = _context.Customers.Find(customerId);
                _context.Customers.Remove(customer);
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
