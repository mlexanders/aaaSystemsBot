using aaaSystemsCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace aaaSystemsApi
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<RoomMessage> RoomMessages { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany<Room>()
                .WithOne()
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<RoomMessage>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<User>().Property(u => u.Id)
                .ValueGeneratedNever();
        }
    }
}
