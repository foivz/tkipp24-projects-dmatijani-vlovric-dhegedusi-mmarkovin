﻿using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
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
        private ILibraryRepository libraryRepository;

        public F14_unitTest()
        {
            membersRepository = A.Fake<IMembersRepository>();
            libraryRepository = A.Fake<ILibraryRepository>();
            memberService = new MemberService(membersRepository, libraryRepository, null, null, null);

            LoggedUser.Username = null;
            LoggedUser.UserType = null;
            LoggedUser.LibraryId = 0;
        }

        private void SetLoggedUser()
        {
            LoggedUser.Username = "username";
            LoggedUser.LibraryId = 1;
        }

        [Fact]
        public void CalculateDaysUntilExpiration_GivenMembershipExpiresIn5days_ReturnDaysUntilexpiration()
        {
            // Arrange
            SetLoggedUser();
            DateTime? expirationDate = DateTime.Today.AddDays(5);
            A.CallTo(() => membersRepository.GetMembershipDate(LoggedUser.Username)).Returns(expirationDate);
            A.CallTo(() => libraryRepository.GetLibraryMembershipDuration(LoggedUser.LibraryId)).Returns(DateTime.Today.AddDays(5));

            // Act
            var daysUntilExpiration = memberService.CalculateDaysUntilExpiration();

            // Assert
            Assert.Equal(5, daysUntilExpiration);
        }


        [Fact]

        public void CalculateDaysUntilExpiration_GivenInvalidUsername_EqualsNull()
        {
            // Arrange
            string username = "nonexisting";
            A.CallTo(() => membersRepository.GetMembershipDate(username)).Returns((DateTime?)null);

            // Act
            var daysUntilExpiration = memberService.CalculateDaysUntilExpiration();

            // Assert
            Assert.Equal(daysUntilExpiration, 0);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void CalculateDaysUntilExpiration_GivenMembershipExpiresInLessThan5days_ReturnDaysUntilexpiration(int daysUntilExpiration)
        {
            // Arrange
            SetLoggedUser();
            DateTime? expirationDate = DateTime.Today.AddDays(daysUntilExpiration);
            A.CallTo(() => libraryRepository.GetLibraryMembershipDuration(1)).Returns(DateTime.Today.AddDays(daysUntilExpiration));
            A.CallTo(() => membersRepository.GetMembershipDate(LoggedUser.Username)).Returns(expirationDate);

            // Act
            var result = memberService.CalculateDaysUntilExpiration();

            // Assert
            Assert.Equal(daysUntilExpiration, result);
        }

        [Fact]
        public void CalculateDaysUntilExpiration_GivenMembershipExpiresIn6days_ReturnsIncorrectDaysUntilExpiration()
        {
            // Arrange
            SetLoggedUser();
            DateTime? expirationDate = DateTime.Today.AddDays(6);
            A.CallTo(() => membersRepository.GetMembershipDate(LoggedUser.Username)).Returns(expirationDate);
            A.CallTo(() => libraryRepository.GetLibraryMembershipDuration(LoggedUser.LibraryId)).Returns(DateTime.Today.AddDays(6));

            // Act
            var daysUntilExpiration = memberService.CalculateDaysUntilExpiration();

            // Assert
            Assert.Equal(daysUntilExpiration, 0);
        }

    }
}
