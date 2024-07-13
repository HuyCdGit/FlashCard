using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashCardAppWebApi.Models
{
    [Table("flashcards")]
    public class Flashcard
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Term is required.")]
        [MaxLength(255, ErrorMessage = "Term cannot exceed 255 characters.")]
        [Column("term")]
        public string? Term { get; set; }

        [Required(ErrorMessage = "Definition is required.")]
        [Column("definition")]
        public string? Definition { get; set; }

        [MaxLength(1000, ErrorMessage = "Image URL cannot exceed 1000 characters.")]
        [Column("image")]
        public string? Image { get; set; }

        [DataType(DataType.Date)]
        [Column("created_date")]
        public DateTime? CreatedDate { get; set; }

        //navigation property
        public required ICollection<FlashcardCategory> FlashcardCategory { get; set; }
    }
}