using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayMusicProject.EntityData
{
    [Table("pmoLyricMusic")]
    public class pmoLyricMusicEntity
    {
        [Key]
        public int IdLyricMusic { get; set; }
        public string LyricMusic { get; set; }
    }
}
