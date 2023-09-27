using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayMusicProject.EntityData
{
    [Table("pmoUser")]
    public class pmoUserEntity
    {
        [Key]
        public int IdUser { get; set; }
        public string AccountUser { get; set; }
        public string AccountPass { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBan { get; set; }
        public string UserImage { get; set; }
        public int SDTUser { get; set; }
        public DateTime TimeCreate { get; set; }
    }
}
