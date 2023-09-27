namespace PlayMusicProject.Models
{
	public class AddCart
	{
		public int IdAddCart { get; set; }
		public int IdUser { get; set; }
		public string NameUser { get; set; }
        public int CountAddCart { get; set; }
		public int IdProductShop { get; set; }
		public Decimal SumPrice { get; set; }
        public string ImageProductShop { get; set; }
        public string NameProductShop { get; set; }
        public Decimal PriceProductShop { get; set; }
	}
}
