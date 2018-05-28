﻿using System;
using System.Drawing;
using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Applitools
{
    [TestFixture]
    [Category("Examples of different baselines")]
    public class BaselinesExamples : BaseClass
    {
        private const string AppName = "sample app 1";
        public string TestCaseName => "Test1";
        [Test]
        public void SetBaselineUsingAppName()
        {
            GoToPricingPage();
            //Here we are initializing the test in Applitools and passing in 4 parameters
            // the IWebDriver, application name, test name, viewport size to open our app in
            Eyes.Open(Driver, AppName, TestCaseName, Resolution1080P);
            //Use the Applitools algorithm to check the whole page for visual validity
            Eyes.CheckWindow();
        }
        [Test]
        public void SetBaselineUsingDifferentAppName()
        {
            GoToPricingPage();
            Eyes.Open(Driver, "app 2", TestCaseName, Resolution1080P);
            Eyes.CheckWindow();
        }

        [Test]
        public void SetBaselineUsingTestName()
        {
            GoToPricingPage();
            //all the parameters are the same, except we provided a new test name
            Eyes.Open(Driver, AppName, "new test name", Resolution1080P);
            Eyes.CheckWindow();
        }

        [Test]
        public void SetBaselineUsingViewportSize()
        {
            GoToPricingPage();
            //all the parameters are the same, except we provided a new viewport size
            Eyes.Open(Driver, AppName, TestCaseName, Resolution720P);
            Eyes.CheckWindow();
        }

        [Test]
        public void TestBaseline()
        {
            GoToPricingPage();
            UpdateElements();
            Eyes.Open(Driver, AppName, TestCaseName, Resolution1080P);
            Eyes.CheckWindow();
        }

        private void UpdateElements()
        {
            var javascript = Driver as IJavaScriptExecutor;
            javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[0].innerText = \"€0\";");
            javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[1].innerText = \"€80\";");
            javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[2].innerText = \"€900\";");
        }
    }
}