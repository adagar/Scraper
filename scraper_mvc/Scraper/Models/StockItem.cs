using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scraper.Models
{
    public class StockItem
    {
        
        public Guid Id { get; set; }
        public Guid SnapshotId { get; set; }
        public string symbol { get; set; }
        public double lastPrice { get; set; }
        public double change { get; set; }
        public double percentChange { get; set; }
        public double volume { get; set; }
        public DateTime snapshotTime { get; set; }
        
    }
}
