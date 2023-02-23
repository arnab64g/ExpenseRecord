using System.ComponentModel.DataAnnotations;

namespace ExpenseRecordMVC.Models
{
    public class ExpenseAddModel
    {
        public DateTimeOffset Date { get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "Select a category")]
        public int CategoryId { get; set; }
        public List<CategorySelectItem>? Categories { get; set; }
    }
}
