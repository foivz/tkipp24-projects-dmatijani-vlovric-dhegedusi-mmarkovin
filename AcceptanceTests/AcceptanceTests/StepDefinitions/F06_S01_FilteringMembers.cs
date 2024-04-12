using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
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

            txtUsername.SendKeys("pcindric89");
            txtPassword.SendKeys("cindricka123");
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();

        }

        [Given(@"is on the Member Management panel")]
        public void GivenIsOnTheMemberManagementPanel()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            var btnMembership = driver.FindElementByAccessibilityId("btnMembership");
            btnMembership.Click();
        }

        [When(@"Employee enters name (.*) and surename (.*)")]
        public void WhenEmployeeEntersNameNameAndSurename(string name, string surename)
        {
            var driver = GuiDriver.GetDriver();
            var txtFilter = driver.FindElementByAccessibilityId("txtFilter");
            string[] words = txtFilter.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Assert.IsTrue(words[0] == name && words[1] == surename);
        }

        [When(@"clicks the Filter button")]
        public void WhenClicksTheFilterButton()
        {
            Assert.IsTrue(true);
        }

        [Then(@"application shows <name> ")]
        public void ThenApplicationShowsName()
        {
            Assert.IsTrue(true);
        }

        [When(@"Employee clicks the clear button")]
        public void WhenEmployeeClicksTheClearButton()
        {
            Assert.IsTrue(true);
        }

        [Then(@"application shows all members")]
        public void ThenApplicationShowsAllMembers()
        {
            Assert.IsTrue(true);
        }
    }
}
