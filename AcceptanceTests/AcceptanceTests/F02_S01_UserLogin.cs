using AcceptanceTests.Support;
using System;
using TechTalk.SpecFlow;

namespace AcceptanceTests
{
    [Binding]
    public class F02_S01_UserLogin
    {
        [Given(@"the user is on the login form")]
        public void GivenTheUserIsOnTheLoginForm()
        {
            var driver = GuiDriver.GetOrCreateDriver();
        }

        [When(@"the user enters noUsername and cindricka(.*)")]
        public void WhenTheUserEntersNoUsernameAndCindricka(int p0)
        {
            throw new PendingStepException();
        }

        [When(@"the user clicks the Login button")]
        public void WhenTheUserClicksTheLoginButton()
        {
            throw new PendingStepException();
        }

        [Then(@"the user shold see Unijeli ste krive korisničke podatke! message")]
        public void ThenTheUserSholdSeeUnijeliSteKriveKorisnickePodatkeMessage()
        {
            throw new PendingStepException();
        }

        [When(@"the user enters pcindric(.*) and cindricka(.*)")]
        public void WhenTheUserEntersPcindricAndCindricka(int p0, int p1)
        {
            throw new PendingStepException();
        }

        [Then(@"the user should see specific employee window")]
        public void ThenTheUserShouldSeeSpecificEmployeeWindow()
        {
            throw new PendingStepException();
        }
    }
}
