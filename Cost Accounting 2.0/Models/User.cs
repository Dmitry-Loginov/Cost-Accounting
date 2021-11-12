using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cost_Accounting_2._0.Models
{
    public enum Role
    {
        Admin,
        User
    }

    public enum Status
    {
        Active,
        Block
    }

    public class User : IdentityUser
    {
        [Required]
        public Status Status { get; set; }
        public List<Bill> Bills { get; set; } = new List<Bill>();
        public List<HistorySign> HistorySignIns { get; set; } = new List<HistorySign>();
        public List<History> Histories { get; set; } = new List<History>();
    }
}
