﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cost_Accounting_2._0.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName ="decimal(10, 4)")]
        public decimal Amount { get; set; }
        public int CreditBillId { get; set; }
        [Display(Name ="Credit")]
        public ActiveBill CreditBill { get; set; }
        public int DebitBillId { get; set; }
        [Display(Name = "Debit")]
        public PassiveBill DebitBill { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
