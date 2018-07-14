using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using Applitools.Selenium;
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



        public By StatisticsRowLocator =>
            By.XPath("//*[@class='et_pb_section et_pb_section_8 et_pb_with_background et_section_regular']");

        public By HeaderLocator => By.XPath("//*[@class='container clearfix et_menu_container']");

        private void ScrollToBottomOfPage()
        {
            //This will scroll to the bottom of the page and wait for 1 second for the action to finish
            Javascript.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            Thread.Sleep(1000);
        }

        private void ScrollToTopOfPage()
        {
            //This will scroll to the top of the page and wait one second
            Javascript.ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
            Thread.Sleep(1000);
        }

        private void ClosePopUp()
        {
            //create a new WebDriverWait object that will poll for 15 seconds for some condition to be true
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
            var isClosed = TryToCloseFirstPopUp(wait);
            //if the first pop up is NOT closed, that means that there was a 2nd pop up that we need to try and close
            if (!isClosed) TryToCloseSecondPopUp(wait);
        }

        private void TryToCloseSecondPopUp(WebDriverWait wait)
        {
            try
            {
                //try to see if the pop up is open and visible for 15 seconds. If it is, click the Close button
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
                //try to see if an element with the title below is visible, if it is, click it and return true.
                var popUpCloseButton = wait.Until(
                    ExpectedConditions.ElementIsVisible(By.XPath("//*[@title='Close']")));
                popUpCloseButton.Click();
                return true;
            }
            catch (Exception)
            {
                //otherwise return false;
                return false;
            }
        }

        private void EnableFullPageScreenshots()
        {
            //capture the full page for validation
            Eyes.ForceFullPageScreenshot = true;
            //stitch the page together using CSS
            Eyes.StitchMode = StitchModes.CSS;
        }

        [Test]
        public void GroupingTestSteps()
        {
            //start a brand new test in Applitools
            Eyes.Open(Driver, AppName, "GroupTestSteps", Resolution1080P);
            //open the ulr below
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com");
            //visual validation checkpoint that is called HomePage
            Eyes.CheckWindow("HomePage");
            //go to another URL
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/blog");
            //2nd step and visual checkpoint called Blog
            Eyes.CheckWindow("Blog");
        }

        //TIP keep test names short so that they can appear in the Test Results view of ApplitoolsS
        [Test]
        public void HomePage1080p()
        {
            //enable full page screenshots so that the entire page is visually validated
            EnableFullPageScreenshots();
            Eyes.Open(Driver, AppName, "1080P", Resolution1080P);
            //Stitch the entire page so that it can be visually validated
            StitchEntirePageThenCheck(MethodBase.GetCurrentMethod().Name);
        }

        [Test]
        public void HomePage1192x969()
        {
            EnableFullPageScreenshots();
            Eyes.Open(Driver, AppName, "1192x969", new Size(1192, 969));
            StitchEntirePageThenCheck(MethodBase.GetCurrentMethod().Name);
        }
        private void StitchEntirePageThenCheck(string name = "")
        {
            //close the pop up offer if it exists
            ClosePopUp();
            //scroll to the bottom of the page to force all of the dynamic elements to load
            ScrollToBottomOfPage();
            //scroll back to the top so that the full page screenshot capture starts at the right place
            ScrollToTopOfPage();
            //Eyes.Check(name, Target.Window().Fully());
            Eyes.MatchTimeout = TimeSpan.FromSeconds(3);
            //perform a visual checkpoint that uses the fluent API to capture a full page screenshot,
            //add a floating region, add a layout region, add a strict region. All applied at the element level
            Eyes.Check(name,
                Target.Window().Fully().Floating(SocialSharingToolbar, 180, 4035, 0, 17)
                    .Layout(SocialSharingToolbar)
                    .Strict(HeaderLocator));
        }
        [Test]
        public void HomePage720p()
        {
            EnableFullPageScreenshots();

            Eyes.Open(Driver, AppName, "720p", Resolution720P);
            StitchEntirePageThenCheck(MethodBase.GetCurrentMethod().Name);
        }

        [Test]
        public void HomePageCheckGalaxyS7Resolution()
        {
            EnableFullPageScreenshots();

            Eyes.Open(Driver, AppName, "GalaxyS7", new Size(360, 560));
            StitchEntirePageThenCheck(MethodBase.GetCurrentMethod().Name);
        }
    }
}