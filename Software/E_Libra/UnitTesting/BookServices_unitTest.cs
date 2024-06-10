﻿using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
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
                },
                new Book
                {
                    id = 2,
                    name = "Title2",
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
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void InsertNewCopies_ReservationsDoNotExist_AddsToCopies(int currCopies)
        {
            // Arrange
            A.CallTo(() => bookRepo.GetBookCurrentCopies(book.id)).Returns(currCopies);

            // Act
            bookServices.InsertNewCopies(5, book);

            // Assert
            A.CallTo(() => bookRepo.GetBookCurrentCopies(book.id)).MustHaveHappened();
            A.CallTo(() => bookRepo.InsertNewCopies(5, book, true)).MustHaveHappened();
        }

        //[Theory]

        //public void InsertNewCopies_ReservationExistMoreRezervationsThanCopies_ReturnsTrue()
        //public void InsertNewCopies_ReservationExistLessRezervationsThanCopies_ReturnsTrue()

        //nema rezervacija tj currentCopies >= 0
        //ima rezervacija, currentCopies <0 i abs(currentCopies) > numCopies
        //ima rezervacija, currentCopies <0 i abs(currentCopies) < numCopies

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
        //TODO GetBookByBarcodeId
        //TODO UpdateBook
        //TODO GetBookBarcode
        //TODO GetBooksByLibrary
    }
}
