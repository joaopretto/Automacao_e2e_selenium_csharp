using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace SeleniumCsharpE2e.Config
{
    public class DriverFactory
    {
        private static IConfiguration Configuration;
        private static string BaseUrl;
        private static int Timeout;

        static DriverFactory()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            BaseUrl = Configuration["Test:BaseUrl"];
            Timeout = int.Parse(Configuration["Test:Timeout"]);
        }

        public static IWebDriver CreateDriver()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments(
                "--disable-gpu",
                "--no-sandbox",
                "--disable-dev-shm-usage",
                "--window-size=1920,1080"
            );

            // Caminho absoluto para o ChromeDriver
            var driverPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Drivers");
            if (!Directory.Exists(driverPath))
            {
                throw new DirectoryNotFoundException($"A pasta Drivers não foi encontrada: {driverPath}");
            }

            var driver = new ChromeDriver(driverPath, chromeOptions);

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Timeout);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Timeout);
            driver.Manage().Window.Maximize();

            try
            {
                driver.Navigate().GoToUrl(BaseUrl);
                WaitForPageLoad(driver);
                return driver;
            }
            catch (Exception ex)
            {
                driver.Quit();
                driver.Dispose();
                throw new Exception($"Falha ao inicializar o driver: {ex.Message}");
            }
        }

        private static void WaitForPageLoad(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeout));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}
