using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Entities
{
    public class UserDetails
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Phones { get; set; }
        public string? Address { get; set; }
        public byte[]? Photo { get; set; }

        [Display(Name ="Total Cost")]
        public decimal? TotalCost { get; set; }
        public DateTimeOffset? Created { get; set; }
    }
}
