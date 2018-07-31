﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Scraper.Models;

namespace Scraper.Controllers
{
    public class ScrapeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var stocks = await RunScraper();

            var model = new StockViewModel()
            {
                Stocks = stocks
            };


            return View(model);
        }

        public Task<StockItem[]> RunScraper()
        {
            Debug.WriteLine("UNLEASH THE SCRAPER!");
            ChromeOptions options = new ChromeOptions();
            //System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", @"C:\chromedriver.exe");
            //options.AddArguments("headless");
            //options.AddArguments("window-size=1200x600");

                using (var driver = new ChromeDriver(@"C:\", options))
                {
                    Debug.WriteLine("Driver found!");
                    /*
                     // * Sample for logging into testing ground
                    driver.Navigate().GoToUrl("http://testing-ground.scraping.pro/login");
                    var userNameField = driver.FindElementById("usr");
                    var userPasswordField = driver.FindElementById("pwd");
                    var loginButton = driver.FindElementByXPath("//input[@value='Login']");

                    userNameField.SendKeys("admin");
                    userPasswordField.SendKeys("12345");
                    loginButton.Click();

                    var result = driver.FindElementByXPath("//div[@id='case_login']/h3").Text;
                    File.WriteAllText("result.txt", result);

                    */
                    const string username = @"angarza@intracitygeeks.org";
                    const string password = @"dogFarts123!";
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    try
                    {
                        driver.Navigate().GoToUrl("https://login.yahoo.com/config/login?.intl=us&.lang=en-US&.src=finance&.done=https%3A%2F%2Ffinance.yahoo.com%2F");

                        AddUsername(driver, username);

                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("login-passwd")));

                        AddPassword(driver, password);

                        Debug.WriteLine("Logged in!");

                        //go to stock page
                        Debug.WriteLine("Go to stocks page...");
                        GoToPortfolio(driver);

                        List<string> rawStocks = PrintPortfolio(driver);
                        Debug.WriteLine(rawStocks);
                        StockItem[] stockItems = ProcessStocks(rawStocks);

                        return Task.FromResult(stockItems);
                        
                    }
                    catch(NoSuchElementException)
                    {
                        InfoDump(driver);
                        //Debug.WriteLine("No element found...");
                        return Task.FromException<StockItem[]>(new NoSuchElementException("No element found"));
                    }
                    catch(StaleElementReferenceException)
                    {
                        Debug.WriteLine("Stale element");
                        AddPassword(driver, password);
                        return Task.FromException<StockItem[]>(new StaleElementReferenceException("No element found"));
                }           
                }      
            }
        public static void InfoDump(ChromeDriver driver)
        {
            var result = driver.PageSource;

            //driver.GetScreenshot().SaveAsFile(@"screen.png", OpenQA.Selenium.ScreenshotImageFormat.Png);
        }
        public static void AddUsername(ChromeDriver driver, string username)
        {
            var userNameField = driver.FindElementById("login-username");
            var loginButton = driver.FindElementById("login-signin");

            userNameField.SendKeys(username + Keys.Enter);
        }
        public static void AddPassword(ChromeDriver driver, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            Debug.WriteLine("Inputting password...");
            var loginButton = driver.FindElementById("login-signin");
            var passwordField = driver.FindElementById("login-passwd");
            passwordField.SendKeys(password + Keys.Enter);
            Debug.WriteLine("Password entered...");
            //driver.Keyboard.SendKeys(Keys.Enter);
            Debug.WriteLine("Password submitted...");
        }
        public static void GoToPortfolio(ChromeDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='fin-tradeit']/div[2]/div/div/div[2]/button[1]")));
            Debug.WriteLine("Portfolio page loaded!");
            var clearPop = driver.FindElementByXPath("//*[@id='fin-tradeit']/div[2]/div/div/div[2]/button[2]");
            clearPop.Click();

            InfoDump(driver);
        }
        public static List<string> PrintPortfolio(ChromeDriver driver)
        {
            List<string> rawStocks = new List<string>();
            var stockTable = driver.FindElementByXPath("//*[@id='main']/section/section[2]/div[2]/table/tbody");
            ICollection<IWebElement> trCollection = stockTable.FindElements(By.TagName("tr"));
            foreach (var stockSymbol in trCollection)
            {
                Debug.WriteLine(stockSymbol.Text);
                rawStocks.Add(stockSymbol.Text);
            }

            return rawStocks;
        }

        public static StockItem[] ProcessStocks(List<string> rawStocks)
        {
            Debug.WriteLine(rawStocks);
            StockItem[] processedStocks = new StockItem[rawStocks.Count];
            var stockCounter = 0;
            //for(var i = 0; i < rawStocks.Count; i++)
            foreach(var rawStock in rawStocks)
            {                
                string[] rawStockInfo = rawStock.Replace(",","").Replace("+","").Split(' ');
                Debug.WriteLine(
                    "Symbol:" + rawStockInfo[0] + 
                    " lastPrice:" + rawStockInfo[1] +
                    " change:" + rawStockInfo[2] +
                    " percentChange:" + rawStockInfo[3] +
                    " volume:" + rawStockInfo[8]);
                StockItem tempStock = new StockItem
                {
                    Id = Guid.NewGuid(),
                    symbol = rawStockInfo[0],
                    lastPrice = Convert.ToDouble(rawStockInfo[1]),
                    change = Convert.ToDouble(rawStockInfo[2]),
                    percentChange = Convert.ToDouble(rawStockInfo[3].Replace(oldValue: "%", newValue: "")),
                    volume = Convert.ToDouble(rawStockInfo[8].Replace(".","").Replace("k", "000").Replace("M","000000").Replace("B","000000000")),
                    snapshotTime = DateTime.Now
                };
                processedStocks[stockCounter] = tempStock;
                stockCounter++;
            }

            return processedStocks;
        }
    }
}