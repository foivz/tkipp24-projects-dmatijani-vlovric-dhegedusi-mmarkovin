using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F02_S01_UserLogin
    {
        [Given(@"the user is on the login form")]
        public void GivenTheUserIsOnTheLoginForm()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            bool isCorrectTitle = driver.Title == "Login";
            Assert.IsTrue(isCorrectTitle);
        }

        [When(@"the user enters username (.*) and password (.*)")]
        public void WhenTheUserEntersUsernameMegiAndPasswordMegi(string username, string password)
        {
            var driver = GuiDriver.GetDriver();

            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");

            txtUsername.SendKeys(username);
            txtPassword.SendKeys(password);
        }

        [When(@"the user clicks the Login button")]
        public void WhenTheUserClicksTheLoginButton()
        {
            var driver = GuiDriver.GetDriver();

            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();
        }

        [Then(@"the user shold see (.*) message")]
        public void ThenTheUserSholdSeeErrorMessage(string error)
        {
            var driver = GuiDriver.GetDriver();
            Assert.IsNotNull(driver);
            driver.SwitchTo().Window(driver.WindowHandles.First());
            Assert.IsNotNull(driver);

            var btnOK = driver.FindElementByName("OK");
            Assert.IsNotNull(btnOK);
            btnOK.Click();
            GuiDriver.Dispose();

        }

        [Then(@"the user should see employee window")]
        public void ThenTheUserShouldSeeEmployeeWindow()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool isCorrectTitle = driver.Title == "EmployeePanel";
            Assert.IsTrue(isCorrectTitle);
        }
    }
}
