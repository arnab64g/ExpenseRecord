using System.ComponentModel.DataAnnotations;
using ExtraLibraries;

namespace ExpenseRecordMVC.Models
{
    public class ExpenseViewCore
    {
        [CustomValidayion(ErrorMessage = "Insert a Valid Date")]
        public DateTimeOffset? Date { get; set; } = DateTimeOffset.Now;
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
