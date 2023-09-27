namespace PlayMusicProject.Models
{
    public class ListUser
    {
        public ListUser(string useName, string passWord, string fullName)
        {
            AccountUser = useName;
            AccountPass = passWord;
            UserName = fullName;
        }

        public ListUser() { }
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
