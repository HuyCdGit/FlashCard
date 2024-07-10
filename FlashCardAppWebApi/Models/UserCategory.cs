using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCardAppWebApi.Models
{
    [Table("user_category")]
    public class UserCategory
    {
        [ForeignKey("User")]
        [Required(ErrorMessage = "User_Id is required.")]
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("Category")]
        [Required(ErrorMessage = "Category_Id is required.")]
        [Column("category_id")]
        public int CategoryId { get; set; }

        //navigation property
        public User? User { get; set; }
        public Category? Category { get; set; }


    }
}