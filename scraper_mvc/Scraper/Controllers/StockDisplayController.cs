using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Scraper.Controllers
{
    public class StockDisplayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}