using System;
using System.IO;
using System.Drawing.Imaging;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
                try
                {
                    driver.Navigate().GoToUrl("https://login.yahoo.com/config/login?.intl=us&.lang=en-US&.src=finance&.done=https%3A%2F%2Ffinance.yahoo.com%2F");
                    var userNameField = driver.FindElementById("login-username");
                    var loginButton = driver.FindElementById("login-signin");

                    userNameField.SendKeys(username);
                    loginButton.Click();

                    var passwordField = driver.FindElementByName("passwd");
                    loginButton.Click();

                    driver.GetScreenshot().SaveAsFile(@"screen.png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                }
                catch(NoSuchElementException)
                {
                    var result = driver.PageSource;
                    File.WriteAllText("result.txt", result);
                    Console.WriteLine("No element found...");
                    driver.GetScreenshot().SaveAsFile(@"screen.png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                }


            }
        }
    }
}
