﻿using Cost_Accounting_2._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Cost_Accounting_2._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(!User.Identity.IsAuthenticated)
                return View();
            return RedirectToAction("Index", "Transaction");
        }
    }
}
