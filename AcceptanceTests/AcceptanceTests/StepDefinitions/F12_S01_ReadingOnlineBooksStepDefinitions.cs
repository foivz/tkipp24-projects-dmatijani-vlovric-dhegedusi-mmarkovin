using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using TechTalk.SpecFlow;
using static System.Net.WebRequestMethods;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F12_S01_ReadingOnlineBooksStepDefinitions
    {
        [Given(@"the user is logged in as a library member")]
        public void GivenTheUserIsLoggedInAsALibraryMember()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");

            txtUsername.SendKeys("mpranjic23");
            txtPassword.SendKeys("pranjicka98");


            bool isUsernameCorrect = txtUsername.Text == "mpranjic23";

            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            Assert.IsTrue(isUsernameCorrect);
            driver.Manage().Window.Maximize();
        }

        [Given(@"the user is on the Book details form")]
        public void GivenTheUserIsOnTheBookDetailsForm()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            var btnSearch = driver.FindElementByAccessibilityId("btnSearch");
            btnSearch.Click();


            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys("Haml");


            var dgvBookSearch = driver.FindElementByAccessibilityId("dgvBookSearch");
            var cellsInFirstRow = dgvBookSearch.FindElementsByClassName("DataGridCell");
            cellsInFirstRow[0].Click();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();



            bool isDigital = driver.FindElementByAccessibilityId("btnAddReview") != null;
            Assert.IsTrue(isDigital);
        }

        [Given(@"there is a digital version of the selected book in the system")]
        public void GivenThereIsADigitalVersionOfTheSelectedBookInTheSystem()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool btnDigital = driver.FindElementByName("Otvori digitalnu verziju") != null;

            Assert.IsTrue(btnDigital);
        }

        [Given(@"the book has a correct link")]
        public void GivenTheBookHasACorrectLink()
        {
            string link = "https://www.gutenberg.org/cache/epub/27761/pg27761-images.html";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
            request.Method = "HEAD";

           
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
             
            bool isLinkValid = response.StatusCode == HttpStatusCode.OK;
            Assert.IsTrue(isLinkValid);
            }

        }

        [When(@"the user clicks on the Digital Version button")]
        public void WhenTheUserClicksOnTheDigitalVersionButton()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            var btnDigital = driver.FindElementByName("Otvori digitalnu verziju");
            btnDigital.Click();
        }

        [Then(@"an in-app web browser should open displaying the text of the book")]
        public void ThenAnIn_AppWebBrowserShouldOpenDisplayingTheTextOfTheBook()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            bool isOpened = driver.FindElementByName("The Project Gutenberg eBook of Hamlet by William Shakespeare, edited by Charles Kean") != null;

            Assert.IsTrue(isOpened);


            GuiDriver.Dispose();
        }

        [Given(@"the user is on the Search books form")]
        public void GivenTheUserIsOnTheSearchBooksForm()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            var btnSearch = driver.FindElementByAccessibilityId("btnSearch");
            btnSearch.Click();

            bool btnDetailsExists = driver.FindElementByAccessibilityId("btnDetails") != null;

            Assert.IsTrue(btnDetailsExists);
            
        }

        [Given(@"there is no digital version of the selected book in the system")]
        public void GivenThereIsNoDigitalVersionOfTheSelectedBookInTheSystem()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            WhenTheUserSelectsABook();
            WhenClicksOnTheDetaljiButton();

            var isDigital = driver.FindElementsByName("Otvori digitalnu verziju");
            Assert.IsTrue(isDigital.Count == 0);

        }

        [When(@"the user selects a book")]
        public void WhenTheUserSelectsABook()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            GivenTheUserIsOnTheSearchBooksForm();

            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys("Tajan");

            var dgvBookSearch = driver.FindElementByAccessibilityId("dgvBookSearch");
            var cellsInFirstRow = dgvBookSearch.FindElementsByClassName("DataGridCell");
            cellsInFirstRow[0].Click();


        }

        [When(@"clicks on the Detalji button")]
        public void WhenClicksOnTheDetaljiButton()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();
        }

        [Then(@"the book details page should not have a Digitalna verzija button")]
        public void ThenTheBookDetailsPageShouldNotHaveADigitalnaVerzijaButton()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var digitalButton = driver.FindElementsByName("Otvori digitalnu verziju");
            Assert.IsTrue(digitalButton.Count == 0);

            GuiDriver.Dispose();
        }

        [Given(@"the book has an invalid link")]
        public void GivenTheBookHasAnInvalidLink()
        {
            string link = "https://weofe.gutenberg/proba";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
            request.Method = "HEAD";

            Assert.ThrowsException<WebException>(() => {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) { }
            });
        }

        [When(@"the user clicks on the Digitalna verzija button")]
        public void WhenTheUserClicksOnTheDigitalnaVerzijaButton()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            var btnSearch = driver.FindElementByAccessibilityId("btnSearch");
            btnSearch.Click();


            var txtSearch = driver.FindElementByAccessibilityId("txtSearch");
            txtSearch.SendKeys("Romeo");


            var dgvBookSearch = driver.FindElementByAccessibilityId("dgvBookSearch");
            var cellsInFirstRow = dgvBookSearch.FindElementsByClassName("DataGridCell");
            cellsInFirstRow[0].Click();

            var btnDetails = driver.FindElementByAccessibilityId("btnDetails");
            btnDetails.Click();

            var btnDigital = driver.FindElementByName("Otvori digitalnu verziju");
            btnDigital.Click();
        }

        [Then(@"the user should see an error message stating Knjiga ima nevažeći link!")]
        public void ThenTheUserShouldSeeAnErrorMessageStatingKnjigaImaNevazeciLink()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            Thread.Sleep(25000);

            bool btnOk = driver.FindElementByName("OK") != null;

            Assert.IsTrue(btnOk);
        }
    }
}
