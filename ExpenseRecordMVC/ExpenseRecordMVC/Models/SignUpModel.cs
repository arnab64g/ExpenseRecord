using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ExpenseRecordMVC.Models
{
    public class SignUpModel
    {
        [Key]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Name field is required")]
        [Display(Name = "Full Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Username required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        public string? Phone { get; set; }

        public string? Address { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set;}

        [Required]
        [Display(Name = "Retype Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password does not match")]
        public string? RetypePassword { get; set;}

        [Required]
        [Display(Name = "Select an Image")]
        public IFormFile? ImageUrl { get; set;}

    }
}
