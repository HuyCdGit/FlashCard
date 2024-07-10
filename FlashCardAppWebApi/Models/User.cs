using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashCardAppWebApi.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(100)]
        [Column("username")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "HashedPassword is required")]
        [MaxLength(200)]
        [Column("hashed_password")]
        public string HashedPassword { get; set; } = "";

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(255)]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^0\d{10}$", ErrorMessage = "Invalid phone number")]
        [MaxLength(20)]
        [Column("phone")]
        public string Phone { get; set; } = "";

        [Required(ErrorMessage = "FullName is required")]
        [MaxLength(255)]
        [Column("full_name")]
        public string FullName { get; set; } = "";

        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(200)]
        [Column("country")]
        public string Country { get; set; } = "";
        //navigation property
        public required ICollection<UserCategory> UserCategory { get; set; }

    }
}
