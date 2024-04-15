using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F01_S02_AddingANewLibraryStepDefinitions
    {
        [Given(@"the user is on the New library screen screen")]
        public void GivenTheUserIsOnTheNewLibraryScreenScreen()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnNewLibrary = driver.FindElementByAccessibilityId("btnNewLibrary");
            btnNewLibrary.Click();

            var newLibraryTitle = driver.FindElementByName("Dodavanje nove knjižnice");
            Assert.IsNotNull(newLibraryTitle);
        }

        [Given(@"the user enters a correct library price per day late")]
        public void GivenTheUserEntersACorrectLibraryPricePerDayLate()
        {
            var driver = GuiDriver.GetDriver();
            var tbLibraryPriceDayLate = driver.FindElementByAccessibilityId("tbLibraryPriceDayLate");
            tbLibraryPriceDayLate.SendKeys("5");
        }

        [Given(@"the user enters correct membership duration")]
        public void GivenTheUserEntersCorrectMembershipDuration()
        {
            var driver = GuiDriver.GetDriver();
            var tbLibraryMembershipDuration = driver.FindElementByAccessibilityId("tbLibraryMembershipDuration");
            tbLibraryMembershipDuration.SendKeys("30");
        }

        [When(@"the library ID is not entered")]
        public void WhenTheLibraryIDIsNotEntered()
        {
            // Doing nothing
        }

        [When(@"the user enters a correct library name")]
        public void WhenTheUserEntersACorrectLibraryName()
        {
            var driver = GuiDriver.GetDriver();
            var tbLibraryName = driver.FindElementByAccessibilityId("tbLibraryName");
            tbLibraryName.SendKeys("Skroy nova knjiynica");
        }

        [When(@"the user enters a correct library OIB")]
        public void WhenTheUserEntersACorrectLibraryOIB()
        {
            var driver = GuiDriver.GetDriver();
            var tbLibraryOIB = driver.FindElementByAccessibilityId("tbLibraryOIB");
            tbLibraryOIB.SendKeys("99999979976");
        }

        [When(@"the user clicks the Save new library button")]
        public void WhenTheUserClicksTheSaveNewLibraryButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnSave = driver.FindElementByAccessibilityId("btnAddNewLibrary");
            btnSave.Click();
        }

        [Then(@"the system should show an error message that the library can't be added")]
        public void ThenTheSystemShouldShowAnErrorMessageThatTheLibraryCantBeAdded()
        {
            MessageBoxTestHelper.CheckIfMessageBoxIsShown();
        }

        [When(@"the library ID (.*) is entered")]
        public void WhenTheLibraryIDIsEntered(int p0)
        {
            var driver = GuiDriver.GetDriver();
            var tbLibraryID = driver.FindElementByAccessibilityId("tbLibraryID");
            tbLibraryID.SendKeys(p0.ToString());
        }

        [When(@"the library OIB is not entered")]
        public void WhenTheLibraryOIBIsNotEntered()
        {
            // Doing nothing
        }

        [When(@"the user enters a correct library ID")]
        public void WhenTheUserEntersACorrectLibraryID()
        {
            var driver = GuiDriver.GetDriver();
            var tbLibraryID = driver.FindElementByAccessibilityId("tbLibraryID");
            tbLibraryID.SendKeys("997");
        }

        [When(@"the library OIB (.*) is entered")]
        public void WhenTheLibraryOIBIsEntered(string p0)
        {
            var driver = GuiDriver.GetDriver();
            var tbLibraryOIB = driver.FindElementByAccessibilityId("tbLibraryOIB");
            tbLibraryOIB.SendKeys(p0.ToString());
        }

        [When(@"the library name is not entered")]
        public void WhenTheLibraryNameIsNotEntered()
        {
            // Doing nothing
        }

        [Then(@"the library should be visible in the All libraries list")]
        public void ThenTheLibraryShouldBeVisibleInTheAllLibrariesList()
        {
            var driver = GuiDriver.GetDriver();

            var btnAllLibraries = driver.FindElementByAccessibilityId("btnAllLibraries");
            btnAllLibraries.Click();

            var dgAllLibraries = driver.FindElementByAccessibilityId("dgAllLibraries");
            Assert.IsNotNull(dgAllLibraries);

            var newLibraryId = driver.FindElementByName("997");
            Assert.IsNotNull(newLibraryId);
            var newLibraryOIB = driver.FindElementByName("99999979976");
            Assert.IsNotNull(newLibraryOIB);

            GuiDriver.Dispose();
        }
    }
}
