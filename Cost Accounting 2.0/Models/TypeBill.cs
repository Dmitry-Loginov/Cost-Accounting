
using System.Collections.Generic;

namespace Cost_Accounting_2._0.Models
{
    public class TypeBill
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public List<Bill> Bills { get; set; }
    }
}
