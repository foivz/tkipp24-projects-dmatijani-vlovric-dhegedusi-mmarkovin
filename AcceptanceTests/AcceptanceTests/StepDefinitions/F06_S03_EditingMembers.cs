using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F06_S03_EditingMembers
    {
        [When(@"employee selects member with username ""([^""]*)"" and clicks edit button")]
        public void WhenEmployeeSelectsMemberWithUsernameAndClicksEditButton(string username)
        {
            var driver = GuiDriver.GetDriver();
            var selectedMember = driver.FindElementByName(username);
            selectedMember.Click();

            var btnEditMember = driver.FindElementByAccessibilityId("btnEditMember");
            btnEditMember.Click();
        }


        [When(@"the employee edits name ""([^""]*)"" and surename ""([^""]*)""")]
        public void WhenTheEmployeeEditsNameAndSurename(string name, string surname)
        {
            var driver = GuiDriver.GetDriver();
            var membersControl = driver.FindElementByName("Uredi člana");
            Assert.IsNotNull(membersControl);

            var txtName = driver.FindElementByAccessibilityId("txtName");
            var txtSurname = driver.FindElementByAccessibilityId("txtSurname");

            txtName.Clear();
            txtSurname.Clear();

            txtName.SendKeys(name);
            txtSurname.SendKeys(surname);
        }

        [When(@"clicks Save button on screeen")]
        public void WhenClicksSaveButtonOnScreeen()
        {
            var driver = GuiDriver.GetDriver();
            var btnSave = driver.FindElementByAccessibilityId("btnSave");
            btnSave.Click();
        }

        [Then(@"the employee should see member managment panel")]
        public void ThenTheEmployeeShouldSeeMemberManagmentPanel()
        {
            var driver = GuiDriver.GetDriver();
            var membersControl = driver.FindElementByName("Članovi knjižnice");
            Assert.IsNotNull(membersControl);
        }

        [Then(@"the table with members should contain edited member ""([^""]*)"" ""([^""]*)""")]
        public void ThenTheTableWithMembersShouldContainEditedMember(string name, string surname)
        {
            var driver = GuiDriver.GetDriver();
            var memberOIB = driver.FindElementByName(name);
            var memberUsername = driver.FindElementByName(surname);
            Assert.IsNotNull(memberOIB);
            Assert.IsNotNull(memberUsername);

            GuiDriver.Dispose();
        }

        [When(@"employee clicks edit button on screeen")]
        public void WhenEmployeeClicksEditButtonOnScreeen()
        {
            var driver = GuiDriver.GetDriver();
            var btnEditMember = driver.FindElementByAccessibilityId("btnEditMember");
            btnEditMember.Click();
        }

        [When(@"clicks Cancle button on screeen")]
        public void WhenClicksCancleButtonOnScreeen()
        {
            var driver = GuiDriver.GetDriver();
            var btnCancel = driver.FindElementByAccessibilityId("btnCancel");
            btnCancel.Click();
        }

        [Then(@"the table without that member ""([^""]*)"" ""([^""]*)""")]
        public void ThenTheTableWithoutThatMember(string name, string surname)
        {
            var driver = GuiDriver.GetDriver();
            var memberOIB = driver.FindElementsByName(name);
            var memberUsername = driver.FindElementsByName(surname);

            bool OIBeExists = memberOIB.Count != 0;
            bool usernameExists = memberUsername.Count != 0;
            Assert.IsFalse(OIBeExists);
            Assert.IsFalse(usernameExists);

            GuiDriver.Dispose();
        }
    }
}
