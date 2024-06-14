using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using EntitiesLayer;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting.Nove_funkcionalnosti.F14
{
    // Magdalena Markovinović
    public class F14_unitTest
    {
        private MemberService memberService;
        private IMembersRepository membersRepository;

        public F14_unitTest()
        {
            membersRepository = A.Fake<IMembersRepository>();
            memberService = new MemberService(membersRepository, null, null, null, null);

            LoggedUser.Username = null;
            LoggedUser.UserType = null;
            LoggedUser.LibraryId = 0;
        }

        [Fact]
        public void MembershipExpieringSoon_GivenMembershipExpiresIn5days_ReturnDaysUntilexpiration()
        {
            // Arrange
            string username = "username";
            DateTime? expirationDate = DateTime.Today.AddDays(5);
            A.CallTo(() => membersRepository.GetMembershipDate(username)).Returns(expirationDate);

            // Act
            var daysUntilExpiration = memberService.MembershipExpieringSoon();

            // Assert
            Assert.Equal(5, daysUntilExpiration);
        }

        [Fact]

        public void MembershipExpieringSoon_GivenInvalidUsername_ReturnsNull()
        {
            // Arrange
            string username = "nonexisting";
            A.CallTo(() => membersRepository.GetMembershipDate(username)).Returns(null);

            // Act
            var daysUntilExpiration = memberService.MembershipExpieringSoon();

            // Assert
            Assert.Null(daysUntilExpiration);
        }
    }
}
