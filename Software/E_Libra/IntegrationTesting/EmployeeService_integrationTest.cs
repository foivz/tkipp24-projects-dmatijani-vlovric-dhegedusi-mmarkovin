using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DataAccessLayer.Repositories;
using BussinessLogicLayer.services;
using EntitiesLayer;
using BussinessLogicLayer.Exceptions;
using System.Data.SqlTypes;

namespace IntegrationTesting
{
    [Collection("Database collection")]
    public class EmployeeService_integrationTest
    {
        private readonly EmployeeService employeeService;
        private readonly DatabaseFixture fixture;

        private readonly Library library;
        private readonly List<Employee> employees;

        public EmployeeService_integrationTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
            this.fixture.ResetDatabase();

            library = new Library
            {
                id = 123
            };
            InsertLibraryIntoDatabase();

            employees = new List<Employee>
            {
                new Employee {
                    name = "Darko",
                    surname = "Daric",
                    username = "ddaric",
                    password = "jakalozinka",
                    OIB = "11892593283",
                    Library = library,
                    Library_id = library.id
                },
                new Employee {
                    name = "Marina",
                    surname = "Misic",
                    username = "mmisic",
                    password = "mypw",
                    OIB = "85738923405",
                    Library = library,
                    Library_id = library.id
                },
                new Employee {
                    name = "Sven",
                    surname = "Sivic",
                    username = "ssivic",
                    password = "graygrey",
                    OIB = "23423423424",
                    Library = library,
                    Library_id = library.id
                },
                new Employee {
                    name = "Lea",
                    surname = "Lalic",
                    username = "llalic",
                    password = "jasamlea",
                    OIB = "22738449503",
                    Library = library,
                    Library_id = library.id
                },
                new Employee {
                    name = "Ana",
                    surname = "Anic",
                    username = "aanic",
                    password = "anapw",
                    OIB = "31567923456",
                    Library = library,
                    Library_id = library.id
                },
                new Employee {
                    name = "Ivan",
                    surname = "Ivic",
                    username = "iivic",
                    password = "ivanpw",
                    OIB = "78912345678",
                    Library = library,
                    Library_id = library.id
                },
                new Employee {
                    name = "Petra",
                    surname = "Preradovic",
                    username = "ppreradovic",
                    password = "petrapw",
                    OIB = "98765432101",
                    Library = library,
                    Library_id = library.id
                },
                new Employee {
                    name = "Marko",
                    surname = "Markovic",
                    username = "mmarkovic",
                    password = "markopw",
                    OIB = "12345678901",
                    Library = library,
                    Library_id = library.id
                }
            };

            employeeService = new EmployeeService();
        }

        //David Matijanić
        [Fact]
        public void GetEmployeesByLibrary_NoEmployeesExist_NoEmployeesReturned()
        {
            //Act
            var retrievedEmployees = employeeService.GetEmployeesByLibrary(library);

            //Assert
            Assert.Empty(retrievedEmployees);
        }

        //David Matijanić
        [Fact]
        public void GetEmployeesByLibrary_LibraryHasEmployees_EmployeesRetrieved()
        {
            //Arrange
            InsertEmployeesIntoDatabase(employees);

            //Act
            var retrievedEmployees = employeeService.GetEmployeesByLibrary(library);

            //Assert
            retrievedEmployees.Should().BeEquivalentTo(employees, options => options
                .Excluding(e => e.Library)
                .Excluding(e => e.id)
            );
        }

        //David Matijanić
        [Fact]
        public void AddEmployee_EmployeeWithSameUsernameExists_ThrowsEmployeeWithSameUsernameException()
        {
            //Arrange
            Employee employee = employees[0];
            Employee employeeToAdd = new Employee
            {
                name = "Novi",
                surname = "Novic",
                username = employee.username,
                password = "newpassword",
                OIB = "11199944832",
                Library = library,
                Library_id = library.id
            };
            InsertEmployeeIntoDatabase(employee);

            //Act & assert
            Assert.Throws<EmployeeWithSameUsernameException>(() => employeeService.AddEmployee(employeeToAdd));
        }

        //David Matijanić
        [Fact]
        public void AddEmployee_EmployeeWithSameOIBExists_ThrowsEmployeeWithSameOIBException()
        {
            //Arrange
            Employee employee = employees[0];
            Employee employeeToAdd = new Employee
            {
                name = "Novi",
                surname = "Novic",
                username = "noviusername",
                password = "newpassword",
                OIB = employee.OIB,
                Library = library,
                Library_id = library.id
            };
            InsertEmployeeIntoDatabase(employee);

            //Act & assert
            Assert.Throws<EmployeeWithSameOIBException>(() => employeeService.AddEmployee(employeeToAdd));
        }

        //David Matijanić
        [Fact]
        public void AddEmployee_EmployeeHasUniqueData_NewEmployeeIsAdded()
        {
            //Arrange
            Employee employee = employees[0];
            Employee employeeToAdd = new Employee
            {
                name = "Novi",
                surname = "Novic",
                username = "noviusername",
                password = "newpassword",
                OIB = "11199944832",
                Library = library,
                Library_id = library.id
            };
            InsertEmployeeIntoDatabase(employee);

            //Act
            int added = employeeService.AddEmployee(employeeToAdd);

            //Assert
            Assert.Equal(1, added);
        }

