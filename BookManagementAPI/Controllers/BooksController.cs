using BookManagementAPI.Models;
using BookManagementAPI.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _bookRepository.GetBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _bookRepository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
/*
        [HttpPost]
        public IActionResult CreateBook(Book book)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(book);
            if (!Validator.TryValidateObject(book, validationContext, validationResults, true))
            {
                return BadRequest(validationResults);
            }

            var createdBook = _bookRepository.AddBook(book);
            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest("ID in URL does not match ID in body.");
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(book);
            if (!Validator.TryValidateObject(book, validationContext, validationResults, true))
            {
                return BadRequest(validationResults);
            }

            var updatedBook = _bookRepository.UpdateBook(book);
            if (updatedBook == null)
            {
                return NotFound();
            }
            return Ok(updatedBook);
        }
*/
        [HttpPost("save")]
        public IActionResult SaveBook(Book book)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(book);
            if (!Validator.TryValidateObject(book, validationContext, validationResults, true))
            {
                return BadRequest(validationResults);
            }
            var savedBook = _bookRepository.SaveBook(book);
            if (savedBook == null)
                return NotFound($"Book with Id {book.Id} not found.");
            if (book.Id == 0) 
                return CreatedAtAction(nameof(GetBook), new { id = savedBook.Id }, savedBook);
            return Ok(savedBook);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookRepository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            _bookRepository.DeleteBook(id);
            return NoContent();
        }
    }
}