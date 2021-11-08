using Cost_Accounting_2._0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cost_Accounting_2._0.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HistoryController : Controller
    {
        ApplicationContext Context { get; set; }
        public HistoryController(ApplicationContext context)
        {
            Context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<History> histories = await Context.Histories.Include(h => h.User).ToListAsync();
            return View(histories);
        }
    }
}
