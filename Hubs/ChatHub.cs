using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using MsnLiteChatApp.Data;
using MsnLiteChatApp.Models;
using System;

namespace MsnLiteChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;

        public ChatHub(AppDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string sender, string receiver, string message)
        {
            // 1. Veritabanına kaydet
            var msg = new Message
            {
                Sender = sender,
                Receiver = receiver,
                Content = message,
                Timestamp = DateTime.Now
            };

            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();

            // 2. Tüm bağlı kullanıcılara gönder
            await Clients.All.SendAsync("ReceiveMessage", sender, receiver, message, msg.Timestamp.ToString("HH:mm"));
        }
    }
}
