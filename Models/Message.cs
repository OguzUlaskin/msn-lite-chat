using System;

namespace MsnLiteChatApp.Models
{
    public class Message
    {
        public int Id { get; set; }               // Her mesajın benzersiz bir ID'si
        public string Sender { get; set; }        // Mesajı gönderenin kullanıcı adı
        public string Receiver { get; set; }      // Mesajı alan kişinin kullanıcı adı
        public string Content { get; set; }       // Mesajın kendisi (metin)
        public DateTime Timestamp { get; set; }
    }
}
