using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scraper.Services;
using Scraper.Models;


namespace Scraper.Controllers
{
    public class ScrapeController : Controller
    {
        private readonly IScrapeService _scrapeService;

        public ScrapeController(IScrapeService scrapeService)
        {
            _scrapeService = scrapeService;
        }
        public async Task<IActionResult> Index()
        {
            var stocks = await _scrapeService.ScrapePortfolioAsync();

            var model = new StockViewModel()
            {
                Stocks = stocks
            };


            return View(model);
        }
    }
}