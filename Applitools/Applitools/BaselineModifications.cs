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
        //todo doesn't work - https://eyes.applitools.com/app/sessions/00000251874712529147/00000251874712528931/steps/1?accountId=QKyK1w_Do0qGwt3VbggQrA~~
        //Docs - http://support.applitools.com/customer/portal/articles/2249374
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
        private void UpdateElements()
        {
            //take the first element with class name et_pb_sum and update the value to what's specified
            Javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[0].innerText = \"€0\";");
            //this does the same thing as the statement above,
            //but uses the 2nd element in the collection instead and updates it to a different value
            Javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[1].innerText = \"€80\";");
            Javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[2].innerText = \"€900\";");
        }
    }
}