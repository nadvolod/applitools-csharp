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

        private void StitchEntirePageThenCheck(string name = "")
        {
            ClosePopUp();
            ScrollToBottomOfPage();
            ScrollToTopOfPage();
            Eyes.Check(name, Target.Window().Fully());
            //Eyes.MatchTimeout = TimeSpan.FromSeconds(3);
            //Eyes.Check("HomePage",
            //    Target.Window().Fully().Floating(SocialSharingToolbar, 180, 4035, 0, 17)
            //        .Layout(SocialSharingToolbar)
            //        .Strict(HeaderLocator)
            //        .Strict(StatisticsRowLocator));
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
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
            var isClosed = TryToCloseFirstPopUp(wait);
            if (!isClosed) TryToCloseSecondPopUp(wait);
        }

        private void TryToCloseSecondPopUp(WebDriverWait wait)
        {
            try
            {
                wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
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
            Eyes.Open(Driver, AppName, "GroupTestSteps", Resolution1080P);

            Driver.Navigate().GoToUrl("https://www.ultimateqa.com");
            Eyes.CheckWindow("HomePage");

            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/blog");
            Eyes.CheckWindow("Blog");
        }

        //TIP keep test names short so that they can appear in the Test Results view of ApplitoolsS
        [Test]
        public void HomePage1080p()
        {
            EnableFullPageScreenshots();
            Eyes.Open(Driver, AppName, "1080P", Resolution1080P);
            StitchEntirePageThenCheck(MethodBase.GetCurrentMethod().Name);
        }

        [Test]
        public void HomePage1192x969()
        {
            EnableFullPageScreenshots();

            Eyes.Open(Driver, AppName, "1192x969", new Size(1192, 969));
            StitchEntirePageThenCheck(MethodBase.GetCurrentMethod().Name);
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