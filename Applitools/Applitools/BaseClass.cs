using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Applitools
{
    public class BaseClass
    {
        public IWebDriver Driver { get; set; }
        public Eyes Eyes { get; set; }
        public void GoToSmallPage()
        {
            //This uses Selenium to navigate to a url of the page below
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/fake-landing-page-small/");
        }

        //This is an NUnit attribute that forces the method below to be executed after every single test execution.
        [TearDown]
        public void TearDownForEverySingleTestMethod()
        {
            //Close applitools eyes so that your test run is saved
            Eyes.Close();
            //Close your Selenium browser
            Driver.Quit();
            //Quit applitools if it is not already closed
            Eyes.AbortIfNotClosed();
        }
    }
}