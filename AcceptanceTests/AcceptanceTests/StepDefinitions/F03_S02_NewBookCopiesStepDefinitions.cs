using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F03_S02_NewBookCopiesStepDefinitions
    {
        [Given(@"the user is on the Add new book copies screen")]
        public void GivenTheUserIsOnTheAddNewBookCopiesScreen()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnCatalogue = driver.FindElementByAccessibilityId("btnBookCatalog");
            btnCatalogue.Click();

            var btnNewBook = driver.FindElementByAccessibilityId("btnNewCopies");
            btnNewBook.Click();
        }

        [Then(@"the employee should remain on the Add new book copies screen")]
        public void ThenTheEmployeeShouldRemainOnTheAddNewBookCopiesScreen()
        {
            var driver = GuiDriver.GetDriver();

            bool screen = driver.FindElementByName("Dodajte nove primjerke knjige") != null;
            Assert.IsTrue(screen);
        }


        [When(@"the employee chooses a book from the list")]
        public void WhenTheEmployeeChoosesABookFromTheList()
        {
            var driver = GuiDriver.GetDriver();

            var btnBook = driver.FindElementByName("Ime 1");
            btnBook.Click();
        }

        [Then(@"the employee should see a warning message that the number of new copies field cannot be empty")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheNumberOfNewCopiesFieldCannotBeEmpty()
        {
            var driver = GuiDriver.GetDriver();

            bool screen = driver.FindElementByName("Broj novih primjeraka mora sadržavati samo brojeve!") != null;
            Assert.IsTrue(screen);
        }


        [Then(@"the employee should see a warning message that the number of copies field has to contain a valid numerical value")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheNumberOfCopiesFieldHasToContainAValidNumericalValue()
        {
            var driver = GuiDriver.GetDriver();

            bool screen = driver.FindElementByName("Broj novih primjeraka mora sadržavati samo brojeve!") != null;
            Assert.IsTrue(screen);
        }

        [When(@"the employee does not choose a book from the list")]
        public void WhenTheEmployeeDoesNotChooseABookFromTheList()
        {
            //nothing
        }

        [When(@"the employee enters a valid number of copies")]
        public void WhenTheEmployeeEntersAValidNumberOfCopies()
        {
            var driver = GuiDriver.GetDriver();

            var txtCopies = driver.FindElementByAccessibilityId("txtNumberCopies");
            txtCopies.SendKeys("1");
        }

        [Then(@"the employee should see a warning message that a book must be chosen")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatABookMustBeChosen()
        {
            var driver = GuiDriver.GetDriver();

            bool screen = driver.FindElementByName("Morate odabrati knjigu!") != null;
            Assert.IsTrue(screen);
        }

        [Then(@"the employee should see a message that the copies have been succesfully added")]
        public void ThenTheEmployeeShouldSeeAMessageThatTheCopiesHaveBeenSuccesfullyAdded()
        {
            var driver = GuiDriver.GetDriver();

            bool screen = driver.FindElementByName("Uspješno!") != null;
            Assert.IsTrue(screen);
        }

        [Then(@"the employee should see a refreshed list")]
        public void ThenTheEmployeeShouldSeeARefreshedList()
        {
            //nothing
        }

        [When(@"the employee enters an existing book name into the book search field")]
        public void WhenTheEmployeeEntersAnExistingBookNameIntoTheBookSearchField()
        {
            var driver = GuiDriver.GetDriver();

            var txtCopies = driver.FindElementByAccessibilityId("txtBookName");
            txtCopies.SendKeys("Labirint");
        }

        [Then(@"the empoloyee sees the desired book in the list")]
        public void ThenTheEmpoloyeeSeesTheDesiredBookInTheList()
        {
            var driver = GuiDriver.GetDriver();

            bool screen = driver.FindElementByName("Labirint Iluzija") != null;
            Assert.IsTrue(screen);
        }
    }
}
