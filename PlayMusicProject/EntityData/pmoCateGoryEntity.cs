using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayMusicProject.EntityData
{
    [Table("pmoCateGory")]
    public class pmoCateGoryEntity
    {
        [Key]
        public int IdCategory { get; set; }
        public string NameCategory { get; set; }
        public DateTime DateTimeCreate { get; set; }
    }
}
