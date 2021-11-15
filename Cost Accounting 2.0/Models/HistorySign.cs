using System;
using System.ComponentModel.DataAnnotations;

namespace Cost_Accounting_2._0.Models
{
    public class HistorySign
    {
        public int Id { get; set; }
        public User User { get; set; }
        [Display(Name ="Date, time")]
        public DateTime DateTime { get; set; }
        public string Action { get; set; }
    }
}
