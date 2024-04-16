using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F06_S01_FilteringMembers
    {
        [Given(@"the user is logged into app as employee")]
        public void GivenTheUserIsLoggedIntoAppAsEmployee()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");

            txtUsername.SendKeys("hmihovic");
            txtPassword.SendKeys("volimcitati");
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();

        }

        [Given(@"is on the Member Management panel")]
        public void GivenIsOnTheMemberManagementPanel()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.Manage().Window.Maximize();
            var btnMembership = driver.FindElementByAccessibilityId("btnMembership");
            btnMembership.Click();

            var membersPanel = driver.FindElementByName("Èlanovi knjižnice");
            Assert.IsNotNull(membersPanel);
        }

        [When(@"Employee enters name (.*) and surename (.*)")]
        public void WhenEmployeeEntersNameNameAndSurename(string name, string surename)
        {
            var driver = GuiDriver.GetDriver();
            var txtFilter = driver.FindElementByAccessibilityId("txtFilter");

            txtFilter.SendKeys(name + " " + surename);
        }

        [When(@"clicks the Filter button")]
        public void WhenClicksTheFilterButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnFilter = driver.FindElementByAccessibilityId("btnFilter");
            btnFilter.Click();
        }

        [Then(@"application shows empty table")]
        public void ThenApplicationShowsEmptyTable()
        {
            var driver = GuiDriver.GetDriver();
            var dgvName = driver.FindElementsByName("Petra");
            bool nameExists = dgvName.Count != 0;
            Assert.IsFalse(nameExists);
            var dgvSurename = driver.FindElementsByName("Perkoviæ");
            bool surnameExists = dgvSurename.Count != 0;
            Assert.IsFalse(surnameExists);

            GuiDriver.Dispose();

        }

        [Then(@"application shows (.*) and (.*)")]
        public void ThenApplicationShowsNameAndSurename(string name, string surename)
        {
            var driver = GuiDriver.GetDriver();
            var dgvName = driver.FindElementByName(name);
            Assert.IsNotNull(dgvName);
            var dgvSurename = driver.FindElementByName(surename);
            Assert.IsNotNull(dgvSurename);

            GuiDriver.Dispose();
        }

        [When(@"Employee clicks the clear button")]
        public void WhenEmployeeClicksTheClearButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnClearFilter = driver.FindElementByAccessibilityId("btnClearFilter");
            btnClearFilter.Click();
        }

        [Then(@"application shows all members")]
        public void ThenApplicationShowsAllMembers()
        {
            var driver = GuiDriver.GetDriver();
            var dgvMembers = driver.FindElementByAccessibilityId("dgvMembers");
            var rows = dgvMembers.FindElementsByName("456");
            bool rowsExist = rows.Count() > 3;
            Assert.IsNotNull(rowsExist);

            GuiDriver.Dispose();
        }
    }
}
