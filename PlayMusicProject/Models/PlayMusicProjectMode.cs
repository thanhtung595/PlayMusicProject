namespace PlayMusicProject.Models
{
    public class PlayMusicProjectMode
    {
        public List<User> User { get; set; }
        public List<User> CheckFromat { get; set; }
        public List<ListUser> ListUser { get; set; }
        public List<User> MessageBoxIdUserReceive { get; set; }
        public List<MyFriendUser> MyFriendUser { get; set; }
        public List<Music> Music { get; set; }
        public List<CateGory> CateGory { get; set; }
        public List<LyricMusic> LyricMusic { get; set; }
        public List<Artists> Artists { get; set; }
        public List<BrandShop> BrandShop { get; set; }
        public int CountBrandShop { get; set; }
        public List<CategoryShop> CategoryShop { get; set; }
        public List<ItemCategoryShop> ItemCategoryShop { get; set; }
        public List<ProductShop> ProductShop { get; set; }
        public List<AddCart> AddCart { get; set; }
        public List<Banner> Banner { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public string name { get; set; }
        public int CategoryId { get; set; }
    }
}
