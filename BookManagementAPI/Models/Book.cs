using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Year must be greater than 0")]
        public int Year { get; set; }
    }
}
