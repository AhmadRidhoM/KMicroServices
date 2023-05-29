using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketingService.Models
{
    [Table("MarketingArea", Schema = "masters")]
    public class Marketing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("MarketingAreaId")]
        public int MarketingAreaId { get; set; }

        [Column("MarketingAreaCode")]
        public string MarketingAreaCode { get; set; }

        [Column("MarketingAreaDescription")]
        public string MarketingAreaDescription { get; set; }
    }
}
