using aaaSystemsCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace aaaSystemsApi
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<RoomMessage> RoomMessages { get; set; } = null!;
        public DbSet<Participant> Participants { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
