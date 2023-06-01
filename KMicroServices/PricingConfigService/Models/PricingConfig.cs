using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PricingConfigService.Models
{
    [Table("PricingConfig",Schema ="references")]
    public class PricingConfig
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PricingConfigId { get; set; }
        public string ProductCode { get; set; }
        public string StoreCode { get; set; }
        public string MarketingArea { get; set; }
        public string CustomerType { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public decimal Amount { get; set; }
    }
}
