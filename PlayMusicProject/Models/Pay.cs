namespace PlayMusicProject.Models
{
    public class Pay
    {
        public int IdPay { get; set; }
        public int IdUser { get; set; }
        public int CountCart { get; set; }
		public string IdProductString { get; set; }
		public string CountProductString { get; set; }
		public Decimal TotalPay { get; set; }
        public int ActionPay { get; set; }
    }
}
