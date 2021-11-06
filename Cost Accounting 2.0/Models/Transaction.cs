using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cost_Accounting_2._0.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName ="decimal(18, 18)")]
        public decimal Amount { get; set; }
        public int CreditBillId { get; set; }
        public Bill CreditBill { get; set; }
        public int DebitBillId { get; set; }
        public Bill DebitBill { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
