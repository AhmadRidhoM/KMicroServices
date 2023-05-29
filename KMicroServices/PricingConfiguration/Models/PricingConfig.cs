using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PricingConfiguration.Models
{
    [Table("pricingConfiguration", Schema ="masters")]
    public class PricingConfig
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PricingConfigID")]
        public int PricingConfigID { get; set; }

        [Column("ProductCode")]
        public string ProductCode { get; set; }

        [Column("StoreCode")]
        public string StoreCode { get; set; }

        [Column("MarketingArea")]
        public string MarketingArea { get; set; }

        [Column("CustomerType")]
        public string CustomerType { get; set; }

        [Column("ValidFrom")]
        public DateTime ValidFrom { get; set; }

        [Column("ValidTo")]
        public DateTime ValidTo { get; set; }

        [Column("Amount")]
        public decimal Amount { get; set; }
    }
}
