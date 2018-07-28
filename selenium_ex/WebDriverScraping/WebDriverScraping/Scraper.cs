using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Lambda.Core;

[assembly: LambdaSerializerAttribute (typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace WebDriverScraping
{
    class Scraper
    {
        public object TestFunction()
        {
            return new
            {
                message = "Hello from Lambda!",
                time = DateTime.Now
            };
        }
    }
}
