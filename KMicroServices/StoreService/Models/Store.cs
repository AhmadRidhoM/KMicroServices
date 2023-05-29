using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Models
{
    [Table("Store", Schema ="masters")]
    public class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("StoreID")]
        public int StoreID { get; set; }

        [Column("StoreCode")]
        public string StoreCode { get; set; }

        [Column("StoreDescription")]
        public string StoreDescription { get; set; }

        [Column("StoreAddress")]
        public string StoreAddress { get; set; }

        [Column("StorePhone")]
        public string StorePhone { get; set; }

        [Column("MarketingArea")]
        public string MarketingArea { get; set; }
    }
}
