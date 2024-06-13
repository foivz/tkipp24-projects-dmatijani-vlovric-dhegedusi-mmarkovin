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

namespace IntegrationTesting
{
    [Collection("Database collection")]
    public class EmployeeService_integrationTest
    {
        private readonly EmployeeService employeeService;
        private readonly DatabaseFixture fixture;

        private Library library;
        private List<Employee> employees;

        public EmployeeService_integrationTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
            this.fixture.ResetDatabase();

            library = new Library
            {
                id = 123,
                name = "Testna knjiznica",
                OIB = "11112222333",
                price_day_late = 3.5m,
                membership_duration = GetDateFromMembershipDuration(30)
            };
            InsertLibraryIntoDatabase(library);

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

        private DateTime GetDateFromMembershipDuration(decimal duration)
        {
            DateTime startDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
            DateTime durationDate = startDate.AddDays(Convert.ToDouble(duration) - 1);

            return durationDate;
        }

        private void InsertLibraryIntoDatabase(Library library)
        {
            var formattedMembershipDuration = library.membership_duration.ToString("yyyy-MM-dd");
            string sqlInsertLibrary = $"INSERT [dbo].[Library] ([id], [name], [OIB], [price_day_late], [membership_duration]) VALUES ('{library.id}', '{library.name}', '{library.OIB}', {library.price_day_late}, '{formattedMembershipDuration}');";
            Helper.ExecuteCustomSql(sqlInsertLibrary);
        }

        private void InsertEmployeesIntoDatabase(List<Employee> employeeList)
        {
            foreach (Employee employee in employeeList)
            {
                string sqlInsertEmployee = $"INSERT [dbo].[Employee] ([name], [surname], [username], [password], [OIB], [Library_id]) VALUES ('{employee.name}', '{employee.surname}', '{employee.username}', '{employee.password}', '{employee.OIB}', {employee.Library_id});";
                Helper.ExecuteCustomSql(sqlInsertEmployee);
            }
        } 
    }
}
