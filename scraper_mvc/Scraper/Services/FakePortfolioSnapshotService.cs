using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scraper.Models;

namespace Scraper.Services
{
    public class FakePortfolioSnapshotService : IStockItemService
    {
        public Task<StockItem[]> GetPorfolioAsync()
        {
            var stock1 = new StockItem
            {

            }
        }
    }
}
