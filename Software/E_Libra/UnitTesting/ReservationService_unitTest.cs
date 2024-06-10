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
    public class ReservationService_unitTest
    {
        readonly IReservationRepository reservationRepo;
        readonly ReservationService reservationService;

        readonly IBookRepository bookRepo;
        readonly BookServices bookServices;

        readonly IMembersRepository membersRepo;

        readonly Book book;
        readonly Reservation reservation;

        public ReservationService_unitTest()
        {
            reservationRepo = A.Fake<IReservationRepository>();
            bookRepo = A.Fake<IBookRepository>();
            membersRepo = A.Fake<IMembersRepository>();
            bookServices = new BookServices(bookRepo, reservationRepo, membersRepo);
            reservationService = new ReservationService(reservationRepo, bookServices);

            book = new Book
            {
                id = 1,
                name = "Book1",
                description = "Description",
                publish_date = DateTime.Now,
                pages_num = 100,
                digital = 0,
                url_digital = null,
                url_photo = null,
                total_copies = 10,
                Genre = new Genre { id = 1, name = "Genre1" },
                Library_id = 1
            };

            reservation = new Reservation
            {
                idReservation = 1,
                Member_id = 1,
                Book_id = 1,
                Book = book,
            };
        }

        [Fact]
        public void CheckNumberOfReservations_GivenFunctionIsCalled_ReturnsNumberOfReservations()
        {
            //Arrange
            int id = 1;
            A.CallTo(() => reservationRepo.CheckNumberOfReservations(id)).Returns(1);

            //Act
            var result = reservationService.CheckNumberOfReservations(id);

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void CheckExistingReservation_GivenFunctionIsCalled_ReturnsTrue()
        {
            //Arrange
            int bookId = 1;
            int memberId = 1;
            A.CallTo(() => reservationRepo.CheckExistingReservation(bookId, memberId)).Returns(true);

            //Act
            var result = reservationService.CheckExistingReservation(bookId, memberId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void AddReservation_GivenReservationIsAdded_ReturnsOne()
        {
            //Arrange
            A.CallTo(() => reservationRepo.Add(reservation, true)).Returns(1);

            //Act
            var result = reservationService.AddReservation(reservation);

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void GetReservationForMember_GivenMemberIDIsPassed_ReturnsReservationList()
        {
            //Arrange
            int memberId = 1;
            var reservations = new List<ReservationViewModel>
            {
                new ReservationViewModel { ReservationId = 1, BookName = "Book1", Date = DateTime.Now },
                new ReservationViewModel { ReservationId = 2, BookName = "Book2", Date = DateTime.Now },
            };
            A.CallTo(() => reservationRepo.GetReservationsForMember(memberId)).Returns(reservations);

            //Act
            var result = reservationService.GetReservationForMember(memberId);

            //Assert
            Assert.Equal(reservations, result);
        }
        //TODO Megi GetReservationsForMemberNormal

        [Fact]
        public void CountExistingReservations_GivenMemberIDIsPassed_ReturnsNumberOfReservations()
        {
            //Arrange
            int memberId = 1;
            A.CallTo(() => reservationRepo.CountExistingReservations(memberId)).Returns(1);

            //Act
            var result = reservationService.CountExistingReservations(memberId);

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void EnterDateForReservation_GivenBookIsPassed_ReturnsTrue()
        {
            //Arrange
            A.CallTo(() => reservationRepo.EnterDateForReservation(book)).Returns(true);

            //Act
            var result = reservationService.EnterDateForReservation(book);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckReservationDates_OverdueReservationsArePassed_ReturnsVoid()
        {
            //Arrange
            var overdueReservations = new List<Reservation>
            {
                new Reservation { Member_id = 1, Book_id = book.id, reservation_date = DateTime.Now.AddDays(-4), Book = book },
            }.AsQueryable();

            A.CallTo(() => reservationRepo.GetOverdueReservations()).Returns(overdueReservations);
            //Act
            reservationService.CheckReservationDates();

            //Assert
            A.CallTo(() => bookRepo.InsertOneCopy(book, true)).MustHaveHappened();
            A.CallTo(() => reservationRepo.EnterDateForReservation(book)).MustHaveHappened();
            A.CallTo(() => reservationRepo.Remove(A<Reservation>.Ignored, false)).MustHaveHappened();
            A.CallTo(() => reservationRepo.SaveChanges()).MustHaveHappened();
        }

        [Fact]
        public void CheckReservationDates_NoOverdueReservationsArePassed_ReturnsVoid()
        {
            //Arrange
            var overdueReservations = new List<Reservation>().AsQueryable();

            A.CallTo(() => reservationRepo.GetOverdueReservations()).Returns(overdueReservations);
            //Act
            reservationService.CheckReservationDates();

            //Assert
            A.CallTo(() => bookRepo.InsertOneCopy(book, true)).MustNotHaveHappened();
            A.CallTo(() => reservationRepo.EnterDateForReservation(book)).MustNotHaveHappened();
            A.CallTo(() => reservationRepo.Remove(A<Reservation>.Ignored, false)).MustNotHaveHappened();
            A.CallTo(() => reservationRepo.SaveChanges()).MustHaveHappened();
        }

        [Fact]
        public void ReturnBook_GivenBookIsPassed_ReturnsVoid()
        {
            //Arrange

            //Act
            reservationService.ReturnBook(book);

            //Assert
            A.CallTo(() => bookRepo.InsertOneCopy(book, true)).MustHaveHappened();
            A.CallTo(() => reservationRepo.EnterDateForReservation(book)).MustHaveHappened();
        }

        [Fact]
        public void GetReservationId_GivenMemberIDAndBookIDArePassed_ReturnsReservationID()
        {
            //Arrange
            int memberId = 1;
            int bookId = 1;
            A.CallTo(() => reservationRepo.GetReservationId(memberId, bookId)).Returns(2);

            //Act
            var result = reservationService.GetReservationId(memberId, bookId);

            //Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void GetReservationPosition_GivenReservationIDAndBookIDArePassed_ReturnsPosition()
        {
            //Arrange
            int reservationId = 1;
            int bookId = 1;
            A.CallTo(() => reservationRepo.GetReservationPosition(reservationId, bookId)).Returns(2);

            //Act
            var result = reservationService.GetReservationPosition(reservationId, bookId);

            //Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void ShowExistingReservations_GivenFunctionIsCalled_ReturnsString()
        {
            //Arrange
            A.CallTo(() => reservationRepo.ShowExistingReservations()).Returns("Podigni rezervacije");

            //Act
            var result = reservationService.ShowExistingReservations();

            //Assert
            Assert.Equal("Podigni rezervacije", result);
        }

        [Fact]
        public void CheckValidReservationFroMember_GivenMemberIDAndBookIDArePassed_ReturnsReservation()
        {
            //Arrange
            int memberId = 1;
            int bookId = 1;

            A.CallTo(() => reservationRepo.CheckValidReservationFroMember(memberId, bookId)).Returns(reservation);

            //Act
            var result = reservationService.CheckValidReservationFroMember(memberId, bookId);

            //Assert
            Assert.Equal(reservation, result);
        }

        [Fact]
        public void RemoveReservation_GivenReservationIsPassed_ReturnsOne()
        {
            //Arrange
            A.CallTo(() => reservationRepo.Remove(reservation, true)).Returns(1);

            //Act
            var result = reservationService.RemoveReservation(reservation);

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void RemoveReservationFromList_ReservationDateIsNull_ReturnsTrue()
        {
            //Arrange
            reservation.reservation_date = null;
            A.CallTo(() => reservationRepo.GetReservationById(reservation.idReservation)).Returns(reservation);
            
            //Act
            reservationService.RemoveReservationFromList(reservation.idReservation);

            //Assert
            A.CallTo(() => reservationRepo.Remove(reservation, true)).MustHaveHappened();
            A.CallTo(() => bookRepo.InsertOneCopy(book, true)).MustHaveHappened();
        }

        [Fact]
        public void RemoveReservationFromList_ReservationDateIsNotNull_ReturnsTrue()
        {
            //Arrange
            reservation.reservation_date = DateTime.Now;
            A.CallTo(() => reservationRepo.GetReservationById(reservation.idReservation)).Returns(reservation);

            //Act
            reservationService.RemoveReservationFromList(reservation.idReservation);

            //Assert
            A.CallTo(() => reservationRepo.Remove(reservation, true)).MustHaveHappened();
            A.CallTo(() => bookRepo.InsertOneCopy(book, true)).MustHaveHappened();
            A.CallTo(() => reservationRepo.EnterDateForReservation(book)).MustHaveHappened();
        }

        [Fact]
        public void Constructor_InitializesAll()
        {
            //Arrange
            var testService = new ReservationService();
            //Act

            //Assert
            Action[] actions =
            {
                () => Assert.NotNull(testService.reservationRepository),
                () => Assert.NotNull(testService.bookService),
                () => Assert.IsType<ReservationRepository>(testService.reservationRepository),
                () => Assert.IsType<BookServices>(testService.bookService)
            };
            Assert.Multiple(actions);
        }
    }
}
