using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F03_S04_ArchiveStepDefinitions
    {
        [Given(@"the user is on the Archive screen")]
        public void GivenTheUserIsOnTheArchiveScreen()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnCatalogue = driver.FindElementByAccessibilityId("btnBookCatalog");
            btnCatalogue.Click();

            var btnNewBook = driver.FindElementByAccessibilityId("btnArchiveList");
            btnNewBook.Click();
        }

        [Then(@"the employee should see a list of archived books")]
        public void ThenTheEmployeeShouldSeeAListOfArchivedBooks()
        {
            var driver = GuiDriver.GetDriver();

            bool book = driver.FindElementByName("ImeSve 1") != null;
            Assert.IsTrue(book);
        }

        [When(@"the employee clicks on the Back button from the archive")]
        public void WhenTheEmployeeClicksOnTheBackButtonFromTheArchive()
        {
            var driver = GuiDriver.GetDriver();

            var btnBack = driver.FindElementByAccessibilityId("btnBack");
            btnBack.Click();
        }

    }
}
