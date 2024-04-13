using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F05_S01_BorrowedBooksVisibleStepDefinitions
    {
        [Given(@"an employee from a library which has borrows is logged in")]
        public void GivenAnEmployeeFromALibraryWhichHasBorrowsIsLoggedIn()
        {
            if (GuiDriver.GetDriver() != null) GuiDriver.Dispose();
            var driver = GuiDriver.GetOrCreateDriver();
            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");

            txtUsername.SendKeys("pcindric89");
            txtPassword.SendKeys("cindricka123");

            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            Assert.IsNotNull(btnLogin);

            btnLogin.Click();
        }

        [When(@"the user clicks the Borrows button")]
        public void WhenTheUserClicksTheBorrowsButton()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnBorrows = driver.FindElementByAccessibilityId("btnBorrow");
            btnBorrows.Click();
        }

        [Then(@"the user should know that the system was loading the borrows")]
        public void ThenTheUserShouldKnowThatTheSystemWasLoadingTheBorrows()
        {
            var driver = GuiDriver.GetDriver();
            var loader = driver.FindElementByAccessibilityId("Loading");
            Assert.IsNotNull(loader);
        }

        [Then(@"the user should see all borrowed books for his library")]
        public void ThenTheUserShouldSeeAllBorrowedBooksForHisLibrary()
        {
            var driver = GuiDriver.GetDriver();
            var dgAllBorrows = driver.FindElementByAccessibilityId("dgAllBorrows");
            Assert.IsNotNull(dgAllBorrows);
            var firstBorrow = driver.FindElementByName("18935995 - Hamlet");
            Assert.IsNotNull(firstBorrow);
            GuiDriver.Dispose();
        }

        [Given(@"an employee from a library which has NO borrows is logged in")]
        public void GivenAnEmployeeFromALibraryWhichHasNOBorrowsIsLoggedIn()
        {
            if (GuiDriver.GetDriver() != null) GuiDriver.Dispose();
            var driver = GuiDriver.GetOrCreateDriver();
            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");

            txtUsername.SendKeys("svavic");
            txtPassword.SendKeys("skakavac");

            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            Assert.IsNotNull(btnLogin);

            btnLogin.Click();
        }

        [Then(@"the user should be notified that there are no borrows")]
        public void ThenTheUserShouldBeNotifiedThatThereAreNoBorrows()
        {
            var driver = GuiDriver.GetDriver();
            var noBorrowsMessage = driver.FindElementByAccessibilityId("noBorrowsMessage");
            Assert.IsNotNull(noBorrowsMessage);
            GuiDriver.Dispose();
        }
    }
}
