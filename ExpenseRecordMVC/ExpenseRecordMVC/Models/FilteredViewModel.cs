using DatabaseLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace ExpenseRecordMVC.Models
{
    public class FilteredViewModel
    {
        [Display(Name = "From Date")]
        public DateTimeOffset? FromDate { get; set; }

        [Display(Name = "To Date")]
        public DateTimeOffset? ToDate { get; set;}

        [Display(Name = "Total Cost")]
        public decimal? TotalCost { get; set; }
        public IEnumerable<ExpenseView> expenseViews { get; set; }  
    }
}
