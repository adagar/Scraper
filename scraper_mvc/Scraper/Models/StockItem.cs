using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Scraper.Models
{
    public class StockItem
    {
        [BsonElement("Id")]
        public Guid Id { get; set; }
        [BsonElement("symbol")]
        public string symbol { get; set; }
        [BsonElement("lastPrice")]
        public double lastPrice { get; set; }
        [BsonElement("change")]
        public double change { get; set; }
        [BsonElement("percentChange")]
        public double percentChange { get; set; }
        [BsonElement("volume")]
        public double volume { get; set; }
        [BsonElement("snapshotTime")]
        public DateTime snapshotTime { get; set; }
        
    }
}
