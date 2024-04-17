using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AcceptanceTests.Support
{
    public static class MessageBoxTestHelper {
        public static void CheckIfMessageBoxIsShown() {
            var driver = GuiDriver.GetDriver();
            Assert.IsNotNull(driver);
            driver.SwitchTo().Window(driver.WindowHandles.First());
            Assert.IsNotNull(driver);

            var btnOK = driver.FindElementByName("OK");
            Assert.IsNotNull(btnOK);
            btnOK.Click();
            GuiDriver.Dispose();
        }
    }
}
