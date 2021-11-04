using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cost_Accounting_2._0.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<HistorySign> HistorySigns { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Transaction>().Property(t => t.Amount).HasDefaultValue(0);
            builder.Entity<Transaction>().HasOne(t => t.DebitAccount).WithMany(da => da.Transactions).OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(builder);
        }
    }
}
