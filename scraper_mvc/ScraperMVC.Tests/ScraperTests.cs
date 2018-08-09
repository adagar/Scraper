using System;
using Scraper;
using NUnit.Framework;
using System.Threading.Tasks;


namespace ScraperMVC.Tests
{
    [TestFixture]
    public class ScraperTests
    {
        [Test]
        public void ScraperShouldReturn10Items()
        {
            var scraper = new Scraper.Services.ScrapeService();

            Task<Scraper.Models.StockItem[]> output = scraper.ScrapePortfolioAsync();
            output.Wait();
            Assert.AreEqual(output.Result.Length, 10);
        }
    }
}
