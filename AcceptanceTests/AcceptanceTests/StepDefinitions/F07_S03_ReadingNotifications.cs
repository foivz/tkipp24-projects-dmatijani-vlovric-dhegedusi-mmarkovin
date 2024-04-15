using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F07_S03_ReadingNotifications
    {
        [Given(@"Given the user is logged into app as member")]
        public void GivenGivenTheUserIsLoggedIntoAppAsMember()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");

            txtUsername.SendKeys("mpranjic23");
            txtPassword.SendKeys("pranjicka98");
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();
        }

        [Given(@"the member is on members panel")]
        public void GivenTheMemberIsOnMembersPanel()
        {

            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            bool isCorrectTitle = driver.Title == "MyLibra";
            Assert.IsTrue(isCorrectTitle);
        }

        [When(@"member clicks Notifocation buttons")]
        public void WhenMemberClicksNotifocationButtons()
        {
            var driver = GuiDriver.GetDriver();
            driver.Manage().Window.Maximize();
            var btnNotifications = driver.FindElementByAccessibilityId("btnNotifications");
            btnNotifications.Click();
        }

        [Then(@"the notification control should appear")]
        public void ThenTheNotificationControlShouldAppear()
        {
            var driver = GuiDriver.GetDriver();
            driver.Manage().Window.Maximize();
            var notificationsControl = driver.FindElementByName("Sve obavijesti");
            Assert.IsNotNull(notificationsControl);

            GuiDriver.Dispose();   
        }

        [Given(@"the member is on notifications control")]
        public void GivenTheMemberIsOnNotificationsControl()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            var btnNotifications = driver.FindElementByAccessibilityId("btnNotifications");
            btnNotifications.Click();

            var notificationsControl = driver.FindElementByName("Sve obavijesti");
            Assert.IsNotNull(notificationsControl);
        }

        [When(@"member clicks Read button")]
        public void WhenMemberClicksReadButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnReadNotif = driver.FindElementByAccessibilityId("btnReadNotif");
            btnReadNotif.Click();
        }

        [Then(@"all read notifications should appear")]
        public void ThenAllReadNotificationsShouldAppear()
        {
            // do nothing

            GuiDriver.Dispose();
        }

        [When(@"member clicks Unread button")]
        public void WhenMemberClicksUnreadButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnUnreadNotif = driver.FindElementByAccessibilityId("btnUnreadNotif");
            btnUnreadNotif.Click();
        }

        [Then(@"all unread notifications should appear")]
        public void ThenAllUnreadNotificationsShouldAppear()
        {
            // do nothing

            GuiDriver.Dispose();
        }

        [When(@"member clicks Details button")]
        public void WhenMemberClicksDetailsButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnNotificationDetails = driver.FindElementByAccessibilityId("btnNotificationDetails");
            btnNotificationDetails.Click();
        }

        [Then(@"an error message should appear wit message ""([^""]*)""")]
        public void ThenAnErrorMessageShouldAppearWitMessage(string error)
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnOK = driver.FindElementByName("OK");
            Assert.IsNotNull(btnOK);
            btnOK.Click();

            GuiDriver.Dispose();
        }

        [When(@"selects notification with ""([^""]*)""")]
        public void WhenSelectsNotificationWith(string title)
        {
            var driver = GuiDriver.GetDriver();
            var selectedNotif = driver.FindElementByName(title);
            Assert.IsNotNull(selectedNotif);

            selectedNotif.Click();
        }

        [Then(@"selected notification detail control should apear ""([^""]*)""")]
        public void ThenSelectedNotificationDetailControlShouldApear(string title)
        {
            var driver = GuiDriver.GetDriver();
            var selectedNotif = driver.FindElementByName(title);
            Assert.IsNotNull(selectedNotif);

            GuiDriver.Dispose();
        }
    }
}
