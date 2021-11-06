using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cost_Accounting_2._0.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [Display(Name ="Credit")]
        public string Credit { get; set; }
        public List<SelectListItem> CreditListBills { get; set; }
        [Required]
        [Display(Name = "Debit")]
        public string Debit { get; set; }
        public List<SelectListItem> DebitListBills { get; set; }
        [Required]
        public DateTime Date { get; set; }

    }
}
