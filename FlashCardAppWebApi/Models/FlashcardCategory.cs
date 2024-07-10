using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashCardAppWebApi.Models
{
    [Table("flashcard_category")]
    public class FlashcardCategory
    {
        [ForeignKey("Flashcard")]
        [Required(ErrorMessage = "Flashcard_Id is required.")]
        [Column("flashcard_id")]
        public int FlashcardId { get; set; }

        [ForeignKey("Category")]
        [Required(ErrorMessage = "Category_Id is required.")]
        [Column("category_id")]
        public int CategoryId { get; set; }

        //navigation property
        public Flashcard? Flashcard { get; set; }
        public Category? Category { get; set; }
    }
}