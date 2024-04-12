using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F01_S06_AddingANewEmployeeStepDefinitions
    {
        [Given(@"the user is on the New employee screen")]
        public void GivenTheUserIsOnTheNewEmployeeScreen()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnNewEmployee = driver.FindElementByAccessibilityId("btnNewEmployee");
            btnNewEmployee.Click();

            var newEmployeeTitle = driver.FindElementByName("Dodavanje novog zaposlenika");
            Assert.IsNotNull(newEmployeeTitle);
        }

        [When(@"the library for employee is not chosen")]
        public void WhenTheLibraryForEmployeeIsNotChosen()
        {
            // Does nothing
        }

        [When(@"the user enters a correct employee OIB")]
        public void WhenTheUserEntersACorrectEmployeeOIB()
        {
            var driver = GuiDriver.GetDriver();
            var tbEmployeeOIB = driver.FindElementByAccessibilityId("tbEmployeeOIB");
            tbEmployeeOIB.SendKeys("99999987321");
        }

        [When(@"the user enters an employee name")]
        public void WhenTheUserEntersAnEmployeeName()
        {
            var driver = GuiDriver.GetDriver();
            var tbEmployeeName = driver.FindElementByAccessibilityId("tbEmployeeName");
            tbEmployeeName.SendKeys("Novic");
        }

        [When(@"the user enters a correct employee username")]
        public void WhenTheUserEntersACorrectEmployeeUsername()
        {
            var driver = GuiDriver.GetDriver();
            var tbEmployeeUsername = driver.FindElementByAccessibilityId("tbEmployeeUsername");
            tbEmployeeUsername.SendKeys("novic99");
        }

        [When(@"the user enters a correct employee password")]
        public void WhenTheUserEntersACorrectEmployeePassword()
        {
            var driver = GuiDriver.GetDriver();
            var tbEmployeePassword = driver.FindElementByAccessibilityId("tbEmployeePassword");
            tbEmployeePassword.SendKeys("secretpw");
        }

        [When(@"the user clicks the Save new employee button")]
        public void WhenTheUserClicksTheSaveNewEmployeeButton()
        {
            var driver = GuiDriver.GetDriver();
            var btnSave = driver.FindElementByAccessibilityId("btnAddNewLibrary");
            btnSave.Click();
        }

        [Then(@"the system should show an error message that the employee can't be added")]
        public void ThenTheSystemShouldShowAnErrorMessageThatTheEmployeeCantBeAdded()
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

        [When(@"the employee OIB is not entered")]
        public void WhenTheEmployeeOIBIsNotEntered()
        {
            // Does nothing
        }

        [When(@"the library for employee is chosen")]
        public void WhenTheLibraryForEmployeeIsChosen()
        {
            var driver = GuiDriver.GetDriver();
            var cboLibrary = driver.FindElementByAccessibilityId("cboLibrary");
            cboLibrary.Click();

            var chosenLibrary = driver.FindElementByName("456 - Gradska knjižnica Slatina");
            chosenLibrary.Click();
        }

        [When(@"the employee OIB (.*) is entered")]
        public void WhenTheEmployeeOIBIsEntered(string p0)
        {
            var driver = GuiDriver.GetDriver();
            var tbEmployeeOIB = driver.FindElementByAccessibilityId("tbEmployeeOIB");
            tbEmployeeOIB.SendKeys(p0);
        }

        [When(@"the employee username is not entered")]
        public void WhenTheEmployeeUsernameIsNotEntered()
        {
            // Does nothing
        }

        [When(@"the employee username (.*) is entered")]
        public void WhenTheEmployeeUsernameIsEntered(string p0)
        {
            var driver = GuiDriver.GetDriver();
            var tbEmployeeUsername = driver.FindElementByAccessibilityId("tbEmployeeUsername");
            tbEmployeeUsername.SendKeys(p0);
        }

        [When(@"the user doesn't enter the employee password")]
        public void WhenTheUserDoesntEnterTheEmployeePassword()
        {
            // Does nothing
        }

        [Then(@"the employee should be visible in the All employees list for the chosen library")]
        public void ThenTheEmployeeShouldBeVisibleInTheAllEmployeesListForTheChosenLibrary()
        {
            var driver = GuiDriver.GetDriver();
            var dgAllEmployees = driver.FindElementByAccessibilityId("dgAllEmployees");
            Assert.IsNotNull(dgAllEmployees);

            var newEmployeeOIB = driver.FindElementByName("99999987321");
            Assert.IsNotNull(newEmployeeOIB);

            GuiDriver.Dispose();
        }
    }
}
