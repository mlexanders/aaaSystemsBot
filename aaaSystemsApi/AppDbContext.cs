using aaaSystemsCommon.Entity;
using Microsoft.EntityFrameworkCore;

namespace aaaSystemsApi
{
    public class AppDbContext : DbContext
    {
        public DbSet<Sender> Senders { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<RoomMessage> RoomMessages { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sender>()
                .HasMany<Room>()
                .WithOne()
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<RoomMessage>()
                .HasOne<Sender>()
                .WithMany()
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Sender>().Property(u => u.Id)
                .ValueGeneratedNever();
        }
    }
}
