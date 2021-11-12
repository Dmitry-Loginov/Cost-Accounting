using Cost_Accounting_2._0.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cost_Accounting_2._0.ViewModels
{
    public class TransactionListViewModel
    {
        [Required]
        [Column(TypeName = "decimal(10, 4)")]
        public decimal AmountFrom { get; set; }
        public decimal AmountTo { get; set; }
        [Required]
        [Display(Name = "Credit")]
        public string Credit { get; set; }
        public List<SelectListItem> CreditListBills { get; set; }
        [Required]
        [Display(Name = "Debit")]
        public string Debit { get; set; }
        public List<SelectListItem> DebitListBills { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
