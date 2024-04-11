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
            throw new PendingStepException();
        }

        [Then(@"an error window appears with the message Niste napisali recenziju za ovu knjigu!")]
        public void ThenAnErrorWindowAppearsWithTheMessageNisteNapisaliRecenzijuZaOvuKnjigu()
        {
            throw new PendingStepException();
        }
    }
}
