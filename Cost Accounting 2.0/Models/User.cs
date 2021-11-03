using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Cost_Accounting_2._0.Models
{
    public enum Role
    {
        Admin,
        User
    }

    public class User : IdentityUser
    {
        public List<Account> Accounts { get; set; } = new List<Account>();
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public List<HistorySign> HistorySignIns { get; set; } = new List<HistorySign>();
    }
}
