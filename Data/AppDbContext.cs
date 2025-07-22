using Microsoft.EntityFrameworkCore;
using MsnLiteChatApp.Models;

namespace MsnLiteChatApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
