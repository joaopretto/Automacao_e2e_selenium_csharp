using SeleniumE2ECsharp.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace SeleniumE2ECsharp.Steps
{
    [Binding]
    public class DemoQaSteps
    {
        private readonly IWebDriver webdriver;
        public BasePage basePage;
        public ElementObject elementObject;
        
        public DemoQaSteps(IWebDriver driver)
        {
            webdriver = driver;
            basePage = new BasePage(webdriver);
            elementObject = new ElementObject();
        }

        [Given(@"I navigate to the homepage")]
        public void GivenINavigateToTheHomepage()
        {
            webdriver.Navigate().GoToUrl(Config.TestConfig.BaseUrl);
        }
        
        [When(@"I click on Element card")]
        public void WhenIclickonElementcard()
        {
            elementObject.ClickOnElementCard();
        }

        [Then(@"the page title should be ""(.*)""")]
        public void ThenThePageTitleShouldBe(string expectedTitle)
        {
            Assert.IsTrue(basePage.GetTitleUrl(expectedTitle));
        }
    }
}