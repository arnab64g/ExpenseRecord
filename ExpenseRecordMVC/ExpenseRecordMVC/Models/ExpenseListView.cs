using System.ComponentModel.DataAnnotations;

namespace ExpenseRecordMVC.Models
{
    public class ExpenseListView
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public string? CategoryName { get; set; }
        public string? Amount { get; set; }
    }
}
