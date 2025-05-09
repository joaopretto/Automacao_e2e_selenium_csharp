using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using SeleniumE2ECsharp.Config;

namespace SeleniumE2ECsharp.Hooks
{
    [Binding]
    public sealed class SpecHooks
    {
        private readonly IObjectContainer _container;

        public SpecHooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var driver = DriverFactory.CreateDriver();
            DriverContext.SetDriver(driver);
            _container.RegisterInstanceAs(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            DriverContext.QuitDriver();
        }
    }
}