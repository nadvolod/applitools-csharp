using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Drawing;

namespace Applitools
{
    [TestFixture]
    public class SeleniumExamples : BaseClass
    {

        [Test]
        public void SetBaseline()
        {
            GoToPricingPage();
            VisualCheckpoint();
        }

        private void VisualCheckpoint()
        {
            //Here we are initializing the test in Applitools and passing in 4 parameters
            // the IWebDriver, application name, test name, viewport size to open our app in
            Eyes.Open(Driver, "Small Landing Page", "TC1", new Size(1910, 1079));
            //Use the Applitools algorithm to check the whole page for visual validity
            Eyes.CheckWindow();
        }

        [Test]
        public void TestBaseline()
        {
            GoToPricingPage();
            UpdateElements();
            VisualCheckpoint();
        }

        private void UpdateElements()
        {
            var javascript = Driver as IJavaScriptExecutor;
            javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[0].innerText = \"€0\";");
            javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[1].innerText = \"€80\";");
            javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[2].innerText = \"€900\";");
        }
    }
}
