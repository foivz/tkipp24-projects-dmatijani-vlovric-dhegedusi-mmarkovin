using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Appium.Windows;
using System.Security.Cryptography.X509Certificates;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F10_S01_ReviewingAllReviewsStepDefinitions
    {
        [Given(@"the user has selected a book and is on the book details page")]
        public void GivenTheUserHasSelectedABookAndIsOnTheBookDetailsPage()
        {
            var driver = GuiDriver.GetOrCreateDriver();

                var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
                var txtPassword = driver.FindElementByAccessibilityId("txtPassword");

                txtUsername.SendKeys("mpranjic23");
                txtPassword.SendKeys("pranjicka98");


                var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
                btnLogin.Click();
                driver.SwitchTo().Window(driver.WindowHandles.First());

                driver.Manage().Window.Maximize();
                var btnSearch = driver.FindElementByAccessibilityId("btnSearch");
                btnSearch.Click();


                var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
                txtSearch.SendKeys("Haml");


                var dgvBookSearch = driver.FindElementByAccessibilityId("dgvBookSearch");
                var cellsInFirstRow = dgvBookSearch.FindElementsByClassName("DataGridCell");
                cellsInFirstRow[0].Click();

                var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
                btnDetails.Click();



            bool btnAddReview = driver.FindElementByAccessibilityId("btnAddReview") != null;
            Assert.IsTrue(btnAddReview);
        }

        [When(@"the user presses the Recenzije button")]
        public void WhenTheUserPressesTheButton()
        {
            var driver = GuiDriver.GetDriver();
            var reviewButton = driver.FindElementByAccessibilityId("btnAddReview");

            reviewButton.Click();
        }

        [Then(@"the user should see a form with all written reviews for the selected book")]
        public void ThenTheUserShouldSeeAFormWithAllWrittenReviewsForTheSelectedBook()
        {
            var driver = GuiDriver.GetDriver();
            bool isOpened = driver.FindElementByAccessibilityId("dgReviews") != null;

            Assert.IsTrue(isOpened);

            GuiDriver.Dispose();

        }
    }
}
