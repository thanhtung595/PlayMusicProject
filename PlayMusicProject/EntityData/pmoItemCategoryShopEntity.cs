using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayMusicProject.EntityData
{
	[Table("pmoItemCategoryShop")]
	public class pmoItemCategoryShopEntity
	{
        [Key]
        public int IdItemCategoryShop { get; set; }
        public string NameItemCategoryShop { get; set; }
        public int IdCategoryShop { get; set; }
	}
}
