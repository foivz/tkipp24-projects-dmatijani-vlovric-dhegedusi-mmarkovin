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
/*            var driver = GuiDriver.GetOrCreateDriver();

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

            var btnBorrow = driver.FindElementByName("Posudi");
            btnBorrow.Click();

            bool HasUserBorrowedBook = true;
            Assert.IsTrue(HasUserBorrowedBook);

            driver.SwitchTo().Window(driver.WindowHandles.First());
            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            GuiDriver.Dispose(); */

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


            /*           var rows = dgAllBorrows.FindElementsByClassName("DataGridRow");
                        int count = rows.Count - 2;
                        var lastRow = rows[count];
                        var cells = lastRow.FindElements(By.ClassName("DataGridCell"));
                        cells[0].Click();

                        var btnBorrowBook = driver.FindElementByAccessibilityId("btnBorrowBook");
                        btnBorrowBook.Click();

                        var tbBorrowDuration = driver.FindElementByAccessibilityId("tbBorrowDuration");
                        tbBorrowDuration.SendKeys("30");

                        var btnAddNewBorrow = driver.FindElementByAccessibilityId("btnAddNewBorrow");
                        btnAddNewBorrow.Click(); */

        }

        [Given(@"the user is on All Reviews form")]
        public void GivenTheUserIsOnAllReviewsForm() {
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

            var btnReviews = driver.FindElementByAccessibilityId("btnAddReview");
            btnReviews.Click();

        }

        [Given(@"the user has previously written a review for the selected book")]
        public void GivenTheUserHasPreviouslyWrittenAReviewForTheSelectedBook()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var btnAddReview = driver.FindElementByAccessibilityId("btnAddReview");
            btnAddReview.Click();

            var cboRating = driver.FindElementByAccessibilityId("cboRating");
            var selectElement = new SelectElement(cboRating);
            selectElement.SelectByIndex(0);

            var btnNewReview = driver.FindElementByAccessibilityId("btnAddReview");
            btnAddReview.Click();

           var dgReviews = driver.FindElementByAccessibilityId("dgReviews");
            var rows = dgReviews.FindElementsByClassName("DataGridRow");

            bool hasUserWrittenReview = false;


            foreach (var row in rows) {
                var cells = row.FindElements(By.TagName("DataGridCell"));

                var usernameCell = cells[0];

                string username = usernameCell.Text;

                if (username == "Marija Pranjic") {
                    hasUserWrittenReview = true;
                    break;
                }
            }
            Assert.IsTrue(hasUserWrittenReview);

        }


        [When(@"the user selects the Add Review option")]
        public void WhenTheUserSelectsTheAddReviewOption()
        {
            var driver = GuiDriver.GetOrCreateDriver();

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
            var driver = GuiDriver.GetOrCreateDriver();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            var isWindowOpened = driver.FindElementByAccessibilityId("2") != null;
            Assert.IsTrue(isWindowOpened);

        }


        [Given(@"the user has not written a review for the selected book")]
        public void GivenTheUserHasNotWrittenAReviewForTheSelectedBook()
        {
            throw new PendingStepException();
        }

        [Given(@"the user is on the Add Review form")]
        public void GivenTheUserIsOnTheAddReviewForm()
        {
            throw new PendingStepException();
        }

        [When(@"the user selects a rating for the review")]
        public void WhenTheUserSelectsARatingForTheReview()
        {
            throw new PendingStepException();
        }

        [When(@"the user enters an optional comment")]
        public void WhenTheUserEntersAnOptionalComment()
        {
            throw new PendingStepException();
        }

        [When(@"the user clicks the Dodaj button")]
        public void WhenTheUserClicksTheDodajButton()
        {
            throw new PendingStepException();
        }

        [Then(@"the review is stored in the database")]
        public void ThenTheReviewIsStoredInTheDatabase()
        {
            throw new PendingStepException();
        }

        [Then(@"the user is shown All Reviews form where he can also see his review")]
        public void ThenTheUserIsShownAllReviewsFormWhereHeCanAlsoSeeHisReview()
        {
            throw new PendingStepException();
        }

        [When(@"the user clicks the Add button")]
        public void WhenTheUserClicksTheAddButton()
        {
            throw new PendingStepException();
        }

        [When(@"the user doesn't fill the comment field")]
        public void WhenTheUserDoesntFillTheCommentField()
        {
            throw new PendingStepException();
        }

        [Then(@"the user is shown the All Reviews form where he can also see his review")]
        public void ThenTheUserIsShownTheAllReviewsFormWhereHeCanAlsoSeeHisReview()
        {
            throw new PendingStepException();
        }

        [When(@"the user selects a rating")]
        public void WhenTheUserSelectsARating()
        {
            throw new PendingStepException();
        }

        [When(@"the user clicks the Odustani button")]
        public void WhenTheUserClicksTheOdustaniButton()
        {
            throw new PendingStepException();
        }

        [Then(@"the review is not stored in the database")]
        public void ThenTheReviewIsNotStoredInTheDatabase()
        {
            throw new PendingStepException();
        }

        [Then(@"the user is taken back to the All Reviews form")]
        public void ThenTheUserIsTakenBackToTheAllReviewsForm()
        {
            throw new PendingStepException();
        }

        [When(@"the user presses the Cancel button")]
        public void WhenTheUserPressesTheCancelButton()
        {
            throw new PendingStepException();
        }

        [When(@"the user presses the Dodaj button without selecting a rating")]
        public void WhenTheUserPressesTheDodajButtonWithoutSelectingARating()
        {
            throw new PendingStepException();
        }

        [Then(@"the application does not add the review to the database")]
        public void ThenTheApplicationDoesNotAddTheReviewToTheDatabase()
        {
            throw new PendingStepException();
        }

        [Then(@"the application remains on Add Review form")]
        public void ThenTheApplicationRemainsOnAddReviewForm()
        {
            throw new PendingStepException();
        }

        [Then(@"an error window appears with the message Moraš dodati ocjenu prije objavljivanja recenzije!")]
        public void ThenAnErrorWindowAppearsWithTheMessageMorasDodatiOcjenuPrijeObjavljivanjaRecenzije()
        {
            throw new PendingStepException();
        }

        [Given(@"the user has never borrowed the book through the system")]
        public void GivenTheUserHasNeverBorrowedTheBookThroughTheSystem()
        {
            throw new PendingStepException();
        }

        [Then(@"the application does not open a form for writing a new review")]
        public void ThenTheApplicationDoesNotOpenAFormForWritingANewReview()
        {
            throw new PendingStepException();
        }

        [Then(@"an error window appears with the message Moraš posuditi knjigu prije pisanja recenzije!")]
        public void ThenAnErrorWindowAppearsWithTheMessageMorasPosuditiKnjiguPrijePisanjaRecenzije()
        {
            throw new PendingStepException();
        }
    }
}
