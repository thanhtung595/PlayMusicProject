namespace PlayMusicProject.Models
{
    public class User
    {

        public User(string useName, string passWord, string fullName)
        {
            AccountUser = useName;
            AccountPass = passWord;
            UserName = fullName;
        }

        public User() { }
        public int IdUser { get; set; }
        public string AccountUser { get; set; }
        public string AccountPass { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public string IsAdminString { get; set; }
        public bool IsBan { get; set; }
        public string IsBanString { get; set; }
        public string UserImage { get; set; }
        public int SDTUser { get; set; }
        public DateTime TimeCreate { get; set; }
        public bool IsMyFriendUser { get; set; }
        public int IdMyFriendUser { get; set; }
        public int IdUserReceive { get; set; }
    }
}
