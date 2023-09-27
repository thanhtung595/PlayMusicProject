using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayMusicProject.EntityData
{
	[Table("pmoCategoryShop")]
	public class pmoCategoryShopEntity
	{
        [Key]
        public int IdCategoryShop { get; set; }
        public string NameCategoryShop { get; set; }
	}
}
