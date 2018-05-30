using System;
using System.Drawing;
using System.Web.UI.WebControls;
using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Applitools
{
    [TestFixture]
    [Category("MatchLevels")]
    public class MatchLevels : BaseClass
    {
        private const string AppName = "sample app 1";
        private string TestCaseName => "Test1";

        [Test]
        public void ContentMatchLevel()
        {
            Eyes.MatchLevel = MatchLevel.Content;
            GoToPricingPage();
            ChangeToEuroAndUpdateColor();
            Eyes.Open(Driver, AppName, TestCaseName, Resolution1080P);
            Eyes.CheckWindow("MatchLevel.Content");
        }
        private void ChangeToEuroAndUpdateColor()
        {
            //Create an object that can execute javascript commands
            var javascript = Driver as IJavaScriptExecutor;
            //take the first element with class name et_pb_sum and update the value to what's specified
            javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[0].innerText = \"€0\";");
            var element = Driver.FindElement(
                By.TagName("h1"));
            javascript.ExecuteScript(
                "arguments[0].setAttribute('style', 'color:#3d72e7!important')",element);
        }
    }
}