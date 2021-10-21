using Microsoft.AspNetCore.Identity;

namespace Cost_Accounting_2._0.Models
{
    enum Role
    {
        Admin,
        User
    }

    public class User : IdentityUser
    {

    }
}
