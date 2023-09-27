using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayMusicProject.EntityData
{
	[Table("pmoBrandShop")]
	public class pmoBrandShopEntity
	{
        [Key]
        public int IdBrandShop { get; set; }
        public string NameBrandShop { get; set; }
	}
}
