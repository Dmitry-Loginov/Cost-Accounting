using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cost_Accounting_2._0.Models
{
    public enum TypeBills
    {
        Active,
        Passive
    }

    public class Bill
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="The field Name is required")]
        public string Name { get; set; }
        [Display(Name ="User")]
        public string UserId { get; set; }
        public User User { get; set; }
        [Required(ErrorMessage ="The fiels Balance is required")]
        [Column(TypeName = "decimal(10, 4)")]
        [Display(Name="Balance")]
        public decimal StartAmount { get; set; }
        [Display(Name ="Type bill")]
        public int TypeBillId { get; set; }
        public TypeBill TypeBill { get; set; }
        public List<Transaction> CreditTransactions { get; set; }
        public List<Transaction> DebitTransactions { get; set; }
    }
}
