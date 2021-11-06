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
        public List<Bill> Bills { get; set; } = new List<Bill>();
        public List<HistorySign> HistorySignIns { get; set; } = new List<HistorySign>();
    }
}
