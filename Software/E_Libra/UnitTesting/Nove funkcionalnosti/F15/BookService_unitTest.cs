using FakeItEasy;
using DataAccessLayer.Interfaces;
using BussinessLogicLayer.services;
using EntitiesLayer.Entities;
using System.Collections.Generic;
using Xunit;

namespace UnitTesting.Nove_funkcionalnosti.F15 {
    public class BookService_unitTest {
        readonly IBookRepository _bookRepo;
        readonly IReservationRepository _reservationRepo;
        readonly IMembersRepository _memberRepo;
        readonly BookServices _bookService;

        public BookService_unitTest() {
            _bookRepo = A.Fake<IBookRepository>();
            _reservationRepo = A.Fake<IReservationRepository>();
            _memberRepo = A.Fake<IMembersRepository>();
            _bookService = new BookServices(_bookRepo, _reservationRepo, _memberRepo);
        }

        // Domagoj Hegedušić
        [Fact]
        public void GetTopBorrowedBooks_GivenLibraryId_ReturnsTopBorrowedBooks() {
            // Arrange
            int libraryId = 1;
            var expectedBooks = new List<MostPopularBooksViewModel>
            {
                new MostPopularBooksViewModel { Book_Name = "Hamlet", Author_Name = "William Shakespeare", Times_Borrowed = 10, Url_Photo = "probniurl.com", Order_Number = 1 },
                new MostPopularBooksViewModel { Book_Name = "Odiseja", Author_Name = "Homer", Times_Borrowed = 8, Url_Photo = "probniurl.com", Order_Number = 2 }
            };

            A.CallTo(() => _bookRepo.GetTopBooks(libraryId)).Returns(expectedBooks);

            // Act
            var result = _bookService.GetTopBorrowedBooks(libraryId);

            // Assert
            Assert.Equal(expectedBooks.Count, result.Count);
            for (int i = 0; i < expectedBooks.Count; i++) {
                Assert.Equal(expectedBooks[i].Book_Name, result[i].Book_Name);
                Assert.Equal(expectedBooks[i].Author_Name, result[i].Author_Name);
                Assert.Equal(expectedBooks[i].Times_Borrowed, result[i].Times_Borrowed);
                Assert.Equal(expectedBooks[i].Url_Photo, result[i].Url_Photo);
                Assert.Equal(expectedBooks[i].Order_Number, result[i].Order_Number);
            }
        }

    }
}
