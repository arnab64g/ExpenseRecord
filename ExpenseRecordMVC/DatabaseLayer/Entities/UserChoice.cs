using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Entities
{
    public class UserChoice
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        [Required(ErrorMessage = "Category name cannot be empty.")]
        public string? CategoryName { get; set; }
    }
}
