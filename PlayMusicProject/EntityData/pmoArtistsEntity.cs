using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayMusicProject.EntityData
{
    [Table("pmoArtists")]
    public class pmoArtistsEntity
    {
        [Key]
        public int IdArtists { get; set; }
        public string NameArtists { get; set; }
        public DateTime DateTimeCreate { get; set; }
        public string ImageArtists { get; set; }
    }
}
