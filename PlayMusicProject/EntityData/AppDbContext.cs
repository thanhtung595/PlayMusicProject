using Microsoft.EntityFrameworkCore;
using PlayMusicProject.EntityData;

namespace PlayMusicProject.EntityData
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<pmoUserEntity> UserEntity { get; set; }
        public DbSet<pmoMusicEntity> MusicEntity { get; set; }
        public DbSet<pmoCateGoryEntity> CateGoryEntity { get; set; }
        public DbSet<pmoArtistsEntity> ArtistsEntity { get; set; }
        public DbSet<pmoLyricMusicEntity> LyricMusicEntity { get; set; }
        public DbSet<pmoBrandShopEntity> BrandShopEntity { get; set; }
        public DbSet<pmoCategoryShopEntity> CategoryShopEntity { get; set; }
        public DbSet<pmoItemCategoryShopEntity> ItemCategoryShopEntity { get; set; }
        public DbSet<pmoProductShopEntity> ProductShopEntity { get; set; }
        public DbSet<pmoAddCartEntity> AddCartEntity { get; set; }
        public DbSet<pmoBannerEntity> BannerEntity { get; set; }
        public DbSet<pmoMyFriendUserEntity> MyFriendUserEntity { get; set; }
        public DbSet<pmoMessageEntity> MessageEntity { get; set; }
        public DbSet<pmoPayEntity> PayEntity { get; set; }
    }
}
