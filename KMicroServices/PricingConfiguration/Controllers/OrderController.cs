using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderService.BusinessLogic;
using PricingConfiguration;
using PricingConfiguration.Models;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderServiceDbContext _dbContext;

        public OrderController(OrderServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAllOrder()
        {
            return _dbContext.Orders.Where(x => !x.IsCancel).ToList();
        }

        [HttpGet("{orderId:int}")]
        public async Task<ActionResult<Order>> GetOrderById(int orderId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order != null)
            {
                return order;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateOrder>> AddOrder(CreateOrder order)
        {

            OrderBL orderBL = new OrderBL();
            var addResult = orderBL.AddOrder(order);

            if (addResult == "Success")
            {
                return Ok();
            }
            else
            {
                return BadRequest(addResult);
            }

        }

        [HttpPut("{orderId:int}")]
        public async Task<ActionResult<Order>> UpdateCancelOrder(int orderid)
        {
            var order = _dbContext.Orders.Find(orderid);
            if (order != null)
            {
                order.IsCancel = true;
                _dbContext.SaveChanges();

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
