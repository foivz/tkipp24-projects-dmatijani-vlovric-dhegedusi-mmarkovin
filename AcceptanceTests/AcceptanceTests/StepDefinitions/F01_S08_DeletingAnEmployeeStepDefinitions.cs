using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F01_S08_DeletingAnEmployeeStepDefinitions
    {
        [Then(@"the system should show an error message that it cannot delete the employee selection")]
        public void ThenTheSystemShouldShowAnErrorMessageThatItCannotDeleteTheEmployeeSelection()
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

        [When(@"the user chooses one employee to delete")]
        public void WhenTheUserChoosesOneEmployeeToDelete()
        {
            var driver = GuiDriver.GetDriver();
            var employeeToDelete = driver.FindElementByName("99999987321");
            employeeToDelete.Click();
        }

        [When(@"the user clicks the Delete employee button")]
        public void WhenTheUserClicksTheDeleteEmployeeButton()
        {
            var driver = GuiDriver.GetDriver();
            driver.SwitchTo().Window(driver.WindowHandles.First());

            var btnDeleteEmployee = driver.FindElementByAccessibilityId("btnRemoveEmployee");
            btnDeleteEmployee.Click();
        }

        [Then(@"the system should warn the user before deleting an employee")]
        public void ThenTheSystemShouldWarnTheUserBeforeDeletingAnEmployee()
        {
            var driver = GuiDriver.GetDriver();
            var oldDriver = driver;
            Assert.IsNotNull(driver);
            driver.SwitchTo().Window(driver.WindowHandles.First());
            Assert.IsNotNull(driver);
            Assert.AreNotEqual(driver, oldDriver);
        }

        [Then(@"the system should delete the selected employee")]
        public void ThenTheSystemShouldDeleteTheSelectedEmployee()
        {
            // Currently nothing (deletes automatically)
        }

        [When(@"the user chooses multiple employees to delete")]
        public void WhenTheUserChoosesMultipleEmployeesToDelete()
        {
            var driver = GuiDriver.GetDriver();
            var employeeToDelete1 = driver.FindElementByName("12312312334");
            var employeeToDelete2 = driver.FindElementByName("12345678901");

            Actions action = new Actions(driver);
            action.KeyDown(Keys.LeftControl)
                  .Click(employeeToDelete1)
                  .Click(employeeToDelete2)
                  .KeyUp(Keys.LeftControl)
                  .Perform();
        }
    }
}
