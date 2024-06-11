using BussinessLogicLayer.Exceptions;
using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using EntitiesLayer;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting {
    public class LibraryService_unitTest {
        private LibraryService libraryService;
        private readonly ILibraryRepository libraryRepository;
        private IQueryable<Library> libraries { get; set; }

        public LibraryService_unitTest() {
            libraryRepository = A.Fake<ILibraryRepository>();
            libraryService = new LibraryService(libraryRepository, null, null, null, null);

            libraries = new List<Library> {
                new Library {
                    id = 1,
                    name = "Knjiznica 1",
                    OIB = "11111111111",
                    price_day_late = 3,
                    membership_duration = GetDateFromMembershipDuration(30),
                    Employees = new List<Employee> {
                        new Employee()
                    },
                    Members = new List<Member>(),
                    Books = new List<Book>(),
                    Notifications = new List<Notification>()
                },
                new Library {
                    id = 2,
                    name = "Knjiznica 2",
                    OIB = "22222222222",
                    price_day_late = 5,
                    membership_duration = GetDateFromMembershipDuration(24),
                    Employees = new List<Employee>(),
                    Members = new List<Member> {
                        new Member()
                    },
                    Books = new List<Book>(),
                    Notifications = new List<Notification>()
                },
                new Library {
                    id = 3,
                    name = "Knjiznica 3",
                    OIB = "33333333333",
                    price_day_late = Convert.ToDecimal(3.2),
                    membership_duration = GetDateFromMembershipDuration(35),
                    Employees = new List<Employee>(),
                    Members = new List<Member>(),
                    Books = new List<Book> {
                        new Book()
                    },
                    Notifications = new List<Notification>()
                },
                new Library {
                    id = 4,
                    name = "Knjiznica 4",
                    OIB = "44444444444",
                    price_day_late = Convert.ToDecimal(4.5),
                    membership_duration = GetDateFromMembershipDuration(31),
                    Employees = new List<Employee>(),
                    Members = new List<Member>(),
                    Books = new List<Book>(),
                    Notifications = new List<Notification> {
                        new Notification()
                    }
                },
                new Library {
                    id = 5,
                    name = "Knjiznica 5",
                    OIB = "55555555555",
                    price_day_late = Convert.ToDecimal(4.3),
                    membership_duration = GetDateFromMembershipDuration(68),
                    Employees = new List<Employee>(),
                    Members = new List<Member>(),
                    Books = new List<Book>(),
                    Notifications = new List<Notification>()
                }
            }.AsQueryable();
        }

        //David Matijanić
        [Fact]
        public void Constructor_WhenLibraryServiceIsInstantiated_ItIsNotNull() {
            //Arrange & act
            var service = new LibraryService();

            //Assert
            Assert.NotNull(service);
        }

        [Fact]
        public void GetAllLibraries_NoLibrariesExist_NoLibrariesReturned() {
            //Arrange
            A.CallTo(() => libraryRepository.GetAll()).Returns(new List<Library>().AsQueryable());

            //Act
            var retrievedLibraries = libraryService.GetAllLibraries();

            //Assert
            Assert.Empty(retrievedLibraries);
        }

        [Fact]
        public void GetAllLibraries_LibrariesExist_LibrariesRetrieved() {
            //Arrange
            A.CallTo(() => libraryRepository.GetAll()).Returns(libraries);

            //Act
            var retrievedLibraries = libraryService.GetAllLibraries();

            //Assert
            Assert.Equal(libraries, retrievedLibraries);
        }

        [Fact]
        public async Task GetAllLibrariesAsync_NoLibrariesExist_NoLibrariesReturned() {
            //Arrange
            A.CallTo(() => libraryRepository.GetAllLibrariesAsync()).Returns(Task.FromResult(new List<Library>()));

            //Act
            var retrievedLibraries = await libraryService.GetAllLibrariesAsync();

            //Assert
            Assert.Empty(retrievedLibraries);
        }

        [Fact]
        public async Task GetAllLibrariesAsync_LibrariesExist_LibrariesRetrieved() {
            //Arrange
            A.CallTo(() => libraryRepository.GetAllLibrariesAsync()).Returns(Task.FromResult(libraries.ToList()));

            //Act
            var retrievedLibraries = await libraryService.GetAllLibrariesAsync();

            //Assert
            Assert.Equal(libraries, retrievedLibraries);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void AddLibrary_LibraryWithSameIDExists_ThrowsLibraryWithSameIDException(int libraryId) {
            //Arrange
            Library library = new Library {
                id = libraryId
            };
            PrepareLibraryRepositoryMethods(library);

            //Act & assert
            Assert.Throws<LibraryWithSameIDException>(() => libraryService.AddLibrary(library));
        }

        [Theory]
        [InlineData("11111111111")]
        [InlineData("22222222222")]
        [InlineData("33333333333")]
        [InlineData("44444444444")]
        [InlineData("55555555555")]
        public void AddLibrary_LibraryWithSameOIBExists_ThrowsLibraryWithSameOIBException(string libraryOib) {
            //Arrange
            Library library = new Library {
                id = 6,
                OIB = libraryOib
            };
            PrepareLibraryRepositoryMethods(library);

            //Act & assert
            Assert.Throws<LibraryWithSameOIBException>(() => libraryService.AddLibrary(library));
        }

        [Fact]
        public void AddLibrary_LibraryWithUniqueIdAndOib_LibraryIsAdded() {
            //Arrange
            Library library = new Library {
                id = 6,
                OIB = "66666666666",
                name = "Nova knjiznica"
            };
            PrepareLibraryRepositoryMethods(library);
            A.CallTo(() => libraryRepository.Add(library)).Invokes(call => {
                List<Library> newLibraries = libraries.ToList();
                newLibraries.Add(library);
                libraries = newLibraries.AsQueryable();
            });

            //Act
            libraryService.AddLibrary(library);

            //Assert
            Assert.Contains(library, libraries);
        }

        [Fact]
        public void DeleteLibrary_LibraryHasEmployees_ThrowsLibraryHasEmployeesException() {
            //Arrange
            Library library = libraries.ToList()[0];
            PrepareServicesForDeleteMethod(library);

            //Act & assert
            Assert.Throws<LibraryHasEmployeesException>(() => libraryService.DeleteLibrary(library));
        }

        [Fact]
        public void DeleteLibrary_LibraryHasMembers_ThrowsLibraryHasMemberrsException() {
            //Arrange
            Library library = libraries.ToList()[1];
            PrepareServicesForDeleteMethod(library);

            //Act & assert
            Assert.Throws<LibraryHasMembersException>(() => libraryService.DeleteLibrary(library));
        }

        [Fact]
        public void DeleteLibrary_LibraryHasBooks_ThrowsLibraryHasBooksException() {
            //Arrange
            Library library = libraries.ToList()[2];
            PrepareServicesForDeleteMethod(library);

            //Act & assert
            Assert.Throws<LibraryHasBooksException>(() => libraryService.DeleteLibrary(library));
        }

        [Fact]
        public void DeleteLibrary_LibraryHasNotifications_ThrowsLibraryException() {
            //Arrange
            Library library = libraries.ToList()[3];
            PrepareServicesForDeleteMethod(library);

            //Act & assert
            Assert.Throws<LibraryException>(() => libraryService.DeleteLibrary(library));
        }

        [Fact]
        public void DeleteLibrary_LibraryHasNothing_DeletesLibrary() {
            //Arrange
            Library library = libraries.ToList()[4];
            PrepareServicesForDeleteMethod(library);
            A.CallTo(() => libraryRepository.Remove(library)).Invokes(call => {
                var librariesWithNoDeleted = libraries.Where(l => l.id != library.id);
                libraries = librariesWithNoDeleted;
            });

            //Act
            libraryService.DeleteLibrary(library);

            //Assert
            Assert.DoesNotContain(library, libraries);
        }

        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void UpdateLibrary_NoLibraryWithThatIdExists_ThrowsLibraryWithSameIdException(int libraryId) {
            //Arrange
            Library library = new Library {
                id = libraryId,
                name = "Izmijenjena"
            };
            PrepareLibraryRepositoryMethods(library);

            //Act & assert
            Assert.Throws<LibraryWithSameIDException>(() => libraryService.UpdateLibrary(library));
        }

        [Theory]
        [InlineData(1, "22222222222")]
        [InlineData(2, "33333333333")]
        [InlineData(3, "44444444444")]
        [InlineData(4, "55555555555")]
        [InlineData(5, "11111111111")]
        public void UpdateLibrary_LibraryWithOibAlreadyExists_ThrowsLibraryWithSameOIBException(int libraryId, string libraryOib) {
            //Arrange
            Library library = new Library {
                id = libraryId,
                OIB = libraryOib,
                name = "Izmijenjena"
            };
            PrepareLibraryRepositoryMethods(library);

            //Act & assert
            Assert.Throws<LibraryWithSameOIBException>(() => libraryService.UpdateLibrary(library));
        }

        [Fact]
        public void UpdateLibrary_LibraryExistsAndHasNameChanged_LibraryIsUpdated() {
            //Arrange
            Library library = libraries.First();
            Library newLibrary = new Library {
                id = library.id,
                OIB = library.OIB,
                name = "Izmijenjena"
            };
            PrepareLibraryRepositoryMethods(newLibrary);

            //Act
            libraryService.UpdateLibrary(newLibrary);
            library = libraries.First();

            //Assert
            Assert.Equal(newLibrary.name, library.name);
        }

        [Fact]
        public void UpdateLibrary_LibraryExistsAndHasOIBChanged_LibraryIsUpdated() {
            //Arrange
            Library library = libraries.First();
            Library newLibrary = new Library {
                id = library.id,
                OIB = "12345678901"
            };
            PrepareLibraryRepositoryMethods(newLibrary);

            //Act
            libraryService.UpdateLibrary(newLibrary);
            library = libraries.First();

            //Assert
            Assert.Equal(newLibrary.OIB, library.OIB);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void GetLibraryPriceDayLate_LibraryIdIsEntered_CorrectPriceDayLateIsReturned(int libraryId) {
            //Arrange
            decimal correctPriceDayLate = libraries.Where(l => l.id == libraryId).First().price_day_late;
            A.CallTo(() => libraryRepository.GetLibraryPriceDayLate(libraryId)).Returns(correctPriceDayLate);

            //Act
            decimal priceDayLate = libraryService.GetLibraryPriceDayLate(libraryId);

            //Assert
            Assert.Equal(correctPriceDayLate, priceDayLate);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void GetLibraryPriceDayLate_LibraryIsEntered_CorrectPriceDayLateIsReturned(int libraryId) {
            //Arrange
            Library library = new Library { id = libraryId };
            decimal correctPriceDayLate = libraries.Where(l => l.id == libraryId).First().price_day_late;
            A.CallTo(() => libraryRepository.GetLibraryPriceDayLate(libraryId)).Returns(correctPriceDayLate);

            //Act
            decimal priceDayLate = libraryService.GetLibraryPriceDayLate(library);

            //Assert
            Assert.Equal(correctPriceDayLate, priceDayLate);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void GetLibraryMembershipDuration(int libraryId) {
            //Arrange
            DateTime correctMembershipDurationDatetime = libraries.Where(l => l.id == libraryId).First().membership_duration;
            decimal correctMembershipDuration = CalculateMembershipDurationFromDate(correctMembershipDurationDatetime);
            A.CallTo(() => libraryRepository.GetLibraryMembershipDuration(libraryId)).Returns(correctMembershipDurationDatetime);

            //Act
            decimal membershipDuration = libraryService.GetLibraryMembershipDuration(libraryId);

            //Assert
            Assert.Equal(correctMembershipDuration, membershipDuration);
        }

        //David Matijanić
        [Fact]
        public void Dispose_CallsDisposeOnRepository() {
            //Act
            libraryService.Dispose();

            //Assert
            A.CallTo(() => libraryRepository.Dispose()).MustHaveHappened();
        }

        private void PrepareLibraryRepositoryMethods(Library library) {
            A.CallTo(() => libraryRepository.GetLibrariesById(library.id)).Returns(libraries.Where(l => l.id == library.id));
            A.CallTo(() => libraryRepository.GetLibrariesByOIB(library.OIB)).Returns(libraries.Where(l => l.OIB == library.OIB));
            A.CallTo(() => libraryRepository.Update(library, true)).Invokes(call => {
                foreach (var l in libraries.Where(l => l.id == library.id)) {
                    l.name = library.name;
                    l.OIB = library.OIB;
                    l.price_day_late = library.price_day_late;
                    l.address = library.address;
                    l.email = library.email;
                    l.membership_duration = library.membership_duration;
                }
            });
        }

        private void PrepareServicesForDeleteMethod(Library library) {
            IEmpoloyeeRepositroy employeeRepository = A.Fake<IEmpoloyeeRepositroy>();
            A.CallTo(() => employeeRepository.GetEmployeesByLibrary(library)).Returns(library.Employees.AsQueryable());
            EmployeeService employeeService = new EmployeeService(employeeRepository, null, null);

            IMembersRepository memberRepository = A.Fake<IMembersRepository>();
            A.CallTo(() => memberRepository.GetMembersByLibrary(library.id)).Returns(library.Members.AsQueryable());
            MemberService memberService = new MemberService(memberRepository, null, null, null, null);

            IBookRepository bookRepository = A.Fake<IBookRepository>();
            A.CallTo(() => bookRepository.GetBooksByLibrary(library.id)).Returns(library.Books.AsQueryable());
            BookServices bookService = new BookServices(bookRepository, null, null);

            INotificationsRepository notificationRepository = A.Fake<INotificationsRepository>();
            A.CallTo(() => notificationRepository.GetAllNotificationsForLibrary(library.id)).Returns(library.Notifications.AsQueryable());
            NotificationService notificationService = new NotificationService(notificationRepository, null);

            libraryService = new LibraryService(libraryRepository, employeeService, memberService, bookService, notificationService);
        }

        private decimal CalculateMembershipDurationFromDate(DateTime date) {
            return (date - new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Unspecified)).Days + 1;
        }

        private DateTime GetDateFromMembershipDuration(decimal duration) {
            DateTime startDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
            DateTime durationDate = startDate.AddDays(Convert.ToDouble(duration) - 1);

            return durationDate;
        }
    }
}
