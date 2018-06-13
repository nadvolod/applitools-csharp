using System;
using System.Drawing;
using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Applitools
{
    public class BaseClass
    {
        public IWebDriver Driver { get; set; }
        public Eyes Eyes { get; set; }
        public Size Resolution720P => new Size(1280, 720);
        public Size Resolution1080P => new Size(1920, 1080);
        public const string AppName = "sample app 1";
        public string TestCaseName => "Test1";
        public By PriceLocator => By.ClassName("et_pb_sum");
        public By SubheaderLocator => By.XPath("//*[@class='et_pb_text_inner']/p");
        public static BatchInfo BatchName { get; set; }

        public IWebElement SocialSharingToolbar => Driver.FindElement(
            By.XPath("//*[@class='et_social_sidebar_networks et_social_visible_sidebar " +
                     "et_social_slideright et_social_animated et_social_rectangle et_social_sidebar_flip " +
                     "et_social_sidebar_withcounts et_social_withtotalcount et_social_mobile_on']"));
        

        public IJavaScriptExecutor Javascript { get; set; }


        public void Open1080PBaseline()
        {
            //Start test in Applitools to compare to the baseline created by the parameters below.
            Eyes.Open(Driver, AppName, TestCaseName, Resolution1080P);
        }
        public void GoToPricingPage()
        {
            //This uses Selenium to navigate to a url of the page below
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/fake-pricing-page/");
        }
        [SetUp]
        public void SetupForEverySingleTestMethod()
        {
            //create a new chrome driver
            Driver = new ChromeDriver();
            //set an implicit wait for Selenium so that if it doesn't find an element, it will keep trying for specified amount of time
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
            //Creating an object that can execute Javascript commands in the browser
            Javascript = (IJavaScriptExecutor) Driver;
        }
        //This is an NUnit attribute that forces the method below to be executed after every single test execution.
        [TearDown]
        public void TearDownForEverySingleTestMethod()
        {
            //Close your Selenium browser
            Driver.Quit();
            //Close applitools eyes so that your test run is saved
            Eyes.Close();
            //Quit applitools if it is not already closed
            Eyes.AbortIfNotClosed();
        }
    }
}