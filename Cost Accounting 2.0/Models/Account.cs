using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cost_Accounting_2._0.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public User User { get; set; }
    }
}
