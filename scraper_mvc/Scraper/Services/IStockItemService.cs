using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Scraper.Models;


namespace Scraper.Services
{
    public interface IStockItemService
    {
        Task<StockItem[]> GetSnapshotStocksAsync(Guid snapshotId);
    }
}
