using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Entities
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Username { get; set; }
        
        public int? CategoryId { get; set; }
        public DateTimeOffset? Date { get; set; }
        
        public decimal? Amount { get; set; }

        [MaxLength(200)]
        public string? Discription { get; set; }
    }
}
