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
            //modelBuilder.Entity<RoomMessage>()
            //    .Property(b => b.DateTime)
            //    .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Room>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction); //TODO

            modelBuilder.Entity<RoomMessage>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction); //TODO 
        }
    }
}
