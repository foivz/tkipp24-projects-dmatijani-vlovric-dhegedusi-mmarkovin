using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F01_S07_EditingEmployeeInfoStepDefinitions
    {
        [Given(@"the user chooses a library with employees")]
        public void GivenTheUserChoosesALibraryWithEmployees()
        {
            var driver = GuiDriver.GetDriver();
            var cboLibrary = driver.FindElementByAccessibilityId("cboLibrary");
            cboLibrary.Click();

            var library = driver.FindElementByName("456 - Gradska knjižnica Slatina");
            library.Click();
        }

        [Given(@"the user chooses an employee from the All employees list")]
        public void GivenTheUserChoosesAnEmployeeFromTheAllEmployeesList()
        {
            var driver = GuiDriver.GetDriver();
            var employee = driver.FindElementByName("mjakic2");
            employee.Click();
        }

        [Given(@"the user clicks on the Edit employee button")]
        public void GivenTheUserClicksOnTheEditEmployeeButton()
        {
            var driver = GuiDriver.GetDriver();
            var editButton = driver.FindElementByAccessibilityId("btnEditEmployee");
            editButton.Click();
        }

        [When(@"the user removes the employee name")]
        public void WhenTheUserRemovesTheEmployeeName()
        {
            var driver = GuiDriver.GetDriver();
            var tbEmployeeName = driver.FindElementByAccessibilityId("tbEmployeeName");
            for (int i = 0; i < 12; i++) {
                tbEmployeeName.SendKeys(Keys.Backspace);
                tbEmployeeName.SendKeys(Keys.Delete);
            }
        }

        [When(@"the user clicks the Save changes for the employee button")]
        public void WhenTheUserClicksTheSaveChangesForTheEmployeeButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnSaveChanges = driver.FindElementByAccessibilityId("btnAddNewLibrary");
            btnSaveChanges.Click();
        }

        [Then(@"the system should show the employee without the name")]
        public void ThenTheSystemShouldShowTheEmployeeWithoutTheName()
        {
            var driver = GuiDriver.GetDriver();
            var employeeOIB = driver.FindElementByName("12312312334");
            Assert.IsNotNull(employeeOIB);
        }

        [When(@"the user removes the employee surname")]
        public void WhenTheUserRemovesTheEmployeeSurname()
        {
            var driver = GuiDriver.GetDriver();
            var tbEmployeeSurname = driver.FindElementByAccessibilityId("tbEmployeeSurname");
            for (int i = 0; i < 12; i++) {
                tbEmployeeSurname.SendKeys(Keys.Backspace);
                tbEmployeeSurname.SendKeys(Keys.Delete);
            }
        }

        [Then(@"the system should show the employee without the surname")]
        public void ThenTheSystemShouldShowTheEmployeeWithoutTheSurname()
        {
            var driver = GuiDriver.GetDriver();
            var employeeOIB = driver.FindElementByName("12312312334");
            Assert.IsNotNull(employeeOIB);
        }

        [When(@"the user removes the employee username")]
        public void WhenTheUserRemovesTheEmployeeUsername()
        {
            var driver = GuiDriver.GetDriver();
            var tbEmployeeUsername = driver.FindElementByAccessibilityId("tbEmployeeUsername");
            for (int i = 0; i < 12; i++) {
                tbEmployeeUsername.SendKeys(Keys.Backspace);
                tbEmployeeUsername.SendKeys(Keys.Delete);
            }
        }

        [Then(@"the system should show an error message that the employee can't be modified")]
        public void ThenTheSystemShouldShowAnErrorMessageThatTheEmployeeCantBeModified()
        {
            MessageBoxTestHelper.CheckIfMessageBoxIsShown();
        }

        [When(@"the user removes the employee password")]
        public void WhenTheUserRemovesTheEmployeePassword()
        {
            var driver = GuiDriver.GetDriver();
            var tbEmployeePassword = driver.FindElementByAccessibilityId("tbEmployeePassword");
            for (int i = 0; i < 12; i++) {
                tbEmployeePassword.SendKeys(Keys.Backspace);
                tbEmployeePassword.SendKeys(Keys.Delete);
            }
        }

        [When(@"the user changes the employee's name")]
        public void WhenTheUserChangesTheEmployeesName()
        {
            WhenTheUserRemovesTheEmployeeName();
            var driver = GuiDriver.GetDriver();
            var tbEmployeeName = driver.FindElementByAccessibilityId("tbEmployeeName");
            tbEmployeeName.SendKeys("Mihalino");
        }

        [When(@"the user enters the employee surname")]
        public void WhenTheUserEntersTheEmployeeSurname()
        {
            WhenTheUserRemovesTheEmployeeSurname();
            var driver = GuiDriver.GetDriver();
            var tbEmployeeSurname = driver.FindElementByAccessibilityId("tbEmployeeSurname");
            tbEmployeeSurname.SendKeys("Jakovinovic");
        }

        [Then(@"the employee changes should be visible in the All employees list")]
        public void ThenTheEmployeeChangesShouldBeVisibleInTheAllEmployeesList()
        {
            var driver = GuiDriver.GetDriver();
            var dgAllEmployees = driver.FindElementByAccessibilityId("dgAllEmployees");
            Assert.IsNotNull(dgAllEmployees);

            var newEmployeeOIB = driver.FindElementByName("12312312334");
            Assert.IsNotNull(newEmployeeOIB);
            var newEmployeeName = driver.FindElementByName("Mihalino");
            Assert.IsNotNull(newEmployeeName);
            var newEmployeeSurname = driver.FindElementByName("Jakovinovic");
            Assert.IsNotNull(newEmployeeSurname);

            GuiDriver.Dispose();
        }
    }
}
