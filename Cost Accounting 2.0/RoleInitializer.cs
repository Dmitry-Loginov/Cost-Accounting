using Cost_Accounting_2._0.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Cost_Accounting_2._0
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetNames(typeof(Role)))
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}