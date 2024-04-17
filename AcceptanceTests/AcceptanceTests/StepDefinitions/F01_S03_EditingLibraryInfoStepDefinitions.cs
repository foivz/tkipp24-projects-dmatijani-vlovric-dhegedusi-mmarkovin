using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F01_S03_EditingLibraryInfoStepDefinitions
    {
        [Given(@"the user chooses a library from the list")]
        public void GivenTheUserChoosesALibraryFromTheList()
        {
            var driver = GuiDriver.GetDriver();
            var libraryToEdit = driver.FindElementByName("997");
            libraryToEdit.Click();
        }

        [Given(@"the user clicks on the Edit library button")]
        public void GivenTheUserClicksOnTheEditLibraryButton()
        {
            var driver = GuiDriver.GetDriver();
            var editButton = driver.FindElementByAccessibilityId("btnEditLibrary");
            editButton.Click();
        }

        [When(@"the user removes the library name")]
        public void WhenTheUserRemovesTheLibraryName()
        {
            var driver = GuiDriver.GetDriver();
            var tbLibraryName = driver.FindElementByAccessibilityId("tbLibraryName");
            for (int i = 0; i < 30; i ++) {
                tbLibraryName.SendKeys(Keys.Backspace);
                tbLibraryName.SendKeys(Keys.Delete);
            }
        }

        [When(@"the user clicks the Save changes for the library button")]
        public void WhenTheUserClicksTheSaveChangesForTheLibraryButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnSaveChanges = driver.FindElementByAccessibilityId("btnAddNewLibrary");
            btnSaveChanges.Click();
        }

        [Then(@"The system should show an error message that the library can't be modified")]
        public void ThenTheSystemShouldShowAnErrorMessageThatTheLibraryCantBeModified()
        {
            MessageBoxTestHelper.CheckIfMessageBoxIsShown();
        }

        [When(@"the user removes the library OIB")]
        public void WhenTheUserRemovesTheLibraryOIB()
        {
            var driver = GuiDriver.GetDriver();
            var tbLibraryOIB = driver.FindElementByAccessibilityId("tbLibraryOIB");
            for (int i = 0; i < 15; i++) {
                tbLibraryOIB.SendKeys(Keys.Backspace);
                tbLibraryOIB.SendKeys(Keys.Delete);
            }
        }

        [When(@"the user removes the price per day late")]
        public void WhenTheUserRemovesThePricePerDayLate()
        {
            var driver = GuiDriver.GetDriver();
            var tbLibraryPriceDayLate = driver.FindElementByAccessibilityId("tbLibraryPriceDayLate");
            for (int i = 0; i < 5; i++) {
                tbLibraryPriceDayLate.SendKeys(Keys.Backspace);
                tbLibraryPriceDayLate.SendKeys(Keys.Delete);
            }
        }

        [When(@"the user removes the membership duration")]
        public void WhenTheUserRemovesTheMembershipDuration()
        {
            var driver = GuiDriver.GetDriver();
            var tbLibraryMembershipDuration = driver.FindElementByAccessibilityId("tbLibraryMembershipDuration");
            for (int i = 0; i < 5; i++) {
                tbLibraryMembershipDuration.SendKeys(Keys.Backspace);
                tbLibraryMembershipDuration.SendKeys(Keys.Delete);
            }
        }

        [When(@"the user doesn't change anything about the library")]
        public void WhenTheUserDoesntChangeAnythingAboutTheLibrary()
        {
            // Doing nothing
        }

        [Then(@"the system should not change anything about the library")]
        public void ThenTheSystemShouldNotChangeAnythingAboutTheLibrary()
        {
            MessageBoxTestHelper.CheckIfMessageBoxIsShown();
        }

        [When(@"the user changes the library's name")]
        public void WhenTheUserChangesTheLibrarysName()
        {
            WhenTheUserRemovesTheLibraryName();

            var driver = GuiDriver.GetDriver();
            var tbLibraryName = driver.FindElementByAccessibilityId("tbLibraryName");
            tbLibraryName.SendKeys("Skroy promijenjeno ime");
        }

        [When(@"the user enters the library address")]
        public void WhenTheUserEntersTheLibraryAddress()
        {
            var driver = GuiDriver.GetDriver();
            var tbLibraryAddress = driver.FindElementByAccessibilityId("tbLibraryAddress");
            tbLibraryAddress.SendKeys("Adresa nove knjiynice");
        }

        [Then(@"the library changes should be visible in the All libraries list")]
        public void ThenTheLibraryChangesShouldBeVisibleInTheAllLibrariesList()
        {
            var driver = GuiDriver.GetDriver();

            var btnAllLibraries = driver.FindElementByAccessibilityId("btnAllLibraries");
            btnAllLibraries.Click();

            var dgAllLibraries = driver.FindElementByAccessibilityId("dgAllLibraries");
            Assert.IsNotNull(dgAllLibraries);

            var changedLibraryName = driver.FindElementByName("Skroz promijenjeno ime");
            Assert.IsNotNull(changedLibraryName);
            var changedLibraryAddress = driver.FindElementByName("Adresa nove knjiznice");
            Assert.IsNotNull(changedLibraryAddress);

            GuiDriver.Dispose();
        }
    }
}
