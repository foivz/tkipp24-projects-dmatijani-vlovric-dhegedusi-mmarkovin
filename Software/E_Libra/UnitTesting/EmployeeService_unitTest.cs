using BussinessLogicLayer.Exceptions;
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

            LoggedUser.Username = null;
            LoggedUser.UserType = null;
            LoggedUser.LibraryId = 0;
        }

        //David Matijanić
        [Fact]
        public void Constructor_WhenEmployeeServiceIsInstantiated_ItIsNotNull() {
            //Arrange & act
            var service = new EmployeeService();

            //Assert
            Assert.NotNull(service);
        }

        //Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_ValidCredentials_SetsLoggedEmployee() {
            // Arrange
            string username = "empl1";
            string password = "password1";
            var returnedEmployee = new List<Employee>
            {
                new Employee { username = username, password = password, Library_id = 1 }
            }.AsQueryable();

            A.CallTo(() => employeeRepository.GetEmployeeLogin(username, password)).Returns(returnedEmployee);

            // Act
            employeeService.CheckLoginCredentials(username, password);

            // Assert
            Assert.Equal(username, LoggedUser.Username);
            Assert.Equal(Role.Employee, LoggedUser.UserType);
            Assert.Equal(1, LoggedUser.LibraryId);
        }

        //Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_InvalidCredentials_DoesNotSetLoggedEmployee() {
            // Arrange
            string username = "invalidUsername";
            string password = "invalidPassword";
            var returnedEmployee = new List<Employee>().AsQueryable();

            A.CallTo(() => employeeRepository.GetEmployeeLogin(username, password)).Returns(returnedEmployee);

            // Act
            employeeService.CheckLoginCredentials(username, password);

            // Assert
            Assert.Null(LoggedUser.Username);
            Assert.Null(LoggedUser.UserType);
            Assert.Equal(0, LoggedUser.LibraryId);
        }

        // Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_NoMatchingUsers_DoesNotSetLoggedEmpoyee() {
            // Arrange
            string username = "nonexistent";
            string password = "password";
            var returnedEmployee = new List<Employee>().AsQueryable();

            A.CallTo(() => employeeRepository.GetEmployeeLogin(username, password)).Returns(returnedEmployee);

            // Act
            employeeService.CheckLoginCredentials(username, password);

            // Assert
            Assert.Null(LoggedUser.Username);
            Assert.Null(LoggedUser.UserType);
            Assert.Equal(0, LoggedUser.LibraryId);
        }

        // Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_MultipleMatchingUsers_DoesNotSetLoggedEmployee() {
            // Arrange
            string username = "empl1";
            string password = "password1";
            var returnedEmployee = new List<Employee>
            {
                new Employee { username = username, password = password, Library_id = 1 },
                new Employee { username = username, password = password, Library_id = 2 }
            }.AsQueryable();

            A.CallTo(() => employeeRepository.GetEmployeeLogin(username, password)).Returns(returnedEmployee);

            // Act
            employeeService.CheckLoginCredentials(username, password);

            // Assert
            Assert.Null(LoggedUser.Username);
            Assert.Null(LoggedUser.UserType);
            Assert.Equal(0, LoggedUser.LibraryId);
        }

        // Magdalena Markovinović
        [Fact]
        public void GetEmployeeLibraryId_ReturnsCorrectId() {
            // Arrange
            string username = "testuser";
            int expectedLibraryId = 1;
            A.CallTo(() => employeeRepository.GetEmployeeLibraryId(username)).Returns(expectedLibraryId);

            // Act
            int actualLibraryId = employeeService.GetEmployeeLibraryId(username);

            // Assert
            Assert.Equal(expectedLibraryId, actualLibraryId);
        }

        // Magdalena Markovinović
        [Fact]
        public void GetEmployeeId_ReturnsCorrectId() {
            // Arrange
            string username = "testuser";
            int expectedEmployeeId = 2;
            A.CallTo(() => employeeRepository.GetEmployeeId(username)).Returns(expectedEmployeeId);

            // Act
            int actualEmployeeId = employeeService.GetEmployeeId(username);

            // Assert
            Assert.Equal(expectedEmployeeId, actualEmployeeId);
        }

        //David Matijanić
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

        //David Matijanić
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

        //David Matijanić
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void AddEmployee_EmployeeWithSameIdExists_ThrowsEmployeeWithSameIdException(int employeeId) {
            //Arrange
            Employee employee = new Employee { id = employeeId };
            PrepareEmployeeRepositoryMethods(employee);

            //Act & assert
            Assert.Throws<EmployeeWithSameIDException>(() => employeeService.AddEmployee(employee));
        }

        //David Matijanić
        [Theory]
        [InlineData("ddaric")]
        [InlineData("mmisic")]
        [InlineData("ssivic")]
        [InlineData("llalic")]
        [InlineData("aanic")]
        [InlineData("iivic")]
        [InlineData("ppreradovic")]
        [InlineData("mmarkovic")]
        public void AddEmployee_EmployeeWithSameUsernameExists_ThrowsEmployeeWithSameUsernameException(string employeeUsername) {
            //Arrange
            Employee employee = new Employee { 
                id = 9,
                username = employeeUsername
            };
            PrepareEmployeeRepositoryMethods(employee);

            //Act & assert
            Assert.Throws<EmployeeWithSameUsernameException>(() => employeeService.AddEmployee(employee));
        }

        //David Matijanić
        [Theory]
        [InlineData("11892593283")]
        [InlineData("85738923405")]
        [InlineData("23423423424")]
        [InlineData("22738449503")]
        [InlineData("31567923456")]
        [InlineData("78912345678")]
        [InlineData("98765432101")]
        [InlineData("12345678901")]
        public void AddEmployee_EmployeeWithSameOIBExists_ThrowsEmployeeWithSameOIBException(string employeeOib) {
            //Arrange
            Employee employee = new Employee {
                id = 9,
                username = "novinovic",
                OIB = employeeOib
            };
            PrepareEmployeeRepositoryMethods(employee);

            //Act & assert
            Assert.Throws<EmployeeWithSameOIBException>(() => employeeService.AddEmployee(employee));
        }

        //David Matijanić
        [Fact]
        public void AddEmployee_EmployeeHasUniqueData_NewEmployeeIsAdded() {
            //Arrange
            Employee employee = new Employee {
                id = 9,
                username = "novinovic",
                OIB = "11111122223"
            };
            PrepareEmployeeRepositoryMethods(employee);
            A.CallTo(() => employeeRepository.Add(employee, true)).Invokes(call => {
                var newEmployees = employees.ToList();
                newEmployees.Add(employee);
                employees = newEmployees.AsQueryable();
            });

            //Act
            employeeService.AddEmployee(employee);

            //Assert
            Assert.Contains(employee, employees);
        }

        //David Matijanić
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void UpdateEmployee_EmployeeWithSameIdExists_ThrowsEmployeeWithSameIdException(int employeeId) {
            //Arrange
            Employee employee = new Employee { id = employeeId };
            PrepareEmployeeRepositoryMethods(employee);

            //Act & assert
            Assert.Throws<EmployeeWithSameIDException>(() => employeeService.UpdateEmployee(employee));
        }

        //David Matijanić
        [Theory]
        [InlineData("11111111111")]
        [InlineData("22222222222")]
        [InlineData("33333333333")]
        public void UpdateEmployee_NonExistingOIBEntered_ThrowsEmployeeWithSameOIBException(string employeeOib) {
            //Arrange
            Employee employee = new Employee {
                id = 9,
                username = "ddaric",
                OIB = employeeOib
            };
            PrepareEmployeeRepositoryMethods(employee);

            //Act & assert
            Assert.Throws<EmployeeWithSameOIBException>(() => employeeService.UpdateEmployee(employee));
        }

        //David Matijanić
        [Theory]
        [InlineData("mmisic")]
        [InlineData("ssivic")]
        [InlineData("llalic")]
        [InlineData("aanic")]
        [InlineData("iivic")]
        [InlineData("ppreradovic")]
        [InlineData("mmarkovic")]
        public void UpdateEmployee_EmployeeWithSameUsernameExists_ThrowsEmployeeWithSameUsernameException(string employeeUsername) {
            //Arrange
            Employee employee = new Employee {
                id = 1,
                OIB = "11892593283",
                username = employeeUsername
            };
            PrepareEmployeeRepositoryMethods(employee);

            //Act & assert
            Assert.Throws<EmployeeWithSameUsernameException>(() => employeeService.UpdateEmployee(employee));
        }

        //David Matijanić
        [Fact]
        public void UpdateEmployee_EmployeeWithOIBExists_EmployeeIsUpdated() {
            //Arrange
            Employee employee = employees.First();
            Employee changedEmployee = new Employee {
                OIB = employee.OIB,
                username = employee.username,
                name = "Promijenjen"
            };
            PrepareEmployeeRepositoryMethods(changedEmployee);
            A.CallTo(() => employeeRepository.Update(changedEmployee, true)).Invokes(call => {
                foreach (var e in employees.Where(ee => ee.OIB == changedEmployee.OIB)) {
                    e.name = changedEmployee.name;
                }
            });

            //Act
            employeeService.UpdateEmployee(changedEmployee);
            employee = employees.First();

            //Assert
            Assert.Equal("Promijenjen", employee.name);
        }

        //David Matijanić
        [Fact]
        public void DeleteEmployee_EmployeeHasActiveBorrows_ThrowsEmployeeException() {
            //Arrange
            Employee employee = employees.First();
            PrepareBorrowAndArchiveServices(employee, new List<Borrow> { new Borrow() }, new List<Archive> { new Archive() });

            //Act & assert
            Assert.Throws<EmployeeException>(() => employeeService.DeleteEmployee(employee));
        }

        //David Matijanić
        [Fact]
        public void DeleteEmployee_EmployeeHasActiveArchives_ThrowsEmployeeException() {
            //Arrange
            Employee employee = employees.First();
            PrepareBorrowAndArchiveServices(employee, new List<Borrow>(), new List<Archive> { new Archive() });

            //Act & assert
            Assert.Throws<EmployeeException>(() => employeeService.DeleteEmployee(employee));
        }

        //David Matijanić
        [Fact]
        public void DeleteEmployee_EmployeeHasNoActiveBorrowsOrArchives_EmployeeIsDeleted() {
            //Arrange
            Employee employee = employees.First();
            PrepareBorrowAndArchiveServices(employee, new List<Borrow>(), new List<Archive>());

            //Act
            employeeService.DeleteEmployee(employee);

            //Assert
            Assert.DoesNotContain(employee, employees);
        }

        //David Matijanić
        [Fact]
        public void GetEmployeesByUsernam_NoEmployeesExist_NoEmployeesReturned() {
            //Arrange
            A.CallTo(() => employeeRepository.GetEmployeeId("ddaric")).Returns(1);
            A.CallTo(() => employeeRepository.GetEmployeesById(1)).Returns(new List<Employee>().AsQueryable());

            //Act
            var retrievedEmployee = employeeService.GetEmployeeByUsername("ddaric");

            //Assert
            Assert.Null(retrievedEmployee);
        }

        //David Matijanić
        [Theory]
        [InlineData("ddaric")]
        [InlineData("mmisic")]
        [InlineData("ssivic")]
        [InlineData("llalic")]
        [InlineData("aanic")]
        [InlineData("iivic")]
        [InlineData("ppreradovic")]
        [InlineData("mmarkovic")]
        public void GetEmployeeByUsername_EmployeeWithUsernameExists_EmployeeReturned(string employeeUsername) {
            //Arrange
            A.CallTo(() => employeeRepository.GetEmployeeId(employeeUsername)).Returns(employees.Where(e => e.username == employeeUsername).First().id);
            A.CallTo(() => employeeRepository.GetEmployeesById(A<int>.Ignored)).ReturnsLazily((int _id) => {
                return employees.Where(e => e.id == _id);
            });

            //Act
            var retrievedEmployee = employeeService.GetEmployeeByUsername(employeeUsername);

            //Assert
            Assert.Equal(employeeUsername, retrievedEmployee.username);
        }

        //David Matijanić
        [Fact]
        public void GetEmployeeByUsername_NonExistingEmployeeUsername_EmployeeIsNotReturned() {
            //Arrange
            string username = "nonexisting";
            A.CallTo(() => employeeRepository.GetEmployeeId(username)).Returns(-99);
            A.CallTo(() => employeeRepository.GetEmployeesById(A<int>.Ignored)).ReturnsLazily((int _id) => {
                return employees.Where(e => e.id == _id);
            });

            //Act
            var retrievedEmployee = employeeService.GetEmployeeByUsername(username);

            //Assert
            Assert.Null(retrievedEmployee);
        }

        //TODO: Implementirati DISPOSE kad EmployeeService bude imao IDisposable implementiran! (@dmatijani21)

        //David Matijanić
        private void PrepareEmployeeRepositoryMethods(Employee employee) {
            A.CallTo(() => employeeRepository.GetEmployeesById(employee.id)).Returns(employees.Where(e => e.id == employee.id));
            A.CallTo(() => employeeRepository.GetEmployeesByUsername(employee.username)).Returns(employees.Where(e => e.username == employee.username));
            A.CallTo(() => employeeRepository.GetEmployeesByOIB(employee.OIB)).Returns(employees.Where(e => e.OIB == employee.OIB));
        }

        //David Matijanić
        private void PrepareBorrowAndArchiveServices(Employee employee, List<Borrow> borrows, List<Archive> archives) {
            IBorrowRepository borrowRepository = A.Fake<IBorrowRepository>();
            A.CallTo(() => borrowRepository.GetBorrowsForEmployee(employee.id)).Returns(borrows.AsQueryable());
            BorrowService borrowService = new BorrowService(borrowRepository, null, null);

            IArchiveRepository archiveRepository = A.Fake<IArchiveRepository>();
            A.CallTo(() => archiveRepository.GetArchivesForEmployee(employee.id)).Returns(archives.AsQueryable());
            ArchiveServices archiveService = new ArchiveServices(archiveRepository);

            A.CallTo(() => employeeRepository.Remove(employee)).Invokes(call => {
                employees = employees.Where(e => e.id != employee.id);
            });

            employeeService = new EmployeeService(employeeRepository, borrowService, archiveService);
        }
    }
}
