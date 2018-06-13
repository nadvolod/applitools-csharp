using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Applitools
{
    [TestFixture]
    [Category("Examples of different baselines")]
    public class BaselineModifications : BaseClass
    {
        [Test]
        public void FullPageScreenshot()
        {
            Eyes.ForceFullPageScreenshot = true;
            GoToPricingPage();
            Eyes.Open(Driver, AppName, TestCaseName, Resolution1080P);
            Eyes.CheckWindow("FullPageScreenshot");
        }
        [Test]
        public void FullPageScreenshotWithCssStitching()
        {
            Eyes.ForceFullPageScreenshot = true;
            Eyes.StitchMode = StitchModes.CSS;
            GoToPricingPage();
            Eyes.Open(Driver, AppName, TestCaseName, Resolution1080P);
            Javascript.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            Eyes.Check("CssStitching", Target.Window().Fully());
        }
    }
}