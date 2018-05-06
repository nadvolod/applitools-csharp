using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Applitools
{
    [TestFixture]
    public class TenMinuteDemo
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
                ApiKey = Environment.GetEnvironmentVariable("APPLITOOLS_API_KEY",
                EnvironmentVariableTarget.User)
            };
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/fake-landing-page/");
        }
        [Test]
        public void SetBaseline()
        {
            Eyes.Open(Driver, "Fake Landing Page", "TC1", new System.Drawing.Size(1024,768));
            Eyes.CheckWindow();
        }
        [Test]
        public void TestBaseline()
        {
            var element = Driver.FindElement(
                By.XPath("//h1"));
            var javascript = Driver as IJavaScriptExecutor;
            javascript.ExecuteScript("arguments[0].setAttribute('style','font-weight:100')", element);
            Eyes.Open(Driver, "Fake Landing Page", "TC1", new System.Drawing.Size(1024,768));
            Eyes.CheckWindow();
        }
        [TearDown]
        public void TearDownForEverySingleTestMethod()
        {
            Eyes.Close();
            Driver.Quit();
            Eyes.AbortIfNotClosed();
        }
    }
}
