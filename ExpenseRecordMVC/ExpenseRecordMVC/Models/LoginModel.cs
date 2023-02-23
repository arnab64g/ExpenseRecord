using System.ComponentModel.DataAnnotations;

namespace ExpenseRecordMVC.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Username or Email")]
        public string? UsernameOrPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
