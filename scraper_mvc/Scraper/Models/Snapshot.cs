using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scraper.Models
{
    public class Snapshot
    {
        public Guid Id { get; set; }
        public StockItem[] SnapshotStocks { get; set; }
        public DateTime SnapshotTime { get; set; }
    }
}
