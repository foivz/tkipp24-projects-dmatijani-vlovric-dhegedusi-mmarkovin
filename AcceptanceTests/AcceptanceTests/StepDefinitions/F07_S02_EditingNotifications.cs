using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F07_S02_EditingNotifications
    {
        [Given(@"the user is logged into app with epmloyee credentials")]
        public void GivenTheUserIsLoggedIntoAppWithEpmloyeeCredentials()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var txtUsername = driver.FindElementByAccessibilityId("txtUsername");
            var txtPassword = driver.FindElementByAccessibilityId("txtPassword");

            txtUsername.SendKeys("hmihovic");
            txtPassword.SendKeys("volimcitati");
            var btnLogin = driver.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();
        }

        [Given(@"is on the notifications control")]
        public void GivenIsOnTheNotificationsControl()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.Manage().Window.Maximize();
            var btnNotifications = driver.FindElementByAccessibilityId("btnNotifications");
            btnNotifications.Click();

            var notificationsControl = driver.FindElementByName("Sve obavijesti");
            Assert.IsNotNull(notificationsControl);
        }

        [When(@"employee clicks edit button")]
        public void WhenEmployeeClicksEditButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnNotificationUpdate = driver.FindElementByAccessibilityId("btnNotificationUpdate");
            btnNotificationUpdate.Click();
        }

        [Then(@"the error message should appear ""([^""]*)""")]
        public void ThenTheErrorMessageShouldAppear(string title)
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

        [When(@"the employee selects member with title ""([^""]*)""")]
        public void WhenTheEmployeeSelectsMemberWithTitle(string title)
        {
            var driver = GuiDriver.GetDriver();
            var dgvElement = driver.FindElementByName(title);
            dgvElement.Click();

        }

        [When(@"enters new title ""([^""]*)""")]
        public void WhenEntersNewTitle(string newTitle)
        {
            var driver = GuiDriver.GetDriver();
            var txtTitle = driver.FindElementByAccessibilityId("txtTitle");

            txtTitle.Clear();
            txtTitle.SendKeys(newTitle);
        }

        [When(@"clicks Save button")]
        public void WhenClicksSaveButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnSave = driver.FindElementByAccessibilityId("btnSave");
            btnSave.Click();

            var btnOK = driver.FindElementByName("OK");
            Assert.IsNotNull(btnOK);
            btnOK.Click();

        }

        [Then(@"the employee should see notifications window")]
        public void ThenTheEmployeeShouldSeeNotificationsWindow()
        {
            var driver = GuiDriver.GetDriver();
            var notificationsControl = driver.FindElementByName("Sve obavijesti");
            Assert.IsNotNull(notificationsControl);

        }

        [Then(@"the last row in table should have notification with title ""([^""]*)""")]
        public void ThenTheLastRowInTableShouldHaveNotificationWithTitle(string title)
        {
            var driver = GuiDriver.GetDriver();
            var editedNotification = driver.FindElementByName(title);
            Assert.IsNotNull(editedNotification);

            GuiDriver.Dispose();
        }

        [When(@"clicks Cancel button")]
        public void WhenClicksCancelButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnCancel = driver.FindElementByAccessibilityId("btnCancel");
            btnCancel.Click();
        }

        [Then(@"the table should not contain edited notification")]
        public void ThenTheTableShouldNotContainEditedNotification()
        {

            var driver = GuiDriver.GetDriver();
            var editedNotification = driver.FindElementsByName("Drugi naslov");
            bool exsists = editedNotification.Count != 0;
            Assert.IsFalse(exsists);

            GuiDriver.Dispose();
        }
    }
}
