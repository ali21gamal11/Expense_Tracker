using Microsoft.EntityFrameworkCore;
namespace Expense_Tracker.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
       DbContextOptions<ApplicationDbContext> options)
       : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>()
            .Property(e => e.Amount)
            .HasPrecision(18, 2);
    }

}
