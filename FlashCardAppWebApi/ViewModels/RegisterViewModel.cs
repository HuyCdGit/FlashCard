using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCardAppWebApi.ViewModels
{
    public class RegisterViewModel
    {
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; } = "";
        public string? Phone { get; set; }
        public string? FullName { get; set; }
        public DateTime? DateOfBrith {get; set;}
        public string? Country { get; set; }
    }
    
}