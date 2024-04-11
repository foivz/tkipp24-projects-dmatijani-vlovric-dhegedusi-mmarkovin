using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F03_S01_NewBookEntryStepDefinitions
    {
        [Given(@"the user is logged in as an employee")]
        public void GivenTheUserIsLoggedInAsAnEmployee()
        {
            if (GuiDriver.GetDriver() != null) GuiDriver.Dispose();
            var driver = GuiDriver.GetOrCreateDriver();
            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");

            txtUsername.SendKeys("mmarkic");
            txtPassword.SendKeys("12345");

            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");

            btnLogin.Click();
        }

        [Given(@"the user is on the New book entry screen")]
        public void GivenTheUserIsOnTheNewBookEntryScreen()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnCatalogue = driver.FindElementByAccessibilityId("btnBookCatalog");
            btnCatalogue.Click();

            var btnNewBook = driver.FindElementByAccessibilityId("btnNewBook");
            btnNewBook.Click();
        }

        [When(@"the employee leaves the book name field empty")]
        public void WhenTheEmployeeLeavesTheBookNameFieldEmpty()
        {
            //nothing
        }

        [When(@"the employee clicks on the Insert button")]
        public void WhenTheEmployeeClicksOnTheInsertButton()
        {
            var driver = GuiDriver.GetDriver();

            var btnInsert = driver.FindElementByAccessibilityId("btnSave");
            btnInsert.Click();
        }

        [Then(@"the employee should remain on the same screen")]
        public void ThenTheEmployeeShouldRemainOnTheSameScreen()
        {
            var driver = GuiDriver.GetDriver();
            bool SameScreen = driver.FindElementByName("Unos nove knjige") != null;
            Assert.IsTrue(SameScreen);
        }

        [Then(@"the employee should see a warning message that the book name cannot be empty")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheBookNameCannotBeEmpty()
        {
            var driver = GuiDriver.GetDriver();
            bool message = driver.FindElementByName("Morate unijeti ime knjige!") != null;
            Assert.IsTrue(message);
        }

        [When(@"the employee enters all required fields and options")]
        public void WhenTheEmployeeEntersAllRequiredFieldsAndOptions()
        {
            //obavezni ime knjige, broj primjeraka, žanr, jel digitalna, autor
            var driver = GuiDriver.GetDriver();

            var txtName = driver.FindElementByAccessibilityId("txtName");
            var txtNumberCopies = driver.FindElementByAccessibilityId("txtNumberCopies");
            var cmbGenre = driver.FindElementByAccessibilityId("cmbGenre");
            var cmbAuthor = driver.FindElementByAccessibilityId("cmbAuthor");
            var radioButton = driver.FindElementByName("Da");

            txtName.SendKeys("ImeAutomatsko");
            txtNumberCopies.SendKeys("1");
            radioButton.Click();

            cmbGenre.Click();
            var chooseGenre = driver.FindElementByName("Romantika");
            chooseGenre.Click();

            cmbAuthor.Click();
            var chooseAuthor = driver.FindElementByName("Harper Vincet");
            chooseAuthor.Click();
        }

        [When(@"the employee enters a date different than dd-MM-yyyy")]
        public void WhenTheEmployeeEntersADateDifferentThanDd_MM_Yyyy()
        {
            var driver = GuiDriver.GetDriver();

            var txtDate = driver.FindElementByAccessibilityId("txtDate");
            txtDate.SendKeys("5.9.2002.");
        }

        [When(@"the employee clicks the Insert button")]
        public void WhenTheEmployeeClicksTheInsertButton()
        {
            var driver = GuiDriver.GetDriver();

            var btnInsert = driver.FindElementByAccessibilityId("btnSave");
            btnInsert.Click();
        }

        [Then(@"the employee should see a warning message that the date has to be in the dd-MM-yyyy format")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheDateHasToBeInTheDd_MM_YyyyFormat()
        {
            var driver = GuiDriver.GetDriver();
            bool message = driver.FindElementByName("Neispravan format datuma! Primjer formata je 05-09-2002") != null;
            Assert.IsTrue(message);
        }

        [When(@"the employee enters all required fields and options before the number of pages")]
        public void WhenTheEmployeeEntersAllRequiredFieldsAndOptionsBeforeTheNumberOfPages()
        {
            var driver = GuiDriver.GetDriver();

            var txtName = driver.FindElementByAccessibilityId("txtName");
            var txtNumberCopies = driver.FindElementByAccessibilityId("txtNumberCopies");
            var cmbGenre = driver.FindElementByAccessibilityId("cmbGenre");
            var cmbAuthor = driver.FindElementByAccessibilityId("cmbAuthor");
            var radioButton = driver.FindElementByName("Ne");

            txtName.SendKeys("ImeNegativno1");
            txtNumberCopies.SendKeys("1");
            radioButton.Click();

            cmbGenre.Click();
            var chooseGenre = driver.FindElementByName("Romantika");
            chooseGenre.Click();

            cmbAuthor.Click();
            var chooseAuthor = driver.FindElementByName("Harper Vincet");
            chooseAuthor.Click();
        }

        [When(@"the employee enters a non numerical or negative value into the number of pages field")]
        public void WhenTheEmployeeEntersANonNumericalOrNegativeValueIntoTheNumberOfPagesField()
        {
            var driver = GuiDriver.GetDriver();

            var txtNumPages = driver.FindElementByAccessibilityId("txtNumberPages");
            txtNumPages.SendKeys("a");
        }

        [Then(@"the employee should see a warning message that the number of pages can only be a valid numerical value")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheNumberOfPagesCanOnlyBeAValidNumericalValue()
        {
            var driver = GuiDriver.GetDriver();

            bool message = driver.FindElementByName("Polja u koja se upisuje broj moraju sadržavati samo brojeve!") != null;
            Assert.IsTrue(message);
        }

        [When(@"the employee enters all required fields and options before the radio button")]
        public void WhenTheEmployeeEntersAllRequiredFieldsAndOptionsBeforeTheRadioButton()
        {
            var driver = GuiDriver.GetDriver();

            var txtName = driver.FindElementByAccessibilityId("txtName");
            var txtNumberCopies = driver.FindElementByAccessibilityId("txtNumberCopies");
            var cmbGenre = driver.FindElementByAccessibilityId("cmbGenre");
            var cmbAuthor = driver.FindElementByAccessibilityId("cmbAuthor");

            txtName.SendKeys("a");
            txtNumberCopies.SendKeys("1");

            cmbGenre.Click();
            var chooseGenre = driver.FindElementByName("Romantika");
            chooseGenre.Click();

            cmbAuthor.Click();
            var chooseAuthor = driver.FindElementByName("Harper Vincet");
            chooseAuthor.Click();
        }

        [When(@"the employee leaves the radio button selection empty")]
        public void WhenTheEmployeeLeavesTheRadioButtonSelectionEmpty()
        {
            //nothing
        }

        [Then(@"the employee should see a warning message that one of the two radio button options has to be chosen")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatOneOfTheTwoRadioButtonOptionsHasToBeChosen()
        {
            var driver = GuiDriver.GetDriver();

            bool message = driver.FindElementByName("Morate odabrati je li knjiga digitalna!") != null;
            Assert.IsTrue(message);
        }

        [When(@"the employee enters all required fields and options before the number of copies")]
        public void WhenTheEmployeeEntersAllRequiredFieldsAndOptionsBeforeTheNumberOfCopies()
        {
            var driver = GuiDriver.GetDriver();

            var txtName = driver.FindElementByAccessibilityId("txtName");
            var cmbGenre = driver.FindElementByAccessibilityId("cmbGenre");
            var cmbAuthor = driver.FindElementByAccessibilityId("cmbAuthor");
            var radioButton = driver.FindElementByName("Ne");

            txtName.SendKeys("a");
            radioButton.Click();

            cmbGenre.Click();
            var chooseGenre = driver.FindElementByName("Romantika");
            chooseGenre.Click();

            cmbAuthor.Click();
            var chooseAuthor = driver.FindElementByName("Harper Vincet");
            chooseAuthor.Click();
        }

        [When(@"the employee leaves the number of copies field empty")]
        public void WhenTheEmployeeLeavesTheNumberOfCopiesFieldEmpty()
        {
            //nothing
        }

        [Then(@"the employee should see a warning message that the number of copies field cannot be empty")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheNumberOfCopiesFieldCannotBeEmpty()
        {
            var driver = GuiDriver.GetDriver();

            bool message = driver.FindElementByName("Morate unijeti broj primjeraka knjige! Ako je knjiga digitalna unesite 0") != null;
            Assert.IsTrue(message);
        }

        [When(@"the employee enters a non numerical or negative value into the number of copies field")]
        public void WhenTheEmployeeEntersANonNumericalOrNegativeValueIntoTheNumberOfCopiesField()
        {
            var driver = GuiDriver.GetDriver();

            var txtNumberCopies = driver.FindElementByAccessibilityId("txtNumberCopies");
            txtNumberCopies.SendKeys("a");
        }

        [Then(@"the employee should see a warning message that the number of copies can only be a valid numerical value")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheNumberOfCopiesCanOnlyBeAValidNumericalValue()
        {
            var driver = GuiDriver.GetDriver();

            bool message = driver.FindElementByName("Polja u koja se upisuje broj moraju sadržavati samo brojeve!") != null;
            Assert.IsTrue(message);
        }

        [When(@"the employee enters all required fields and options before the genre selection")]
        public void WhenTheEmployeeEntersAllRequiredFieldsAndOptionsBeforeTheGenreSelection()
        {
            throw new PendingStepException();
        }

        [When(@"the employee leaves the genre selection empty")]
        public void WhenTheEmployeeLeavesTheGenreSelectionEmpty()
        {
            throw new PendingStepException();
        }

        [Then(@"the employee should see a warning message that the genre selection cannot be empty")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheGenreSelectionCannotBeEmpty()
        {
            throw new PendingStepException();
        }

        [When(@"the employee enters all required fields and options before the author selection")]
        public void WhenTheEmployeeEntersAllRequiredFieldsAndOptionsBeforeTheAuthorSelection()
        {
            throw new PendingStepException();
        }

        [When(@"the employee leaves the author selection empty")]
        public void WhenTheEmployeeLeavesTheAuthorSelectionEmpty()
        {
            throw new PendingStepException();
        }

        [Then(@"the employee should see a warning message that the author selection cannot be empty")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheAuthorSelectionCannotBeEmpty()
        {
            throw new PendingStepException();
        }

        [Given(@"the employee is on the New genre screen")]
        public void GivenTheEmployeeIsOnTheNewGenreScreen()
        {
            throw new PendingStepException();
        }

        [When(@"the employee leaves the genre name field empty")]
        public void WhenTheEmployeeLeavesTheGenreNameFieldEmpty()
        {
            throw new PendingStepException();
        }

        [Then(@"the employee should see a warning message that the genre name field cannot be empty")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheGenreNameFieldCannotBeEmpty()
        {
            throw new PendingStepException();
        }

        [Given(@"the employee is on the New author screen")]
        public void GivenTheEmployeeIsOnTheNewAuthorScreen()
        {
            throw new PendingStepException();
        }

        [When(@"the employee leaves the author name field empty")]
        public void WhenTheEmployeeLeavesTheAuthorNameFieldEmpty()
        {
            throw new PendingStepException();
        }

        [Then(@"the employee should see a warning message that the author name field cannot be empty")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheAuthorNameFieldCannotBeEmpty()
        {
            throw new PendingStepException();
        }

        [When(@"the employee enters a name into the author name field")]
        public void WhenTheEmployeeEntersANameIntoTheAuthorNameField()
        {
            throw new PendingStepException();
        }

        [When(@"the employee leaves the author last name field empty")]
        public void WhenTheEmployeeLeavesTheAuthorLastNameFieldEmpty()
        {
            throw new PendingStepException();
        }

        [Then(@"the employee should see a warning message that the author last name field cannot be empty")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheAuthorLastNameFieldCannotBeEmpty()
        {
            throw new PendingStepException();
        }

        [When(@"the employee enters all required fields before the birth date")]
        public void WhenTheEmployeeEntersAllRequiredFieldsBeforeTheBirthDate()
        {
            throw new PendingStepException();
        }

        [When(@"the employee enters valid inputs, chooses options and selects dropdowns for name, radio button, number of copies, genre and author")]
        public void WhenTheEmployeeEntersValidInputsChoosesOptionsAndSelectsDropdownsForNameRadioButtonNumberOfCopiesGenreAndAuthor()
        {
            throw new PendingStepException();
        }

        [Then(@"the employee should see a message that the insertion was succesful")]
        public void ThenTheEmployeeShouldSeeAMessageThatTheInsertionWasSuccesful()
        {
            throw new PendingStepException();
        }

        [When(@"the employee enters and chooses all valid information")]
        public void WhenTheEmployeeEntersAndChoosesAllValidInformation()
        {
            throw new PendingStepException();
        }

        [When(@"the employee enters the genre name field")]
        public void WhenTheEmployeeEntersTheGenreNameField()
        {
            throw new PendingStepException();
        }

        [Then(@"the employee should be redirected to the New book entry screen")]
        public void ThenTheEmployeeShouldBeRedirectedToTheNewBookEntryScreen()
        {
            throw new PendingStepException();
        }

        [When(@"the employee enters the required fields")]
        public void WhenTheEmployeeEntersTheRequiredFields()
        {
            throw new PendingStepException();
        }

        [When(@"the employee enters all information")]
        public void WhenTheEmployeeEntersAllInformation()
        {
            throw new PendingStepException();
        }

        [When(@"the employee clicks on the Back button")]
        public void WhenTheEmployeeClicksOnTheBackButton()
        {
            throw new PendingStepException();
        }

        [Then(@"the employee should be redirected to the Action choice screen")]
        public void ThenTheEmployeeShouldBeRedirectedToTheActionChoiceScreen()
        {
            throw new PendingStepException();
        }

        [Then(@"the employee should see all his entered inputs there")]
        public void ThenTheEmployeeShouldSeeAllHisEnteredInputsThere()
        {
            throw new PendingStepException();
        }
    }
}
