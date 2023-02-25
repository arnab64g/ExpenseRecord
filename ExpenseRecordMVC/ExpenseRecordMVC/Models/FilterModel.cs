namespace ExpenseRecordMVC.Models
{
    public class FilterModel
    {
        public DateTimeOffset? FromDate { get; set; } 
        public DateTimeOffset? ToDate { get; set; } 
        public List<string>? CategoryFilters { get; set; }
    }
}
