using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using BussinessLogicLayer.services;
using EntitiesLayer;
using BussinessLogicLayer.Exceptions;

namespace IntegrationTesting
{
    [Collection("Database collection")]
    public class LibraryService_integrationTest
    {
        private readonly LibraryService libraryService;
        private readonly DatabaseFixture fixture;

        private readonly List<Library> libraries;

        public LibraryService_integrationTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
            this.fixture.ResetDatabase();

            libraries = new List<Library>
            {
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
                    price_day_late = 3,
                    membership_duration = GetDateFromMembershipDuration(35)
                },
                new Library {
                    id = 4,
                    name = "Knjiznica 4",
                    OIB = "44444444444",
                    price_day_late = 4,
                    membership_duration = GetDateFromMembershipDuration(31)
                },
                new Library {
                    id = 5,
                    name = "Knjiznica 5",
                    OIB = "55555555555",
                    price_day_late = 4,
                    membership_duration = GetDateFromMembershipDuration(68)
                }
            };
            InsertLibrariesIntoDatabase(libraries);

            libraryService = new LibraryService();
        }

        //David Matijanić
        [Fact]
        public void GetAllLibraries_LibrariesExist_LibrariesRetrieved()
        {
            //Act
            var retrievedLibraries = libraryService.GetAllLibraries();

            //Assert
            retrievedLibraries.Should().BeEquivalentTo(libraries, options => options
                .Excluding(l => l.Members)
                .Excluding(l => l.Employees)
                .Excluding(l => l.Books)
                .Excluding(l => l.Notifications)
            );
        }

        //David Matijanić
        [Fact]
        public async Task GetAllLibrariesAsync_LibrariesExist_LibrariesRetrieved()
        {
            //Act
            var retrievedLibraries = await libraryService.GetAllLibrariesAsync();

            //Assert
            retrievedLibraries.Should().BeEquivalentTo(libraries, options => options
                .Excluding(l => l.Members)
                .Excluding(l => l.Employees)
                .Excluding(l => l.Books)
                .Excluding(l => l.Notifications)
            );
        }

        //David Matijanić
        [Fact]
        public void AddLibrary_LibraryWithSameIDExists_ThrowsLibraryWithSameIDException()
        {

            //Arrange
            var newLibrary = new Library
            {
                id = 1,
                name = "Knjiznica 6",
                OIB = "66666676666",
                price_day_late = 3,
                membership_duration = GetDateFromMembershipDuration(30)
            };

            //Act
            Action act = () => libraryService.AddLibrary(newLibrary);

            //Assert
            act.Should().Throw<LibraryWithSameIDException>().WithMessage("Knjižnica sa istim ID već postoji!");
        }

        //David Matijanić
        [Fact]
        public void AddLibrary_LibraryWithSameOIBExists_ThrowsLibraryWithSameOIBException()
        {
            //Arrange
            var newLibrary = new Library
            {
                id = 6,
                name = "Knjiznica 6",
                OIB = "55555555555",
                price_day_late = 3,
                membership_duration = GetDateFromMembershipDuration(30)
            };

            //Act
            Action act = () => libraryService.AddLibrary(newLibrary);

            //Assert
            act.Should().Throw<LibraryWithSameOIBException>().WithMessage("Knjižnica sa istim OIB već postoji!");
        }

        //David Matijanić
        [Fact]
        public void AddLibrary_LibraryWithUniqueIdAndOIB_LibraryIsAdded()
        {
            //Arrange
            var newLibrary = new Library
            {
                id = 6,
                name = "Knjiznica 6",
                OIB = "66666666667",
                price_day_late = 3,
                membership_duration = GetDateFromMembershipDuration(30)
            };

            //Act
            var result = libraryService.AddLibrary(newLibrary);

            //Assert
            Assert.Equal(1, result);
        }

        //David Matijanić
        [Fact]
        public void DeleteLibrary_LibraryHasEmployees_ThrowsLibraryHasEmployeesException()
        {
            //Arrange
            Library library = libraries.First();
            string sqlEmployee = "INSERT INTO [dbo].[Employee] ([username], [password], [OIB], [library_id]) " +
                $"VALUES (N'zaposlenik1', N'lozinka1', '11111111111', {library.id})";
            Helper.ExecuteCustomSql(sqlEmployee);

            //Act
            Action act = () => libraryService.DeleteLibrary(library);

            //Assert
            act.Should().Throw<LibraryHasEmployeesException>();
        }

        //David Matijanić
        [Fact]
        public void DeleteLibrary_LibraryHasMembers_ThrowsLibraryHasMembersException()
        {
            //Arrange
            Library library = libraries.First();
            string sqlMember = "INSERT INTO [dbo].[Member] ([name], [surname], [username], [password], [OIB], [Library_id], [barcode_id]) " + $"VALUES ('Member', 'Memberic', 'member1', '123', '12344433112', {library.id}, '1243nsdf')";
            Helper.ExecuteCustomSql(sqlMember);

            //Act
            Action act = () => libraryService.DeleteLibrary(library);

            //Assert
            act.Should().Throw<LibraryHasMembersException>();
        }

        //David Matijanić
        [Fact]
        public void DeleteLibrary_LibraryHasBooks_ThrowsLibraryHasBooksException()
        {
            //Arrange
            Library library = libraries.First();
            string sqlGenre = $"INSERT [dbo].[Genre] ([name]) VALUES ('Knjigin zanr')";
            Helper.ExecuteCustomSql(sqlGenre);
            
            string sqlBook = $"INSERT [dbo].[Book] ([name], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) VALUES ('Knjiznicina knjiga', 82, 0, '234xcv', 5, 4, 1, {library.id});";
            Helper.ExecuteCustomSql(sqlBook);

            //Act
            Action act = () => libraryService.DeleteLibrary(library);

            //Assert
            act.Should().Throw<LibraryHasBooksException>();
        }

        //David Matijanić
        [Fact]
        public void DeleteLibrary_LibraryHasNotifications_ThrowsLibraryException()
        {
            //Arrange
            Library library = libraries.First();
            string sqlNotification = $"INSERT [dbo].[Notification] ([title], [description], [Library_id]) VALUES ('Naslov notifikacije', 'Ovo je notifikacija koja sprjecava brisanje', {library.id})";
            Helper.ExecuteCustomSql(sqlNotification);

            //Act
            Action act = () => libraryService.DeleteLibrary(library);

            //Assert
            act.Should().Throw<LibraryException>();
        }

        //David Matijanić
        [Fact]
        public void DeleteLibrary_LibraryHasNothing_DeletesLibrary()
        {
            //Arrange
            Library library = libraries.First();

            //Act
            int deleted = libraryService.DeleteLibrary(library);

            //Assert
            Assert.Equal(1, deleted);
        }

        //David Matijanić
        [Fact]
        public void UpdateLibrary_LibraryWithSameOIBExists_ThrowsLibraryWithSameOIBException()
        {
            //Arrange
            Library library = libraries.First();
            library.OIB = "55555555555";

            //Act
            Action act = () => libraryService.UpdateLibrary(library);

            //Assert
            act.Should().Throw<LibraryWithSameOIBException>().WithMessage("Druga knjižnica već ima taj OIB!");
        }

        //David Matijanić
        [Fact]
        public void UpdateLibrary_LibraryExistsAndHasNameChanged_LibraryIsUpdated()
        {
            //Arrange
            Library library = libraries.First();
            library.name = "Novi naziv";

            //Act
            int updated = libraryService.UpdateLibrary(library);

            //Assert
            Assert.Equal(1, updated);
        }

        //David Matijanić
        [Fact]
        public void GetLibraryPriceDayLate_LibraryIsEntered_CorrectPriceDayLateIsReturned()
        {
            //Arrange
            Library library = libraries.First();

            //Act
            decimal priceDayLate = libraryService.GetLibraryPriceDayLate(library);

            //Assert
            Assert.Equal(library.price_day_late, priceDayLate);
        }

        //David Matijanić
        [Fact]
        public void GetLibraryMembershipDuration_LibraryIdIsEntered_CorrectMembershipDurationIsReturned()
        {
            //Arrange
            Library library = libraries.First();

            //Act
            decimal membershipDuration = libraryService.GetLibraryMembershipDuration(library.id);

            //Assert
            Assert.Equal(CalculateMembershipDurationFromDate(library.membership_duration), membershipDuration);
        }
        
        private void InsertLibrariesIntoDatabase(List<Library> libraryList)
        {
            foreach (var library in libraryList)
            {
                InsertLibraryIntoDatabase(library);
            }
        }

        private void InsertLibraryIntoDatabase(Library library)
        {
            string createLibrary =
            "INSERT [dbo].[Library] ([id], [name], [OIB], [phone], [email], [price_day_late], [address], [membership_duration]) " +
            $"VALUES ({library.id}, N'{library.name}', '{library.OIB}', NULL, NULL, {library.price_day_late}, NULL, '{library.membership_duration.ToString("yyyy-MM-dd")}')";
            Helper.ExecuteCustomSql(createLibrary);
        }

        private DateTime GetDateFromMembershipDuration(decimal duration)
        {
            DateTime startDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
            DateTime durationDate = startDate.AddDays(Convert.ToDouble(duration) - 1);

            return durationDate;
        }

        private decimal CalculateMembershipDurationFromDate(DateTime date)
        {
            return (date - new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Unspecified)).Days + 1;
        }
    }
}
