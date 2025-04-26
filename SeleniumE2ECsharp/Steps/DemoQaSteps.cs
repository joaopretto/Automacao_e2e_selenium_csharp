using SeleniumE2ECsharp.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace SeleniumE2ECsharp.Steps
{
    [Binding]
    public class DemoQaSteps
    {
        private readonly IWebDriver _driver;
        private readonly BasePage _basePage;

        public DemoQaSteps(IWebDriver driver)
        {
            _driver = driver;
            _basePage = new BasePage(driver);
        }

        [Given(@"I navigate to the homepage")]
        public void GivenINavigateToTheHomepage()
        {
            _basePage.NavigateTo(Config.TestConfig.BaseUrl);
        }

        [Then(@"the page title should be ""(.*)""")]
        public void ThenThePageTitleShouldBe(string expectedTitle)
        {
            Assert.AreEqual(expectedTitle, _driver.Title);
        }
    }
}