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
            #region HasKey
            modelBuilder.Entity<Sender>()
                .HasKey(s => s.ChatId);

            modelBuilder.Entity<Dialog>()
                .HasKey(s => s.ChatId);

            modelBuilder.Entity<DialogMessage>()
                .HasKey(s => s.MessageId);

            #endregion

            modelBuilder.Entity<Dialog>()
                .HasMany<DialogMessage>()
                .WithOne()
                .HasForeignKey(d => d.ChatId);

            modelBuilder.Entity<Sender>()
                .HasOne<Dialog>()
                .WithOne()
                .HasForeignKey<Sender>(s => s.PK);

            #region ValueGeneratedNever
            modelBuilder.Entity<Sender>().Property(s => s.ChatId)
                   .ValueGeneratedNever();

            modelBuilder.Entity<Dialog>().Property(d => d.ChatId)
                .ValueGeneratedNever();

            modelBuilder.Entity<DialogMessage>().Property(m => m.MessageId)
                .ValueGeneratedNever();
            #endregion

            #region Ignore
            modelBuilder.Entity<Sender>()
        .Ignore(e => e.PK);

            modelBuilder.Entity<Dialog>()
                .Ignore(e => e.PK);

            modelBuilder.Entity<DialogMessage>()
                .Ignore(e => e.PK); 
            #endregion
        }
    }
}
