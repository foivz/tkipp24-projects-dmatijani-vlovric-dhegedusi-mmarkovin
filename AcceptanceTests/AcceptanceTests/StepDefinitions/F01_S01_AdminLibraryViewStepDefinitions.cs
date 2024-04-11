using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F01_S01_AdminLibraryViewStepDefinitions
    {
        [Given(@"the user is logged in as an administrator")]
        public void GivenTheUserIsLoggedInAsAnAdministrator()
        {
            if (GuiDriver.GetDriver() != null) GuiDriver.Dispose();
            var driver = GuiDriver.GetOrCreateDriver();
            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");

            txtUsername.SendKeys("admin2");
            txtPassword.SendKeys("dmatijani21");

            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            Assert.IsNotNull(btnLogin);

            btnLogin.Click();
        }

        [Given(@"the user is on the All libraries screen")]
        public void GivenTheUserIsOnTheAllLibrariesScreen()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnAllLibraries = driver.FindElementByAccessibilityId("btnAllLibraries");
            btnAllLibraries.Click();

            var dgAllLibraries = driver.FindElementByAccessibilityId("dgAllLibraries");
            Assert.IsNotNull(dgAllLibraries);
        }

        [When(@"there are no libraries in the database")]
        public void WhenThereAreNoLibrariesInTheDatabase()
        {
            // Skip since can't be tested or adjusted to be that way
        }

        [Then(@"the user should know that the system was loading the libraries")]
        public void ThenTheUserShouldKnowThatTheSystemWasLoadingTheLibraries()
        {
            var driver = GuiDriver.GetDriver();
            var loader = driver.FindElementByAccessibilityId("Loading");
            Assert.IsNotNull(loader);
        }

        [Then(@"the user should be notified that there are no libraries")]
        public void ThenTheUserShouldBeNotifiedThatThereAreNoLibraries()
        {
            var driver = GuiDriver.GetDriver();
            var noLibrariesMessage = driver.FindElementByAccessibilityId("noLibrariesMessage");
            Assert.IsNotNull(noLibrariesMessage);
            GuiDriver.Dispose();
        }

        [When(@"there is at least one library in the database")]
        public void WhenThereIsAtLeastOneLibraryInTheDatabase()
        {
            // Skip since can't be tested or adjusted to be that way
        }

        [Then(@"the user should see all libraries")]
        public void ThenTheUserShouldSeeAllLibraries()
        {
            var driver = GuiDriver.GetDriver();
            var dgAllLibraries = driver.FindElementByAccessibilityId("dgAllLibraries");
            Assert.IsNotNull(dgAllLibraries);
            var firstLibrary = driver.FindElementByName("123 - Gradska knjižnica i citaonica Varaždin");
            Assert.IsNotNull(firstLibrary);
            GuiDriver.Dispose();
        }
    }
}
