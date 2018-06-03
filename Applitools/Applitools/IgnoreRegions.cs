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
        }
        [Test]
        public void IgnoreRegionUsingBy()
        {
            Eyes.Open(Driver, AppName, "IgnoreRegionUsingBy", Resolution1080P);
            //Ignoring an element with By
            Eyes.Check("IgnoreRegionUsingBy", Target.Window().Ignore(PriceLocator));
        }
        [Test]
        public void IgnoreRegionUsingFindElements()
        {
            Eyes.Open(Driver, AppName, "IgnoreRegionUsingFindElements", Resolution1080P);

            //Ignoring with FindElement()
            Eyes.Check("IgnoreRegionUsingFindElements", Target.Window().
                Ignore(Driver.FindElements(PriceLocator)[1]));
        }
        [Test]
        public void IgnoreMultipleElements()
        {
            Eyes.Open(Driver, AppName, "IgnoreMultipleElements", Resolution1080P);

            //Ignore multiple elements with a single Check()
            Eyes.Check("IgnoreMultipleElements", Target.Window().Ignore(PriceLocator, SubheaderLocator));
        }
        [Test]
        public void FloatingRegion()
        {
            Eyes.Open(Driver, AppName, "FloatingRegion", Resolution1080P);

            //This will add a floating region to the social sharing toolbar
            Eyes.Check("FloatingRegion", Target.Window().Floating(SocialSharingToolbar));
        }
        [Test]
        public void StrictRegion()
        {
            Eyes.Open(Driver, AppName, "StrictRegion", Resolution1080P);

            Eyes.MatchLevel = MatchLevel.Layout;
            //This will add a floating region to the social sharing toolbar
            Eyes.Check("StrictRegion", Target.Window().Strict(PriceLocator));
        }

        [Test]
        public void ContentRegion()
        {
            Eyes.Open(Driver, AppName, "ContentRegion", Resolution1080P);

            //This will add a floating region to the social sharing toolbar
            Eyes.Check("ContentRegion", Target.Window().Content(PriceLocator));
        }
        [Test]
        public void LayoutRegion()
        {
            Eyes.Open(Driver, AppName, "LayoutRegion", Resolution1080P);

            //This will add a floating region to the social sharing toolbar
            Eyes.Check("LayoutRegion", Target.Window().Layout(
                Driver.FindElement(By.XPath("//*[@class='et_pb_module et_pb_pricing " +
                                            "clearfix et_pb_pricing_1 et_pb_pricing_tables_0 " +
                                            "et_pb_no_featured_in_first_row']"))));
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