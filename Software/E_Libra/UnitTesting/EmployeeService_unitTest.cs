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

namespace UnitTesting {
    public class EmployeeService_unitTest {
        private EmployeeService employeeService;
        private readonly IEmpoloyeeRepositroy employeeRepository;
        private IQueryable<Employee> employees { get; set; }

        public EmployeeService_unitTest() {
            employeeRepository = A.Fake<IEmpoloyeeRepositroy>();
            employeeService = new EmployeeService(employeeRepository, null, null);
            employees = new List<Employee> {
                new Employee {
                    id = 1,
                    name = "Darko",
                    surname = "Daric",
                    username = "ddaric",
                    password = "jakalozinka",
                    OIB = "11892593283",
                    Library = new Library {
                        id = 111
                    },
                    Library_id = 111
                },
                new Employee {
                    id = 2,
                    name = "Marina",
                    surname = "Misic",
                    username = "mmisic",
                    password = "mypw",
                    OIB = "85738923405",
                    Library = new Library {
                        id = 111
                    },
                    Library_id = 111
                },
                new Employee {
                    id = 3,
                    name = "Sven",
                    surname = "Sivic",
                    username = "ssivic",
                    password = "graygrey",
                    OIB = "23423423424",
                    Library = new Library {
                        id = 222
                    },
                    Library_id = 222
                },
                new Employee {
                    id = 4,
                    name = "Lea",
                    surname = "Lalić",
                    username = "llalic",
                    password = "jasamlea",
                    OIB = "22738449503",
                    Library = new Library {
                        id = 222
                    },
                    Library_id = 222
                },
                new Employee {
                    id = 5,
                    name = "Ana",
                    surname = "Anić",
                    username = "aanic",
                    password = "anapw",
                    OIB = "31567923456",
                    Library = new Library {
                        id = 333
                    },
                    Library_id = 333
                },
                new Employee {
                    id = 6,
                    name = "Ivan",
                    surname = "Ivić",
                    username = "iivic",
                    password = "ivanpw",
                    OIB = "78912345678",
                    Library = new Library {
                        id = 333
                    },
                    Library_id = 333
                },
                new Employee {
                    id = 7,
                    name = "Petra",
                    surname = "Preradović",
                    username = "ppreradovic",
                    password = "petrapw",
                    OIB = "98765432101",
                    Library = new Library {
                        id = 444
                    },
                    Library_id = 444
                },
                new Employee {
                    id = 8,
                    name = "Marko",
                    surname = "Marković",
                    username = "mmarkovic",
                    password = "markopw",
                    OIB = "12345678901",
                    Library = new Library {
                        id = 444
                    },
                    Library_id = 444
                }
            }.AsQueryable();
        }

        [Fact]
        public void Constructor_WhenEmployeeServiceIsInstantiated_ItIsNotNull() {
            //Arrange & act
            var service = new EmployeeService();

            //Assert
            Assert.NotNull(service);
        }

        [Fact]
        public void GetEmployeesByLibrary_NoEmployeesExist_NoEmployeesReturned() {
            //Arrange
            Library library = new Library { id = 1 };
            A.CallTo(() => employeeRepository.GetEmployeesByLibrary(library)).Returns(new List<Employee>().AsQueryable());

            //Act
            var retrievedEmployees = employeeService.GetEmployeesByLibrary(library);

            //Assert
            Assert.Empty(retrievedEmployees);
        }

        [Theory]
        [InlineData(111)]
        [InlineData(222)]
        [InlineData(333)]
        [InlineData(444)]
        [InlineData(0)]
        public void GetEmployeesByLibrary_LibraryHasEmployees_EmployeesRetrieved(int libraryId) {
            //Arrange
            Library library = new Library { id = libraryId };
            A.CallTo(() => employeeRepository.GetEmployeesByLibrary(library)).Returns(employees.Where(e => e.Library.id == libraryId));

            //Act
            var retrievedEmployees = employeeService.GetEmployeesByLibrary(library);

            //Assert
            Assert.Equal(employees.Where(e => e.Library.id == libraryId).ToList(), retrievedEmployees);
        }
    }
}
