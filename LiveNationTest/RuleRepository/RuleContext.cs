using NumberConverter.Rules;
using Microsoft.EntityFrameworkCore;

namespace RuleRepository
{
    public class RuleContext : DbContext
    {
        public RuleContext(DbContextOptions<RuleContext> options)
            : base(options)
        {
            Console.WriteLine("context created");
        }

        public DbSet<Rule> Rules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("context seeding");
            modelBuilder.Entity<Rule>().HasKey(r => r.Id);

            // Seed the database with default values
            modelBuilder.Entity<Rule>().HasData(
                new Rule() { Id = 1, RuleOperator = RuleOperator.DivisibleBy, Operand = 3, ExpectedResult = 0, ResultString = "Live" },
                new Rule() { Id = 2, RuleOperator = RuleOperator.DivisibleBy, Operand = 5, ExpectedResult = 0, ResultString = "Nation" }
            );
        }
    }
}