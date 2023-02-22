using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name of Category")]
        [Required(ErrorMessage = "Category name should not be empty")]
        public string? Name { get; set; }
    }
}
