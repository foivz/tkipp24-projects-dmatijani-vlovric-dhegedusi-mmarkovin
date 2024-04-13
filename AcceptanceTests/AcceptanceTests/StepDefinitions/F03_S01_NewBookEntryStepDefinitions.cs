using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
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
            txtNumPages.SendKeys(Keys.Subtract);
            txtNumPages.SendKeys("12");
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

        [When(@"the employee enters all required fields and options except the number of copies")]
        public void WhenTheEmployeeEntersAllRequiredFieldsAndOptionsExceptTheNumberOfCopies()
        {
            var driver = GuiDriver.GetDriver();

            var txtName = driver.FindElementByAccessibilityId("txtName");
            var cmbGenre = driver.FindElementByAccessibilityId("cmbGenre");
            var cmbAuthor = driver.FindElementByAccessibilityId("cmbAuthor");
            var radioButton = driver.FindElementByName("Da");

            txtName.SendKeys("ImeAutomatsko");
            radioButton.Click();

            cmbGenre.Click();
            var chooseGenre = driver.FindElementByName("Romantika");
            chooseGenre.Click();

            cmbAuthor.Click();
            var chooseAuthor = driver.FindElementByName("Harper Vincet");
            chooseAuthor.Click();
        }


        [When(@"the employee enters a non numerical or negative value into the number of copies field")]
        public void WhenTheEmployeeEntersANonNumericalOrNegativeValueIntoTheNumberOfCopiesField()
        {
            var driver = GuiDriver.GetDriver();

            var txtNumberCopies = driver.FindElementByAccessibilityId("txtNumberCopies");
            txtNumberCopies.SendKeys(Keys.Subtract);
            txtNumberCopies.SendKeys("13");
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
            var driver = GuiDriver.GetDriver();

            var txtName = driver.FindElementByAccessibilityId("txtName");
            var txtCopies = driver.FindElementByAccessibilityId("txtNumberCopies");
            var cmbAuthor = driver.FindElementByAccessibilityId("cmbAuthor");
            var radioButton = driver.FindElementByName("Ne");

            txtName.SendKeys("a");
            txtCopies.SendKeys("1");
            radioButton.Click();

            cmbAuthor.Click();
            var chooseAuthor = driver.FindElementByName("Harper Vincet");
            chooseAuthor.Click();
        }

        [When(@"the employee leaves the genre selection empty")]
        public void WhenTheEmployeeLeavesTheGenreSelectionEmpty()
        {
            //nothing
        }

        [Then(@"the employee should see a warning message that the genre selection cannot be empty")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheGenreSelectionCannotBeEmpty()
        {
            var driver = GuiDriver.GetDriver();

            bool message = driver.FindElementByName("Morate odabrati žanr!") != null;
            Assert.IsTrue(message);
        }

        [When(@"the employee enters all required fields and options before the author selection")]
        public void WhenTheEmployeeEntersAllRequiredFieldsAndOptionsBeforeTheAuthorSelection()
        {
            var driver = GuiDriver.GetDriver();

            var txtName = driver.FindElementByAccessibilityId("txtName");
            var txtCopies = driver.FindElementByAccessibilityId("txtNumberCopies");
            var cmbGenre = driver.FindElementByAccessibilityId("cmbGenre");
            var radioButton = driver.FindElementByName("Ne");

            txtName.SendKeys("a");
            txtCopies.SendKeys("1");
            radioButton.Click();

            cmbGenre.Click();
            var chooseGenre = driver.FindElementByName("Romantika");
            chooseGenre.Click();
        }

        [When(@"the employee leaves the author selection empty")]
        public void WhenTheEmployeeLeavesTheAuthorSelectionEmpty()
        {
            //nothing
        }

        [Then(@"the employee should see a warning message that the author selection cannot be empty")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheAuthorSelectionCannotBeEmpty()
        {
            var driver = GuiDriver.GetDriver();

            bool message = driver.FindElementByName("Morate odabrati autora!") != null;
            Assert.IsTrue(message);
        }

        [Given(@"the employee is on the New genre screen")]
        public void GivenTheEmployeeIsOnTheNewGenreScreen()
        {
            var driver = GuiDriver.GetDriver();

            var btnNewGenre = driver.FindElementByAccessibilityId("btnNewGenre");
            btnNewGenre.Click();

        }

        [When(@"the employee leaves the genre name field empty")]
        public void WhenTheEmployeeLeavesTheGenreNameFieldEmpty()
        {
            //nothing
        }

        [Then(@"the employee should remain on the New genre screen")]
        public void ThenTheEmployeeShouldRemainOnTheNewGenreScreen()
        {
            var driver = GuiDriver.GetDriver();
            bool SameScreen = driver.FindElementByName("Unos novog žanra") != null;
            Assert.IsTrue(SameScreen);
        }

        [Then(@"the employee should see a message that the genre insertion was succesful")]
        public void ThenTheEmployeeShouldSeeAMessageThatTheGenreInsertionWasSuccesful()
        {
            var driver = GuiDriver.GetDriver();

            bool message = driver.FindElementByName("Uspješno!") != null;
            Assert.IsTrue(message);
        }


        [Then(@"the employee should see a warning message that the genre name field cannot be empty")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheGenreNameFieldCannotBeEmpty()
        {
            var driver = GuiDriver.GetDriver();

            bool message = driver.FindElementByName("Morate unijeti ime žanra!") != null;
            Assert.IsTrue(message);
        }

        [Given(@"the employee is on the New author screen")]
        public void GivenTheEmployeeIsOnTheNewAuthorScreen()
        {
            var driver = GuiDriver.GetDriver();

            var btnNewGenre = driver.FindElementByAccessibilityId("btnNewAuthor");
            btnNewGenre.Click();
        }

        [When(@"the employee leaves the author name field empty")]
        public void WhenTheEmployeeLeavesTheAuthorNameFieldEmpty()
        {
            //nothing
        }

        [Then(@"the employee should remain on the New author screen")]
        public void ThenTheEmployeeShouldRemainOnTheNewAuthorScreen()
        {
            var driver = GuiDriver.GetDriver();
            bool SameScreen = driver.FindElementByName("Unos novog autora") != null;
            Assert.IsTrue(SameScreen);
        }


        [Then(@"the employee should see a warning message that the author name field cannot be empty")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheAuthorNameFieldCannotBeEmpty()
        {
            var driver = GuiDriver.GetDriver();

            bool message = driver.FindElementByName("Morate unijeti ime autora!") != null;
            Assert.IsTrue(message);
            //faila jer pise knjige
        }

        [When(@"the employee enters a name into the author name field")]
        public void WhenTheEmployeeEntersANameIntoTheAuthorNameField()
        {
            var driver = GuiDriver.GetDriver();

            var txtName = driver.FindElementByAccessibilityId("txtName");
            txtName.SendKeys("Ivan");
        }

        [When(@"the employee leaves the author last name field empty")]
        public void WhenTheEmployeeLeavesTheAuthorLastNameFieldEmpty()
        {
            //nothing
        }

        [Then(@"the employee should see a warning message that the author last name field cannot be empty")]
        public void ThenTheEmployeeShouldSeeAWarningMessageThatTheAuthorLastNameFieldCannotBeEmpty()
        {
            var driver = GuiDriver.GetDriver();

            bool message = driver.FindElementByName("Morate unijeti prezime!") != null;
            Assert.IsTrue(message);
        }

        [When(@"the employee enters all required fields before the birth date")]
        public void WhenTheEmployeeEntersAllRequiredFieldsBeforeTheBirthDate()
        {
            var driver = GuiDriver.GetDriver();

            var txtName = driver.FindElementByAccessibilityId("txtName");
            var txtSurname = driver.FindElementByAccessibilityId("txtSurname");
            txtName.SendKeys("Ivan");
            txtSurname.SendKeys("Papa");
        }

        [When(@"the employee enters a birth date different than dd-MM-yyyy")]
        public void WhenTheEmployeeEntersABirthDateDifferentThanDd_MM_Yyyy()
        {
            var driver = GuiDriver.GetDriver();

            var txtDate = driver.FindElementByAccessibilityId("txtBirthDate");
            txtDate.SendKeys("5.9.2002.");
        }


        [When(@"the employee enters valid inputs, chooses options and selects dropdowns for name, radio button, number of copies, genre and author")]
        public void WhenTheEmployeeEntersValidInputsChoosesOptionsAndSelectsDropdownsForNameRadioButtonNumberOfCopiesGenreAndAuthor()
        {
            var driver = GuiDriver.GetDriver();

            var txtName = driver.FindElementByAccessibilityId("txtName");
            var txtCopies = driver.FindElementByAccessibilityId("txtNumberCopies");
            var cmbGenre = driver.FindElementByAccessibilityId("cmbGenre");
            var cmbAuthor = driver.FindElementByAccessibilityId("cmbAuthor");
            var radioButton = driver.FindElementByName("Ne");

            txtName.SendKeys("Ime 1");
            //OVJD MIJENJAT SVAKI PUT
            txtCopies.SendKeys("1");
            radioButton.Click();

            cmbGenre.Click();
            var chooseGenre = driver.FindElementByName("Romantika");
            chooseGenre.Click();

            cmbAuthor.Click();
            var chooseAuthor = driver.FindElementByName("Harper Vincet");
            chooseAuthor.Click();
        }

        [Then(@"the employee should see a message that the insertion was succesful")]
        public void ThenTheEmployeeShouldSeeAMessageThatTheInsertionWasSuccesful()
        {
            var driver = GuiDriver.GetDriver();

            bool message = driver.FindElementByName("Uspješno") != null;
            Assert.IsTrue(message);
        }

        [When(@"the employee enters and chooses all valid information")] //za knjigu
        public void WhenTheEmployeeEntersAndChoosesAllValidInformation()
        {
            var driver = GuiDriver.GetDriver();

            var txtName = driver.FindElementByAccessibilityId("txtName");
            var txtCopies = driver.FindElementByAccessibilityId("txtNumberCopies");
            var cmbGenre = driver.FindElementByAccessibilityId("cmbGenre");
            var cmbAuthor = driver.FindElementByAccessibilityId("cmbAuthor");
            var radioButton = driver.FindElementByName("Ne");

            var txtDescription = driver.FindElementByAccessibilityId("txtDescription");
            var txtDate = driver.FindElementByAccessibilityId("txtDate");
            var txtPages = driver.FindElementByAccessibilityId("txtNumberPages");
            var txtImage = driver.FindElementByAccessibilityId("txtLinkPicture");

            txtName.SendKeys("ImeSve 1");
            txtDescription.SendKeys("OpisSve 1");
            txtDate.SendKeys("03");
            txtDate.SendKeys(Keys.Subtract);
            txtDate.SendKeys("04");
            txtDate.SendKeys(Keys.Subtract);
            txtDate.SendKeys("1998");

            txtPages.SendKeys("101");
            //txtImage.SendKeys("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTxByMe5tXy2r41PqgovT1yfm1R9pPAjiKfZCKzfK0sZw&s");
            //OVJD MIJENJAT SVAKI PUT
            txtCopies.SendKeys("1");
            radioButton.Click();

            cmbGenre.Click();
            var chooseGenre = driver.FindElementByName("Romantika");
            chooseGenre.Click();

            cmbAuthor.Click();
            var chooseAuthor = driver.FindElementByName("Harper Vincet");
            chooseAuthor.Click();
        }

        [When(@"the employee enters the genre name field")]
        public void WhenTheEmployeeEntersTheGenreNameField()
        {
            var driver = GuiDriver.GetDriver();

            var txtName = driver.FindElementByAccessibilityId("txtName");
            txtName.SendKeys("Ime 1");
            //OVDJE MIJENJAT SVAKI PUT
        }

        [Then(@"the employee should be redirected to the New book entry screen")]
        public void ThenTheEmployeeShouldBeRedirectedToTheNewBookEntryScreen()
        {
            var driver = GuiDriver.GetDriver();


            var btnOk = driver.FindElementByName("OK");
            btnOk.Click();

            bool SameScreen = driver.FindElementByName("Unos nove knjige") != null;
            Assert.IsTrue(SameScreen);
        }

        [When(@"the employee enters the required fields")] //autor
        public void WhenTheEmployeeEntersTheRequiredFields()
        {
            var driver = GuiDriver.GetDriver();

            var txtName = driver.FindElementByAccessibilityId("txtName");
            var txtSurname = driver.FindElementByAccessibilityId("txtSurname");

            txtName.SendKeys("Autor 1");
            txtSurname.SendKeys("Prezime 1");
            //OVO MIJENJAT
        }

        [When(@"the employee enters all information")] //autor
        public void WhenTheEmployeeEntersAllInformation()
        {
            var driver = GuiDriver.GetDriver();

            var txtName = driver.FindElementByAccessibilityId("txtName");
            var txtSurname = driver.FindElementByAccessibilityId("txtSurname");
            var txtBirthDate = driver.FindElementByAccessibilityId("txtBirthDate");

            txtName.SendKeys("AutorSve 1");
            txtSurname.SendKeys("PrezimeSve 1");
            //OVO MIJENJAT
            txtBirthDate.SendKeys("03");
            txtBirthDate.SendKeys(Keys.Subtract);
            txtBirthDate.SendKeys("12");
            txtBirthDate.SendKeys(Keys.Subtract);
            txtBirthDate.SendKeys("2001");
        }

        [When(@"the employee clicks on the Back button")]
        public void WhenTheEmployeeClicksOnTheBackButton()
        {
            var driver = GuiDriver.GetDriver();

            var btnBack = driver.FindElementByAccessibilityId("btnCancel");
            btnBack.Click();
        }

        [Then(@"the employee should be redirected to the Action choice screen")]
        public void ThenTheEmployeeShouldBeRedirectedToTheActionChoiceScreen()
        {
            var driver = GuiDriver.GetDriver();
            bool SameScreen = driver.FindElementByName("Odabir radnje") != null;
            Assert.IsTrue(SameScreen);
        }

        [Then(@"the employee should see all his entered inputs there")] //od autora do knjige
        public void ThenTheEmployeeShouldSeeAllHisEnteredInputsThere()
        {
            var driver = GuiDriver.GetDriver();

            var txtName = driver.FindElementByAccessibilityId("txtName");
            string nameValue = txtName.Text;
            bool empty = string.IsNullOrEmpty(nameValue);
            Assert.IsFalse(empty);
        }

        [Given(@"the employee entered some information on the New book screen")]
        public void GivenTheEmployeeEnteredSomeInformationOnTheNewBookScreen()
        {
            var driver = GuiDriver.GetDriver();

            var txtName = driver.FindElementByAccessibilityId("txtName");
            txtName.SendKeys("Test");
        }

    }
}
