using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Entities
{
    public class UserDetailsCore
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(50)]
        public string? Username { get; set; }

        [MaxLength(20)]
        public string? Phones { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }
        public byte[]? Photo { get; set; }

        public DateTimeOffset? Created { get; set; }
    }

    public class UserDetails : UserDetailsCore
    {
        [Display(Name = "Total Cost")]
        public decimal? TotalCost { get; set; }
    }
}
