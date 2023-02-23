using DatabaseLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Data
{
    public class ExpenseDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<UserChoice> UserChoices { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conStr = @"Server=(localdb)\mssqllocaldb;Database=ExpenseData;Trusted_Connection=True";

            optionsBuilder.UseSqlServer(conStr);
        }
    }
}
