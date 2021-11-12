using Cost_Accounting_2._0.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cost_Accounting_2._0.ViewModels
{
    public enum TypeBill
    {
        Active,
        Passive
    }

    public class BillViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 4)")]
        [Display(Name = "Balance")]
        public decimal StartAmount { get; set; }
        [Required]
        public int TypeBillId { get; set; }
        public TypeBill TypeBill { get; set; }
    }
}
