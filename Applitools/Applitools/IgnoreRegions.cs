using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Applitools
{
    [TestFixture]
    public class IgnoreRegions : BaseClass
    {
        [SetUp]
        public void ExecuteBeforeEveryTest()
        {
            //Will open the fake pricing page
            GoToPricingPage();
            //Will update the subheading text and the currency.
            UpdateSubheadingAndCurrency();
            //Starts the applitools test with 4 parameters passed in
            Open1080PBaseline();
        }
        [Test]
        public void IgnoreRegionUsingBy()
        {
            //Ignoring an element with By
            Eyes.Check(TestCaseName, Target.Window().Ignore(PriceLocator));
        }
        [Test]
        public void IgnoreRegionUsingFindElements()
        {
            //Ignoring with FindElement()
            Eyes.Check(TestCaseName, Target.Window().
                Ignore(Driver.FindElements(PriceLocator)[1]));
        }
        [Test]
        public void IgnoreMultipleElements()
        {

            //Ignore multiple elements with a single Check()
            Eyes.Check(TestCaseName, Target.Window().Ignore(PriceLocator, SubheaderLocator));
        }
        private void UpdateSubheadingAndCurrency()
        {
            var subheadingElement = Driver.FindElement(SubheaderLocator);
            //Update the subheader text to common visual validation problems
            Javascript.ExecuteScript(
                $"arguments[0].textContent=" +
                $"\"These are the best plans amongst all companie's in the world\"", subheadingElement);
            //take the first element with class name et_pb_sum and update the value to what's specified
            Javascript.ExecuteScript(
                "document.getElementsByClassName('et_pb_sum')[0].innerText = \"USD 0\";");
        }

    }
}