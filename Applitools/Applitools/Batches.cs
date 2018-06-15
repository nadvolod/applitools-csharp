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
            var xpathString = "//*[@class='et_pb_module et_pb_posts et_pb_bg_layout_light  et_pb_blog_";
            ClosePopUp();
            Javascript.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            Eyes.Check("HomePage",Target.Window().Fully().
                Floating(SocialSharingToolbar).
                Layout(By.XPath($"{xpathString}0']/..")).
                Layout(By.XPath($"{xpathString}1']/..")).
                Layout(By.XPath($"{xpathString}2']/..")));
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

        [Test]
        public void GroupingTestSteps()
        {
            Eyes.Open(Driver, AppName, "IgnoreRegionUsingBy", Resolution1080P);
            //Ignoring an element with By
            Eyes.CheckWindow();

            Driver.Navigate().GoToUrl("https://www.ultimateqa.com");
            Eyes.CheckWindow();

            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/blog");
            Eyes.CheckWindow();
        }
    }
}