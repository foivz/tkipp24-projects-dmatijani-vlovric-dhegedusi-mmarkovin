using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Appium.Windows;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F10_S01_ReviewingAllReviewsStepDefinitions
    {
        [Given(@"the user has selected a book and is on the book details page")]
        public void GivenTheUserHasSelectedABookAndIsOnTheBookDetailsPage()
        {
            var driver = GuiDriver.GetDriver();
            bool isOpened = driver.FindElementByAccessibilityId("btnAddReview") != null;

            Assert.IsTrue(isOpened);
        }

        [When(@"the user presses the ""([^""]*)"" button")]
        public void WhenTheUserPressesTheButton(string recenzije)
        {
            var driver = GuiDriver.GetDriver();
            var reviewButton = driver.FindElementByAccessibilityId("btnAddReview");

            reviewButton.Click();
        }

        [Then(@"the user should see a form with all written reviews for the selected book")]
        public void ThenTheUserShouldSeeAFormWithAllWrittenReviewsForTheSelectedBook()
        {
            var driver = GuiDriver.GetDriver();
            bool isOpened = driver.FindElementByAccessibilityId("ucReviewsList") != null;

            Assert.IsTrue(isOpened);

        }
    }
}
