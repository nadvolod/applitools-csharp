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
        [Test]
        public void ContentMatchLevel()
        {
            Eyes.MatchLevel = MatchLevel.Content;
            GoToPricingPage();
            ChangeToEuroAndUpdateColor();
            Eyes.Open(Driver, AppName, TestCaseName, Resolution1080P);
            Eyes.CheckWindow("MatchLevel.Content");
        }
        [Test]
        public void LayoutMatchLevel()
        {
            Eyes.MatchLevel = MatchLevel.Layout;
            GoToPricingPage();
            ChangeToEuroAndUpdateColor();
            Eyes.Open(Driver, AppName, TestCaseName, Resolution1080P);
            Eyes.CheckWindow("MatchLevel.Content");
        }
        private void ChangeToEuroAndUpdateColor()
        {
            //take the first element with class name et_pb_sum and update the value to what's specified
            Javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[0].innerText = \"€0\";");
            var element = Driver.FindElement(
                By.TagName("h1"));
            Javascript.ExecuteScript(
                "arguments[0].setAttribute('style', 'color:#3d72e7!important')",element);
        }
    }
}