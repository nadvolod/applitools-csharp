using System;
using System.Drawing;
using System.Threading;
using Applitools.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Applitools
{
    [TestFixture]
    [Parallelizable]
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
            ScrollToBottomOfPage();
            ScrollToTopOfPage();
            Eyes.MatchTimeout = TimeSpan.FromSeconds(3);
            Eyes.Check("HomePage",
                Target.Window().Fully().Floating(SocialSharingToolbar, 180, 4035, 0, 17)
                    .Layout(SocialSharingToolbar)
                    .Strict(HeaderLocator)
                    .Strict(StatisticsRowLocator));
        }

        public By StatisticsRowLocator =>
            By.XPath("//*[@class='et_pb_section et_pb_section_8 et_pb_with_background et_section_regular']");

        public By HeaderLocator => By.XPath("//*[@class='container clearfix et_menu_container']");

        private void ScrollToBottomOfPage()
        {
            Javascript.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            Thread.Sleep(1000);
        }

        private void ScrollToTopOfPage()
        {
            Javascript.ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
        }

        private void ClosePopUp()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
            var isClosed = TryToCloseFirstPopUp(wait);
            if (!isClosed) TryToCloseSecondPopUp(wait);
        }

        private void TryToCloseSecondPopUp(WebDriverWait wait)
        {
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

        private static bool TryToCloseFirstPopUp(WebDriverWait wait)
        {
            try
            {
                var popUpCloseButton = wait.Until(
                    ExpectedConditions.ElementIsVisible(By.XPath("//*[@title='Close']")));
                popUpCloseButton.Click();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [Test]
        public void GroupingTestSteps()
        {
            Eyes.Open(Driver, AppName, "IgnoreRegionUsingBy", Resolution1080P);
            Eyes.CheckWindow();

            Driver.Navigate().GoToUrl("https://www.ultimateqa.com");
            Eyes.CheckWindow();

            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/blog");
            Eyes.CheckWindow();
        }
    }
}