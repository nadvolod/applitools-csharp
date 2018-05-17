using System;
using System.Drawing;
using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace Applitools
{
    [TestFixture]
    [Category("Examples of different baselines")]
    public class BaselinesExamples : BaseClass
    {
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
            GoToSmallPage();
        }

        private const string AppName = "sample app 1";

        public string TestCaseName => "Test1";

        public Size Resolution720P => new Size(1280, 720);

        //Here we are going to set a new baseline when we use Eyes.Open()
        [Test]
        public void SetBaselineUsingAppName()
        {
            //Here we are initializing the test in Applitools and passing in 4 parameters
            // the IWebDriver, application name, test name, viewport size to open our app in
            Eyes.Open(Driver, AppName, TestCaseName, Resolution720P);
            //Use the Applitools algorithm to check the whole page for visual validity
            Eyes.CheckWindow();
        }

        [Test]
        public void SetBaselineUsingTestName()
        {
            //all the parameters are the same, except we provided a new test name
            Eyes.Open(Driver, AppName, "new test name", Resolution720P);
            Eyes.CheckWindow();
        }

        [Test]
        public void SetBaselineUsingViewportSize()
        {
            //all the parameters are the same, except we provided a new viewport size
            Eyes.Open(Driver, AppName, TestCaseName, new Size(1920, 1080));
            Eyes.CheckWindow();
        }
    }
}