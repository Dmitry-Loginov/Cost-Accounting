using System;

namespace Cost_Accounting_2._0.Models
{
    public class History
    {
        public int Id { get; set; }
        public string TypeObject { get; set; }
        public int ObjectId { get; set; }
        public string TypeOperation { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}
