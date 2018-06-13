using System;
using System.Drawing;
using Applitools.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Applitools
{
    [TestFixture]
    public class Batches : BaseClass2
    {
       
        [Test]
        public void HomePageCheck720p()
        {
            Eyes.Open(Driver, AppName, "720p", Resolution720P);
        }



        [Test]
        public void HomePageCheck1080p()
        {
            Eyes.Open(Driver, AppName, "1080P", Resolution1080P);
        }
        [Test]
        public void HomePageCheckGalaxyS7Resolution()
        {
            Eyes.Open(Driver, AppName, "GalaxyS7", new Size(360, 560));
        }
        [Test]
        public void HomePageCheck1192x969()
        {
            Eyes.Open(Driver, AppName, "1192x969", new Size(1192, 969));
        }
        //This is an NUnit attribute that forces the method below to be executed after every single test execution.
        [TearDown]
        public void TearDownForEverySingleTestMethod()
        {
            StitchEntirePageThenCheck();

            //Close your Selenium browser
            Driver.Quit();
            //Close applitools eyes so that your test run is saved
            Eyes.Close();
            //Quit applitools if it is not already closed
            Eyes.AbortIfNotClosed();
        }
        private void StitchEntirePageThenCheck()
        {
            ClosePopUp();
            Javascript.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            Eyes.Check(Target.Window().Fully());
        }

        private void ClosePopUp()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
            try
            {
                var popUpCloseButton = wait.Until(
                    ExpectedConditions.ElementIsVisible(By.XPath("//*[name()='svg']")));
                popUpCloseButton.Click();
            }
            catch (Exception)
            {
                //do nothing
            }
        }
    }
}