using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F05_S01_ReturningABorrowedBookStepDefinitions
    {
        [When(@"the user clicks the Current borrows tab")]
        public void WhenTheUserClicksTheCurrentBorrowsTab()
        {
            var driver = GuiDriver.GetDriver();
            var currentBorrowsTab = driver.FindElementByName("Trenutno posuđene");
            currentBorrowsTab.Click();
        }

        [When(@"the user clicks the borrow for the book (.*)")]
        public void WhenTheUserClicksTheBorrowForTheBook(string p0)
        {
            var driver = GuiDriver.GetDriver();
            var borrow = driver.FindElementByName(p0);
            borrow.Click();
        }

        [When(@"the user clicks the Return book button")]
        public void WhenTheUserClicksTheReturnBookButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnReturn = driver.FindElementByAccessibilityId("btnReturnBook");
            btnReturn.Click();
        }

        [When(@"the user clicks the Check borrow button")]
        public void WhenTheUserClicksTheCheckBorrowButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnCheck = driver.FindElementByAccessibilityId("btnCheckBorrow");
            btnCheck.Click();
        }

        [When(@"the user clicks the Return this book button")]
        public void WhenTheUserClicksTheReturnThisBookButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnReturn = driver.FindElementByAccessibilityId("btnReturnBorrow");
            btnReturn.Click();
        }

        [Then(@"the user should see the book (.*) in the Returned borrows tab")]
        public void ThenTheUserShouldSeeTheBookInTheReturnedBorrowsTab(string p0)
        {
            var driver = GuiDriver.GetDriver();
            var returnedBorrowsTab = driver.FindElementByName("Prethodne");
            returnedBorrowsTab.Click();
            var borrow = driver.FindElementByName(p0);
            Assert.IsNotNull(borrow);
        }

        [Then(@"the system should show an error that the book can't be returned")]
        public void ThenTheSystemShouldShowAnErrorThatTheBookCantBeReturned()
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

        [Then(@"the system should show a message that there isn't a current borrow")]
        public void ThenTheSystemShouldShowAMessageThatThereIsntACurrentBorrow()
        {
            var driver = GuiDriver.GetDriver();
            var errorMessage = driver.FindElementByName("Ne postoji aktualna posudba!");
            Assert.IsNotNull(errorMessage);
        }
    }
}
