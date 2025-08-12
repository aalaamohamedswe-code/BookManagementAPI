using BookManagementAPI;
using BookManagementAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static System.Reflection.Metadata.BlobBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookDB>(opt => opt.UseInMemoryDatabase("BookDb"));
builder.Services.AddScoped<IBookRepository, BookRepository>();

var app = builder.Build();

app.MapPost("/api/books", (Book book, IBookRepository repo) =>
{
    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(book);

    if (!Validator.TryValidateObject(book, validationContext, validationResults, true))
    {
        return Results.BadRequest(validationResults);
    }

    var createdBook = repo.AddBook(book);
    return Results.Created($"/books/{createdBook.Id}", createdBook);
});

app.MapGet("/api/books/{id}", (int id, IBookRepository repo) =>
{
    var book = repo.GetBook(id);
    if (book != null)
        return Results.Ok(book);
    else
        return Results.NotFound();
});

app.MapGet("/api/books", (IBookRepository repo) =>
{
    var books = repo.GetBooks();
    return Results.Ok(books);
});

app.MapDelete("/api/books/{id}", (int id, IBookRepository repo) =>
{
    var book = repo.GetBook(id);
    if (book == null)
        return Results.NotFound();

    repo.DeleteBook(id);
    return Results.NoContent();
});

app.MapPut("/api/books/{id}", (int id, Book updatedBook, IBookRepository repo) =>
{
    var book = repo.GetBook(id);
    if (book == null) return Results.NotFound();

    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(updatedBook);

    if (!Validator.TryValidateObject(updatedBook, validationContext, validationResults, true))
    {
        return Results.BadRequest(validationResults);
    }

    updatedBook.Id = id;
    var result = repo.UpdateBook(updatedBook);
    return Results.Ok(result);
});


app.Run();
