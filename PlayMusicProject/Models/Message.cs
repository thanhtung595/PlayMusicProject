namespace PlayMusicProject.Models
{
    public class Message
    {
        public int IdMessage { get; set; }
        public int IdUser { get; set; }
        public int IdUserSend { get; set; }
        public int IdUserReceive { get; set; }
        public string TextChatMessage { get; set; }
        public string TimeChatMessage { get; set; }
        public bool IsChatMessage { get; set; }
    }
}
