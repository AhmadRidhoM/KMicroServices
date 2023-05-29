using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PricingConfiguration;
using PricingConfiguration.Models;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly OrderServiceDbContext _dbContext;

        public OrderDetailController(OrderServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet("{orderId:int}")]   
        public async Task<ActionResult<OrderDetail>> GetOrderDetailByOrderId(int orderId)
        {
            var orderDetail = await _dbContext.OrderDetails.FindAsync(orderId);
            if (orderDetail != null)
            {
                return orderDetail;
            }
            else
            {
                return NotFound();
            }
        }

        
    }
}
