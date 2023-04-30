using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
}
