using DatabaseLayer.Entities;

namespace ExpenseRecordMVC.Models
{
    public class FilteredViewModel
    {
        public DateTimeOffset? FromDate { get; set; }
        public DateTimeOffset? ToDate { get; set;}
        public decimal? TotalCost { get; set; }
        public IEnumerable<ExpenseView> expenseViews { get; set; }  
    }
}
