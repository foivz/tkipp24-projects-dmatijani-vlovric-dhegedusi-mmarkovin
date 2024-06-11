using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
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
                    membership_duration = GetDateFromMembershipDuration(30)
                },
                new Library {
                    id = 2,
                    name = "Knjiznica 2",
                    OIB = "22222222222",
                    price_day_late = 5,
                    membership_duration = GetDateFromMembershipDuration(24)
                },
                new Library {
                    id = 3,
                    name = "Knjiznica 3",
                    OIB = "33333333333",
                    price_day_late = Convert.ToDecimal(3.2),
                    membership_duration = GetDateFromMembershipDuration(35)
                },
                new Library {
                    id = 4,
                    name = "Knjiznica 4",
                    OIB = "44444444444",
                    price_day_late = Convert.ToDecimal(4.5),
                    membership_duration = GetDateFromMembershipDuration(31)
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
