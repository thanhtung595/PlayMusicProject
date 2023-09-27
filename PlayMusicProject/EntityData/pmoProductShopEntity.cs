using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayMusicProject.EntityData
{
	[Table("pmoProductShop")]
	public class pmoProductShopEntity
	{
        [Key]
        public int IdProductShop { get; set; }
        public string NameProductShop { get; set; }
        public int IdBrandShop { get; set; }
        public int IdItemCategoryShop { get; set; }
        public bool NewProductShop { get; set; }
        public Decimal PriceProductShop { get; set; }
        public string ImageProductShop { get; set; }
    }
}
