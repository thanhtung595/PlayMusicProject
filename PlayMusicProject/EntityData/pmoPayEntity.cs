using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayMusicProject.EntityData
{
    [Table("pmoPay")]
    public class pmoPayEntity
    {
        [Key]
        public int IdPay { get; set; }
        public int IdUser { get; set; }
        public int CountCart { get; set; }
        public string IdProductString { get; set; }
        public string CountProductString { get; set; }
		public Decimal TotalPay { get; set; }
        public int ActionPay { get; set; }
    }
}
