using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F10_S02_WritingReviewsStepDefinitions
    {

        [Given(@"the user has previously borrowed the book through the system")]
        public void GivenTheUserHasPreviouslyBorrowedTheBookThroughTheSystem() {

            var driver = GuiDriver.GetOrCreateDriver();

            var txtUsernameE = driver.FindElementByAccessibilityId("txtUsername");
            var txtPasswordE = driver.FindElementByAccessibilityId("txtPassword");

            txtUsernameE.SendKeys("mpranjic23");
            txtPasswordE.SendKeys("pranjicka98");

            var btnLoginE = driver.FindElementByAccessibilityId("btnLogin");
            btnLoginE.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.Manage().Window.Maximize();

            var btnBorrowE = driver.FindElementByAccessibilityId("btnBorrow");
            btnBorrowE.Click();

            AppiumWebElement dgAllBorrows = driver.FindElementByAccessibilityId("dgAllBorrows");
            ReadOnlyCollection<AppiumWebElement> cells = dgAllBorrows.FindElementsByClassName("DataGridCell");

            string bookName = "Hamlet";
            bool bookFound = false;

            foreach (AppiumWebElement cell in cells) {
                if (cell.Text.Contains(bookName)) {
                    bookFound = true;
                    break;
                }
            }
            Assert.IsTrue(bookFound);
            GuiDriver.Dispose();

        }

        [Given(@"the user is on All Reviews form")]
        public void GivenTheUserIsOnAllReviewsForm() {
            var driver = GuiDriver.GetOrCreateDriver();
            GuiDriver.Dispose();
            driver = GuiDriver.GetOrCreateDriver();



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

            var btnReviews = driver.FindElementByAccessibilityId("btnAddReview");
            btnReviews.Click();

        }

        [Given(@"the user has previously written a review for the selected book")]
        public void GivenTheUserHasPreviouslyWrittenAReviewForTheSelectedBook()
        {

            GivenTheUserIsOnAllReviewsForm();

            var driver = GuiDriver.GetOrCreateDriver();
            var dgReviews = driver.FindElementByAccessibilityId("dgReviews");
            var rows = dgReviews.FindElementsByClassName("DataGridRow");

            bool hasUserWrittenReview = false;


            foreach (var row in rows) {
                var cells = row.FindElementsByClassName("DataGridCell");

                foreach (var cell in cells) {
                    string cellText = cell.Text.Trim();

                    if (cellText == "Marija Pranjic") {
                        hasUserWrittenReview = true;
                        break;
                    }
                }
            }
            Assert.IsTrue(hasUserWrittenReview);

        }


        [When(@"the user selects the Add Review option")]
        public void WhenTheUserSelectsTheAddReviewOption()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            GuiDriver.Dispose();
            GivenTheUserIsOnAllReviewsForm();

            driver = GuiDriver.GetOrCreateDriver();
            var btnAddReview = driver.FindElementByAccessibilityId("btnAddReview");
            btnAddReview.Click();
        }

        [Then(@"the application remains on the All Reviews form")]
        public void ThenTheApplicationRemainsOnTheAllReviewsForm()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var isBtnRemoveReview = driver.FindElementByAccessibilityId("btnRemoveReview") != null;
            Assert.IsTrue(isBtnRemoveReview);
        }

        [Then(@"the application displays an error message Već si napisao recenziju za ovu knjigu!")]
        public void ThenTheApplicationDisplaysAnErrorMessageVecSiNapisaoRecenzijuZaOvuKnjigu()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool isBtnOk = driver.FindElementByAccessibilityId("2") != null;
            Assert.IsTrue(isBtnOk);
            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            GuiDriver.Dispose();

        }


        [Given(@"the user has not written a review for the selected book")]
        public void GivenTheUserHasNotWrittenAReviewForTheSelectedBook()
        {

            var driver = GuiDriver.GetOrCreateDriver();
            GuiDriver.Dispose();
            GivenTheUserIsOnAllReviewsForm();

            driver = GuiDriver.GetOrCreateDriver();
            var dgReviews = driver.FindElementByAccessibilityId("dgReviews");
            var rows = dgReviews.FindElementsByClassName("DataGridRow");

            bool hasUserWrittenReview = false;


            foreach (var row in rows) {
                var cells = row.FindElementsByClassName("DataGridCell");

                foreach (var cell in cells) {
                    string cellText = cell.Text.Trim();

                    if (cellText == "Marija Pranjic") {
                        hasUserWrittenReview = true;
                        break;
                    }
                }
            }



            Assert.IsTrue(!hasUserWrittenReview);
        }

        [Given(@"the user is on the Add Review form")]
        public void GivenTheUserIsOnTheAddReviewForm()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            GuiDriver.Dispose();

            GivenTheUserIsOnAllReviewsForm();
            driver = GuiDriver.GetOrCreateDriver();

            var btnReviews = driver.FindElementByAccessibilityId("btnAddReview");
            btnReviews.Click();

        }

        [When(@"the user selects a rating for the review")]
        public void WhenTheUserSelectsARatingForTheReview()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var cboRating = driver.FindElementByAccessibilityId("cboRating");
            cboRating.Click();

            var listItem = driver.FindElementByName("4");
            listItem.Click();
        }

        [When(@"the user enters an optional comment")]
        public void WhenTheUserEntersAnOptionalComment()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            var txtComment = driver.FindElementByAccessibilityId("txtComment");

            txtComment.SendKeys("Knjiga nije loša");
        }

        [When(@"the user clicks the Dodaj button")]
        public void WhenTheUserClicksTheDodajButton()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            var btnAddNewReview = driver.FindElementByAccessibilityId("btnAddReview");
            btnAddNewReview.Click();
        }

        [Then(@"the user is shown All Reviews form where he can also see his review")]
        public void ThenTheUserIsShownAllReviewsFormWhereHeCanAlsoSeeHisReview()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool isDgReviewsList = driver.FindElementByAccessibilityId("dgReviews") != null;
            var dgReviews = driver.FindElementByAccessibilityId("dgReviews");
            var rows = dgReviews.FindElementsByClassName("DataGridRow");

            bool hasUserWrittenReview = false;


            foreach (var row in rows) {
                var cells = row.FindElementsByClassName("DataGridCell");

                foreach (var cell in cells) {
                    string cellText = cell.Text.Trim();

                    if (cellText == "Marija Pranjic") {
                        hasUserWrittenReview = true;
                        break;
                    }
                }
            }

            Assert.IsTrue(isDgReviewsList && hasUserWrittenReview);
            GuiDriver.Dispose();
        }

        [Then(@"the user is shown the All Reviews form where he can also see his review")]
        public void ThenTheUserIsShownTheAllReviewsFormWhereHeCanAlsoSeeHisReview()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool isDgReviewsList = driver.FindElementByAccessibilityId("dgReviews") != null;
            var dgReviews = driver.FindElementByAccessibilityId("dgReviews");
            var rows = dgReviews.FindElementsByClassName("DataGridRow");

            bool hasUserWrittenReview = false;


            foreach (var row in rows) {
                var cells = row.FindElementsByClassName("DataGridCell");

                foreach (var cell in cells) {
                    string cellText = cell.Text.Trim();

                    if (cellText == "Marija Pranjic") {
                        hasUserWrittenReview = true;
                        break;
                    }
                }
            }

            Assert.IsTrue(isDgReviewsList && hasUserWrittenReview);
            GuiDriver.Dispose();
        }

        [When(@"the user selects a rating")]
        public void WhenTheUserSelectsARating()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var cboRating = driver.FindElementByAccessibilityId("cboRating");
            cboRating.Click();

            var listItem = driver.FindElementByName("4");
            listItem.Click();

        }

        [When(@"the user clicks the Odustani button")]
        public void WhenTheUserClicksTheOdustaniButton()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var btnCancel = driver.FindElementByAccessibilityId("btnCancel");
            btnCancel.Click();
        }

        [Then(@"the user is taken back to the All Reviews form")]
        public void ThenTheUserIsTakenBackToTheAllReviewsForm()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool isDgReviewsList = driver.FindElementByAccessibilityId("dgReviews") != null;

            Assert.IsTrue(isDgReviewsList);
            GuiDriver.Dispose();
        }

        [When(@"the user presses the Cancel button")]
        public void WhenTheUserPressesTheCancelButton()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var btnCancel = driver.FindElementByAccessibilityId("btnCancel");
            btnCancel.Click();
        }

        [When(@"the user presses the Dodaj button without selecting a rating")]
        public void WhenTheUserPressesTheDodajButtonWithoutSelectingARating()
        {
            GivenTheUserIsOnTheAddReviewForm();
            var driver = GuiDriver.GetOrCreateDriver();
            var btnAddReview = driver.FindElementByAccessibilityId("btnAddReview");
            btnAddReview.Click();
        }


        [Then(@"the application remains on Add Review form")]
        public void ThenTheApplicationRemainsOnAddReviewForm()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool cboRating = driver.FindElementByAccessibilityId("cboRating") != null;
            Assert.IsTrue(cboRating);
        }

        [Then(@"an error window appears with the message Moraš dodati ocjenu prije objavljivanja recenzije!")]
        public void ThenAnErrorWindowAppearsWithTheMessageMorasDodatiOcjenuPrijeObjavljivanjaRecenzije()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool isBtnOk = driver.FindElementByAccessibilityId("2") != null;
            Assert.IsTrue(isBtnOk);
            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            GuiDriver.Dispose();
        }

        [Given(@"the user has never borrowed the book through the system")]
        public void GivenTheUserHasNeverBorrowedTheBookThroughTheSystem()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var txtUsernameE = driver.FindElementByAccessibilityId("txtUsername");
            var txtPasswordE = driver.FindElementByAccessibilityId("txtPassword");

            txtUsernameE.SendKeys("mpranjic23");
            txtPasswordE.SendKeys("pranjicka98");

            var btnLoginE = driver.FindElementByAccessibilityId("btnLogin");
            btnLoginE.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.Manage().Window.Maximize();

            var btnBorrowE = driver.FindElementByAccessibilityId("btnBorrow");
            btnBorrowE.Click();

            AppiumWebElement dgAllBorrows = driver.FindElementByAccessibilityId("dgAllBorrows");
            ReadOnlyCollection<AppiumWebElement> cells = dgAllBorrows.FindElementsByClassName("DataGridCell");

            string bookName = "Romeo";
            bool bookFound = false;

            foreach (AppiumWebElement cell in cells) {
                if (cell.Text.Contains(bookName)) {
                    bookFound = true;
                    break;
                }
            }
            Assert.IsTrue(!bookFound);
        }

        [Then(@"the application does not open a form for writing a new review")]
        public void ThenTheApplicationDoesNotOpenAFormForWritingANewReview()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            GuiDriver.Dispose();
            driver = GuiDriver.GetOrCreateDriver();



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
            txtSearch.SendKeys("Romeo");


            var dgvBookSearch = driver.FindElementByAccessibilityId("dgvBookSearch");
            var cellsInFirstRow = dgvBookSearch.FindElementsByClassName("DataGridCell");
            cellsInFirstRow[0].Click();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            var btnReviews = driver.FindElementByAccessibilityId("btnAddReview");
            btnReviews.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());

            bool dgReviews = driver.FindElementByAccessibilityId("dgReviews") != null;
            Assert.IsTrue(dgReviews);

        }

        [Then(@"an error window appears with the message Moraš posuditi knjigu prije pisanja recenzije!")]
        public void ThenAnErrorWindowAppearsWithTheMessageMorasPosuditiKnjiguPrijePisanjaRecenzije()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            var btnReviews = driver.FindElementByAccessibilityId("btnAddReview");
            btnReviews.Click();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool isBtnOk = driver.FindElementByAccessibilityId("2") != null;
            Assert.IsTrue(isBtnOk);
            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            GuiDriver.Dispose();
        }

        [Given(@"the user is on All Reviews form for the selected book")]
        public void GivenTheUserIsOnAllReviewsFormForTheSelectedBook() {
            var driver = GuiDriver.GetOrCreateDriver();
            GuiDriver.Dispose();
            driver = GuiDriver.GetOrCreateDriver();



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
            txtSearch.SendKeys("Romeo");


            var dgvBookSearch = driver.FindElementByAccessibilityId("dgvBookSearch");
            var cellsInFirstRow = dgvBookSearch.FindElementsByClassName("DataGridCell");
            cellsInFirstRow[0].Click();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            var btnReviews = driver.FindElementByAccessibilityId("btnAddReview");
            btnReviews.Click();
        }

    }
}
