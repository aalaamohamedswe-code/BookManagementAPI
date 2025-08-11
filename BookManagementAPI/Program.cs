using BookManagementAPI;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static System.Reflection.Metadata.BlobBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookDB>(opt => opt.UseInMemoryDatabase("BookDb"));

var app = builder.Build();

app.MapPost("/api/books", (Book book, BookDB context) =>
{
    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(book);

    if (!Validator.TryValidateObject(book, validationContext, validationResults, true))
    {
        return Results.BadRequest(validationResults);
    }
    context.Books.Add(book);
    context.SaveChanges();

    return Results.Created($"/books/{book.Id}", book);
});
app.MapGet("/api/books/{id}", (int id, BookDB db) =>
{
    var book = db.Books.Find(id);  
    if (book != null)
        return Results.Ok(book);   
    else
        return Results.NotFound(); 
});
app.MapGet("/api/books", (BookDB db) =>
{
    var books = db.Books.ToList();  
    return Results.Ok(books);       
});

app.MapDelete("/api/books/{id}", (int id, BookDB db) =>
{
    var book = db.Books.Find(id);  
    if (book == null)
        return Results.NotFound();  

    db.Books.Remove(book);  
    db.SaveChanges();      

    return Results.NoContent();  
});
app.MapPut("/api/books/{id}", (int id, Book updatedBook, BookDB db) =>
{
    var book = db.Books.Find(id);
    if (book == null) return Results.NotFound();

    book.Title = updatedBook.Title;
    book.Author = updatedBook.Author;
    book.Year = updatedBook.Year;

    db.SaveChanges();
    return Results.Ok();
});

app.Run();
