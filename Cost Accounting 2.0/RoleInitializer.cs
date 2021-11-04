using Cost_Accounting_2._0.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Cost_Accounting_2._0
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            foreach (var role in Enum.GetNames(typeof(Role)))
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            User admin = new User { Email = "admin@mail.ru", UserName = "admin@mail.ru" };

            IdentityResult adminResult = null;
            var userFind = await userManager.FindByIdAsync("admin@mail.ru");

            if (userFind == null)
                adminResult = await userManager.CreateAsync(admin, "admin");

            if (adminResult?.Succeeded == true)
            {
                await userManager.AddToRoleAsync(admin, Role.Admin.ToString());
            }
        }
    }
}