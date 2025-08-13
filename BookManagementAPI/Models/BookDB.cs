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

    }
}
