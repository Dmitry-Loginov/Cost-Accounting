using Cost_Accounting_2._0.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cost_Accounting_2._0
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager, ApplicationContext context)
        {
            foreach (var role in Enum.GetNames(typeof(Role)))
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            foreach (var type in Enum.GetNames(typeof(TypeBill)))
            {
                if (context.TypeBills.ToList().Count < 2)
                {
                    context.TypeBills.Add(new TypeBill { Type = type });
                }
                context.SaveChanges();
            }
        }
    }
}