using BookManagementAPI.Models;

namespace BookManagementAPI.@interface
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
        Book? GetBook(int bookId);
        Book AddBook(Book book);
        Book? UpdateBook(Book book);
        void DeleteBook(int bookId);
    }
}
