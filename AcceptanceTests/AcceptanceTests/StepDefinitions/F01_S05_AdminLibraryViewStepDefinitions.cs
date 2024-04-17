using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F01_S05_AdminLibraryViewStepDefinitions
    {
        [Given(@"the user is on the All employees screen")]
        public void GivenTheUserIsOnTheAllEmployeesScreen()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnAllLibraries = driver.FindElementByAccessibilityId("btnAllEmployees");
            btnAllLibraries.Click();

            var dgAllEmployees = driver.FindElementByAccessibilityId("dgAllEmployees");
            Assert.IsNotNull(dgAllEmployees);
        }

        [When(@"the administrator chooses a library with no employees")]
        public void WhenTheAdministratorChoosesALibraryWithNoEmployees()
        {
            var driver = GuiDriver.GetDriver();
            var cboLibrary = driver.FindElementByAccessibilityId("cboLibrary");
            cboLibrary.Click();

            var libraryWithNoEmployees = driver.FindElementByName("12343 - Knjnaziv");
            libraryWithNoEmployees.Click();
        }

        [Then(@"the user should be notified that there are no employees for the chosen library")]
        public void ThenTheUserShouldBeNotifiedThatThereAreNoEmployeesForTheChosenLibrary()
        {
            var driver = GuiDriver.GetDriver();
            var noEmployeesMessage = driver.FindElementByAccessibilityId("noEmployeesMessage");
            Assert.IsNotNull(noEmployeesMessage);
            GuiDriver.Dispose();
        }

        [When(@"the administrator chooses a library with atleast one employee")]
        public void WhenTheAdministratorChoosesALibraryWithAtleastOneEmployee()
        {
            var driver = GuiDriver.GetDriver();
            var cboLibrary = driver.FindElementByAccessibilityId("cboLibrary");
            cboLibrary.Click();

            var libraryWithEmployees = driver.FindElementByName("123 - Gradska knjižnica i citaonica Varaždin");
            libraryWithEmployees.Click();
        }

        [Then(@"the user should be shown the employees for that library")]
        public void ThenTheUserShouldBeShownTheEmployeesForThatLibrary()
        {
            var driver = GuiDriver.GetDriver();
            var firstEmployee = driver.FindElementByName("pcindric89");
            Assert.IsNotNull(firstEmployee);
            GuiDriver.Dispose();
        }
    }
}
