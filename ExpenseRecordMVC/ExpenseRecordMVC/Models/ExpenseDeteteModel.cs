namespace ExpenseRecordMVC.Models
{
    public class ExpenseDetailsModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? CategoryName { get; set; }
        public DateTimeOffset? Date { get; set; }
        public decimal? Amount { get; set; }
    }
}
