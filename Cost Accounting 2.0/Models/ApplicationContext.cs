using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cost_Accounting_2._0.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<ActiveBill> ActiveBills { get; set; }
        public DbSet<PassiveBill> PassiveBills { get; set; }
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
            builder.Entity<ActiveBill>().HasIndex(u => u.Name);

            builder.Entity<History>().HasOne(t => t.User).WithMany(da => da.Histories).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<HistorySign>().HasOne(t => t.User).WithMany(da => da.HistorySignIns).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ActiveBill>().HasOne(t => t.User).WithMany(da => da.Bills).OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(builder);
        }
    }
}
