using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F03_S03_BookArchivingStepDefinitions
    {
        [Given(@"the user is on the Archive book screen")]
        public void GivenTheUserIsOnTheArchiveBookScreen()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnCatalogue = driver.FindElementByAccessibilityId("btnBookCatalog");
            btnCatalogue.Click();

            var btnNewBook = driver.FindElementByAccessibilityId("btnArchive");
            btnNewBook.Click();
        }

        [When(@"the employee doesn't choose a book from the list")]
        public void WhenTheEmployeeDoesntChooseABookFromTheList()
        {
            //nothing
        }

        [When(@"the employee clicks the Archive button")]
        public void WhenTheEmployeeClicksTheArchiveButton()
        {
            var driver = GuiDriver.GetDriver();

            var btnCatalogue = driver.FindElementByAccessibilityId("btnSave");
            btnCatalogue.Click();
        }

        [Then(@"the employee should remain on the Archive book screen")]
        public void ThenTheEmployeeShouldRemainOnTheArchiveBookScreen()
        {
            var driver = GuiDriver.GetDriver();

            bool screen = driver.FindElementByName("Arhivirajte knjige") != null;
            Assert.IsTrue(screen);
        }

        [Then(@"the employee should see a warning message that a book has to be selected")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatABookHasToBeSelected()
        {
            var driver = GuiDriver.GetDriver();

            bool messageBox = driver.FindElementByName("Morate odabrati knjigu!") != null;
            Assert.IsTrue(messageBox);
        }

        [When(@"the employee enters an existing book name into the archive book search field")]
        public void WhenTheEmployeeEntersAnExistingBookNameIntoTheArchiveBookSearchField()
        {
            var driver = GuiDriver.GetDriver();

            var txtCopies = driver.FindElementByAccessibilityId("txtBookName");
            txtCopies.SendKeys("ImeSve 1");
        }

        [Then(@"the empoloyee sees the desired book in the list to archive")]
        public void ThenTheEmpoloyeeSeesTheDesiredBookInTheListToArchive()
        {
            var driver = GuiDriver.GetDriver();

            bool foundBook = driver.FindElementByName("ImeSve 1") != null;
            Assert.IsTrue(foundBook);
        }

        [When(@"the employee chooses a book from the list to archive")]
        public void WhenTheEmployeeChoosesABookFromTheListToArchive()
        {
            var driver = GuiDriver.GetDriver();

            var book = driver.FindElementByName("ImeSve 1");
            book.Click();
        }

        [When(@"the employee clicks on the Archive button")]
        public void WhenTheEmployeeClicksOnTheArchiveButton()
        {
            var driver = GuiDriver.GetDriver();

            var book = driver.FindElementByAccessibilityId("btnSave");
            book.Click();
        }

        [Then(@"the employee should see a message that the book is succesfully archived")]
        public void ThenTheEmployeeShouldSeeAMessageThatTheBookIsSuccesfullyArchived()
        {
            var driver = GuiDriver.GetDriver();

            bool messageBox = driver.FindElementByName("Uspješno!") != null;
            Assert.IsTrue(messageBox);
        }

        [Then(@"the employee should see a refreshed list of books")]
        public void ThenTheEmployeeShouldSeeARefreshedListOfBooks()
        {
            //nothing
        }
    }
}
