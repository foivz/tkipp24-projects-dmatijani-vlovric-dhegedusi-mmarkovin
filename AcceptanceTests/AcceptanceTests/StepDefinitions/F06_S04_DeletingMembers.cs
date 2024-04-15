using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F06_S04_DeletingMembers
    {
        [When(@"employee clicks delete button")]
        public void WhenEmployeeClicksDeleteButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnDeleteMember = driver.FindElementByAccessibilityId("btnDeleteMember");
            btnDeleteMember.Click();
        }

        [When(@"employee selects employee selects member with username ""([^""]*)""")]
        public void WhenEmployeeSelectsEmployeeSelectsMemberWithUsername(string username)
        {
            var driver = GuiDriver.GetDriver();
            var selectedmember = driver.FindElementByName(username);
            selectedmember.Click();
        }

        [When(@"employee clicks ok button on alert window")]
        public void WhenEmployeeClicksOkButtonOnAlertWindow()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnOK = driver.FindElementByName("OK");
            Assert.IsNotNull(btnOK);
            btnOK.Click();
        }

        [Then(@"the employee should see members menagment window")]
        public void ThenTheEmployeeShouldSeeMembersMenagmentWindow()
        {
            var driver = GuiDriver.GetDriver();
            var membersControl = driver.FindElementByName("Èlanovi knjižnice");
            Assert.IsNotNull(membersControl);
        }

        [Then(@"the table should still show that member ""([^""]*)""")]
        public void ThenTheTableShouldStillShowThatMember(string username)
        {
            var driver = GuiDriver.GetDriver();
            var member = driver.FindElementByName(username);
            Assert.IsNotNull(member);

            GuiDriver.Dispose();
        }

        [Then(@"the table without that member ""([^""]*)""")]
        public void ThenTheTableWithoutThatMember(string username)
        {
            var driver = GuiDriver.GetDriver();
            var member = driver.FindElementsByName(username);
            bool exists = member.Count != 0;
            Assert.IsFalse(exists);

            GuiDriver.Dispose();
        }
    }
}
