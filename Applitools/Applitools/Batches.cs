using System;
using System.Drawing;
using Applitools.Selenium;
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
        public void CheckWebsiteIn720p()
        {
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com");
            Eyes.Open(Driver, AppName, "720p", Resolution720P);
            StitchEntirePageThenCheck();
        }
        [Test]
        public void CheckWebsiteIn1080p()
        {
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com");
            Eyes.Open(Driver, AppName, "1080P", Resolution1080P);
            StitchEntirePageThenCheck();
        }
        [Test]
        public void CheckWebsiteOnGalaxyS7Resolution()
        {
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com");
            Eyes.Open(Driver, AppName, "GalaxyS7", new Size(360, 560));
            StitchEntirePageThenCheck();
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