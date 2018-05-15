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
        public ChromeDriver Driver { get; private set; }
        public Eyes Eyes { get; private set; }
        [SetUp]
        public void SetupForEverySingleTestMethod()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Eyes = new Eyes
            {
                //This sets the Applitools API key so that you can access the account
                //ApiKey = "vDPsWHm9wt7dIAvfQRH79HF105is4Lhc9710rH1xW7BUl0146";
                ApiKey = Environment.GetEnvironmentVariable("APPLITOOLS_API_KEY",
                EnvironmentVariableTarget.User)

            };
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/fake-landing-page-small/");
        }
        [Test]
        public void SetBaseline()
        {
            Eyes.Open(Driver, "Small Landing Page", "TC1", new System.Drawing.Size(1024,768));
            Eyes.CheckWindow();
        }
        [Test]
        public void TestBaseline()
        {
            var element = Driver.FindElement(By.XPath("//h1"));
            Driver.ExecuteScript(
                "document.getElementsByTagName('h1')[0].innerText = \"1000's of Courses\";", element);
            Eyes.Open(Driver, "Small Landing Page", "TC1", new System.Drawing.Size(1024,768));
            Eyes.CheckWindow();
        }
        [TearDown]
        public void TearDownForEverySingleTestMethod()
        {
            Driver.Quit();
            Eyes.AbortIfNotClosed();
        }
    }
}
