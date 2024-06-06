using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F07_S01_WritingNotificationsStepDefinitions
    {
        [Given(@"Given the user is logged into app as employee")]
        public void GivenGivenTheUserIsLoggedIntoAppAsEmployee()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");

            txtUsername.SendKeys("pcindric89");
            txtPassword.SendKeys("cindricka123");
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();
        }

        [Given(@"is on the notifications panel")]
        public void GivenIsOnTheNotificationsPanel()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.Manage().Window.Maximize();
            var btnNotifications = driver.FindElementByAccessibilityId("btnNotifications");
            btnNotifications.Click();

            var notificationsControl = driver.FindElementByName("Sve obavijesti");
            Assert.IsNotNull(notificationsControl);
        }

        [When(@"clicks buton New notification")]
        public void WhenClicksButonNewNotification()
        {
            var driver = GuiDriver.GetDriver();
            var btnNewNotification = driver.FindElementByAccessibilityId("btnNewNotification");
            btnNewNotification.Click();
        }

        [When(@"the New notification screen appears")]
        public void WhenTheNewNotificationScreenAppears()
        {
            var driver = GuiDriver.GetDriver();
            var newNotificationsControl = driver.FindElementByName("Nova obavijest");
            Assert.IsNotNull(newNotificationsControl);
        }

        [When(@"the employee enters title (.*) and description (.*)")]
        public void WhenTheEmployeeEntersTitleAndDescription(string title, string description)
        {
            var driver = GuiDriver.GetDriver();
            var txtTitle = driver.FindElementByAccessibilityId("txtTitle");
            var txtDescription = driver.FindElementByAccessibilityId("txtDescription");

            txtTitle.SendKeys(title);
            txtDescription.SendKeys(description);
        }

        [When(@"clisks save button")]
        public void WhenClisksSaveButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnSave = driver.FindElementByAccessibilityId("btnSave");
            btnSave.Click();

            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnOK = driver.FindElementByName("OK");
            btnOK.Click();
        }

        [Then(@"tables last row should contain notification with (.*) and (.*)")]
        public void ThenTablesLastRowShouldContainNotifiCationWithAnd(string title, string description)
        {
            var driver = GuiDriver.GetDriver();
            var newNotificationTitle = driver.FindElementByName(title);
            var newNotificationDescription = driver.FindElementByName(description);
            Assert.IsNotNull(newNotificationTitle);
            Assert.IsNotNull(newNotificationDescription);

            GuiDriver.Dispose();
        }

        [When(@"clisks cancel button")]
        public void WhenClisksCancelButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnCancel = driver.FindElementByAccessibilityId("btnCancel");
            btnCancel.Click();
        }

        [Then(@"Then tables should not contain the message")]
        public void ThenThenTablesShouldNotContainTheMessage()
        {
            var driver = GuiDriver.GetDriver();
            var notificationsControl = driver.FindElementByName("Sve obavijesti");
            Assert.IsNotNull(notificationsControl);

            var newNotification = driver.FindElementsByName("Jerkolilili");
            bool exsists = newNotification.Count != 0;
            Assert.IsFalse(exsists);

            GuiDriver.Dispose();
        }
    }
}
