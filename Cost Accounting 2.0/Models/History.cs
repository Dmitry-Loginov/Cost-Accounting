using System;
using System.ComponentModel.DataAnnotations;

namespace Cost_Accounting_2._0.Models
{
    public class History
    {
        public int Id { get; set; }
        [Display(Name ="Type object")]
        public string TypeObject { get; set; }
        [Display(Name ="Object id")]
        public int ObjectId { get; set; }
        [Display(Name = "Type operation")]
        public string TypeOperation { get; set; }
        [Display(Name ="User id")]
        public string UserId { get; set; }
        public User User { get; set; }
        [Display(Name ="Date, time")]
        public DateTime DateTime { get; set; }
    }
}
