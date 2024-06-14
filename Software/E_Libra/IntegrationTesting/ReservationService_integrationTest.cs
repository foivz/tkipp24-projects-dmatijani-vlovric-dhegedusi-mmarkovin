using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using BussinessLogicLayer.services;
using EntitiesLayer;

namespace IntegrationTesting
{
    [Collection("Database collection")]
    public class ReservationService_integrationTest
    {
        readonly ReservationService reservationService;
        readonly DatabaseFixture databaseFixture;
        readonly List<Book> books;

        readonly string createLibrary =
            "INSERT [dbo].[Library] ([id], [name], [OIB], [phone], [email], [price_day_late], [address], [membership_duration]) " +
            "VALUES (1, N'Knjiznica', 12345, 331, N'email', 3, N'adresa', GETDATE())";

        readonly string createGenre =
            "INSERT [dbo].[Genre] ([name]) VALUES (N'zanr')";

        readonly Genre genre = new Genre { id = 1, name = "zanr" };

        private List<Book> CreateThreeBooks(int libraryId = 1, int startFrom = 1)
        {
            List<Book> books = new List<Book>
            {
                new Book
                {
                    id = 0 + startFrom,
                    name = "BookName1",
                    description = null,
                    publish_date = null,
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12345",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = genre,
                    Library_id = libraryId,
                    Genre_id = genre.id
                },
                new Book
                {
                    id = 1 + startFrom,
                    name = "BookName2",
                    description = null,
                    publish_date = null,
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12346",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = genre,
                    Library_id = libraryId,
                    Genre_id = genre.id
                },
                new Book
                {
                    id = 2 + startFrom,
                    name = "BookName3",
                    description = null,
                    publish_date = null,
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12347",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = genre,
                    Library_id = libraryId,
                    Genre_id = genre.id
                }
            };

            string sqlBook1 = $"INSERT INTO [dbo].[Book] ([name], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) " +
                   $"VALUES ('{books[0].name}', '{books[0].pages_num}', '{books[0].digital}', '{books[0].barcode_id}', '{books[0].total_copies}', '{books[0].current_copies}', '{books[0].Genre.id}', '{books[0].Library_id}');";
            string sqlBook2 = $"INSERT INTO [dbo].[Book] ([name], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) " +
                   $"VALUES ('{books[1].name}', '{books[1].pages_num}', '{books[1].digital}', '{books[1].barcode_id}', '{books[1].total_copies}', '{books[1].current_copies}', '{books[1].Genre.id}', '{books[1].Library_id}');";
            string sqlBook3 = $"INSERT INTO [dbo].[Book] ([name], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) " +
                   $"VALUES ('{books[2].name}', '{books[2].pages_num}', '{books[2].digital}', '{books[2].barcode_id}', '{books[2].total_copies}', '{books[2].current_copies}', '{books[2].Genre.id}', '{books[2].Library_id}');";

            Helper.ExecuteCustomSql(sqlBook1);
            Helper.ExecuteCustomSql(sqlBook2);
            Helper.ExecuteCustomSql(sqlBook3);

            return books;
        }

        private void CreateMembers()
        {
            string sqlMember1 = "INSERT INTO [dbo].[Member] ([name], [surname], [OIB], [membership_date], [barcode_id], [username], [password], [Library_id]) " +
                "VALUES (N'ime', N'prezime', 1234, GETDATE(), 12345, 'Member1', '123', 1);";
            string sqlMember2 = "INSERT INTO [dbo].[Member] ([name], [surname], [OIB], [membership_date], [barcode_id], [username], [password], [Library_id]) " +
                "VALUES (N'ime2', N'prezime2', 1235, GETDATE(), 12346, 'Member2', '123', 1);";
            Helper.ExecuteCustomSql(sqlMember1);
            Helper.ExecuteCustomSql(sqlMember2);
        }

