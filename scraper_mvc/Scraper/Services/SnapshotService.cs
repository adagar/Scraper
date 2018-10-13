using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scraper.Models;
using Scraper.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Scraper.Services
{
    public class SnapshotService : ISnapshotService
    {
        private readonly ApplicationDbContext _context;

        public SnapshotService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Snapshot[]> GetSnapshotsAsync()
        {
            var stocks = await _context.Stocks
                .ToArrayAsync();

            foreach(var stock in stocks)
            {
                Debug.WriteLine(
                    "Symbol:" + stock.symbol +
                    " Snapshot:" + stock.SnapshotId +
                    " SnapshotTime:" + stock.snapshotTime);
            }

            var snapshotCount = stocks.Count() / 10;
            Snapshot[] snapshotArray = new Snapshot[snapshotCount];

            for(var i = 0; i < snapshotCount; i++)
            {
                var stocksFromOneSnapshot = new StockItem[10];

                for(var j = 0; j < 10; j++)
                {
                    stocksFromOneSnapshot[j] = stocks[i * 10 + j];
                }

                snapshotArray[i] = new Snapshot
                {
                    Id = stocksFromOneSnapshot[0].SnapshotId,
                    SnapshotTime = stocksFromOneSnapshot[0].snapshotTime,
                    SnapshotStocks = stocksFromOneSnapshot
                };
            }

            var testSnapshot1 = new Snapshot
            {
                Id = Guid.NewGuid(),
                SnapshotStocks = new StockItem[] { stocks[0], stocks[1], stocks[2] },
                SnapshotTime = stocks[0].snapshotTime
            };

            var testSnapshot2 = new Snapshot
            {
                Id = Guid.NewGuid(),
                SnapshotStocks = new StockItem[] { stocks[3], stocks[4], stocks[5] },
                SnapshotTime = stocks[10].snapshotTime
            };

            return snapshotArray;
        }
    }
}
