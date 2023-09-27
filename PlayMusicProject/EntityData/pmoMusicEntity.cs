using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayMusicProject.EntityData
{
    [Table("pmoMusic")]
    public class pmoMusicEntity
    {
        [Key]
        public int IdMusic { get; set; }
        public string NameMusic { get; set; }
        public string DescribeMusic { get; set; }
        public int IdLyricMusic { get; set; }
        public string ImageMusic { get; set; }
        public string AudioMusic { get; set; }
        public int IdCategory { get; set; }
        public int IdArtists { get; set; }
        public int TopMusic { get; set; }
        public DateTime DateTimeCreate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsNew { get; set; }
    }
}
