using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F08_S01_BookCatalogueSearchAndFilterStepDefinitions
    {
        [Given(@"the user is logged in as a member")]
        public void GivenTheUserIsLoggedInAsAMember()
        {
            if (GuiDriver.GetDriver() != null) GuiDriver.Dispose();
            var driver = GuiDriver.GetOrCreateDriver();
            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");

            txtUsername.SendKeys("anabol");
            txtPassword.SendKeys("123");

            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");

            btnLogin.Click();
        }

        [Given(@"the user is on the Search library catalogue screen")]
        public void GivenTheUserIsOnTheSearchLibraryCatalogueScreen()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnCatalogue = driver.FindElementByAccessibilityId("btnSearch");
            btnCatalogue.Click();

            driver.Manage().Window.Maximize();
        }

        [When(@"the member enters information such as author name, author last name, year of release or book name of books that exist")]
        public void WhenTheMemberEntersInformationSuchAsAuthorNameAuthorLastNameYearOfReleaseOrBookNameOfBooksThatExist()
        {
            var driver = GuiDriver.GetDriver();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys("Smith");
        }

        [Then(@"the member should see the book with that information in the list")]
        public void ThenTheMemberShouldSeeTheBookWithThatInformationInTheList()
        {
            var driver = GuiDriver.GetDriver();

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");

            string value = "Emma Smith";
            bool valueFound = false;

            foreach(var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                var author = cells[2].Text;
                if(author == value)
                {
                    valueFound = true;
                    break;
                }
            }
            Assert.IsTrue(valueFound);
        }

        [When(@"the member enters information into the search bar")]
        public void WhenTheMemberEntersInformationIntoTheSearchBar()
        {
            var driver = GuiDriver.GetDriver();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys("Shake");
        }

        [When(@"the member includes digital books into the search")]
        public void WhenTheMemberIncludesDigitalBooksIntoTheSearch()
        {
            //nothing vec su ukljucene
        }

        [Then(@"the member should see digital books with that information")]
        public void ThenTheMemberShouldSeeDigitalBooksWithThatInformation()
        {
            var driver = GuiDriver.GetDriver();

            bool found = driver.FindElementByName("Da") != null;
            Assert.IsTrue(found);
        }

        [When(@"the member selects one type of criteria")]
        public void WhenTheMemberSelectsOneTypeOfCriteria()
        {
            var driver = GuiDriver.GetDriver();

            var cmbCriteria = driver.FindElementByAccessibilityId("cmbFilter");
            cmbCriteria.Click();

            var txtCriteria = driver.FindElementByName("Žanr");
            txtCriteria.Click();
        }

        [When(@"the member enters information into the filtered search bar")]
        public void WhenTheMemberEntersInformationIntoTheFilteredSearchBar()
        {
            var driver = GuiDriver.GetDriver();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys("Tragedija");
        }


        [Then(@"the member should see books that match information of only the chosen criteria")]
        public void ThenTheMemberShouldSeeBooksThatMatchInformationOfOnlyTheChosenCriteria()
        {
            var driver = GuiDriver.GetDriver();

            bool found = driver.FindElementByName("Tragedija") != null;
            Assert.IsTrue(found);
        }

        [When(@"the member chooses a search criteria")]
        public void WhenTheMemberChoosesASearchCriteria()
        {
            var driver = GuiDriver.GetDriver();

            var cmbCriteria = driver.FindElementByAccessibilityId("cmbFilter");
            cmbCriteria.Click();

            var txtCriteria = driver.FindElementByName("Godina");
            txtCriteria.Click();
        }

        [When(@"the member includes digital books")]
        public void WhenTheMemberIncludesDigitalBooks()
        {
            //nothing
        }

        [When(@"the member types into the search bar")]
        public void WhenTheMemberTypesIntoTheSearchBar()
        {
            var driver = GuiDriver.GetDriver();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys("20");
        }

        [When(@"the member clicks the button Clear filters")]
        public void WhenTheMemberClicksTheButtonClearFilters()
        {
            var driver = GuiDriver.GetDriver();

            var txtSearch = driver.FindElementByAccessibilityId("btnClear");
            txtSearch.Click();
        }

        [Then(@"the member should see that all of the inputs have been returned to default and empty")]
        public void ThenTheMemberShouldSeeThatAllOfTheInputsHaveBeenReturnedToDefaultAndEmpty()
        {
            var driver = GuiDriver.GetDriver();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            bool search = string.IsNullOrEmpty(txtSearch.Text);
            var cmb = driver.FindElementByAccessibilityId("cmbFilter");
            bool filter = cmb.Text == "Svi kriteriji";
            
            Assert.IsTrue(search && filter);
        }

        [When(@"the member chooses a book from the list")]
        public void WhenTheMemberChoosesABookFromTheList()
        {
            var driver = GuiDriver.GetDriver();

            var book = driver.FindElementByName("Ime1");
            book.Click();
        }

        [When(@"the member clicks on the See details button")]
        public void WhenTheMemberClicksOnTheSeeDetailsButton()
        {
            var driver = GuiDriver.GetDriver();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();
        }

        [Then(@"the member should be redirected to the Book details screen")]
        public void ThenTheMemberShouldBeRedirectedToTheBookDetailsScreen()
        {
            var driver = GuiDriver.GetDriver();

            bool screen = driver.FindElementByName("Informacije o knjizi") != null;

            Assert.IsTrue(screen);
        }

        [When(@"the member doesn't choose a book from the list")]
        public void WhenTheMemberDoesntChooseABookFromTheList()
        {
            //nothing
        }

        [Then(@"the member should stay at the same screen")]
        public void ThenTheMemberShouldStayAtTheSameScreen()
        {
            var driver = GuiDriver.GetDriver();

            bool screen = driver.FindElementByName("Pretraživanje kataloga knjižnice") != null;

            Assert.IsTrue(screen);
        }

        [Then(@"the member should see a warning message that a book has to be chosen")]
        public void ThenTheMemberShouldSeeAWarningMessageThatABookHasToBeChosen()
        {
            var driver = GuiDriver.GetDriver();

            bool screen = driver.FindElementByName("Morate odabrati knjigu!") != null;

            Assert.IsTrue(screen);
        }
    }
}
