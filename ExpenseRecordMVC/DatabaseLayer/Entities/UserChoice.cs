using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.Entities
{
    public class UserChoice
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public int? CategoryID { get; set; }
    }
}
