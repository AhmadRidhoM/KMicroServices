using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PricingConfiguration.Models
{
    [Table("Order", Schema ="transactions")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerNo { get; set; }
        public string StoreCode { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancel { get; set; }
    }

    public class CreateOrder : Order
    {
       
        public List<OrderDetail> OrderDetails { get; set; }
    }

    
}
