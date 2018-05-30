﻿using System;
using System.Drawing;
using System.Web.UI.WebControls;
using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Applitools
{
    [TestFixture]
    [Category("Examples of different baselines")]
    public class BaselineModifications : BaseClass
    {
        [Test]
        public void UseIgnoreRegions()
        {
            GoToPricingPage();
            UpdateElements();
            Eyes.Open(Driver, AppName, TestCaseName, Resolution1080P);
            ////Ignoring with By
            //Eyes.Check(TestCaseName, Target.Window().Ignore(
            //    By.ClassName("et_pb_sum")));
            ////Ignoring with FindElement()
            //Eyes.Check(TestCaseName, Target.Window().
            //    Ignore(Driver.FindElements(By.ClassName("et_pb_sum"))[1]));
            Eyes.Check(TestCaseName, Target.Window().
                Ignore(Driver.FindElement(By.ClassName("et_pb_sum")),
                    Driver.FindElements(By.ClassName("et_pb_sum"))[1]));
        }

        [Test]
        public void FullPageScreenshot()
        {
            Eyes.ForceFullPageScreenshot = true;
            GoToPricingPage();
            Eyes.Open(Driver, AppName, TestCaseName, Resolution1080P);
            Eyes.CheckWindow("FullPageScreenshot");
        }
        //todo doesn't work - https://eyes.applitools.com/app/sessions/00000251874712529147/00000251874712528931/steps/1?accountId=QKyK1w_Do0qGwt3VbggQrA~~
        //Docs - http://support.applitools.com/customer/portal/articles/2249374
        [Test]
        public void FullPageScreenshotWithCssStitching()
        {
            Eyes.ForceFullPageScreenshot = true;
            Eyes.StitchMode = StitchModes.CSS;
            var javascript = Driver as IJavaScriptExecutor;
            GoToPricingPage();
            Eyes.Open(Driver, AppName, TestCaseName, Resolution1080P);
            javascript.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            Eyes.Check("CssStitching", Target.Window().Fully());
        }
        private void UpdateElements()
        {
            //Create an object that can execute javascript commands
            var javascript = Driver as IJavaScriptExecutor;
            //take the first element with class name et_pb_sum and update the value to what's specified
            javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[0].innerText = \"€0\";");
            //this does the same thing as the statement above,
            //but uses the 2nd element in the collection instead and updates it to a different value
            javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[1].innerText = \"€80\";");
            javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[2].innerText = \"€900\";");
        }
    }
}