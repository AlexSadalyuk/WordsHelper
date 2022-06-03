using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class WordsDbContext : DbContext
    {
        public WordsDbContext(DbContextOptions<WordsDbContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<Translate> Translates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>()
                .HasOne(x => x.Translate)
                .WithOne(x => x.Word)
                .HasForeignKey<Word>(x => x.TranslateId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}