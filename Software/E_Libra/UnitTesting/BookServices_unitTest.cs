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
using static DataAccessLayer.Repositories.BookRepository;

namespace UnitTesting
{
    public class BookServices_unitTest
    {
        readonly IBookRepository bookRepo;
        readonly BookServices bookServices;
        readonly IReservationRepository reservationRepo;
        readonly IMembersRepository memberRepo;
        readonly Book book;
        readonly Author author;
        readonly IQueryable<Book> books;
        readonly IQueryable<BookViewModel> bookViewModels;

        public BookServices_unitTest()
        {
            bookRepo = A.Fake<IBookRepository>();
            reservationRepo = A.Fake<IReservationRepository>();
            memberRepo = A.Fake<IMembersRepository>();
            bookServices = new BookServices(bookRepo, reservationRepo, memberRepo);

            book = new Book
            {
                id = 1,
                name = "Title",
            };

            author = new Author
            {
                idAuthor = 1,
                name = "Name",
                surname = "Surname",
            };

            books = new List<Book>
            {
                new Book
                {
                    id = 1,
                    name = "Title",
                    barcode_id = "sdf42",
                    Library_id = 1,
                    Library = new Library {
                        id = 1
                    }
                },
                new Book
                {
                    id = 2,
                    name = "Title2",
                    barcode_id = "dfg41",
                    Library_id = 1,
                    Library = new Library {
                        id = 1
                    }
                },
                new Book
                {
                    id = 3,
                    name = "Title3",
                    barcode_id = "sgvfsd",
                    Library_id = 2,
                    Library = new Library {
                        id = 2
                    }
                },
                new Book
                {
                    id = 4,
                    name = "Title4",
                    barcode_id = "3454363",
                    Library_id = 2,
                    Library = new Library {
                        id = 2
                    }
                }
            }.AsQueryable();

            bookViewModels = new List<BookViewModel>
            {
                new BookViewModel
                {
                    Id = 1,
                    Name = "Title",
                },
                new BookViewModel
                {
                    Id = 2,
                    Name = "Title2",
                }
            }.AsQueryable();

        }

        //David Matijanić
        [Fact]
        public void Constructor_WhenBookServiceIsInstantiated_ItIsNotNull() {
            //Arrange & act
            var service = new BookServices();

            //Assert
            Assert.NotNull(service);
        }

