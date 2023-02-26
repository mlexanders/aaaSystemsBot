using aaaSystemsCommon.Entity;
using Microsoft.EntityFrameworkCore;

namespace aaaSystemsApi
{
    public class AppDbContext : DbContext
    {
        public DbSet<Sender> Senders { get; set; } = null!;
        public DbSet<Dialog> Dialogs { get; set; } = null!;
        public DbSet<DialogMessage> DialogMessages { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region P_Key
            modelBuilder.Entity<Sender>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Dialog>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<DialogMessage>()
                .HasKey(s => s.Id);
            #endregion

            modelBuilder.Entity<Sender>()
                .HasMany<DialogMessage>()
                .WithOne()
                .HasForeignKey(m => m.ChatId);

            modelBuilder.Entity<Sender>()
                .HasOne<Dialog>()
                .WithOne(d => d.Sender)
                .HasForeignKey<Dialog>(d => d.ChatId);

            #region ValueGeneratedNever
            modelBuilder.Entity<Sender>().Property(s => s.Id)
                   .ValueGeneratedNever();

            modelBuilder.Entity<DialogMessage>().Property(m => m.Id)
                .ValueGeneratedNever();
            #endregion
        }
    }
}