using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Entities
{
    public class ExpenseView
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset? Date { get; set; }
        [Display(Name = "Category Name")]
        public string? CategoryName { get; set; }
        public decimal? Amount { get; set; }
    }
}
