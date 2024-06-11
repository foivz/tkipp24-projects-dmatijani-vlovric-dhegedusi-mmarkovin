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
        private IEmpoloyeeRepositroy empoloyeeRepositroy;
        private EmployeeService employeeService;
        public EmployeeService_unitTest()
        {
            empoloyeeRepositroy = A.Fake<IEmpoloyeeRepositroy>();
            employeeService = new EmployeeService(empoloyeeRepositroy, null, null);
        }

        //Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_ValidCredentials_SetsLoggedUser()
        {
            // Arrange
            string username = "empl1";
            string password = "password1";
            var returnedEmployee = new List<Employee>
        {
            new Employee { username = username, password = password, Library_id = 1 }
            }.AsQueryable();

            A.CallTo(() => empoloyeeRepositroy.GetEmployeeLogin(username, password)).Returns(returnedEmployee);

            // Act
            employeeService.CheckLoginCredentials(username, password);

            // Assert
            Assert.Equal(username, LoggedUser.Username);
            Assert.Equal(Role.Employee, LoggedUser.UserType);
            Assert.Equal(1, LoggedUser.LibraryId);
        }
        //Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_InvalidCredentials_DoesNotSetLoggedUser()
        {
            // Arrange
            string username = "invalidUsername";
            string password = "invalidPassword";
            var returnedEmployee = new List<Employee>().AsQueryable();

            A.CallTo(() => empoloyeeRepositroy.GetEmployeeLogin(username, password)).Returns(returnedEmployee);

            // Act
            employeeService.CheckLoginCredentials(username, password);

            // Assert
            Assert.Null(LoggedUser.Username);
            Assert.Null(LoggedUser.UserType);
            Assert.Equal(0, LoggedUser.LibraryId);
        }

        // Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_NoMatchingUsers_DoesNotSetLoggedUser()
        {
            // Arrange
            string username = "nonexistent";
            string password = "password";
            var returnedEmployee = new List<Employee>().AsQueryable();

            A.CallTo(() => empoloyeeRepositroy.GetEmployeeLogin(username, password)).Returns(returnedEmployee);

            // Act
            employeeService.CheckLoginCredentials(username, password);

            // Assert
            Assert.Null(LoggedUser.Username);
            Assert.Null(LoggedUser.UserType);
            Assert.Equal(0, LoggedUser.LibraryId);
        }
        // Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_MultipleMatchingUsers_DoesNotSetLoggedUser()
        {
            // Arrange
            string username = "empl1";
            string password = "password1";
            var returnedEmployee = new List<Employee>
        {
            new Employee { username = username, password = password, Library_id = 1 },
            new Employee { username = username, password = password, Library_id = 2 }
        }.AsQueryable();

            A.CallTo(() => empoloyeeRepositroy.GetEmployeeLogin(username, password)).Returns(returnedEmployee);

            // Act
            employeeService.CheckLoginCredentials(username, password);

            // Assert
            Assert.Null(LoggedUser.Username);
            Assert.Null(LoggedUser.UserType);
            Assert.Equal(0, LoggedUser.LibraryId);
        }
    }
}
