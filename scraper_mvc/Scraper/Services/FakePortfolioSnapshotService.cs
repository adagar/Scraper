using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scraper.Models;

namespace Scraper.Services
{
    public class FakePortfolioSnapshotService : IStockItemService
    {
        public Task<StockItem[]> GetPortfolioAsync()
        {
            var stock1 = new StockItem
            {
                symbol = "DPZ",
                lastPrice = 271.75,
                change = -5.42,
                percentChange = -1.96,
                volume = 690500000,
                snapshotTime = DateTime.Now
            };

            var stock2 = new StockItem
            {
                symbol = "EPIX",
                lastPrice = 371.75,
                change = 6.42,
                percentChange = 2.96,
                volume = 230,
                snapshotTime = DateTime.Now
            };

            return Task.FromResult(new[] { stock1, stock2 });
        }
    }
}
