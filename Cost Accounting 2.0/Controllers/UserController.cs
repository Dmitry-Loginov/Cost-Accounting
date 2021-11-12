using Cost_Accounting_2._0.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Cost_Accounting_2._0.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationContext _context;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(_userManager.Users.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string ids)
        {
            if (ids == null)
                return Redirect("Index");
            string[] idsSplit = ids.Split(',');
            var currentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;

            if (idsSplit.Contains(currentUser.Id))
            {
                await _signInManager.SignOutAsync();
            }
            Models.User[] users = new User[idsSplit.Length];
            for (int i = 0; i < idsSplit.Length; i++)
            {
                users[i] = _userManager.FindByIdAsync(idsSplit[i]).Result;
            }

            for (int i = 0; i < idsSplit.Length; i++)
            {
                await _userManager.DeleteAsync(users[i]);
            }
            return Redirect("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Block(string ids)
        {
            if (ids == null)
                return Redirect("Index");
            var currentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            string[] idsSplit = ids.Split(',');
            if (idsSplit.Contains(currentUser.Id))
            {
                await _signInManager.SignOutAsync();
            }
            Models.User[] users = new User[idsSplit.Length];
            for (int i = 0; i < idsSplit.Length; i++)
            {
                users[i] = _userManager.FindByIdAsync(idsSplit[i]).Result;
            }

            for (int i = 0; i < idsSplit.Length; i++)
            {
                users[i].Status = Status.Block;
                await _userManager.UpdateAsync(users[i]);
            }
            return Redirect("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Unblock(string ids)
        {
            if (ids == null)
                return Redirect("Index");
            string[] idsSplit = ids.Split(',');
            Models.User[] users = new User[idsSplit.Length];
            for (int i = 0; i < idsSplit.Length; i++)
            {
                users[i] = _userManager.FindByIdAsync(idsSplit[i]).Result;
            }

            for (int i = 0; i < idsSplit.Length; i++)
            {
                users[i].Status = Status.Active;
                await _userManager.UpdateAsync(users[i]);
            }
            return Redirect("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Restore(string ids)
        {
            if (ids == null)
                return Redirect("Index");
            string[] idsSplit = ids.Split(',');
            Models.User[] users = new User[idsSplit.Length];
            for (int i = 0; i < idsSplit.Length; i++)
            {
                users[i] = _userManager.FindByIdAsync(idsSplit[i]).Result;
            }

            for (int i = 0; i < idsSplit.Length; i++)
            {
                users[i].IsDeleted = false;
                await _userManager.UpdateAsync(users[i]);
            }
            return Redirect("Index");
        }
    }
}