        private void CreateTwoReservationsForMemberOne()
        {
            string sqlReservation1 = "INSERT INTO [dbo].[Reservation] ([Member_id], [Book_id]) VALUES (1, 1);";
            string sqlReservation2 = "INSERT INTO [dbo].[Reservation] ([Member_id], [Book_id]) VALUES (1, 2);";
            Helper.ExecuteCustomSql(sqlReservation1);
            Helper.ExecuteCustomSql(sqlReservation2);
        }

        private void CreateReservation()
        {
            string sqlReservation = "INSERT INTO [dbo].[Reservation] ([reservation_date], [Member_id], [Book_id]) VALUES (GETDATE(), 1, 1);";
            Helper.ExecuteCustomSql(sqlReservation);
        }

        public ReservationService_integrationTest(DatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
            this.databaseFixture.ResetDatabase();
            reservationService = new ReservationService();

            Helper.ExecuteCustomSql(createLibrary);
            Helper.ExecuteCustomSql(createGenre);

            CreateMembers();
            this.books = CreateThreeBooks();
        }

        //Viktor Lovrić
        [Fact]
        public void CheckNumberOfReservations_GivenFunctionIsCalled_ReturnsNumberOfReservations()
        {
            //Arrange
            string sqlReservation1 = "INSERT INTO [dbo].[Reservation] ([reservation_date], [Member_id], [Book_id]) VALUES (GETDATE(), 1, 1);";
            string sqlReservation2 = "INSERT INTO [dbo].[Reservation] ([reservation_date], [Member_id], [Book_id]) VALUES (GETDATE(), 2, 1);";
            Helper.ExecuteCustomSql(sqlReservation1);
            Helper.ExecuteCustomSql(sqlReservation2);

            //Act
            int result = reservationService.CheckNumberOfReservations(1);

            //Assert
            result.Should().Be(2);
        }

        //Viktor Lovrić
        [Fact]
        public void CheckExistingReservation_GivenFunctionIsCalled_ReturnsTrue()
        {
            //Arrange
            CreateReservation();

            //Act
            bool result = reservationService.CheckExistingReservation(1, 1);

            //Assert
            result.Should().BeTrue();
        }

        //Viktor Lovrić
        [Fact]
        public void AddReservation_GivenReservationIsAdded_ReturnsOne()
        {
            //Arrange
            var reservation = new Reservation
            {
                reservation_date = DateTime.Now,
                Member_id = 1,
                Book_id = 1
            };

            //Act
            int result = reservationService.AddReservation(reservation);

            //Assert
            result.Should().Be(1);
        }

        //Viktor Lovrić
        [Fact]
        public void GetReservationForMember_GivenFunctionIsCalled_ReturnsReservationsForMember()
        {
            //Arrange
            CreateTwoReservationsForMemberOne();

            List<ReservationViewModel> reservationViewModels = new List<ReservationViewModel>
            {
                new ReservationViewModel
                {
                    ReservationId = 1,
                    BookName = "BookName1",
                    Date = null,
                },
                new ReservationViewModel
                {
                    ReservationId = 2,
                    BookName = "BookName2",
                    Date = null,
                }
            };

            //Act
            var result = reservationService.GetReservationForMember(1);

            //Assert
            result.Should().BeEquivalentTo(reservationViewModels);
        }

        //Viktor Lovrić
        [Fact]
        public void CountExistingReservations_GivenMemberIDIsPassed_ReturnsNumberOfReservations()
        {
            //Arrange
            CreateTwoReservationsForMemberOne();

            //Act
            int result = reservationService.CountExistingReservations(1);

            //Assert
            result.Should().Be(2);
        }

        //Viktor Lovrić
        [Fact]
        public void EnterDateForReservation_GivenBookIsPassed_ReturnsTrue()
        {
            //Arrange
            CreateTwoReservationsForMemberOne();

            //Act
            bool result = reservationService.EnterDateForReservation(books[0]);

            //Assert
            result.Should().BeTrue();
        }

