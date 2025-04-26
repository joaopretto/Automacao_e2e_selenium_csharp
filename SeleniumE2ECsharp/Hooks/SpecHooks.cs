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
            _container.RegisterInstanceAs<IWebDriver>(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var driver = _container.Resolve<IWebDriver>();
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                // Tirar screenshot em caso de falha
            }
            driver.Quit();
        }
    }
}