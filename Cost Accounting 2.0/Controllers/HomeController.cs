using Cost_Accounting_2._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace Cost_Accounting_2._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        ApplicationContext Context { get; set; }

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            Context = context;
        }

        public IActionResult Index()
        {
            return View(Context.Transactions.Include(t => t.User).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
