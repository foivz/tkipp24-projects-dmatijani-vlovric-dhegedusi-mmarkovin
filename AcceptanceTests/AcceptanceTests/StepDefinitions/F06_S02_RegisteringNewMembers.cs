using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F06_S02_RegisteringNewMembers
    {
        [Given(@"clicks register member button")]
        public void GivenClicksRegisterMemberButton()
        {

            var driver = GuiDriver.GetDriver();
            var btnMemberRegistration = driver.FindElementByAccessibilityId("btnMemberRegistration");
            btnMemberRegistration.Click();
        }

        [When(@"the employee enters OIB (.*) username (.*) and password (.*)")]
        public void WhenTheEmployeeEntersOIBUsernameAndPassword(string OIB, string username, string password)
        {
            var driver = GuiDriver.GetDriver();
            var txtOIB = driver.FindElementByAccessibilityId("txtOIB");
            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");

            txtOIB.SendKeys(OIB);
            txtUsername.SendKeys(username);
            txtPassword.SendKeys(password);
        }

        [When(@"clicks the generate barcode button")]
        public void WhenClicksTheGenerateBarcodeButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnGenerateBarcode = driver.FindElementByAccessibilityId("btnGenerateBarcode");
            btnGenerateBarcode.Click();
        }

        [When(@"clicks Save button on that screen")]
        public void WhenClicksSaveButtonOnThatScreen()
        {
            var driver = GuiDriver.GetDriver();
            var btnSave = driver.FindElementByAccessibilityId("btnSave");
            btnSave.Click();
        }

        [Then(@"the error message should appear")]
        public void ThenTheErrorMessageShouldAppear()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnOK = driver.FindElementByName("OK");
            Assert.IsNotNull(btnOK);
            btnOK.Click();

            GuiDriver.Dispose();
        }

        [When(@"the employee enters name (.*) surename (.*) OIB (.*) username (.*) password (.*)")]
        public void WhenTheEmployeeEntersNameSurenameOIBUsernamePassword(string name, string surname, string OIB, string username, string password)
        {
            var driver = GuiDriver.GetDriver();
            var txtOIB = driver.FindElementByAccessibilityId("txtOIB");
            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");
            var txtName = driver.FindElementByAccessibilityId("txtName");
            var txtSurname = driver.FindElementByAccessibilityId("txtSurname");

            txtOIB.SendKeys(OIB);
            txtUsername.SendKeys(username);
            txtPassword.SendKeys(password);
            txtName.SendKeys(name);
            txtSurname.SendKeys(surname);
        }

        [Then(@"the member managment panel should be visible")]
        public void ThenTheMemberManagmentPanelShouldBeVisible()
        {
            var driver = GuiDriver.GetDriver();
            var membersControl = driver.FindElementByName("Članovi knjižnice");
            Assert.IsNotNull(membersControl);
        }

        [Then(@"the table with members should contain member with (.*) (.*)")]
        public void ThenTheTableWithMembersShouldContainMemberWith(string OIB, string username)
        {
            var driver = GuiDriver.GetDriver();
            var memberOIB = driver.FindElementByName(OIB);
            var memberUsername = driver.FindElementByName(username);
            Assert.IsNotNull(memberOIB);
            Assert.IsNotNull(memberUsername);

            GuiDriver.Dispose();
        }

        [When(@"clicks Cancle button")]
        public void WhenClicksCancleButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnCancel = driver.FindElementByAccessibilityId("btnCancel");
            btnCancel.Click();
        }

        [Then(@"the table should not contain that member (.*) (.*)")]
        public void ThenTheTableShouldNotContainThatMember( string OIB, string username)
        {
            var driver = GuiDriver.GetDriver();
            var memberOIB = driver.FindElementsByName(OIB);
            var memberUsername = driver.FindElementsByName(username);

            bool OIBeExists = memberOIB.Count != 0;
            bool usernameExists = memberUsername.Count != 0;
            Assert.IsFalse(OIBeExists);
            Assert.IsFalse(usernameExists);

            GuiDriver.Dispose();
        }

    }
}
