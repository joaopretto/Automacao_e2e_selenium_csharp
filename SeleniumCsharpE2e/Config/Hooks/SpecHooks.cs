using TechTalk.SpecFlow;

namespace SeleniumCsharpE2e.Config.Hooks
{
    [Binding]
    public class SpecHooks : HasWebDriverAccess
    {
        [AfterScenario]
        public void TearDown()
        {
            DisposeWebDriver();
        }
    }
}