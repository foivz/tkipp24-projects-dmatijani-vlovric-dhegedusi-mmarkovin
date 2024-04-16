using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F10_S03_DeletingReviewsStepDefinitions
    {
        F10_S02_WritingReviewsStepDefinitions helperClass = new F10_S02_WritingReviewsStepDefinitions();
        [When(@"the user presses the Obriši Recenziju button")]
        public void WhenTheUserPressesTheObrisiRecenzijuButton()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            var btnRemoveReview = driver.FindElementByAccessibilityId("btnRemoveReview");
            btnRemoveReview.Click();
        }

        [Then(@"the user's review is deleted from the database")]
        public void ThenTheUsersReviewIsDeletedFromTheDatabase()
        {
            var driver = GuiDriver.GetDriver();
            var dgReviews = driver.FindElementByAccessibilityId("dgReviews");
            var rows = dgReviews.FindElementsByClassName("DataGridRow");

            bool isReviewRemovedFromDB = true;


            foreach (var row in rows) {
                var cells = row.FindElements(By.TagName("DataGridCell"));

                var usernameCell = cells[0];

                string username = usernameCell.Text;

                if (username == "Marija Pranjic")
                {
                    isReviewRemovedFromDB = false;
                    break;
                }
            }
            Assert.IsTrue(isReviewRemovedFromDB);
        }

        [Then(@"a window appears with the message Vaša recenzija je uspješno obrisana!")]
        public void ThenAWindowAppearsWithTheMessageVasaRecenzijaJeUspjesnoObrisana()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool btnOk = driver.FindElementByAccessibilityId("2") != null;
            Assert.IsTrue(btnOk);
        }

        [Given(@"the user has not previously written a review for the selected book")]
        public void GivenTheUserHasNotPreviouslyWrittenAReviewForTheSelectedBook()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            GuiDriver.Dispose();
            helperClass.GivenTheUserIsOnAllReviewsForm();

            driver = GuiDriver.GetOrCreateDriver();
            var dgReviews = driver.FindElementByAccessibilityId("dgReviews");
            var rows = dgReviews.FindElementsByClassName("DataGridRow");

            bool hasUserWrittenReview = false;


            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");

                foreach (var cell in cells)
                {
                    string cellText = cell.Text.Trim();

                    if (cellText == "Marija Pranjic")
                    {
                        hasUserWrittenReview = true;
                        break;
                    }
                }
            }



            Assert.IsTrue(!hasUserWrittenReview);
        }

        [Then(@"an error window appears with the message Niste napisali recenziju za ovu knjigu!")]
        public void ThenAnErrorWindowAppearsWithTheMessageNisteNapisaliRecenzijuZaOvuKnjigu()
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
    }
}
