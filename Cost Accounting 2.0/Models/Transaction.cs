using System;
using System.ComponentModel.DataAnnotations;

namespace Cost_Accounting_2._0.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int CreditAccountId { get; set; }
        public Account CreditAccount { get; set; }
        [Required]
        public int DebitAccountId { get; set; }
        public Account DebitAccount { get; set; }
        public User User { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
