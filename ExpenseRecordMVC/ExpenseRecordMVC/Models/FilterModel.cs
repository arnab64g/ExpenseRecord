using System.ComponentModel.DataAnnotations;

namespace ExpenseRecordMVC.Models
{
    public class FilterModel
    {
        [Display(Name ="From Date")]
        public DateTimeOffset? FromDate { get; set; }

        [Display(Name = "To Date")]
        public DateTimeOffset? ToDate { get; set; } 

        public HashSet<string>? CategoryFilters { get; set; }
    }
}
