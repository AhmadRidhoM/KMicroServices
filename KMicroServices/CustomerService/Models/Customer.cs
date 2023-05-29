using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerService.Models
{
    [Table("Customer", Schema = "masters")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CustomerId")]
        public int CustomerId { get; set; }

        [Column("CustomerNo")]
        public string CustomerNo { get; set; }

        [Column("CustomerName")]
        public string CustomerName { get; set; }

        [Column("CustomerAddress")]
        public string CustomerAddress { get; set; }

        [Column("CustomerPhone")]
        public string CustomerPhone { get; set; }

        [Column("CustomerType")]
        public string CustomerType { get; set; }
    }
}
