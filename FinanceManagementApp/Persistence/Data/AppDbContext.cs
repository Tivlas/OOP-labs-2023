using Domain.Entities;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;
public class AppDbContext : DbContext
{
    public DbSet<SimpleAccount> SimpleAccounts { get; set; }
    public DbSet<TransactionCategory> TransactionCategories { get; set; }
    public DbSet<SimpleTransaction> SimpleTransactions { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().HasAlternateKey(u => u.Email);
        modelBuilder.Entity<User>().HasMany(u => u.SimpleAccounts).WithOne(sa => sa.User).HasForeignKey(u => u.UserId);
        modelBuilder.Entity<User>().HasMany(u => u.SimpleTransactions).WithOne(st => st.User).HasForeignKey(u => u.UserId);
        modelBuilder.Entity<User>().HasMany(u => u.TransactionCategories).WithOne(tc => tc.User).HasForeignKey(u => u.UserId);
        modelBuilder.Entity<SimpleTransaction>().HasOne(st => st.SimpleAccount).WithMany(sa => sa.SimpleTransactions).HasForeignKey(st => st.SimpleAccountId);
    }
}