        [Fact]
        public void AddBook_GivenBookAndAuthor_ReturnsTrue()
        {
            // Arrange
            A.CallTo(() => bookRepo.Add(book, author, true)).Returns(1);

            // Act
            var result = bookServices.AddBook(book, author);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetAllBooks_GivenFunctionIsCalled_ReturnsListOfBooks()
        {
            // Arrange
            A.CallTo(() => bookRepo.GetAll()).Returns(books);

            // Act
            var result = bookServices.GetAllBooks();

            // Assert
            Assert.Equal(result, books);
        }

        [Fact]
        public void GetNonArchivedBooks_GivenFunctionIsCalled_ReturnsListOfBooks()
        {
            // Arrange
            A.CallTo(() => bookRepo.GetNonArchivedBooks(false)).Returns(books);

            // Act
            var result = bookServices.GetNonArchivedBooks(false);

            // Assert
            Assert.Equal(result, books);
        }

        [Fact]
        public void InsertOneCopy_GivenBook_ReturnsTrue()
        {
            // Arrange
            A.CallTo(() => bookRepo.InsertOneCopy(book, true)).Returns(1);

            // Act
            var result = bookServices.InsertOneCopy(book);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RemoveOneCopy_GivenBook_ReturnsTrue()
        {
            // Arrange
            A.CallTo(() => bookRepo.RemoveOneCopy(book, true)).Returns(1);

            // Act
            var result = bookServices.RemoveOneCopy(book);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void InsertNewCopies_ReservationsDoNotExist_AddsToCopies(int currCopies)
        {
            // Arrange
            A.CallTo(() => bookRepo.GetBookCurrentCopies(book.id)).Returns(currCopies);

            // Act
            bookServices.InsertNewCopies(5, book);

            // Assert
            A.CallTo(() => bookRepo.GetBookCurrentCopies(book.id)).MustHaveHappened();
            A.CallTo(() => bookRepo.InsertNewCopies(A<int>.Ignored, book, true)).MustHaveHappened(1, Times.Exactly);
        }

        [Fact]
        public void InsertNewCopies_ReservationsExistMoreRezervationsThanCopies_ConfirmsReservations()
        {
            // Arrange
            A.CallTo(() => bookRepo.GetBookCurrentCopies(book.id)).Returns(-5);
            A.CallTo(() => reservationRepo.EnterDateForReservation(book)).Returns(true);

            // Act
            bookServices.InsertNewCopies(4, book);

            // Assert
            A.CallTo(() => bookRepo.GetBookCurrentCopies(book.id)).MustHaveHappened();
            A.CallTo(() => reservationRepo.EnterDateForReservation(book)).MustHaveHappened(4, Times.Exactly);
            A.CallTo(() => bookRepo.InsertNewCopies(1, book, true)).MustHaveHappened(4, Times.Exactly);
        }

        [Fact]
        public void InsertNewCopies_ReservationExistLessRezervationsThanCopies_ConfirmsReservationsAndAddsCopies()
        {
            // Arrange
            int currCopies = -3;
            A.CallTo(() => bookRepo.GetBookCurrentCopies(book.id)).Returns(-3);
            A.CallTo(() => reservationRepo.EnterDateForReservation(book))
                .Invokes(() => currCopies++)
                .ReturnsLazily(() => currCopies <= 0);

            // Act
            bookServices.InsertNewCopies(5, book);

            // Assert
            A.CallTo(() => bookRepo.GetBookCurrentCopies(book.id)).MustHaveHappened();
            A.CallTo(() => reservationRepo.EnterDateForReservation(book)).MustHaveHappened(4, Times.Exactly);
            A.CallTo(() => bookRepo.InsertNewCopies(1, book, true)).MustHaveHappened(3, Times.Exactly);
            A.CallTo(() => bookRepo.InsertNewCopies(2, book, true)).MustHaveHappened(1, Times.Exactly);
        }

        [Fact]
        public void ArchiveBook_GivenBookAndArchive_ReturnsTrue()
        {
            // Arrange
            Archive archive = new Archive
            {
                Book_id = 1,
                Employee_id = 1,
                arhive_date = DateTime.Now,
            };

            A.CallTo(() => bookRepo.ArhiveBook(book, archive)).Returns(1);

            // Act
            var result = bookServices.ArchiveBook(book, archive);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetNonArchivedBooksByName_GivenSearchTerm_ReturnsListOfBooks()
        {
            // Arrange
            A.CallTo(() => bookRepo.GetNonArchivedBooksByName("Title")).Returns(books);

            // Act
            var result = bookServices.GetNonArchivedBooksByName("Title");

            // Assert
            Assert.Equal(result, books);
        }

        [Fact]
        public void SearchBooks_GivenSearchTermAndDigital_ReturnsListOfBooks()
        {
            // Arrange
            A.CallTo(() => bookRepo.SearchBooks("Title", false)).Returns(bookViewModels);

            // Act
            var result = bookServices.SearchBooks("Title", false);

            // Assert
            Assert.Equal(result, bookViewModels);
        }

        [Fact]
        public void GetBooksByGenre_GivenGenreNameAndDigital_ReturnsListOfBooks()
        {
            // Arrange
            A.CallTo(() => bookRepo.GetBooksByGenre("Genre", false)).Returns(bookViewModels);

            // Act
            var result = bookServices.GetBooksByGenre("Genre", false);

            // Assert
            Assert.Equal(result, bookViewModels);
        }

        [Fact]
        public void GetBooksByAuthor_GivenAuthorNameAndDigital_ReturnsListOfBooks()
        {
            // Arrange
            A.CallTo(() => bookRepo.GetBooksByAuthor("Author", false)).Returns(bookViewModels);

            // Act
            var result = bookServices.GetBooksByAuthor("Author", false);

            // Assert
            Assert.Equal(result, bookViewModels);
        }

        [Fact]
        public void GetBooksByYear_GivenYearAndDigital_ReturnsListOfBooks()
        {
            // Arrange
            A.CallTo(() => bookRepo.GetBooksByYear(2021, false)).Returns(bookViewModels);

            // Act
            var result = bookServices.GetBooksByYear(2021, false);

            // Assert
            Assert.Equal(result, bookViewModels);
        }

        [Fact]
        public void GetBookById_GivenId_ReturnsBook()
        {
            // Arrange
            A.CallTo(() => bookRepo.GetBookById(1, true)).Returns(book);

            // Act
            var result = bookServices.GetBookById(1);

            // Assert
            Assert.Equal(result, book);
        }

        [Fact]
        public void GetWishlistBooksForMember_GivenFunctionIsCalled_ReturnsListOfBooks()
        {
            // Arrange
            A.CallTo(() => bookRepo.GetWishlistBooksForMember(A<string>.Ignored)).Returns(bookViewModels);

            // Act
            var result = bookServices.GetWishlistedBooks();

            // Assert
            Assert.Equal(result, bookViewModels);
        }

        [Fact]
        public void AddBookToWishlist_GivenBookId_ReturnsTrue()
        {
            // Arrange
            A.CallTo(() => memberRepo.GetMemberId(A<string>.Ignored)).Returns(1);
            A.CallTo(() => bookRepo.AddBookToWishlist(1,1)).Returns(true);

            // Act
            var result = bookServices.AddBookToWishlist(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RemoveBookFromWishlist_GivenBookId_ReturnsTrue()
        {
            // Arrange
            A.CallTo(() => memberRepo.GetMemberId(A<string>.Ignored)).Returns(1);
            A.CallTo(() => bookRepo.RemoveBookFromWishlist(1, 1)).Returns(true);

            // Act
            var result = bookServices.RemoveBookFromWishlist(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Constructor_InitializesAll()
        {
            //Arrange
            var testService = new BookServices();
            //Act

            //Assert
            Action[] actions =
            {
                () => Assert.NotNull(testService.bookRepository),
                () => Assert.NotNull(testService.reservationRepository),
                () => Assert.NotNull(testService.memberRepository),
                () => Assert.IsType<BookRepository>(testService.bookRepository),
                () => Assert.IsType<ReservationRepository>(testService.reservationRepository),
                () => Assert.IsType<MemberRepository>(testService.memberRepository),
            };
            Assert.Multiple(actions);
        }

        [Fact]
        public void Dispose_FunctionIsCalled_DisposesAll()
        {
            //Arrange

            //Act
            bookServices.Dispose();

            //Assert
            Action[] actions =
            {
                () => A.CallTo(() => bookRepo.Dispose()).MustHaveHappened(),
                () => A.CallTo(() => reservationRepo.Dispose()).MustHaveHappened(),
                //TODO () => A.CallTo(() => memberRepo.Dispose()).MustHaveHappened(),
            };
            Assert.Multiple(actions);
        }

        //David Matijanić
        [Theory]
        [InlineData("doesntExist")]
        [InlineData("alsoDoesntExist")]
        [InlineData("isThisEvenABarcode?")]
        public void GetBookByBarcodeId_BarcodeDoesntExist_BookNotFoundExceptionThrown(string barcodeId) {
            //Arrange
            int libraryId = 1;
            A.CallTo(() => bookRepo.GetBookByBarcodeId(barcodeId)).Returns(new List<Book>().AsQueryable());

            //Act & assert
            Assert.Throws<BookNotFoundException>(() => bookServices.GetBookByBarcodeId(libraryId, barcodeId));
        }

        //David Matijanić
        [Theory]
        [InlineData(111, "sdf42")]
        [InlineData(111, "dfg41")]
        [InlineData(111, "sgvfsd")]
        [InlineData(111, "3454363")]
        public void GetBookByBarcodeId_WrongBookLibrary_WrongLibraryException(int libraryId, string barcodeId) {
            //Arrange
            A.CallTo(() => bookRepo.GetBookByBarcodeId(barcodeId)).Returns(books.Where(b => b.barcode_id == barcodeId));

            //Act & assert
            Assert.Throws<WrongLibraryException>(() => bookServices.GetBookByBarcodeId(libraryId, barcodeId));
        }

        //David Matijanić
        [Theory]
        [InlineData(1, "sdf42")]
        [InlineData(1, "dfg41")]
        [InlineData(2, "sgvfsd")]
        [InlineData(2, "3454363")]
        public void GetBookByBarcodeId_CorrectBarcodeAndLibraryEntered_CorrectBookIsRetrieved(int libraryId, string barcodeId) {
            //Arrange
            A.CallTo(() => bookRepo.GetBookByBarcodeId(barcodeId)).Returns(books.Where(b => b.barcode_id == barcodeId));

            //Act
            Book book = bookServices.GetBookByBarcodeId(libraryId, barcodeId);

            //Assert
            Action[] actions = {
                () => Assert.Equal(libraryId, book.Library.id),
                () => Assert.Equal(barcodeId, book.barcode_id)
            };
            Assert.Multiple(actions);
        }

        //David Matijanić
        [Fact]
        public void UpdateBook_BookIsEntered_BookDataIsUpdated() {
            //Arrange
            Book foundBook = books.First();
            Book updatedBook = new Book {
                id = foundBook.id,
                Library_id = foundBook.Library_id,
                Library = foundBook.Library,
                name = "Azurirana knjiga!",
                current_copies = foundBook.current_copies - 1
            };
            A.CallTo(() => bookRepo.Update(updatedBook, true)).Invokes(call => {
                foreach (var b in books.Where(bb => bb.id == updatedBook.id)) {
                    b.Library_id = updatedBook.Library_id;
                    b.Library = updatedBook.Library;
                    b.name = updatedBook.name;
                    b.current_copies = updatedBook.current_copies;
                }
            }).Returns(1);

            //Act
            bookServices.UpdateBook(updatedBook);
            foundBook = books.First();

            //Assert
            Action[] actions = {
                () => Assert.Equal(updatedBook.name, foundBook.name),
                () => Assert.Equal(updatedBook.current_copies, foundBook.current_copies)
            };
            Assert.Multiple(actions);
        }

        //David Matijanić
        [Fact]
        public void UpdateBook_BookDoesntExist_ReturnsZero() {
            //Arrange
            Book updatedBook = new Book {
                id = 55,
                Library_id = 2,
                Library = new Library {
                    id = 2
                },
                name = "Ova knjiga ne postoji!",
                current_copies = 7
            };
            A.CallTo(() => bookRepo.Update(updatedBook, true)).Returns(0);

            //Act
            int changedAmount = bookServices.UpdateBook(updatedBook);

            //Assert
            Assert.Equal(0, changedAmount);
        }

        //David Matijanić
        [Theory]
        [InlineData("sdf42", 1)]
        [InlineData("dfg41", 2)]
        [InlineData("sgvfsd", 3)]
        [InlineData("3454363", 4)]
        public void GetBookBarcode_BarcodeExists_ReturnsTheCorrectBookBarcode(string barcode, int id) {
            //Arrange
            A.CallTo(() => bookRepo.GetBookBarcode(id)).Returns(books.Where(b => b.id == id).Select(b => b.barcode_id));

            //Act
            string retrievedBarcode = bookServices.GetBookBarcode(id);

            //Assert
            Assert.Equal(barcode, retrievedBarcode);
        }

        //David Matijanić
        [Theory]
        [InlineData(111)]
        [InlineData(222)]
        [InlineData(333)]
        public void GetBookBarcode_IdDoesntExist_ReturnsNull(int id) {
            //Arrange
            A.CallTo(() => bookRepo.GetBookBarcode(id)).Returns(new List<string>().AsQueryable());

            //Act
            string retrievedBarcode = bookServices.GetBookBarcode(id);

            //Assert
            Assert.Null(retrievedBarcode);
        }

        //David Matijanić
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetBooksByLibrary_LibraryIdEntered_CorrectBooksRetrieved(int libraryId) {
            //Arrange
            A.CallTo(() => bookRepo.GetBooksByLibrary(libraryId)).Returns(books.Where(b => b.Library.id == libraryId));

            //Act
            var retrievedBooks = bookServices.GetBooksByLibrary(libraryId);

            //Assert
            Assert.Equal(books.Where(b => b.Library.id == libraryId).ToList(), retrievedBooks);
        }

        //TODO: Implementirati DISPOSE kad EmployeeService bude imao IDisposable implementiran! (@dmatijani21)
    }
}
