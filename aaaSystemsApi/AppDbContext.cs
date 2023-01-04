using aaaSystemsCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace aaaSystemsApi
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Chat> Chats { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options)  : base(options) { }
    }
}
