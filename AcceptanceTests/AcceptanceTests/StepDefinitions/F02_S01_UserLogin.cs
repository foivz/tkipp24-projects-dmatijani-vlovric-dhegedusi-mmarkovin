using System;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class F02_S01_UserLogin
    {
        [Given(@"the user is on the login form")]
        public void GivenTheUserIsOnTheLoginForm()
        {
            throw new PendingStepException();
        }

        [When(@"the user enters username megi and password megi(.*)")]
        public void WhenTheUserEntersUsernameMegiAndPasswordMegi(int p0)
        {
            throw new PendingStepException();
        }

        [When(@"the user clicks the Login button")]
        public void WhenTheUserClicksTheLoginButton()
        {
            throw new PendingStepException();
        }

        [Then(@"the user shold see Članarina je istekla! Članarinu možete produljiti u svojoj knjižnici\. message")]
        public void ThenTheUserSholdSeeClanarinaJeIsteklaClanarinuMozeteProduljitiUSvojojKnjiznici_Message()
        {
            throw new PendingStepException();
        }

        [When(@"the user enters username pcindric(.*) and password cindricka(.*)")]
        public void WhenTheUserEntersUsernamePcindricAndPasswordCindricka(int p0, int p1)
        {
            throw new PendingStepException();
        }

        [Then(@"the user should see employee window")]
        public void ThenTheUserShouldSeeEmployeeWindow()
        {
            throw new PendingStepException();
        }
    }
}
