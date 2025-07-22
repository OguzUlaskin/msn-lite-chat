namespace MsnLiteChatApp.Models
{
    public class User
    {   
        public int Id { get; set; }                 // Otomatik artan ID
        public string Username { get; set; }        // Kullanıcı adı
        public string Password { get; set; }

        public string? ProfileImage { get; set; }

    }
}
