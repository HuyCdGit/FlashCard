using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashCardAppWebApi.Models
{
    [Table("category")]
    public class Category
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "Category name cannot exceed 255 characters.")]
        [Column("categoryName")]
        public string CategoryName { get; set; } = "";

        // Navigation property
        public required ICollection<UserCategory> UserCategory { get; set; }
        public required ICollection<FlashcardCategory> FlashcardCategory { get; set; }
    }
}