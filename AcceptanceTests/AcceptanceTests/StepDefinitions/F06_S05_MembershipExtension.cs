using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F06_S05_MembershipExtension
    {
        [Given(@"Given the user is logged into app as employee hmihovic")]
        public void GivenGivenTheUserIsLoggedIntoAppAsEmployeeHmihovic()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");

            txtUsername.SendKeys("hmihovic");
            txtPassword.SendKeys("volimcitati");
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();
        }

        [When(@"employee clicks extend button")]
        public void WhenEmployeeClicksExtendButton()
        {
            var driver = GuiDriver.GetDriver();

            System.Threading.Thread.Sleep(1000);
            var btnMembership = driver.FindElementByName("Produljenje èlanstva");
            btnMembership.Click();
        }

        [Then(@"the error should appear")]
        public void ThenTheErrorShouldAppear()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnOK = driver.FindElementByName("OK");
            Assert.IsNotNull(btnOK);
            btnOK.Click();

            GuiDriver.Dispose();
        }

        [When(@"the employee selects member with username ""([^""]*)""")]
        public void WhenTheEmployeeSelectsMemberWithUsername(string username)
        {
            var driver = GuiDriver.GetDriver();
            var selectedMember = driver.FindElementByName(username);
            selectedMember.Click();
        }

        [Then(@"the members menagment window should appear")]
        public void ThenTheMembersMenagmentWindowShouldAppear()
        {
            var driver = GuiDriver.GetDriver();
            var membersControl = driver.FindElementByName("Èlanovi knjižnice");
            Assert.IsNotNull(membersControl);
        }

        [Then(@"the membership date of that member should be today's date ""([^""]*)""")]
        public void ThenTheMembershipDateOfThatMemberShouldBeTodaysDate(string date)
        {
            var driver = GuiDriver.GetDriver();
            var todaysDate = driver.FindElementsByName("14/4/2024");
            bool exists = todaysDate.Count != 0;
            Assert.IsNotNull(exists);

            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnOK = driver.FindElementByName("OK");
            Assert.IsNotNull(btnOK);
            btnOK.Click();

            GuiDriver.Dispose();
        }
    }
}
