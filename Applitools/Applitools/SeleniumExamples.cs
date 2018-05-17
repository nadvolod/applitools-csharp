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
            VisualCheckpoint1();
        }

        private void VisualCheckpoint1()
        {
            GoToSmallPage();
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
            //Use Selenium to locate the first h1 element in the HTML
            var element = Driver.FindElement(By.XPath("//h1"));
            //Use Javascript to update the text of the element above so that
            //we can fake a common visual error in the web page
            UpdateElementInnerText(element);
            VisualCheckpoint1();
        }

        private void UpdateElementInnerText(IWebElement element)
        {
            var javascript = Driver as IJavaScriptExecutor;
            javascript.ExecuteScript(
                            "document.getElementsByTagName('h1')[0].innerText = \"1000's of Courses\";", element);
        }
    }
}
