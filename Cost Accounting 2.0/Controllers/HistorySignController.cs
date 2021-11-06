using Cost_Accounting_2._0.Models;
using Cost_Accounting_2._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Cost_Accounting_2._0.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HistorySignController : Controller
    {
        ApplicationContext Context { get; set; }
        public HistorySignController(ApplicationContext context)
        {
            Context = context;
        }
        
        public IActionResult Index()
        {
            List<HistorySign> historySigns = Context.HistorySigns.Include(h => h.User).ToList();
            var viewModel = FillHistorySignViewModel(historySigns);
            return View(viewModel);
        }

        List<HistorySignViewModel> FillHistorySignViewModel(List<HistorySign> historySigns)
        {
            List<HistorySignViewModel> models = new List<HistorySignViewModel>();
            foreach (var item in historySigns)
            {
                models.Add(new HistorySignViewModel {Id = item.Id, Action = item.Action, DateTime = item.DateTime, UserName = item.User.UserName});
            }
            return models;
        }
    }
}
