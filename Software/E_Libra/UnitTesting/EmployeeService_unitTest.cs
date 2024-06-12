using BussinessLogicLayer.services;
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

namespace UnitTesting
{
    public class EmployeeService_unitTest
    {
        private IEmpoloyeeRepositroy empoloyeeRepository;
        private EmployeeService employeeService;
        public EmployeeService_unitTest()
        {
            empoloyeeRepository = A.Fake<IEmpoloyeeRepositroy>();
            employeeService = new EmployeeService(empoloyeeRepository, null, null);

            LoggedUser.Username = null;
            LoggedUser.UserType = null;
            LoggedUser.LibraryId = 0;
        }

        //Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_GivenValidCredentials_SetsLoggedEmployee()
        {
            // Arrange
            string username = "empl1";
            string password = "password1";
            var returnedEmployee = new List<Employee>
        {
            new Employee { username = username, password = password, Library_id = 1 }
            }.AsQueryable();

            A.CallTo(() => empoloyeeRepository.GetEmployeeLogin(username, password)).Returns(returnedEmployee);

            // Act
            employeeService.CheckLoginCredentials(username, password);

            // Assert
            Assert.Equal(username, LoggedUser.Username);
            Assert.Equal(Role.Employee, LoggedUser.UserType);
            Assert.Equal(1, LoggedUser.LibraryId);


        }
        //Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_GivenInvalidCredentials_DoesNotSetLoggedEmployee()
        {
            // Arrange
            string username = "invalidUsername";
            string password = "invalidPassword";
            var returnedEmployee = new List<Employee>().AsQueryable();

            A.CallTo(() => empoloyeeRepository.GetEmployeeLogin(username, password)).Returns(returnedEmployee);

            // Act
            employeeService.CheckLoginCredentials(username, password);

            // Assert
            Assert.Null(LoggedUser.Username);
            Assert.Null(LoggedUser.UserType);
            Assert.Equal(0, LoggedUser.LibraryId);


        }

        // Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_GivenNoMatchingUsers_DoesNotSetLoggedEmpoyee()
        {
            // Arrange
            string username = "nonexistent";
            string password = "password";
            var returnedEmployee = new List<Employee>().AsQueryable();

            A.CallTo(() => empoloyeeRepository.GetEmployeeLogin(username, password)).Returns(returnedEmployee);

            // Act
            employeeService.CheckLoginCredentials(username, password);

            // Assert
            Assert.Null(LoggedUser.Username);
            Assert.Null(LoggedUser.UserType);
            Assert.Equal(0, LoggedUser.LibraryId);

        }
        // Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_GivenMultipleMatchingUsers_DoesNotSetLoggedEmployee()
        {
            // Arrange
            string username = "empl1";
            string password = "password1";
            var returnedEmployee = new List<Employee>
        {
            new Employee { username = username, password = password, Library_id = 1 },
            new Employee { username = username, password = password, Library_id = 2 }
        }.AsQueryable();

            A.CallTo(() => empoloyeeRepository.GetEmployeeLogin(username, password)).Returns(returnedEmployee);

            // Act
            employeeService.CheckLoginCredentials(username, password);

            // Assert
            Assert.Null(LoggedUser.Username);
            Assert.Null(LoggedUser.UserType);
            Assert.Equal(0, LoggedUser.LibraryId);

        }

        // Magdalena Markovinović
        [Fact]
        public void GetEmployeeLibraryId_GivenFunctionIsCalled_ReturnsCorrectId()
        {
            // Arrange
            string username = "testuser";
            int expectedLibraryId = 1;
            A.CallTo(() => empoloyeeRepository.GetEmployeeLibraryId(username)).Returns(expectedLibraryId);

            // Act
            int actualLibraryId = employeeService.GetEmployeeLibraryId(username);

            // Assert
            Assert.Equal(expectedLibraryId, actualLibraryId);
        }

        // Magdalena Markovinović
        [Fact]
        public void GetEmployeeId_GivenFunctionIsCalled_ReturnsCorrectId()
        {
            // Arrange
            string username = "testuser";
            int expectedEmployeeId = 2;
            A.CallTo(() => empoloyeeRepository.GetEmployeeId(username)).Returns(expectedEmployeeId);

            // Act
            int actualEmployeeId = employeeService.GetEmployeeId(username);

            // Assert
            Assert.Equal(expectedEmployeeId, actualEmployeeId);
        }

        // Magdalena Markovinović
        [Fact]
        public void Dispose_GivenFunctionIsCalled_DisposeAll()
        {
            // Arrange
            A.CallTo(() => empoloyeeRepository.Dispose()).DoesNothing();

            // Act
            employeeService.Dispose();

            // Assert
            A.CallTo(() => empoloyeeRepository.Dispose()).MustHaveHappenedOnceExactly();
        }
    }
}
