using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Applitools
{
    [TestFixture]
    public class SeleniumExamples
    {
        public IWebDriver Driver { get; private set; }
        public Eyes Eyes { get; private set; }
        [SetUp]
        public void SetupForEverySingleTestMethod()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Eyes = new Eyes
            {
                /*This sets the Applitools API key in an environmental variable
                 * conversely, you can also set the key like this
                 * ApiKey = "vDPsWHm9wt7dIAvfQRH79HF105is4Lhc9710rH1xW7BUl0146";
                 */
                ApiKey = Environment.GetEnvironmentVariable("APPLITOOLS_API_KEY",
                EnvironmentVariableTarget.User)

            };
            //This uses Selenium to navigate to a url of the page below
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/fake-landing-page-small/");
        }
        [Test]
        public void SetBaseline()
        {
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

        [TearDown]
        public void TearDownForEverySingleTestMethod()
        {
            Driver.Quit();
            Eyes.AbortIfNotClosed();
        }
    }
}
