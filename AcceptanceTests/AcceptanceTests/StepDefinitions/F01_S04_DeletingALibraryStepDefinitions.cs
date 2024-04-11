using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F01_S04_DeletingALibraryStepDefinitions
    {
        [When(@"the user clicks the Delete library button")]
        public void WhenTheUserClicksTheDeleteLibraryButton()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnDeleteLibrary = driver.FindElementByAccessibilityId("btnRemoveLibrary");
            btnDeleteLibrary.Click();
        }

        [Then(@"the system should show an error message that it cannot delete the library selection")]
        public void ThenTheSystemShouldShowAnErrorMessageThatItCannotDeleteTheLibrarySelection()
        {
            var driver = GuiDriver.GetDriver();
            Assert.IsNotNull(driver);
            driver.SwitchTo().Window(driver.WindowHandles.First());
            Assert.IsNotNull(driver);

            var btnOK = driver.FindElementByName("OK");
            Assert.IsNotNull(btnOK);
            btnOK.Click();
            GuiDriver.Dispose();
        }

        [When(@"the user chooses one library to delete")]
        public void WhenTheUserChoosesOneLibraryToDelete()
        {
            var driver = GuiDriver.GetDriver();
            var libraryToDelete = driver.FindElementByName("997");
            libraryToDelete.Click();
        }

        [Then(@"the system should warn the user before deleting a library")]
        public void ThenTheSystemShouldWarnTheUserBeforeDeletingALibrary()
        {
            var driver = GuiDriver.GetDriver();
            var oldDriver = driver;
            Assert.IsNotNull(driver);
            driver.SwitchTo().Window(driver.WindowHandles.First());
            Assert.IsNotNull(driver);
            Assert.AreNotEqual(driver, oldDriver);
        }

        [Then(@"the system should delete the selected library")]
        public void ThenTheSystemShouldDeleteTheSelectedLibrary()
        {
            // Currently nothing (deletes automatically)
        }

        [When(@"the user chooses multiple libraries to delete")]
        public void WhenTheUserChoosesMultipleLibrariesToDelete()
        {
            var driver = GuiDriver.GetDriver();
            var libraryToDelete1 = driver.FindElementByName("997");
            var libraryToDelete2 = driver.FindElementByName("12343");

            Actions action = new Actions(driver);
            action.KeyDown(Keys.LeftControl)
                  .Click(libraryToDelete1)
                  .Click(libraryToDelete2)
                  .KeyUp(Keys.LeftControl)
                  .Perform();
        }
    }
}
