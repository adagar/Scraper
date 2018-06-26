using System;
using System.IO;
using System.Drawing.Imaging;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace WebDriverScraping
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("UNLEASH THE SCRAPER!");
            using (var driver = new ChromeDriver())
            {
                Console.WriteLine("Driver found!");
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

                    wait.Until(ExpectedConditions.ElementIsVisible(By.Id("login-passwd")));

                    AddPassword(driver, password);

                    Console.WriteLine("Logged in!");

                    //go to stock page
                    Console.WriteLine("Go to stocks page...");
                    GoToPortfolio(driver);

                    PrintPortfolio(driver);
                    
                }
                catch(NoSuchElementException)
                {
                    InfoDump(driver);
                    Console.WriteLine("No element found...");
                }
                catch(StaleElementReferenceException)
                {
                    Console.WriteLine("Stale element");
                    AddPassword(driver, password);
                    InfoDump(driver);                    
                }           
            }      
        }
        public static void InfoDump(ChromeDriver driver)
        {
            var result = driver.PageSource;
            File.WriteAllText("result.html", result);

            driver.GetScreenshot().SaveAsFile(@"screen.png", OpenQA.Selenium.ScreenshotImageFormat.Png);
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
            Console.WriteLine("Inputting password...");
            var loginButton = driver.FindElementById("login-signin");
            var passwordField = driver.FindElementById("login-passwd");
            passwordField.SendKeys(password + Keys.Enter);
            Console.WriteLine("Password entered...");
            //driver.Keyboard.SendKeys(Keys.Enter);
            Console.WriteLine("Password submitted...");
        }
        public static void GoToPortfolio(ChromeDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='fin-tradeit']/div[2]/div/div/div[2]/button[1]")));
            Console.WriteLine("Portfolio page loaded!");
            var clearPop = driver.FindElementByXPath("//*[@id='fin-tradeit']/div[2]/div/div/div[2]/button[2]");
            clearPop.Click();

            InfoDump(driver);
        }
        public static void PrintPortfolio(ChromeDriver driver)
        {
            var stockTable = driver.FindElementByXPath("//*[@id='main']/section/section[2]/div[2]/table/tbody");
            ICollection<IWebElement> trCollection = stockTable.FindElements(By.TagName("tr"));
            foreach(var stockSymbol in trCollection)
            {
                Console.WriteLine(stockSymbol.Text);
            }
            

        }
    }
}