        //David Matijanić
        [Fact]
        public void UpdateEmployee_NonExistingOIBEntered_ThrowsEmployeeWithSameOIBException()
        {
            //Arrange
            InsertEmployeesIntoDatabase(employees);
            Employee employeeToUpdate = employees[0];
            employeeToUpdate.OIB = "99999999997";

            //Act & assert
            Assert.Throws<EmployeeWithSameOIBException>(() => employeeService.UpdateEmployee(employeeToUpdate));
        }

        //David Matijanić
        [Fact]
        public void UpdateEmployee_EmployeeWithSameUsernameExists_ThrowsEmployeeWithSameUsernameException()
        {
            //Arrange
            InsertEmployeesIntoDatabase(employees);
            Employee employeeToUpdate = employees[0];
            employeeToUpdate.username = employees[1].username;

            //Act & assert
            Assert.Throws<EmployeeWithSameUsernameException>(() => employeeService.UpdateEmployee(employeeToUpdate));
        }

        //David Matijanić
        [Fact]
        public void UpdateEmployee_EmployeeWithOIBExists_EmployeeIsUpdated()
        {
            //Arrange
            InsertEmployeesIntoDatabase(employees);
            Employee employeeToUpdate = employees[0];
            employees[0].name = "Azurirani";
            employees[1].surname = "Azuriranic";

            //Act
            int updated = employeeService.UpdateEmployee(employeeToUpdate);

            //Assert
            Assert.Equal(1, updated);
        }

        //David Matijanić
        [Fact]
        public void DeleteEmployee_EmployeeHasActiveBorrows_ThrowsEmployeeException()
        {
            //Arrange
            Employee employeeToDelete = employees[0];
            employees[0].id = 1;
            InsertEmployeeIntoDatabase(employeeToDelete);
            InsertBookIntoDatabase();
            InsertMemberIntoDatabase();
            InsertBorrowIntoDatabase();

            //Act & assert
            Assert.Throws<EmployeeException>(() => employeeService.DeleteEmployee(employeeToDelete));
        }

        //David Matijanić
        [Fact]
        public void DeleteEmployee_EmployeeHasActiveArchives_ThrowsEmployeeException()
        {
            //Arrange
            Employee employeeToDelete = employees[0];
            employees[0].id = 1;
            InsertEmployeeIntoDatabase(employeeToDelete);
            InsertBookIntoDatabase();
            InsertArchiveIntoDatabase();

            //Act & assert
            Assert.Throws<EmployeeException>(() => employeeService.DeleteEmployee(employeeToDelete));
        }

        //David Matijanić
        [Fact]
        public void DeleteEmployee_EmployeeHasNoActiveBorrowsOrArchives_EmployeeIsDeleted()
        {
            //Arrange
            Employee employeeToDelete = employees[0];
            employees[0].id = 1;
            InsertEmployeeIntoDatabase(employeeToDelete);

            //Act
            int deleted = employeeService.DeleteEmployee(employeeToDelete);

            //Assert
            Assert.Equal(1, deleted);
        }

        //David Matijanić
        [Fact]
        public void GetEmployeesByUsernam_NoEmployeesExist_NoEmployeesReturned()
        {
            //Act
            var retrievedEmployee = employeeService.GetEmployeeByUsername("ddaric");

            //Assert
            Assert.Null(retrievedEmployee);
        }

        //David Matijanić
        [Fact]
        public void GetEmployeeByUsername_EmployeeWithUsernameExists_EmployeeReturned()
        {
            //Arrange
            InsertEmployeesIntoDatabase(employees);
            string employeeUsername = employees[0].username;

            //Act
            var retrievedEmployee = employeeService.GetEmployeeByUsername(employeeUsername);

            //Assert
            Assert.Equal(employeeUsername, retrievedEmployee.username);
        }

        //David Matijanić
        [Fact]
        public void GetEmployeeByUsername_NonExistingEmployeeUsername_EmployeeIsNotReturned()
        {
            //Arrange
            InsertEmployeesIntoDatabase(employees);
            string username = "nonexisting";

            //Act
            var retrievedEmployee = employeeService.GetEmployeeByUsername(username);

            //Assert
            Assert.Null(retrievedEmployee);
        }

        // Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_EmployeeWithGivenUsernameAndPasswordExists_EmployeeIsLoggedIn()
        {
            //Arrange
            InsertEmployeesIntoDatabase(employees);
            string username = employees[0].username;
            string password = employees[0].password;

            //Act
            employeeService.CheckLoginCredentials(username, password);

            //Assert
            Assert.Equal(username, LoggedUser.Username);
            Assert.Equal(Role.Employee, LoggedUser.UserType);
            Assert.Equal(employees[0].Library_id, LoggedUser.LibraryId);
        }

        // Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_GivenInvalidUsernameAndPassword_EmployeeIsNotLoggedIn()
        {
            //Arrange
            var username = "nonexisting";
            var password = "nonexisting";
            LoggedUser.Username = null;
            LoggedUser.UserType = Role.Member;
            LoggedUser.LibraryId = 0;

            //Act 
            employeeService.CheckLoginCredentials(username, password);

            // Assert
            Assert.NotEqual(username, LoggedUser.Username);
            Assert.NotEqual(Role.Employee, LoggedUser.UserType);
            Assert.NotEqual(1, LoggedUser.LibraryId);
        }

        // Magdalena Markvovinović
        [Fact]
        public void GetEmployeeLibraryId_GivenEmployeeWithUsernameExists_ReturnsLibraryId()
        {
            // Arrange
            InsertEmployeesIntoDatabase(employees);

            // Act 
            employeeService.GetEmployeeLibraryId(employees[0].username);

            // Arrange
            Assert.Equal(employees[0].Library_id, library.id);
        }

        // Magdalena Markvovinović
        [Fact]
        public void GetEmployeeLibraryId_GivenEmployeeWithUsernameDoNotExists_ReturnsNotEqual()
        {
            // Arrange
            var username = "nonexisting";

            // Act 
            var result = employeeService.GetEmployeeLibraryId(username);

            // Arrange
            Assert.NotEqual(result, library.id);
        }

        // Magdalena Markvovinović
        [Fact]
        public void GetEmployeeId_GivenEmployeeWithUsernameExists_ReturnsEmployeeId()
        {
            // Arrange
            InsertEmployeesIntoDatabase(employees);
            employees[0].id = 1;

            // Act 
            var result = employeeService.GetEmployeeId(employees[0].username);

            // Arrange
            Assert.Equal(employees[0].id, result);
        }

        // Magdalena Markvovinović
        [Fact]
        public void GetEmployeeId_GivenEmployeeWithUsernameDoNotExists_ReturnsNull()
        {
            // Arrange
            var username = "nonexisting";

            // Act 
            var result = employeeService.GetEmployeeId(username);

            // Arrange
            Assert.Equal(result, 0);
        }

        private DateTime GetDateFromMembershipDuration(decimal duration)
        {
            DateTime startDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
            DateTime durationDate = startDate.AddDays(Convert.ToDouble(duration) - 1);

            return durationDate;
        }

        private void InsertLibraryIntoDatabase()
        {
            string createLibrary =
            "INSERT [dbo].[Library] ([id], [name], [OIB], [phone], [email], [price_day_late], [address], [membership_duration]) " +
            "VALUES (123, N'Knjiznica', 123, 331, N'email', 3, N'adresa', GETDATE())";
            Helper.ExecuteCustomSql(createLibrary);
        }

        private void InsertEmployeesIntoDatabase(List<Employee> employeeList)
        {
            foreach (Employee employee in employeeList)
            {
                InsertEmployeeIntoDatabase(employee);
            }
        } 

        private void InsertEmployeeIntoDatabase(Employee employee)
        {
            string sqlInsertEmployee = $"INSERT [dbo].[Employee] ([name], [surname], [username], [password], [OIB], [Library_id]) VALUES ('{employee.name}', '{employee.surname}', '{employee.username}', '{employee.password}', '{employee.OIB}', {employee.Library_id});";
            Helper.ExecuteCustomSql(sqlInsertEmployee);
        }

        private void InsertBookIntoDatabase()
        {
            string sqlInsertGenre = "INSERT [dbo].[Genre] ([name]) VALUES ('zanr1')";
            Helper.ExecuteCustomSql(sqlInsertGenre);

            string sqlInsertBook = $"INSERT [dbo].[Book] ([name], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) VALUES ('Book 1', 10, 0, 12345, 10, 10, 1, {library.id});";
            Helper.ExecuteCustomSql(sqlInsertBook);
        }

        private void InsertMemberIntoDatabase()
        {
            string sqlInsertMember = $"INSERT [dbo].[Member] ([name], [surname], [username], [password], [OIB], [Library_id], [barcode_id]) VALUES ('Member', 'Memberic', 'member1', '123', '12344433112', {library.id}, '1243nsdf');";
            Helper.ExecuteCustomSql(sqlInsertMember);
        }

        private void InsertBorrowIntoDatabase()
        {
            string sqlInsertBorrow = $"INSERT [dbo].[Borrow] ([Book_id], [Member_id], [borrow_status], [borrow_date], [return_date], [Employee_borrow_id]) VALUES (1, 1, 1, '2024-01-01', '2024-01-01', 1);";
            Helper.ExecuteCustomSql(sqlInsertBorrow);
        }

        private void InsertArchiveIntoDatabase() {
            string sqlInsertArchive = $"INSERT [dbo].[Archive] ([Book_id], [Employee_id], [arhive_date]) VALUES (1, 1, GETDATE());";
            Helper.ExecuteCustomSql(sqlInsertArchive);
        }
    }
}
