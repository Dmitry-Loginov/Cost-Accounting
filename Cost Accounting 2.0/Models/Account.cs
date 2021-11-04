using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cost_Accounting_2._0.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
