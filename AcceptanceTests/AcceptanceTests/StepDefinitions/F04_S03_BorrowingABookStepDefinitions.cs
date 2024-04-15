using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F04_S03_BorrowingABookStepDefinitions
    {
        [Given(@"a member made a new borrow")]
        public void GivenAMemberMadeANewBorrow()
        {
            if (GuiDriver.GetDriver() != null) GuiDriver.Dispose();
            var driver = GuiDriver.GetOrCreateDriver();
            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");

            txtUsername.SendKeys("mpranjic23");
            txtPassword.SendKeys("pranjicka98");

            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            Assert.IsNotNull(btnLogin);

            btnLogin.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());

            driver.FindElementByName("Pretraži knjige").Click();
            driver.FindElementByAccessibilityId("txtSearch").SendKeys("Ham");
            driver.FindElementByName("Hamlet").Click();
            driver.FindElementByAccessibilityId("btnDetails").Click();
            driver.FindElementByName("Maximise").Click();
            driver.FindElementByName("Posudi").Click();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.FindElementByName("OK").Click();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.FindElementByName("Odjava").Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
        }

        [When(@"the user clicks the Pending borrows tab")]
        public void WhenTheUserClicksThePendingBorrowsTab()
        {
            var driver = GuiDriver.GetDriver();
            var pendingBorrowsTab = driver.FindElementByName("Na čekanju");
            pendingBorrowsTab.Click();
        }

        [When(@"the user clicks on a Pending borrow")]
        public void WhenTheUserClicksOnAPendingBorrow()
        {
            var driver = GuiDriver.GetDriver();
            var pendingBorrow = driver.FindElementByName("18935995 - Hamlet");
            pendingBorrow.Click();
        }

        [When(@"the user clicks the Borrow a new book button")]
        public void WhenTheUserClicksTheBorrowANewBookButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnBorrowANewBook = driver.FindElementByAccessibilityId("btnBorrowBook");
            btnBorrowANewBook.Click();
        }

        [When(@"the user enters the correct borrow duration")]
        public void WhenTheUserEntersTheCorrectBorrowDuration()
        {
            var driver = GuiDriver.GetDriver();
            var tbBorrowDuration = driver.FindElementByAccessibilityId("tbBorrowDuration");
            tbBorrowDuration.SendKeys("30");
        }

        [When(@"the user clicks the Borrow this book button")]
        public void WhenTheUserClicksTheBorrowThisBookButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnBorrowThisBook = driver.FindElementByAccessibilityId("btnAddNewBorrow");
            btnBorrowThisBook.Click();
        }

        [Then(@"the borrow should change it's status to borrowed")]
        public void ThenTheBorrowShouldChangeItsStatusToBorrowed()
        {
            CheckIfBorrowExists("18935995 - Hamlet");
        }

        [When(@"the user enters the member barcode (.*)")]
        public void WhenTheUserEntersTheMemberBarcode(string p0)
        {
            var driver = GuiDriver.GetDriver();
            var tbMemberBarcode = driver.FindElementByAccessibilityId("tbMemberBarcode");
            tbMemberBarcode.SendKeys(p0);
        }

        [When(@"the user doesn't enter the book barcode")]
        public void WhenTheUserDoesntEnterTheBookBarcode()
        {
            // Does nothing
        }

        [Then(@"the system should show an error message that the borrow can't be made")]
        public void ThenTheSystemShouldShowAnErrorMessageThatTheBorrowCantBeMade()
        {
            MessageBoxTestHelper.CheckIfMessageBoxIsShown();
        }

        [When(@"the user enters the book barcode (.*)")]
        public void WhenTheUserEntersTheBookBarcode(string p0)
        {
            var driver = GuiDriver.GetDriver();
            var tbBookBarcode = driver.FindElementByAccessibilityId("tbBookBarcode");
            tbBookBarcode.SendKeys(p0);
        }

        [When(@"the user doesn't enter the member barcode")]
        public void WhenTheUserDoesntEnterTheMemberBarcode()
        {
            // Does nothing
        }

        [When(@"the user doesn't enter the borrow duration")]
        public void WhenTheUserDoesntEnterTheBorrowDuration()
        {
            // Does nothing
        }

        [Then(@"a new book with the status borrowed should appear")]
        public void ThenANewBookWithTheStatusBorrowedShouldAppear()
        {
            CheckIfBorrowExists("65625036 - Prolaz kroz Vrijeme");
        }

        private void CheckIfBorrowExists(string borrowName) {
            var driver = GuiDriver.GetDriver();
            var currentBorrowsTab = driver.FindElementByName("Trenutno posuđene");
            currentBorrowsTab.Click();
            var existingNewBorrow = driver.FindElementByName(borrowName);
            Assert.IsNotNull(existingNewBorrow);
        }
    }
}
