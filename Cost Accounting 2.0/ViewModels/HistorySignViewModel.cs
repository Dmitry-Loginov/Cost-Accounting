using System;
using System.ComponentModel.DataAnnotations;

namespace Cost_Accounting_2._0.ViewModels
{
    public class HistorySignViewModel
    {
        public int Id { get; set; }
        [Display(Name ="User name")]
        public string UserName { get; set; }
        [Display(Name ="Date, time")]
        public DateTime DateTime { get; set; }
        public string Action { get; set; }
    }
}
