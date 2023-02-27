using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Entities
{
    [Display(Name = "User Choice")]
    public class UserChoice
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Username { get; set; }
        
        [Required(ErrorMessage = "Category name cannot be empty.")]
        [MaxLength(50)]
        [Display(Name = "Category Name")]
        public string? CategoryName { get; set; }
    }
}
