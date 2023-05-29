using PricingConfiguration;
using PricingConfiguration.Models;

namespace OrderService.BusinessLogic
{
    public class OrderBL
    {
        private readonly OrderServiceDbContext _dbContext;

        public OrderBL()
        {
        }

        public OrderBL(OrderServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string AddOrder(CreateOrder order)
        {
            Order _order = new Order();
            try
            {
                _order.CustomerNo = order.CustomerNo;
                _order.StoreCode = order.StoreCode;
                _order.OrderDate = order.OrderDate;
                _order.IsCancel = false;

                _dbContext.Orders.Add(_order);
                _dbContext.SaveChanges();

                var orderId = _order.OrderId;

                var addDetail = AddOrderDetail(order);

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string AddOrderDetail(CreateOrder order)
        {
            try
            {
                foreach (var item in order.OrderDetails)
                {
                    OrderDetail detail = new OrderDetail();

                    detail.OrderID = order.OrderId;
                    detail.ProductCode = item.ProductCode;
                    detail.ProductCount = item.ProductCount;
                    detail.UnitPrice = GetPricingConfig(detail.ProductCode, order.StoreCode, detail.ProductCount, order.OrderDate);

                    _dbContext.OrderDetails.Add(detail);
                    _dbContext.SaveChanges();
                }

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public decimal GetPricingConfig(string productCode, string storeCode, int totalUnit, DateTime orderDate)
        {
            PricingConfig _pricingConfig = new PricingConfig();

            var custType = totalUnit < 100 ? "Retail" : "Company";
            var pricingConfig = _dbContext.PricingConfigs.Where(x => x.ProductCode == productCode 
                                                                && x.StoreCode == storeCode
                                                                && x.CustomerType == custType
                                                                && (orderDate >= x.ValidFrom && orderDate <= x.ValidTo)).FirstOrDefault();

            if (pricingConfig != null)
            {
                return pricingConfig.Amount;
            }
            else
            {
                return 0;
            }
        }
    }
}
