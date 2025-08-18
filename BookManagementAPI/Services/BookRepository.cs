using BookManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using BookManagementAPI.interfaces;

namespace BookManagementAPI.Services;
    public class BookRepository : IBookRepository
    {
        private readonly BookDB _context;
        public BookRepository(BookDB context)
        {
            _context = context;
        }
    /*  
       public Book AddBook(Book book)
      {
          var result = _context.Books.Add(book);
          _context.SaveChanges();
          return result.Entity;
      }  
      public Book? UpdateBook(Book book)
      {
          var result = _context.Books.Find(book.Id);

          if (result != null)
          {
              result.Title = book.Title;
              result.Author = book.Author;
              result.Year = book.Year;

              _context.SaveChanges();

              return result;
          }
          return null;
      }
    */
    public Book? SaveBook(Book book)
    {
        if (book.Id == 0)
        {
            var result = _context.Books.Add(book);
            _context.SaveChanges();
            return result.Entity;
        }
        else
        {
            var existingBook = _context.Books.Find(book.Id);

            if (existingBook == null)
            {
                return null;
            }
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Year = book.Year;
            existingBook.PublishDate = book.PublishDate;
            _context.SaveChanges();
            return existingBook;
        }
    }

    public void DeleteBook(int bookId)
        {
            var result = _context.Books.Find(bookId);
            if (result != null)
            {
                _context.Books.Remove(result);
                _context.SaveChanges();
            }
        }
        public Book? GetBook(int bookId)
        {
            return _context.Books.Find(bookId);
        }
        public IEnumerable<Book> GetBooks()
        {
            return _context.Books.ToList();
        }
    }
