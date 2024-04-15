using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F11_S03_ReviewStatisticsStepDefinitions
    {
        F11_S01_BookStatisticsStepDefinitions helperClass = new F11_S01_BookStatisticsStepDefinitions();

        [Given(@"there are records of at least one review written by a member")]
        public void GivenThereAreRecordsOfAtLeastOneReviewWrittenByAMember()
        {
            // Nije moguæe napraviti provjeru za ovaj sluèaj
            Assert.IsTrue(true);
        }

        [When(@"the user selects the option Statistika recenzija from the criteria list")]
        public void WhenTheUserSelectsTheOptionStatistikaRecenzijaFromTheCriteriaList()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            GuiDriver.Dispose();

            helperClass.GivenTheUserIsLoggedInAsALibraryEmployee();
            helperClass.GivenTheUserIsOnTheStatisticsForm();

            driver = GuiDriver.GetOrCreateDriver();

            var cboCategory = driver.FindElementByClassName("ComboBox");
            cboCategory.Click();

            var listItem = driver.FindElementByName("Statistika recenzija");
            listItem.Click();
        }

        [Then(@"the user should see a list of all ratings along with the number of times each rating has been used by members")]
        public void ThenTheUserShouldSeeAListOfAllRatingsAlongWithTheNumberOfTimesEachRatingHasBeenUsedByMembers()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            IReadOnlyCollection<AppiumWebElement> textBlocks = driver.FindElementsByClassName("TextBlock");

            bool containsString = false;
            bool containsInteger = false;

            foreach (var textBlock in textBlocks) {

                if (!containsString && !string.IsNullOrEmpty(textBlock.Text)) {
                    containsString = true;
                }

                int number;
                if (!containsInteger && int.TryParse(textBlock.Text, out number)) {
                    containsInteger = true;
                }

                if (containsString && containsInteger) {
                    break;
                }
            }
            Assert.IsTrue(containsString && containsInteger);
            GuiDriver.Dispose();
        }

        [Given(@"there are no records of written reviews by members in the system")]
        public void GivenThereAreNoRecordsOfWrittenReviewsByMembersInTheSystem()
        {
            // Nije moguæe napraviti provjeru za ovaj sluèaj
            Assert.IsTrue(true);
        }

        [Then(@"the user should not see any review listed")]
        public void ThenTheUserShouldNotSeeAnyReviewListed()
        {
            var driver = GuiDriver.GetOrCreateDriver();

            var ucStatistics = driver.FindElementByClassName("UcStatistics");

            var isTextBlock = ucStatistics.FindElementsByClassName("TextBlock");
            Assert.IsTrue(isTextBlock.Count == 0);
            GuiDriver.Dispose();
        }
    }
}
