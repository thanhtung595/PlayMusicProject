using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayMusicProject.EntityData
{
	[Table("pmoAddCart")]
	public class pmoAddCartEntity
	{
        [Key]
        public int IdAddCart { get; set; }
        public int IdUser { get; set; }
        public int CountAddCart { get; set; }
        public int IdProductShop { get; set; }
		public Decimal SumPrice { get; set; }
		public Decimal PriceProduct { get; set; }
		public string NameProductShop { get; set; }
        public string ImageProductShop { get; set; }
	}
}
