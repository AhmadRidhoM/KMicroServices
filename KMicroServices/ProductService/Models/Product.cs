using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Models
{
    [Table("Product", Schema ="masters")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ProductID")]
        public int ProductID { get; set; }

        [Column("ProductCode")]
        public string ProductCode { get; set; }

        [Column("ProductDescription")]
        public string ProductDescription { get; set; }

        [Column("ProductType")]
        public string ProductType { get; set; }

        [Column("ProductBrand")]
        public string ProductBrand { get; set; }

        [Column("UOM")]
        public string UOM { get; set; }

        [Column("COGS")]
        public string COGS { get; set; }
    }
}
