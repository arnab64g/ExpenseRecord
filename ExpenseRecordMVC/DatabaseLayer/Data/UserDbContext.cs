using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Data
{
    public class UserDbContext : IdentityDbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<UserChoice> UserChoices { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }
    }
}
