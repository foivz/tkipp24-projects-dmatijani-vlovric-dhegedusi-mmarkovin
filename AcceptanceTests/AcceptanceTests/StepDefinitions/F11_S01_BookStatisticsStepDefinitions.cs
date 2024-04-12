using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F11_S01_BookStatisticsStepDefinitions
    {
        [Given(@"the user is logged in as a library employee")]
        public void GivenTheUserIsLoggedInAsALibraryEmployee()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            var txtUsernameE = driver.FindElementByAccessibilityId("txtUsername");
            var txtPasswordE = driver.FindElementByAccessibilityId("txtPassword");

            txtUsernameE.SendKeys("pcindric89");
            txtPasswordE.SendKeys("cindricka123");

            var btnLoginE = driver.FindElementByAccessibilityId("btnLogin");
            btnLoginE.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.Manage().Window.Maximize();

            bool btnMembership = driver.FindElementByAccessibilityId("btnMembership") != null;
            Assert.IsTrue(btnMembership);
        }

        [Given(@"the user is on the Statistics form")]
        public void GivenTheUserIsOnTheStatisticsForm()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var btnStatistics = driver.FindElementByAccessibilityId("btnStatistics");
            btnStatistics.Click();
            bool isDataGrid = driver.FindElementByClassName("DataGrid") != null;
            Assert.IsTrue(isDataGrid);
        }

        [Given(@"there are records of at least one borrowed book in the system")]
        public void GivenThereAreRecordsOfAtLeastOneBorrowedBookInTheSystem()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            GuiDriver.Dispose();
            GivenTheUserIsLoggedInAsALibraryEmployee();
            driver = GuiDriver.GetOrCreateDriver();

            var btnBorrow = driver.FindElementByAccessibilityId("btnBorrow");
            btnBorrow.Click();

            AppiumWebElement dgAllBorrows = driver.FindElementByAccessibilityId("dgAllBorrows");
            IReadOnlyCollection<AppiumWebElement> rows = dgAllBorrows.FindElementsByClassName(("DataGridRow"));

            Assert.IsTrue(rows.Count > 1);

        }

        [When(@"the user selects the option Najposuđenije knjige from the criteria list")]
        public void WhenTheUserSelectsTheOptionNajposudenijeKnjigeFromTheCriteriaList()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            GuiDriver.Dispose();

            GivenTheUserIsLoggedInAsALibraryEmployee();
            GivenTheUserIsOnTheStatisticsForm();

            driver = GuiDriver.GetOrCreateDriver();

            var cboCategory = driver.FindElementByClassName("ComboBox");
            cboCategory.Click();

            var listItem = driver.FindElementByName("Najposuđenije knjige");
            listItem.Click();
        }

        [Then(@"the user should see details of the most borrowed books sorted from most to least borrowed")]
        public void ThenTheUserShouldSeeDetailsOfTheMostBorrowedBooksSortedFromMostToLeastBorrowed()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            AppiumWebElement dgMostPopularBooks = driver.FindElementByAccessibilityId("dgMostPopularBooks");
            IReadOnlyCollection<AppiumWebElement> rows = dgMostPopularBooks.FindElementsByClassName(("DataGridRow"));

            Assert.IsTrue(rows.Count > 1);
            GuiDriver.Dispose();
        }

        [Given(@"there are no records of borrowed books in the system")]
        public void GivenThereAreNoRecordsOfBorrowedBooksInTheSystem()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            GuiDriver.Dispose();
            GivenTheUserIsLoggedInAsALibraryEmployee();
            driver = GuiDriver.GetOrCreateDriver();

            var btnBorrow = driver.FindElementByAccessibilityId("btnBorrow");
            btnBorrow.Click();

            AppiumWebElement dgAllBorrows = driver.FindElementByAccessibilityId("dgAllBorrows");
            IReadOnlyCollection<AppiumWebElement> rows = dgAllBorrows.FindElementsByClassName(("DataGridRow"));

            Assert.IsTrue(rows.Count < 1);
        }

        [Then(@"the user should still see book details")]
        public void ThenTheUserShouldStillSeeBookDetails() {
            var driver = GuiDriver.GetOrCreateDriver();

            AppiumWebElement dgMostPopularBooks = driver.FindElementByAccessibilityId("dgMostPopularBooks");
            IReadOnlyCollection<AppiumWebElement> rows = dgMostPopularBooks.FindElementsByClassName(("DataGridRow"));

            Assert.IsTrue(rows.Count > 1);
        }


        [Then(@"the borrow count on those books will be 0")]
        public void ThenTheBorrowCountOnThoseBooksWillBe() {

            var driver = GuiDriver.GetOrCreateDriver();

            AppiumWebElement dgMostPopularBooks = driver.FindElementByAccessibilityId("dgMostPopularBooks");

            var rows = dgMostPopularBooks.FindElementsByClassName("DataGridRow");

            foreach (var row in rows) {
                var cells = row.FindElementsByClassName("DataGridCell");

                var timesBorrowed = cells[2].Text;
                Assert.IsTrue(timesBorrowed == "0");
            }
            GuiDriver.Dispose();
        }

    }
}
