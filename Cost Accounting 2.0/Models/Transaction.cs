using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cost_Accounting_2._0.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public int CreditAccountId { get; set; }
        public Account CreditAccount { get; set; }
        public int DebitAccountId { get; set; }
        public Account DebitAccount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public List<History> Histories { get; set; }
    }
}
