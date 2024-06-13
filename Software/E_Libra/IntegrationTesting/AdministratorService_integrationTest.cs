using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using BussinessLogicLayer.services;
using EntitiesLayer;
using System.Net.Http.Headers;

namespace IntegrationTesting
{
    [Collection("Database collection")]
    public class AdministratorService_integrationTest
    {
        private readonly AdministratorService administratorService;
        private readonly DatabaseFixture fixture;

        public AdministratorService_integrationTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
            this.fixture.ResetDatabase();

            administratorService = new AdministratorService();

            LoggedUser.Username = null;
            LoggedUser.UserType = null;
            LoggedUser.LibraryId = 1;
        }

        //David Matijanić
        [Theory]
        [InlineData("admin1", "mmarkovin21")]
        [InlineData("admin2", "dmatijani21")]
        [InlineData("admin3", "vlovric21")]
        [InlineData("admin4", "dhegedusi21")]
        public void CheckLoginCredentials_GivenExistingLoginInformation_ReturnsSuccesfullLogin(string username, string password)
        {
            // Act
            administratorService.CheckLoginCredentials(username, password);

            // Assert
            Assert.Equal(username, LoggedUser.Username);
            Assert.Equal(Role.Admin, LoggedUser.UserType);
        }

        //David Matijanić
        [Fact]
        public void CheckLoginCredentials_GivenNonExistingUserInformation_ReturnsUnsucessfulLogin()
        {
            // Arrange
            string username = "invalidUsername";
            string password = "password123";

            // Act
            administratorService.CheckLoginCredentials(username, password);

            // Assert
            Assert.NotEqual(username, LoggedUser.Username);
            Assert.NotEqual(Role.Admin, LoggedUser.UserType);
        }
    }
}
