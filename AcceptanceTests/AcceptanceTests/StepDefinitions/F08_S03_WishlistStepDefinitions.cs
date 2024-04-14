using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F08_S03_WishlistStepDefinitions
    {
        [Given(@"the member is on the details page of a non digital book")]
        public void GivenTheMemberIsOnTheDetailsPageOfANonDigitalBook()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnSearch = driver.FindElementByAccessibilityId("btnSearch");
            btnSearch.Click();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys("Tajanstveni");

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            driver.Manage().Window.Maximize();
        }

        [When(@"the member presses the button Add to wishlist")]
        public void WhenTheMemberPressesTheButtonAddToWishlist()
        {
            var driver = GuiDriver.GetDriver();

            var btnSave = driver.FindElementByAccessibilityId("btnSaveReadList");
            btnSave.Click();
        }

        [Then(@"the member should see a confirmation message")]
        public void ThenTheMemberShouldSeeAConfirmationMessage()
        {
            var driver = GuiDriver.GetDriver();

            bool message = driver.FindElementByName("Uspješno dodana knjiga na popis Želim pročitati!") != null;
            Assert.IsTrue(message);
        }

        [Then(@"the member should see this book in his wishlist")]
        public void ThenTheMemberShouldSeeThisBookInHisWishlist()
        {
            var driver = GuiDriver.GetDriver();

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            var btnWishlist = driver.FindElementByAccessibilityId("btnWishlist");
            btnWishlist.Click();

            var dgv = driver.FindElementByAccessibilityId("dgvWishlist");
            var rows = dgv.FindElementsByClassName("DataGridRow");

            bool found = false;

            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == "Tajanstveni Portreti")
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found);
        }

        [Given(@"the member is on the Wishlist screen")]
        public void GivenTheMemberIsOnTheWishlistScreen()
        {
            var driver = GuiDriver.GetDriver();

            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnDetails = driver.FindElementByAccessibilityId("btnWishlist");
            btnDetails.Click();


        }

        [Given(@"the member has a book added to his wishlist")]
        public void GivenTheMemberHasABookAddedToHisWishlist()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnWishlist = driver.FindElementByAccessibilityId("btnWishlist");
            btnWishlist.Click();

            var dgv = driver.FindElementByAccessibilityId("dgvWishlist");
            var rows = dgv.FindElementsByClassName("DataGridRow");

            Assert.IsTrue(rows.Count >= 2);
        }

        [When(@"the member chooses a book from the list to remove")]
        public void WhenTheMemberChoosesABookFromTheListToRemove()
        {
            var driver = GuiDriver.GetDriver();

            var dgv = driver.FindElementByAccessibilityId("dgvWishlist");
            var rows = dgv.FindElementsByClassName("DataGridRow");

            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");

                if (cells[0].Text == "Tajanstveni Portreti")
                {
                    cells[0].Click();
                    break;
                }
            }
        }

        [When(@"the member clicks on the Remove book from wishlist button")]
        public void WhenTheMemberClicksOnTheRemoveBookFromWishlistButton()
        {
            var driver = GuiDriver.GetDriver();

            var btnRemove = driver.FindElementByAccessibilityId("btnRemove");
            btnRemove.Click();
        }

        [Then(@"the member should see a refreshed list with the book removed")]
        public void ThenTheMemberShouldSeeARefreshedListWithTheBookRemoved()
        {
            var driver = GuiDriver.GetDriver();

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            var btnWishlist = driver.FindElementByAccessibilityId("btnWishlist");
            btnWishlist.Click();

            var dgv = driver.FindElementByAccessibilityId("dgvWishlist");
            var rows = dgv.FindElementsByClassName("DataGridRow");

            bool found = false;

            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == "Tajanstveni Portreti")
                {
                    found = true;
                    break;
                }
            }
            Assert.IsFalse(found);
        }

        [Given(@"the member has that same book added to his wishlist")]
        public void GivenTheMemberHasThatSameBookAddedToHisWishlist()
        {
            //guaranteed
        }

        [When(@"the member clicks the button Add to wishlist")]
        public void WhenTheMemberClicksTheButtonAddToWishlist()
        {
            var driver = GuiDriver.GetDriver();

            var btnSave = driver.FindElementByAccessibilityId("btnSaveReadList");
            btnSave.Click();
        }

        [Then(@"the member should see a warning message that the book is already on his wishlist")]
        public void ThenTheMemberShouldSeeAWarningMessageThatTheBookIsAlreadyOnHisWishlist()
        {
            var driver = GuiDriver.GetDriver();

            bool message = driver.FindElementByName("Knjiga Vam je već na popisu Želim pročitati!") != null;
            Assert.IsTrue(message);
        }

        [Then(@"the member shouldn't see the book in his wishlist two times")]
        public void ThenTheMemberShouldntSeeTheBookInHisWishlistTwoTimes()
        {
            var driver = GuiDriver.GetDriver();

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            var btnWishlist = driver.FindElementByAccessibilityId("btnWishlist");
            btnWishlist.Click();

            var dgv = driver.FindElementByAccessibilityId("dgvWishlist");
            var rows = dgv.FindElementsByClassName("DataGridRow");

            int counter = 0;

            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == "Tajanstveni Portreti")
                {
                    counter++;
                    
                }
            }
            Assert.IsTrue (counter == 1);
        }

        [When(@"the member clicks the Remove book from wishlist button")]
        public void WhenTheMemberClicksTheRemoveBookFromWishlistButton()
        {
            var driver = GuiDriver.GetDriver();

            var btnSave = driver.FindElementByAccessibilityId("btnRemove");
            btnSave.Click();
        }

        [Then(@"the member should see a warning message that a book has to be selected")]
        public void ThenTheMemberShouldSeeAWarningMessageThatABookHasToBeSelected()
        {
            var driver = GuiDriver.GetDriver();

            bool message = driver.FindElementByName("Morate odabrati knjigu!") != null;
            Assert.IsTrue(message);
        }
    }
}
