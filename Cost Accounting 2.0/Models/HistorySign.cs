using System;
using System.Collections.Generic;

namespace Cost_Accounting_2._0.Models
{
    public class HistorySign
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime DateTime { get; set; }
        public string Action { get; set; }
    }
}
