using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Models
{
    public class BookDB : DbContext
    {
        public BookDB(DbContextOptions<BookDB> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title).IsRequired().HasColumnType("text"); 

                entity.Property(e => e.Author).IsRequired().HasColumnType("text");

                entity.Property(e => e.Year).IsRequired();

                entity.Property(e => e.PublishDate).HasColumnType("date"); 
            });
        }
    }
}
