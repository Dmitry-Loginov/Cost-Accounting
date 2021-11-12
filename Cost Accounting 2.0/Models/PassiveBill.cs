using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cost_Accounting_2._0.Models
{
    public class PassiveBill
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
        public List<Transaction> Transactions { get; set; }
    }
}
