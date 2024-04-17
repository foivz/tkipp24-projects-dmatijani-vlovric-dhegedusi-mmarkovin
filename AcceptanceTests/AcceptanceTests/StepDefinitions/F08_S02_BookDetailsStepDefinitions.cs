using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F08_S02_BookDetailsStepDefinitions
    {
        [When(@"the member chooses a book that has all its information entered")]
        public void WhenTheMemberChoosesABookThatHasAllItsInformationEntered()
        {
            var driver = GuiDriver.GetDriver();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys("Tajanstveni");

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();
        }

        [When(@"the member clicks on the Book details button")]
        public void WhenTheMemberClicksOnTheBookDetailsButton()
        {
            var driver = GuiDriver.GetDriver();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            driver.Manage().Window.Maximize();
        }

        [Then(@"the member should see all the book information")]
        public void ThenTheMemberShouldSeeAllTheBookInformation()
        {
            var driver = GuiDriver.GetDriver();
            bool txtName = string.IsNullOrEmpty(driver.FindElementByAccessibilityId("tblName").Text) == false;
            bool txtAuthor = string.IsNullOrEmpty(driver.FindElementByAccessibilityId("tblAuthor").Text) == false;
            bool txtDescription = string.IsNullOrEmpty(driver.FindElementByAccessibilityId("tblDescription").Text) == false;
            bool txtGenre = string.IsNullOrEmpty(driver.FindElementByAccessibilityId("tblGenre").Text) == false;
            bool txtDate = string.IsNullOrEmpty(driver.FindElementByAccessibilityId("tblDate").Text) == false;
            bool txtPages = string.IsNullOrEmpty(driver.FindElementByAccessibilityId("tblPageNum").Text) == false;

            Assert.IsTrue(txtName && txtAuthor && txtDescription && txtGenre && txtDate && txtPages);

        }

        [When(@"the member chooses a book that has only its required information entered")]
        public void WhenTheMemberChoosesABookThatHasOnlyItsRequiredInformationEntered()
        {
            var driver = GuiDriver.GetDriver();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys("Samo");

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();
        }

        [Then(@"the member should see all the required book information")]
        public void ThenTheMemberShouldSeeAllTheRequiredBookInformation()
        {
            var driver = GuiDriver.GetDriver();
            bool txtName = string.IsNullOrEmpty(driver.FindElementByAccessibilityId("tblName").Text) == false;
            bool txtAuthor = string.IsNullOrEmpty(driver.FindElementByAccessibilityId("tblAuthor").Text) == false;
            bool txtGenre = string.IsNullOrEmpty(driver.FindElementByAccessibilityId("tblGenre").Text) == false;

            Assert.IsTrue(txtName && txtAuthor && txtGenre);
        }

        [Then(@"the member should see the non entered information as blank or with a placeholder text")]
        public void ThenTheMemberShouldSeeTheNonEnteredInformationAsBlankOrWithAPlaceholderText()
        {
            var driver = GuiDriver.GetDriver();
            bool txtDescription = string.IsNullOrEmpty(driver.FindElementByAccessibilityId("tblDescription").Text) == true;
            bool txtDate = driver.FindElementByAccessibilityId("tblDate").Text == "Nepoznato";
            bool txtPages = string.IsNullOrEmpty(driver.FindElementByAccessibilityId("tblPageNum").Text) == true;
            Assert.IsTrue(txtDescription && txtDate && txtPages);
        }

        [When(@"the member chooses a book")]
        public void WhenTheMemberChoosesABook()
        {
            var driver = GuiDriver.GetDriver();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys("Tajanstveni");

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();
        }

        [When(@"the member clicks the Back button")]
        public void WhenTheMemberClicksTheBackButton()
        {
            var driver = GuiDriver.GetDriver();

            var btnBack = driver.FindElementByAccessibilityId("btnBack");
            btnBack.Click();
            
        }

        [Then(@"the member should be redirected back to the Search library catalogue screen")]
        public void ThenTheMemberShouldBeRedirectedBackToTheSearchLibraryCatalogueScreen()
        {
            var driver = GuiDriver.GetDriver();

            bool screen = driver.FindElementByName("Pretraživanje kataloga knjižnice") != null;

            Assert.IsTrue(screen);
        }
    }
}
