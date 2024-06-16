using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using FakeItEasy;
using Xunit;

namespace UnitTesting {
    public class AdministratorService_unitTest {
        private readonly IAdministratorRepository administratorRepository;
        private AdministratorService administratorService;

        public AdministratorService_unitTest() {
            administratorRepository = A.Fake<IAdministratorRepository>();
            administratorService = new AdministratorService(administratorRepository);

            LoggedUser.Username = null;
            LoggedUser.UserType = null;
        }

        //David Matijanić
        [Fact]
        public void Constructor_WhenAdministratorServiceIsInstantiated_ItIsNotNull() {
            //Arrange & act
            var service = new AdministratorService();

            //Assert
            Assert.NotNull(service);
        }

        //David Matijanić
        [Fact]
        public void CheckLoginCredentials_GivenValidCredentials_SetsLoggedAdministrator() {
            //Arrange
            string username = "admin1";
            string password = "password";
            var returnedAdmins = new List<Administrator>
            {
                new Administrator { username = username, password = password }
            }.AsQueryable();
            A.CallTo(() => administratorRepository.GetAdministratorLogin(username, password)).Returns(returnedAdmins);

            //Act
            administratorService.CheckLoginCredentials(username, password);

            //Assert
            Action[] actions = {
                () => Assert.Equal(username, LoggedUser.Username),
                () => Assert.Equal(Role.Admin, LoggedUser.UserType)
            };
            Assert.Multiple(actions);
        }

        //David Matijanić
        [Fact]
        public void CheckLoginCredentials_GivenInvalidCredentials_DoesNotSetLoggedAdministrator() {
            //Arrange
            string username = "invalidUsername";
            string password = "invalidPassword";
            var returnedAdmins = new List<Administrator>().AsQueryable();
            A.CallTo(() => administratorRepository.GetAdministratorLogin(username, password)).Returns(returnedAdmins);

            //Act
            administratorService.CheckLoginCredentials(username, password);

            //Assert
            Action[] actions = {
                () => Assert.Null(LoggedUser.Username),
                () => Assert.Null(LoggedUser.UserType)
            };
            Assert.Multiple(actions);
        }

        //David Matijanić
        [Fact]
        public void CheckLoginCredentials_GivenNoMatchingUsers_DoesNotSetLoggedAdministrator() {
            //Arrange
            string username = "nonexistent";
            string password = "password";
            var returnedAdmins = new List<Administrator>().AsQueryable();
            A.CallTo(() => administratorRepository.GetAdministratorLogin(username, password)).Returns(returnedAdmins);

            //Act
            administratorService.CheckLoginCredentials(username, password);

            //Assert
            Action[] actions = {
                () => Assert.Null(LoggedUser.Username),
                () => Assert.Null(LoggedUser.UserType)
            };
            Assert.Multiple(actions);
        }

        //David Matijanić
        [Fact]
        public void CheckLoginCredentials_GivenMultipleMatchingUsers_DoesNotSetLoggedAdministrator() {
            //Arrange
            string username = "admin1";
            string password = "password";
            var returnedAdmins = new List<Administrator>
            {
                new Administrator { username = username, password = password },
                new Administrator { username = username, password = password }
            }.AsQueryable();
            A.CallTo(() => administratorRepository.GetAdministratorLogin(username, password)).Returns(returnedAdmins);

            //Act
            administratorService.CheckLoginCredentials(username, password);

            //Assert
            Action[] actions = {
                () => Assert.Null(LoggedUser.Username),
                () => Assert.Null(LoggedUser.UserType)
            };
            Assert.Multiple(actions);
        }

        //David Matijanić
        [Fact]
        public void Dispose_CallsDisposeOnRepository() {
            //Act
            administratorService.Dispose();

            //Assert
            A.CallTo(() => administratorRepository.Dispose()).MustHaveHappened();
        }
    }
}
