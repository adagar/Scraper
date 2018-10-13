using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scraper.Data;
using Microsoft.EntityFrameworkCore;
using Scraper.Models;

namespace Scraper.Services
{
    public class StockDisplayService : IStockItemService
    {
        private readonly ApplicationDbContext _context;

        public StockDisplayService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StockItem[]> GetSnapshotStocksAsync(Guid snapshotId)
        {
            return await _context.Stocks
                .Where(x => x.SnapshotId == snapshotId)
                .ToArrayAsync();
        }

    }
}
