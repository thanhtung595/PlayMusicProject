using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayMusicProject.EntityData
{
    [Table("pmoBanner")]
    public class pmoBannerEntity
    {
        [Key]
        public int idBanner { get; set; }
        public string imageBanner { get; set; }
    }
}