        //Viktor Lovrić
        [Fact]
        public void GetReservationId_GivenMemberIDAndBookIDArePassed_ReturnsReservationID()
        {
            //Arrange
            CreateTwoReservationsForMemberOne();

            //Act
            int result = reservationService.GetReservationId(1, 2);

            //Assert
            result.Should().Be(2);
        }

        //Viktor Lovrić
        [Fact]
        public void GetReservationPosition_GivenReservationIDAndBookIDArePassed_ReturnsPosition()
        {
            //Arrange
            CreateTwoReservationsForMemberOne();
            string sqlReservation = "INSERT INTO [dbo].[Reservation] ([reservation_date], [Member_id], [Book_id]) VALUES (GETDATE(), 2, 1);";
            Helper.ExecuteCustomSql(sqlReservation);

            //Act
            int result = reservationService.GetReservationPosition(3, 1);

            //Assert
            result.Should().Be(2);
        }

        //Viktor Lovrić
        [Fact]
        public void ShowExistingReservations_GivenFunctionIsCalled_ReturnsString()
        {
            //Arrange
            LoggedUser.Username = "Member1";
            string sqlReservation1 = "INSERT INTO [dbo].[Reservation] ([reservation_date], [Member_id], [Book_id]) VALUES (GETDATE(), 1, 1);";
            string sqlReservation2 = "INSERT INTO [dbo].[Reservation] ([reservation_date], [Member_id], [Book_id]) VALUES (GETDATE(), 1, 2);";
            Helper.ExecuteCustomSql(sqlReservation1);
            Helper.ExecuteCustomSql(sqlReservation2);

            //Act
            string result = reservationService.ShowExistingReservations();

            //Assert
            result.Should().NotBeNullOrEmpty();
        }

        //Viktor Lovrić
        [Fact]
        public void CheckValidReservationForMember_GivenMemberIDAndBookIDArePassed_ReturnsReservation()
        {
            //Arrange
            CreateReservation();

            Reservation reservation = new Reservation
            {
                idReservation = 1,
                reservation_date = DateTime.Now.Date,
                Member_id = 1,
                Book_id = 1
            };

            //Act
            var result = reservationService.CheckValidReservationFroMember(1, 1);

            //Assert
            result.Should().BeEquivalentTo(reservation, options => options
            .Excluding(e => e.Book)
            .Excluding(e => e.Member)
            );
        }

        //Viktor Lovrić
        [Fact]
        public void RemoveReservation_GivenReservationIsRemoved_ReturnsOne()
        {
            //Arrange
            CreateReservation();

            Reservation reservation = new Reservation
            {
                idReservation = 1,
                reservation_date = DateTime.Now.Date,
                Member_id = 1,
                Book_id = 1
            };

            //Act
            int result = reservationService.RemoveReservation(reservation);

            //Assert
            result.Should().Be(1);
        }

        // Magdalena Markovinovič
        [Fact]
        private void GetReservationsForMemberNormal_GivenMemberIDIsPassed_ReturnsReservationsForMember()
        {
            //Arrange
            CreateTwoReservationsForMemberOne();

            List<Reservation> reservations = new List<Reservation>
            {
                new Reservation
                {
                    idReservation = 1,
                    reservation_date = null,
                    Member_id = 1,
                    Book_id = 1
                },
                new Reservation
                {
                    idReservation = 2,
                    reservation_date = null,
                    Member_id = 1,
                    Book_id = 2
                }
            };

            //Act
            var result = reservationService.GetReservationsForMemberNormal(1);

            //Assert
            result.Should().BeEquivalentTo(reservations, options => options
            .Excluding(e => e.Book)
            .Excluding(e => e.Member)
            );
        }

        // Magdalena Markovinovič
        [Fact]
        private void GetReservationsForMemberNormal_GivenInvalidId_ReturnsEmptyList()
        {
            //Arrange
            CreateTwoReservationsForMemberOne();

            //Act
            var result = reservationService.GetReservationsForMemberNormal(2);

            //Assert
            result.Should().BeEmpty();
        }

    }
}
