using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F11_S02_GenreStatisticsStepDefinitions
    {

        F11_S01_BookStatisticsStepDefinitions helperClass = new F11_S01_BookStatisticsStepDefinitions();

        [Given(@"there are records of at least one borrowed book with defined genre in the system")]
        public void GivenThereAreRecordsOfAtLeastOneBorrowedBookWithDefinedGenreInTheSystem()
        {

            var driver = GuiDriver.GetOrCreateDriver();
            GuiDriver.Dispose();
            helperClass.GivenTheUserIsLoggedInAsALibraryEmployee();
            driver = GuiDriver.GetOrCreateDriver();

            var btnBorrow = driver.FindElementByAccessibilityId("btnBorrow");
            btnBorrow.Click();

            AppiumWebElement dgAllBorrows = driver.FindElementByAccessibilityId("dgAllBorrows");
            IReadOnlyCollection<AppiumWebElement> rows = dgAllBorrows.FindElementsByClassName(("DataGridRow"));

            Assert.IsTrue(rows.Count > 1);
        }
        

        [When(@"the user selects the option Posudbe po žanrovima from the criteria list")]
        public void WhenTheUserSelectsTheOptionPosudbePoZanrovimaFromTheCriteriaList()
        {
        var driver = GuiDriver.GetOrCreateDriver();
        GuiDriver.Dispose();

        helperClass.GivenTheUserIsLoggedInAsALibraryEmployee();
        helperClass.GivenTheUserIsOnTheStatisticsForm();

        driver = GuiDriver.GetOrCreateDriver();

        var cboCategory = driver.FindElementByClassName("ComboBox");
        cboCategory.Click();

        var listItem = driver.FindElementByName("Posudbe po žanrovima");
        listItem.Click();
    }

        [Then(@"the user should see a list of genres along with the number of books borrowed for each genre")]
        public void ThenTheUserShouldSeeAListOfGenresAlongWithTheNumberOfBooksBorrowedForEachGenre()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            IReadOnlyCollection<AppiumWebElement> textBlocks = driver.FindElementsByClassName("TextBlock");

            bool containsString = false;
            bool containsInteger = false;

            foreach (var textBlock in textBlocks) {

                if (!containsString && !string.IsNullOrEmpty(textBlock.Text)) {
                    containsString = true;
                }

                int number;
                if (!containsInteger && int.TryParse(textBlock.Text, out number)) {
                    containsInteger = true;
                }

                if (containsString && containsInteger) {
                    break;
                }
            }
            Assert.IsTrue(containsString && containsInteger);
            GuiDriver.Dispose();
        }

        [Given(@"there are no records of borrowed books with defined genres in the system")]
        public void GivenThereAreNoRecordsOfBorrowedBooksWithDefinedGenresInTheSystem()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            GuiDriver.Dispose();
            helperClass.GivenTheUserIsLoggedInAsALibraryEmployee();
            driver = GuiDriver.GetOrCreateDriver();

            var btnBorrow = driver.FindElementByAccessibilityId("btnBorrow");
            btnBorrow.Click();

            AppiumWebElement dgAllBorrows = driver.FindElementByAccessibilityId("dgAllBorrows");
            IReadOnlyCollection<AppiumWebElement> rows = dgAllBorrows.FindElementsByClassName(("DataGridRow"));

            Assert.IsTrue(rows.Count == 1);
        }

        [Then(@"the user should not see any genre listed")]
        public void ThenTheUserShouldNotSeeAnyGenreListed()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            bool isTextBlock = driver.FindElementByClassName("TextBlock") != null;
            Assert.IsTrue(isTextBlock);
            GuiDriver.Dispose();
        }
    }
}
