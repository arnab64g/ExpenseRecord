using System.ComponentModel.DataAnnotations;

namespace ExpenseRecordMVC.Models
{
    public class ExpenseViewCore
    {
        public DateTimeOffset? Date { get; set; }
        
        public decimal? Amount { get; set; }

        [Display(Name = "Select a category")]
        public string? CategoryName { get; set; }
    }

    public class ExpenseAddModel : ExpenseViewCore
    {
        public List<string>? Categories { get; set; } = new List<string>();
    }

    public class ExpenseEditModel : ExpenseAddModel
    {
        public int Id { get; set; }
    }
}
