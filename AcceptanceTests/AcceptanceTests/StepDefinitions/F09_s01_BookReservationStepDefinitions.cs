using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F09_s01_BookReservationStepDefinitions
    {
        public static string bookA { get; set; } = "Book A14";
        public static string bookB { get; set; } = "Book B14";
        public static string bookC { get; set; } = "Book C14";
        public static string bookD { get; set; } = "Book D14";

        public void EnterBook(string name, WindowsDriver<WindowsElement> Driver)
        {
            var driver = Driver;

            var txtName = driver.FindElementByAccessibilityId("txtName");
            var txtNumberCopies = driver.FindElementByAccessibilityId("txtNumberCopies");
            var cmbGenre = driver.FindElementByAccessibilityId("cmbGenre");
            var cmbAuthor = driver.FindElementByAccessibilityId("cmbAuthor");
            var radioButton = driver.FindElementByName("Ne");

            txtName.SendKeys(name);
            txtNumberCopies.SendKeys("0");
            radioButton.Click();

            cmbGenre.Click();
            var chooseGenre = driver.FindElementByName("Romantika");
            chooseGenre.Click();

            cmbAuthor.Click();
            var chooseAuthor = driver.FindElementByName("Harper Vincet");
            chooseAuthor.Click();

            var btnInsert = driver.FindElementByAccessibilityId("btnSave");
            btnInsert.Click();

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();
        }

        [Given(@"there exist books A, B, C and D")]
        public void GivenThereExistBooksABCAndD()
        {
            //moram se odjavit, prijavit, unijet knjige, odjavit, prijavit kao member
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            //logout
            var btnLogout = driver.FindElementByAccessibilityId("btnLogout");
            btnLogout.Click();

            //login
            driver.SwitchTo().Window(driver.WindowHandles.First());
            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");
            txtUsername.SendKeys("pcindric88");
            txtPassword.SendKeys("a");
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();

            //new book screen
            driver.SwitchTo().Window(driver.WindowHandles.First());
            var btnCatalogue = driver.FindElementByAccessibilityId("btnBookCatalog");
            btnCatalogue.Click();

            var btnNewBook = driver.FindElementByAccessibilityId("btnNewBook");
            btnNewBook.Click();

            //entry
            EnterBook(bookA, driver);
            EnterBook(bookB, driver);
            EnterBook(bookC, driver);
            EnterBook(bookD, driver);

            //logout
            btnLogout = driver.FindElementByAccessibilityId("btnLogout");
            btnLogout.Click();

            //login
            driver.SwitchTo().Window(driver.WindowHandles.First());
            txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            txtPassword = driver.FindElementByAccessibilityId("txtPassword");
            txtUsername.SendKeys("anabol");
            txtPassword.SendKeys("123");
            btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();


        }

        [Given(@"the member is on the Search book catalogue screen")]
        public void GivenTheMemberIsOnTheSearchBookCatalogueScreen()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnCatalogue = driver.FindElementByAccessibilityId("btnSearch");
            btnCatalogue.Click();

            driver.Manage().Window.Maximize();
        }

        [When(@"the member chooses a digital book")]
        public void WhenTheMemberChoosesADigitalBook()
        {
            var driver = GuiDriver.GetDriver();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys("Hamlet");

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();
        }

        [Then(@"the member shouldn't see a Reserve button")]
        public void ThenTheMemberShouldntSeeAReserveButton()
        {
            var driver = GuiDriver.GetDriver();

            var btnReserve = driver.FindElementsByName("Rezerviraj");
            bool btnExists = btnReserve.Count() == 0;
            Assert.IsTrue(btnExists);
        }

        [Given(@"the member has (.*) or less reservations")] //2
        public void GivenTheMemberHasOrLessReservations(int p0)
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnReservations = driver.FindElementByAccessibilityId("btnReservations");
            btnReservations.Click();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");

            Assert.IsTrue(rows.Count <= 3);

        }

        [Given(@"the member is on the Details screen of a non digital book A")]
        public void GivenTheMemberIsOnTheDetailsScreenOfANonDigitalBookA()
        {
            var driver = GuiDriver.GetDriver();

            var btnSearch = driver.FindElementByAccessibilityId("btnSearch");
            btnSearch.Click();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys(bookA);

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            driver.Manage().Window.Maximize();
        }

        [When(@"the member clicks the Reserve button")]
        public void WhenTheMemberClicksTheReserveButton()
        {
            var driver = GuiDriver.GetDriver();

            var btnReserve = driver.FindElementByAccessibilityId("btnReserve");
            btnReserve.Click();
        }

        [Then(@"the member should see a reservation confirmation message")]
        public void ThenTheMemberShouldSeeAReservationConfirmationMessage()
        {
            var driver = GuiDriver.GetDriver();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool msg = driver.FindElementByName("Biti ćete 1. na redu čekanja. Potvrdite ili odbijte rezervaciju.") != null;

            Assert.IsTrue(msg);
        }

        [When(@"the member confirms the reservation")]
        public void WhenTheMemberConfirmsTheReservation()
        {
            var driver = GuiDriver.GetDriver();

            var btnAccept = driver.FindElementByName("Potvrđujem");
            btnAccept.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();
        }

        [Then(@"the member should see his reservation position instead of the Reserve button")]
        public void ThenTheMemberShouldSeeHisReservationPositionInsteadOfTheReserveButton()
        {
            var driver = GuiDriver.GetDriver();

            var btnReserve = driver.FindElementsByName("Rezerviraj");
            bool btnExists = btnReserve.Count() == 0;
            Assert.IsTrue(btnExists);
        }

        [When(@"the member goes to the Reservations screen")]
        public void WhenTheMemberGoesToTheReservationsScreen()
        {
            var driver = GuiDriver.GetDriver();

            var btnReservations = driver.FindElementByAccessibilityId("btnReservations");
            btnReservations.Click();
        }

        [Then(@"the member should see book A in his list")]
        public void ThenTheMemberShouldSeeBookAInHisList()
        {
            var driver = GuiDriver.GetDriver();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");

            bool found = false;

            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookA)
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found);
        }

        [Given(@"the member is on the Details screen of a non digital book B")]
        public void GivenTheMemberIsOnTheDetailsScreenOfANonDigitalBookB()
        {
            var driver = GuiDriver.GetDriver();

            var btnSearch = driver.FindElementByAccessibilityId("btnSearch");
            btnSearch.Click();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys(bookB);

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            driver.Manage().Window.Maximize();
        }

        [When(@"the member  declines the confirmation")]
        public void WhenTheMemberDeclinesTheConfirmation()
        {
            var driver = GuiDriver.GetDriver();

            var btnAccept = driver.FindElementByName("Odustani");
            btnAccept.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
        }

        [Then(@"the member shouldn't see book B in his list")]
        public void ThenTheMemberShouldntSeeBookBInHisList()
        {
            var driver = GuiDriver.GetDriver();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");

            bool found = false;

            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookB)
                {
                    found = true;
                    break;
                }
            }
            Assert.IsFalse(found);
        }

        [Given(@"the member reserves books B and C")]
        public void GivenTheMemberReservesBooksBAndC()
        {
            //doc na ekran
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnCatalogue = driver.FindElementByAccessibilityId("btnSearch");
            btnCatalogue.Click();

            driver.Manage().Window.Maximize();

            //rezervirat B

            var btnSearch = driver.FindElementByAccessibilityId("btnSearch");
            btnSearch.Click();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys(bookB);

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            var btnReserve = driver.FindElementByAccessibilityId("btnReserve");
            btnReserve.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            var btnAccept = driver.FindElementByName("Potvrđujem");
            btnAccept.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            btnCatalogue = driver.FindElementByAccessibilityId("btnSearch");
            btnCatalogue.Click();

            //rezrvirat C
            btnSearch = driver.FindElementByAccessibilityId("btnSearch");
            btnSearch.Click();

            txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys(bookC);

            dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            rows = dgv.FindElementsByClassName("DataGridRow");
            cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            btnReserve = driver.FindElementByAccessibilityId("btnReserve");
            btnReserve.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            btnAccept = driver.FindElementByName("Potvrđujem");
            btnAccept.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

        }

        [Given(@"the member is on the Details screen of a non digital book D")]
        public void GivenTheMemberIsOnTheDetailsScreenOfANonDigitalBookD()
        {
            var driver = GuiDriver.GetDriver();

            var btnSearch = driver.FindElementByAccessibilityId("btnSearch");
            btnSearch.Click();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys(bookD);

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            var btnReserve = driver.FindElementByAccessibilityId("btnReserve");
            btnReserve.Click();

        }

        [Then(@"the member should see a warning message")]
        public void ThenTheMemberShouldSeeAWarningMessage()
        {
            var driver = GuiDriver.GetDriver();

            bool msg = driver.FindElementByName("Već imate maksimalan broj rezervacija koji je 3!") != null;
            Assert.IsTrue(msg);

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();
        }

        [Then(@"the member goes to the Reservations screen")]
        public void ThenTheMemberGoesToTheReservationsScreen()
        {
            var driver = GuiDriver.GetDriver();

            var btnReservations = driver.FindElementByAccessibilityId("btnReservations");
            btnReservations.Click();
        }

        [Then(@"the member shouldn't see book D in his list")]
        public void ThenTheMemberShouldntSeeBookDInHisList()
        {
            var driver = GuiDriver.GetDriver();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");

            bool found = false;

            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookD)
                {
                    found = true;
                    break;
                }
            }
            Assert.IsFalse(found);
        }

        [Given(@"the member is on the Reservations screen")]
        public void GivenTheMemberIsOnTheReservationsScreen()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnReservations = driver.FindElementByAccessibilityId("btnReservations");
            btnReservations.Click();
        }

        [Given(@"the member has a reserved book A")]
        public void GivenTheMemberHasAReservedBookA()
        {
            var driver = GuiDriver.GetDriver();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");

            bool found = false;

            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookA)
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found);
        }

        [When(@"the member chooses reserved book A from the list")]
        public void WhenTheMemberChoosesReservedBookAFromTheList()
        {
            var driver = GuiDriver.GetDriver();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            foreach(var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookA)
                {
                    cells[0].Click();
                    break;
                }
            }
        }

        [When(@"the member clicks on the Remove reservation button")]
        public void WhenTheMemberClicksOnTheRemoveReservationButton()
        {
            var driver = GuiDriver.GetDriver();

            var btnRemove = driver.FindElementByAccessibilityId("btnRemove");
            btnRemove.Click();
        }


        [Then(@"the member should see a removal confirmation message")]
        public void ThenTheMemberShouldSeeARemovalConfirmationMessage()
        {
            var driver = GuiDriver.GetDriver();

            bool msg = driver.FindElementByName("Uspješno!") != null;

            Assert.IsTrue(msg);

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();
        }

        [Then(@"the member shouldn't see book A in his list")]
        public void ThenTheMemberShouldntSeeBookAInHisList()
        {
            var driver = GuiDriver.GetDriver();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");

            bool found = false;

            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookA)
                {
                    found = true;
                    break;
                }
            }
            Assert.IsFalse(found);
        }

        [When(@"the member goes to the See details screen of book A")]
        public void WhenTheMemberGoesToTheSeeDetailsScreenOfBookA()
        {
            var driver = GuiDriver.GetDriver();

            var btnSearch = driver.FindElementByAccessibilityId("btnSearch");
            btnSearch.Click();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys(bookA);

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            driver.Manage().Window.Maximize();
        }

        [Then(@"the member should see the Reserve button")]
        public void ThenTheMemberShouldSeeTheReserveButton()
        {
            var driver = GuiDriver.GetDriver();

            bool btnReserve = driver.FindElementByAccessibilityId("btnReserve") != null;
            Assert.IsTrue(btnReserve);
        }

        [Given(@"the member has a reserved book B")]
        public void GivenTheMemberHasAReservedBookB()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnReservations = driver.FindElementByAccessibilityId("btnReservations");
            btnReservations.Click();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");

            bool found = false;

            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookB)
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found);
        }

        [Given(@"an employee entered a new copy of the book B")]
        public void GivenAnEmployeeEnteredANewCopyOfTheBookB()
        {
            var driver = GuiDriver.GetDriver();

            //logout
            var btnLogout = driver.FindElementByAccessibilityId("btnLogout");
            btnLogout.Click();

            //login
            driver.SwitchTo().Window(driver.WindowHandles.First());
            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");
            txtUsername.SendKeys("pcindric88");
            txtPassword.SendKeys("a");
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();

            //catalogue, new copies
            driver.SwitchTo().Window(driver.WindowHandles.First());
            var btnCatalogue = driver.FindElementByAccessibilityId("btnBookCatalog");
            btnCatalogue.Click();

            var btnCopies = driver.FindElementByAccessibilityId("btnNewCopies");
            btnCopies.Click();

            //new book copy
            var txtSearch = driver.FindElementByAccessibilityId("txtBookName");
            txtSearch.SendKeys(bookB);

            var dgv = driver.FindElementByAccessibilityId("dgvBookNamesArchive");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            var txtCopies = driver.FindElementByAccessibilityId("txtNumberCopies");
            txtCopies.SendKeys("1");

            var btnInsert = driver.FindElementByAccessibilityId("btnSave");
            btnInsert.Click();
            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            //logout
            btnLogout = driver.FindElementByAccessibilityId("btnLogout");
            btnLogout.Click();
        }

        [When(@"the member logs in")]
        public void WhenTheMemberLogsIn()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");
            txtUsername.SendKeys("anabol");
            txtPassword.SendKeys("123");
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());

        }

        [Then(@"the member should see a notification with his reservation expiry date")]
        public void ThenTheMemberShouldSeeANotificationWithHisReservationExpiryDate()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            bool btnOK = driver.FindElementByName("OK") != null;
            Assert.IsTrue(btnOK);

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();
        }

        [When(@"the member goes to the Reservations page")]
        public void WhenTheMemberGoesToTheReservationsPage()
        {
            var driver = GuiDriver.GetDriver();

            var btnReservations = driver.FindElementByAccessibilityId("btnReservations");
            btnReservations.Click();
        }

        [Then(@"the member should see a date in the Expiry column of the book")]
        public void ThenTheMemberShouldSeeADateInTheExpiryColumnOfTheBook()
        {
            //bookB da cell sa datumom nije empty
            var driver = GuiDriver.GetDriver();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            bool date = false;

            foreach(var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookB && !string.IsNullOrEmpty(cells[1].Text))
                {
                    date = true;
                    break;
                }
            }
            Assert.IsTrue(date);
        }

        [Given(@"the member reserves book A")]
        public void GivenTheMemberReservesBookA()
        {
            //doc na ekran
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            var btnCatalogue = driver.FindElementByAccessibilityId("btnSearch");
            btnCatalogue.Click();

            driver.Manage().Window.Maximize();

            //rezervirat A

            var btnSearch = driver.FindElementByAccessibilityId("btnSearch");
            btnSearch.Click();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys(bookA);

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            var btnReserve = driver.FindElementByAccessibilityId("btnReserve");
            btnReserve.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            var btnAccept = driver.FindElementByName("Potvrđujem");
            btnAccept.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            //logout
            var btnLogout = driver.FindElementByAccessibilityId("btnLogout");
            btnLogout.Click();
        }

        [Given(@"an employee entered a new copy of the books A and C")]
        public void GivenAnEmployeeEnteredANewCopyOfTheBooksAAndC()
        {
            //login
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");
            txtUsername.SendKeys("pcindric88");
            txtPassword.SendKeys("a");
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            var btnCatalogue = driver.FindElementByAccessibilityId("btnBookCatalog");
            btnCatalogue.Click();

            var btnNewCopies = driver.FindElementByAccessibilityId("btnNewCopies");
            btnNewCopies.Click();

            //entry A
            var txtSearch = driver.FindElementByAccessibilityId("txtBookName");
            txtSearch.SendKeys(bookA);

            var dgv = driver.FindElementByAccessibilityId("dgvBookNamesArchive");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            var txtCopies = driver.FindElementByAccessibilityId("txtNumberCopies");
            txtCopies.SendKeys("1");

            var btnInsert = driver.FindElementByAccessibilityId("btnSave");
            btnInsert.Click();
            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            //entry C
            txtSearch = driver.FindElementByAccessibilityId("txtBookName");
            txtSearch.SendKeys(bookC);

            dgv = driver.FindElementByAccessibilityId("dgvBookNamesArchive");
            rows = dgv.FindElementsByClassName("DataGridRow");
            cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            txtCopies = driver.FindElementByAccessibilityId("txtNumberCopies");
            txtCopies.SendKeys("1");

            btnInsert = driver.FindElementByAccessibilityId("btnSave");
            btnInsert.Click();
            btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            //logout
            var btnLogout = driver.FindElementByAccessibilityId("btnLogout");
            btnLogout.Click();
        }

        [Then(@"the member should see a notification with his reservation expiry dates")]
        public void ThenTheMemberShouldSeeANotificationWithHisReservationExpiryDates()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            bool btnOK = driver.FindElementByName("OK") != null;
            Assert.IsTrue(btnOK);

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();
        }

        [Then(@"the member should see a date in the Expiry column of the (.*) books")]
        public void ThenTheMemberShouldSeeADateInTheExpiryColumnOfTheBooks(int p0)
        {
            var driver = GuiDriver.GetDriver();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            bool dateA = false;
            bool dateC = false;

            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookA && !string.IsNullOrEmpty(cells[1].Text))
                {
                    dateA = true;
                }
                if (cells[0].Text == bookC && !string.IsNullOrEmpty(cells[1].Text))
                {
                    dateC = true;
                }
            }
            Assert.IsTrue(dateA && dateC);
        }

        [Given(@"the member has a reserved book C")]
        public void GivenTheMemberHasAReservedBookC()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            var btnReservations = driver.FindElementByAccessibilityId("btnReservations");
            btnReservations.Click();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");

            bool found = false;

            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookC)
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found);
        }


        [Given(@"no other member has that book reserved")]
        public void GivenNoOtherMemberHasThatBookReserved()
        {
            //guaranteed
        }

        [Given(@"the expiry date column of book C has a date in it")]
        public void GivenTheExpiryDateColumnOfBookCHasADateInIt()
        {
            var driver = GuiDriver.GetDriver();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            bool date = false;

            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookC && !string.IsNullOrEmpty(cells[1].Text))
                {
                    date = true;
                    break;
                }
            }
            Assert.IsTrue(date);
        }

        [When(@"the member chooses book C")]
        public void WhenTheMemberChoosesBookC()
        {
            var driver = GuiDriver.GetDriver();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookC)
                {
                    cells[0].Click();
                    break;
                }
            }
        }

        [When(@"the member clicks the Remove reservation button")]
        public void WhenTheMemberClicksTheRemoveReservationButton()
        {
            var driver = GuiDriver.GetDriver();

            var btnRemove = driver.FindElementByAccessibilityId("btnRemove");
            btnRemove.Click();

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();
        }

        [Then(@"the member shouldn't see book C in his list")]
        public void ThenTheMemberShouldntSeeBookCInHisList()
        {
            var driver = GuiDriver.GetDriver();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");

            bool found = false;

            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookC)
                {
                    found = true;
                    break;
                }
            }
            Assert.IsFalse(found);
        }

        [Then(@"the member should see that the number of available copies of book A on the Details page is (.*)")]
        public void ThenTheMemberShouldSeeThatTheNumberOfAvailableCopiesOfBookAOnTheDetailsPageIs(int p0)
        {
            var driver = GuiDriver.GetDriver();

            var btnSearch = driver.FindElementByAccessibilityId("btnSearch");
            btnSearch.Click();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys(bookC);

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            driver.Manage().Window.Maximize();

            bool copies = driver.FindElementByAccessibilityId("tblAvailable").Text == "Da, broj raspoloživih primjeraka je: 1";
            Assert.IsTrue(copies);
        }

        [Given(@"the member has a reserved book B and a notification")]
        public void GivenTheMemberHasAReservedBookBAndANotification()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            var btnReservations = driver.FindElementByAccessibilityId("btnReservations");
            btnReservations.Click();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");

            bool found = false;

            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookB)
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found);
        }


        [Given(@"the expiry date column of book B has a date in it")]
        public void GivenTheExpiryDateColumnOfBookBHasADateInIt()
        {
            var driver = GuiDriver.GetDriver();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            bool date = false;

            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookB && !string.IsNullOrEmpty(cells[1].Text))
                {
                    date = true;
                    break;
                }
            }
            Assert.IsTrue(date);
        }

        [Given(@"another member has book B reserved")]
        public void GivenAnotherMemberHasBookBReserved()
        {
            //logout
            var driver = GuiDriver.GetDriver();
            var btnLogout = driver.FindElementByAccessibilityId("btnLogout");
            btnLogout.Click();

            //login sa diabol 123
            driver.SwitchTo().Window(driver.WindowHandles.First());
            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");
            txtUsername.SendKeys("diabol");
            txtPassword.SendKeys("123");
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();

            //reserve B
            driver.SwitchTo().Window(driver.WindowHandles.First());
            var btnSearch = driver.FindElementByAccessibilityId("btnSearch");
            btnSearch.Click();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys(bookB);

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            driver.Manage().Window.Maximize();

            var btnReserve = driver.FindElementByAccessibilityId("btnReserve");
            btnReserve.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            var btnAccept = driver.FindElementByName("Potvrđujem");
            btnAccept.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            //logout
            btnLogout = driver.FindElementByAccessibilityId("btnLogout");
            btnLogout.Click();

            //login sa anabol 123
            driver.SwitchTo().Window(driver.WindowHandles.First());
            txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            txtPassword = driver.FindElementByAccessibilityId("txtPassword");
            txtUsername.SendKeys("anabol");
            txtPassword.SendKeys("123");
            btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();
        }

        [When(@"the member chooses book B")]
        public void WhenTheMemberChoosesBookB()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            var btnReservations = driver.FindElementByAccessibilityId("btnReservations");
            btnReservations.Click();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookB)
                {
                    cells[0].Click();
                    break;
                }
            }
        }

        [When(@"the second member logs in")]
        public void WhenTheSecondMemberLogsIn()
        {
            var driver = GuiDriver.GetDriver();
            var btnLogout = driver.FindElementByAccessibilityId("btnLogout");
            btnLogout.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");
            txtUsername.SendKeys("diabol");
            txtPassword.SendKeys("123");
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();
        }

        [Then(@"the second member should see a notification window")]
        public void ThenTheSecondMemberShouldSeeANotificationWindow()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            bool btnOK = driver.FindElementByName("OK") != null;
            Assert.IsTrue(btnOK);

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();
        }

        [Then(@"the second member should see that the number of available copies of book B on the Details page is (.*)")] //0
        public void ThenTheSecondMemberShouldSeeThatTheNumberOfAvailableCopiesOfBookBOnTheDetailsPageIs(int p0)
        {
            var driver = GuiDriver.GetDriver();

            var btnSearch = driver.FindElementByAccessibilityId("btnSearch");
            btnSearch.Click();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys(bookB);

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            driver.Manage().Window.Maximize();

            bool copies = driver.FindElementByAccessibilityId("tblAvailable").Text == "Ne";
            Assert.IsTrue(copies);
        }

        [Given(@"the member has a reserved book D")]
        public void GivenTheMemberHasAReservedBookD()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            var btnSearch = driver.FindElementByAccessibilityId("btnSearch");
            btnSearch.Click();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys(bookD);

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            driver.Manage().Window.Maximize();

            var btnReserve = driver.FindElementByAccessibilityId("btnReserve");
            btnReserve.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            var btnAccept = driver.FindElementByName("Potvrđujem");
            btnAccept.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
            btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();
        }

        [Given(@"the member removes the book D from his reservations")]
        public void GivenTheMemberRemovesTheBookDFromHisReservations()
        {
            var driver = GuiDriver.GetDriver();

            var btnReservations = driver.FindElementByAccessibilityId("btnReservations");
            btnReservations.Click();

            var dgv = driver.FindElementByAccessibilityId("dgvReservations");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            foreach (var row in rows)
            {
                var cells = row.FindElementsByClassName("DataGridCell");
                if (cells[0].Text == bookD)
                {
                    cells[0].Click();
                    break;
                }
            }

            var btnRemove = driver.FindElementByAccessibilityId("btnRemove");
            btnRemove.Click();

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();
        }


        [When(@"an employee enters (.*) new copies of the book D")] //2
        public void WhenAnEmployeeEntersNewCopiesOfTheBookD(int p0)
        {
            var driver = GuiDriver.GetDriver();

            //logout
            var btnLogout = driver.FindElementByAccessibilityId("btnLogout");
            btnLogout.Click();

            //login
            driver.SwitchTo().Window(driver.WindowHandles.First());
            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");
            txtUsername.SendKeys("pcindric88");
            txtPassword.SendKeys("a");
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();

            //catalogue, new copies
            driver.SwitchTo().Window(driver.WindowHandles.First());
            var btnCatalogue = driver.FindElementByAccessibilityId("btnBookCatalog");
            btnCatalogue.Click();

            var btnCopies = driver.FindElementByAccessibilityId("btnNewCopies");
            btnCopies.Click();

            //new book copy
            var txtSearch = driver.FindElementByAccessibilityId("txtBookName");
            txtSearch.SendKeys(bookD);

            var dgv = driver.FindElementByAccessibilityId("dgvBookNamesArchive");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            var txtCopies = driver.FindElementByAccessibilityId("txtNumberCopies");
            txtCopies.SendKeys("2");

            var btnInsert = driver.FindElementByAccessibilityId("btnSave");
            btnInsert.Click();
            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            //logout
            btnLogout = driver.FindElementByAccessibilityId("btnLogout");
            btnLogout.Click();
        }

        [When(@"the member logs in again")]
        public void WhenTheMemberLogsInAgain()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");
            txtUsername.SendKeys("anabol");
            txtPassword.SendKeys("123");
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());
        }

        [Then(@"the member should see that the number of available copies of book D on the Details page is (.*)")] //2
        public void ThenTheMemberShouldSeeThatTheNumberOfAvailableCopiesOfBookDOnTheDetailsPageIs(int p0)
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnOk = driver.FindElementByAccessibilityId("2");
            btnOk.Click();

            var btnSearch = driver.FindElementByAccessibilityId("btnSearch");
            btnSearch.Click();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys(bookD);

            var dgv = driver.FindElementByAccessibilityId("dgvBookSearch");
            var rows = dgv.FindElementsByClassName("DataGridRow");
            var cells = rows[0].FindElementsByClassName("DataGridCell");
            cells[0].Click();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            driver.Manage().Window.Maximize();

            bool copies = driver.FindElementByAccessibilityId("tblAvailable").Text == "Da, broj raspoloživih primjeraka je: 2";
            Assert.IsTrue(copies);
        }


    }
}
