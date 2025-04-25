using System;
using OpenQA.Selenium;
using SeleniumCsharpE2e.PageObject;
using SeleniumCsharpE2e.PageObject.TestBase;
using TechTalk.SpecFlow;

namespace SeleniumCsharp.Steps
{
    [Binding]
    public class DemoQaSteps : TestBase
    {

        DemoQaObject demoQaObject = new DemoQaObject();

        [Given(@"I navigate to DemoQa Website")]
        public void GivenInavigatetoDemoQaWebsite()
        {
            StartURL();
        }

        [Then(@"I the url should be ""(.*)""")]
        public void ThenItheurlshouldbe(string url)
        {
            Assert.True(demoQaObject.GetUrl(url));
        }


        [When(@"I click on the Elements card")]
        public void WhenIclickontheElementscard()
        {
            demoQaObject.ClickOnElementsCard();
        }
    }
}