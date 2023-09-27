using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayMusicProject.EntityData
{
    [Table("pmoMyFriendUser")]
    public class pmoMyFriendUserEntity
    {
        [Key]
        public int IdMyFriendUser { get; set; }
        public int IdUserSend { get; set; }
        public int IdUserReceive { get; set; }
        public bool IsMyFriendUser { get; set; }
    }
}
