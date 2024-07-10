using Microsoft.EntityFrameworkCore;

namespace FlashCardAppWebApi.Models
{
    public class FlashCardAppContext : DbContext
    {
        public FlashCardAppContext(DbContextOptions<FlashCardAppContext> options) : base(options)
        { }
        public DbSet<User> users { get; set; }
        public DbSet<Flashcard> flashcards { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<FlashcardCategory> flashcardCategories { get; set; }
        public DbSet<UserCategory> userCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserCategory>()
                .HasKey(w => new { w.UserId, w.CategoryId });
            modelBuilder.Entity<FlashcardCategory>()
                .HasKey(w => new { w.FlashcardId, w.CategoryId });
        }
    }
}