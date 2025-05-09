using System;
using OpenQA.Selenium;

namespace SeleniumE2ECsharp.Pages
{
    public class BasePage
    {
        protected IWebDriver Driver;

        public BasePage(IWebDriver driver)
        {
            Driver = driver ?? throw new NullReferenceException("Driver não foi inicializado");
        }

        public void NavigateTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public bool GetTitleUrl(string expectedTitle)
        {
            return Driver.Title.Contains(expectedTitle);
        }
    }
}