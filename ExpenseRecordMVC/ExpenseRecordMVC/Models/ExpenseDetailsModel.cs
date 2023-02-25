using System.ComponentModel.DataAnnotations;

namespace ExpenseRecordMVC.Models
{
    public class ExpenseDetailsModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }

        [Display(Name = "Category Name")]
        public string? CategoryName { get; set; }
        public DateTimeOffset? Date { get; set; }
        public decimal? Amount { get; set; }
        [Display(Name = "Description")]
        public string? Discripyion { get; set;}
    }
}
