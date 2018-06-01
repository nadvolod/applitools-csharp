using NUnit.Framework;
using OpenQA.Selenium;

namespace Applitools
{
    [TestFixture]
    [Category("MatchLevels")]
    public class MatchLevels : BaseClass
    {
        [Test]
        public void ContentMatchLevel()
        {
            //Set the applitools match level to Content
            Eyes.MatchLevel = MatchLevel.Content;
            //Will open the fake pricing page
            GoToPricingPage();
            //Will change the first element to a Euro symbol instead of the $ 
            //and will change the collor to a yellow.
            ChangeToEuroAndUpdateColor();
            //Starts the applitools test with 4 parameters passed in
            Eyes.Open(Driver, AppName, TestCaseName, Resolution1080P);
            //Validates whether baseline matches the actual image. Passes in extra metadata
            //about the test case that tells us that this uses MatchLevel.Content
            Eyes.CheckWindow("MatchLevel.Content");
        }
        [Test]
        public void LayoutMatchLevel()
        {
            //Sets the applitools match leve to Layout
            Eyes.MatchLevel = MatchLevel.Layout;
            //Will open the fake pricing page
            GoToPricingPage();
            //Will change the first element to a Euro symbol instead of the $ 
            //and will change the collor to a yellow.
            ChangeToEuroAndUpdateColor();
            //Starts the applitools test with 4 parameters passed in
            Eyes.Open(Driver, AppName, TestCaseName, Resolution1080P);
            //Validates whether baseline matches the actual image. Passes in extra metadata
            //about the test case that tells us that this uses MatchLevel.Layout
            Eyes.CheckWindow("MatchLevel.Layout");
        }
        private void ChangeToEuroAndUpdateColor()
        {
            //take the first element with class name et_pb_sum and update the value to what's specified
            Javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[0].innerText = \"€0\";");
            var element = Driver.FindElement(By.TagName("h1"));
            //Executes some javascript that updates the color of the h1 element on the page to the color specified
            Javascript.ExecuteScript(
                "arguments[0].setAttribute('style', 'color:#f9ca33!important')",element);
        }
    }
}