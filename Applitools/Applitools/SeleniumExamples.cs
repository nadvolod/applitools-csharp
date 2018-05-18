using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Applitools
{
    [TestFixture]
    public class SeleniumExamples : BaseClass
    {

        [Test]
        public void SetBaseline()
        {
            GoToSmallPage();
            VisualCheckpoint1();
        }

        private void VisualCheckpoint1()
        {
            //Here we are initializing the test in Applitools and passing in 4 parameters
            // the IWebDriver, application name, test name, viewport size to open our app in
            Eyes.Open(Driver, "Small Landing Page", "TC1", new System.Drawing.Size(1024, 768));
            //Use the Applitools algorithm to check the whole page for visual validity
            Eyes.CheckWindow();
        }

        [Test]
        public void TestBaseline()
        {
            GoToSmallPage();
            //Use Javascript to update the text of the element above so that
            //we can fake a common visual error in the web page
            UpdateElementInnerText();
            VisualCheckpoint1();
        }

        private void UpdateElementInnerText()
        {
            var javascript = Driver as IJavaScriptExecutor;
            javascript.ExecuteScript(
                            "document.getElementsByTagName('h1')[1].innerText = \"Pick a plan that Works for Your Business Model\";");
            javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[0].innerText = \"$0.99\";");
            javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[2].innerText = \"USD 900\";");
        }
    }
}
