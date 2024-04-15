using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F02_S01_UserLogout
    {
        [Given(@"the user is logged into app")]
        public void GivenTheUserIsLoggedIntoApp()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");

            txtUsername.SendKeys("pcindric89");
            txtPassword.SendKeys("cindricka123");
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();
        }

        [When(@"the user clicks Logout button")]
        public void WhenTheUserClicksLogoutButton()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            var btnLogout = driver.FindElementByAccessibilityId("btnLogout");
            btnLogout.Click();
            driver.SwitchTo().Window(driver.WindowHandles.First());
        }

        [Then(@"the user should see the Login form")]
        public void ThenTheUserShouldSeeTheLoginForm()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool isCorrectTitle = driver.Title == "Login";
            Assert.IsTrue(isCorrectTitle);

            GuiDriver.Dispose();
        }

    }
}
