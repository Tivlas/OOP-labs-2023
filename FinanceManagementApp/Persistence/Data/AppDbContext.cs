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
        //Database.EnsureDeleted(); // на время разработки
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasAlternateKey(u => u.Email);
        modelBuilder.Entity<User>().HasMany(u => u.SimpleAccounts).WithOne(sa => sa.User).HasForeignKey(sa => sa.UserId);
        modelBuilder.Entity<User>().HasMany(u => u.SimpleTransactions).WithOne(st => st.User).HasForeignKey(st => st.UserId);
        modelBuilder.Entity<User>().HasMany(u => u.TransactionCategories).WithOne(tc => tc.User).HasForeignKey(tc => tc.UserId);
        modelBuilder.Entity<SimpleTransaction>().HasOne(st => st.SimpleAccount).WithMany(sa => sa.SimpleTransactions).HasForeignKey(st => st.SimpleAccountId);
        modelBuilder.Entity<SimpleTransaction>().HasOne(st => st.Category).WithMany(tc => tc.SimpleTransactions).HasForeignKey(st => st.CategoryId);

        modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<TransactionCategory>().Property(tc => tc.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<SimpleTransaction>().Property(st => st.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<SimpleAccount>().Property(sa => sa.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<SimpleAccount>().HasAlternateKey(sa => sa.Name);
    }
}
