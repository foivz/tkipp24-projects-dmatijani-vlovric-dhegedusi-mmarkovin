using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F04_S02_BorrowsSortedByStatusStepDefinitions
    {
        [Then(@"the user should see the borrows sorted into different categories")]
        public void ThenTheUserShouldSeeTheBorrowsSortedIntoDifferentCategories()
        {
            var driver = GuiDriver.GetDriver();

            var allBorrowsElements = driver.FindElementsByName("Sve posudbe");
            foreach (var el in allBorrowsElements) {
                el.Click();
            }
            var dgAllBorrows = driver.FindElementByAccessibilityId("dgAllBorrows");
            Assert.IsNotNull(dgAllBorrows);

            var pendingBorrowsTab = driver.FindElementByName("Na čekanju");
            pendingBorrowsTab.Click();
            var dgPendingBorrows = driver.FindElementByAccessibilityId("dgPendingBorrows");
            Assert.IsNotNull(dgPendingBorrows);

            var currentBorrowsTab = driver.FindElementByName("Trenutno posuđene");
            currentBorrowsTab.Click();
            var dgCurrentBorrows = driver.FindElementByAccessibilityId("dgCurrentBorrows");
            Assert.IsNotNull(dgCurrentBorrows);

            var lateBorrowsTab = driver.FindElementByName("Kasne");
            lateBorrowsTab.Click();
            var dgLateBorrows = driver.FindElementByAccessibilityId("dgLateBorrows");
            Assert.IsNotNull(dgLateBorrows);

            var returnedBorrowsTab = driver.FindElementByName("Prethodne");
            returnedBorrowsTab.Click();
            var dgReturnedBorrows = driver.FindElementByAccessibilityId("dgDoneBorrows");
            Assert.IsNotNull(dgReturnedBorrows);
        }
    }
}
