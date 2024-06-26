﻿using BussinessLogicLayer.Exceptions;
using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting {
    public class BorrowService_unitTest {
        private IQueryable<Borrow> borrows { get; set; }
        private readonly IBorrowRepository borrowRepository;
        private BorrowService borrowService { get; set; }

        //David Matijanić
        public BorrowService_unitTest() {
            borrowRepository = A.Fake<IBorrowRepository>();
            List<Member> members = new List<Member> {
                new Member { id = 123, Library_id = 1 },
                new Member { id = 456, Library_id = 2 },
                new Member { id = 789, Library_id = 3 },
                new Member { id = 12, Library_id = 3 }
            };

            borrows = new List<Borrow> {
                new Borrow {
                    idBorrow = 1,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(7),
                    borrow_status = (int)BorrowStatus.Borrowed,
                    Book_id = 3,
                    Member_id = members[0].id,
                    Employee_borrow_id = 111,
                    Book = new Book { id = 3, Library_id = 1, current_copies = 5 },
                    Employee = new Employee { id = 111 },
                    Member = members[0]
                },
                new Borrow {
                    idBorrow = 2,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(14),
                    borrow_status = (int)BorrowStatus.Late,
                    Book_id = 5,
                    Member_id = members[0].id,
                    Employee_borrow_id = 111,
                    Book = new Book { id = 5, Library_id = 1 },
                    Employee = new Employee { id = 111 },
                    Member = members[0]
                },
                new Borrow {
                    idBorrow = 3,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(10),
                    borrow_status = (int)BorrowStatus.Late,
                    Book_id = 8,
                    Member_id = members[1].id,
                    Employee_borrow_id = 333,
                    Book = new Book { id = 8, Library_id = 2 },
                    Employee = new Employee { id = 333 },
                    Member = members[1]
                },
                new Borrow {
                    idBorrow = 4,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(3),
                    borrow_status = (int)BorrowStatus.Returned,
                    Book_id = 13,
                    Member_id = members[1].id,
                    Employee_borrow_id = 333,
                    Employee_return_id = 444,
                    Book = new Book { id = 13, Library_id = 2 },
                    Employee = new Employee { id = 333 },
                    Employee1 = new Employee { id = 444 },
                    Member = members[1]
                },
                new Borrow {
                    idBorrow = 5,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(7),
                    borrow_status = (int)BorrowStatus.Waiting,
                    Book_id = 21,
                    Member_id = members[2].id,
                    Employee_borrow_id = 555,
                    Book = new Book { id = 21, Library_id = 3 },
                    Employee = new Employee { id = 555 },
                    Member = members[2]
                },
                new Borrow {
                    idBorrow = 6,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(14),
                    borrow_status = (int)BorrowStatus.Returned,
                    Book_id = 55,
                    Member_id = members[2].id,
                    Employee_borrow_id = 555,
                    Employee_return_id = 555,
                    Book = new Book { id = 13, Library_id = 3 },
                    Employee = new Employee { id = 555 },
                    Employee1 = new Employee { id = 555 },
                    Member = members[2]
                },
                new Borrow {
                    idBorrow = 7,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(21),
                    borrow_status = (int)BorrowStatus.Returned,
                    Book_id = 21,
                    Member_id = members[3].id,
                    Employee_borrow_id = 6666,
                    Employee_return_id = 555,
                    Book = new Book { id = 21, Library_id = 3 },
                    Employee = new Employee { id = 6666 },
                    Employee1 = new Employee { id = 555 },
                    Member = members[3]
                },
                new Borrow {
                    idBorrow = 8,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(7),
                    borrow_status = (int)BorrowStatus.Waiting,
                    Book_id = 34,
                    Member_id = members[3].id,
                    Employee_borrow_id = 555,
                    Book = new Book { id = 34, Library_id = 3 },
                    Employee = new Employee { id = 555 },
                    Member = members[3]
                },
                new Borrow {
                    idBorrow = 9,
                    borrow_date = DateTime.Now.AddDays(-1),
                    return_date = DateTime.Now.AddDays(6),
                    borrow_status = (int)BorrowStatus.Borrowed,
                    Book_id = 7,
                    Member_id = members[0].id,
                    Employee_borrow_id = 222,
                    Book = new Book { id = 7, Library_id = 1 },
                    Employee = new Employee { id = 222 },
                    Member = members[0]
                },
                new Borrow {
                    idBorrow = 10,
                    borrow_date = DateTime.Now.AddDays(-2),
                    return_date = DateTime.Now.AddDays(5),
                    borrow_status = (int)BorrowStatus.Borrowed,
                    Book_id = 9,
                    Member_id = members[1].id,
                    Employee_borrow_id = 444,
                    Book = new Book { id = 9, Library_id = 2 },
                    Employee = new Employee { id = 444 },
                    Member = members[1]
                }
            }.AsQueryable();
            borrowService = InitialiseBorrowService(borrowRepository);
        }

        //David Matijanić
        [Fact]
        public void Constructor_WhenBorrowServiceIsInstantiated_ItIsNotNull() {
            //Arrange & act
            var service = new BorrowService();

            //Assert
            Assert.NotNull(service);
        }

        //David Matijanić
        [Fact]
        public void GetAllBorrowsForMember_GivenThereAreNoBorrows_NoBorrowsRetrieved() {
            //Arrange
            A.CallTo(() => borrowRepository.GetAllBorrowsForMember(1, 1)).Returns(new List<Borrow>().AsQueryable());

            //Act
            var borrowsForMember = borrowService.GetAllBorrowsForMember(1, 1);

            //Assert
            Assert.Empty(borrowsForMember);
        }

        //David Matijanić
        [Theory]
        [InlineData(123, 1)]
        [InlineData(456, 2)]
        [InlineData(789, 3)]
        [InlineData(012, 3)]
        [InlineData(0, 4)]
        public void GetAllBorrowsForMember_GivenTheCorrectMemberAndLibraryIdIsEntered_BorrowsRetrieved(int memberId, int libraryId) {
            //Arrange
            A.CallTo(() => borrowRepository.GetAllBorrowsForMember(memberId, libraryId)).Returns(borrows.Where(b => b.Member.id == memberId && b.Member.Library_id == libraryId));

            //Act
            var borrowsForMember = borrowService.GetAllBorrowsForMember(memberId, libraryId);

            //Assert
            Assert.Equal(borrows.Where(b => b.Member.id == memberId).ToList(), borrowsForMember);
        }

        //David Matijanić
        [Theory]
        [InlineData(BorrowStatus.Waiting)]
        [InlineData(BorrowStatus.Borrowed)]
        [InlineData(BorrowStatus.Late)]
        [InlineData(BorrowStatus.Returned)]
        public void GetBorrowsForMemberByStatus_GivenThereAreNoBorrows_NoBorrowsRetrieved(BorrowStatus borrowStatus) {
            //Arrange
            A.CallTo(() => borrowRepository.GetBorrowsForMemberByStatus(1, 1, borrowStatus)).Returns(new List<Borrow>().AsQueryable());

            //Act
            var borrowsForMember = borrowService.GetBorrowsForMemberByStatus(1, 1, borrowStatus);

            //Assert
            Assert.Empty(borrowsForMember);
        }

        //David Matijanić
        [Theory]
        [InlineData(123, 1, BorrowStatus.Waiting)]
        [InlineData(123, 1, BorrowStatus.Late)]
        [InlineData(123, 1, BorrowStatus.Returned)]
        [InlineData(789, 3, BorrowStatus.Waiting)]
        [InlineData(789, 3, BorrowStatus.Borrowed)]
        [InlineData(789, 3, BorrowStatus.Late)]
        [InlineData(0, 0, BorrowStatus.Borrowed)]
        [InlineData(0, 0, BorrowStatus.Returned)]
        public void GetBorrowsForMemberByStatus_GivenTheCorrectMemberAndBorrowStatusIsEntered_CorrectBorrowsRetrieved(int memberId, int libraryId, BorrowStatus borrowStatus) {
            //Arrange
            A.CallTo(() => borrowRepository.GetBorrowsForMemberByStatus(memberId, libraryId, borrowStatus)).Returns(borrows.Where(b => b.Member.id == memberId && b.borrow_status == (int)borrowStatus && b.Member.Library_id == libraryId));

            //Act
            var borrowsForMember = borrowService.GetBorrowsForMemberByStatus(memberId, libraryId, borrowStatus);

            //Assert
            Assert.Equal(borrows.Where(b => b.Member.id == memberId && b.borrow_status == (int)borrowStatus).ToList(), borrowsForMember);
        }

        //David Matijanić
        [Fact]
        public void GetBorrowsForMemberAndBook_GivenThereAreNoBorrows_NoBorrowsRetrieved() {
            //Arrange
            A.CallTo(() => borrowRepository.GetBorrowsForMemberAndBook(1, 1, 1)).Returns(new List<Borrow>().AsQueryable());

            //Act
            var borrowsForMember = borrowService.GetBorrowsForMemberAndBook(1, 1, 1);

            //Assert
            Assert.Empty(borrowsForMember);
        }

        //David Matijanić
        [Theory]
        [InlineData(123, 1, 3)]
        [InlineData(123, 1, 5)]
        [InlineData(456, 2, 8)]
        [InlineData(456, 2, 13)]
        [InlineData(789, 3, 21)]
        [InlineData(789, 3, 55)]
        [InlineData(12, 3, 21)]
        [InlineData(12, 3, 34)]
        [InlineData(123, 1, 7)]
        [InlineData(456, 2, 9)]
        public void GetBorrowForMemberAndBook_GivenTheCorrectMemberAndBookIdIsEntered_CorrectBorrowIsRetrieved(int memberId, int libraryId, int bookId) {
            //Arrange
            A.CallTo(() => borrowRepository.GetBorrowsForMemberAndBook(memberId, libraryId, bookId)).Returns(borrows.Where(b => b.Member.id == memberId && b.Book.id == bookId && b.Member.Library_id == libraryId));

            //Act
            var borrowsForMember = borrowService.GetBorrowsForMemberAndBook(memberId, libraryId, bookId);

            //Assert
            Assert.Equal(borrows.Where(b => b.Member.id == memberId && b.Book.id == bookId).ToList(), borrowsForMember);
        }

        //David Matijanić
        [Fact]
        public async Task GetAllBorrowsForLibraryAsync_GivenThereAreNoBorrows_NoBorrowsRetrieved() {
            //Arrange
            A.CallTo(() => borrowRepository.GetAllBorrowsForLibraryAsync(5)).Returns(Task.FromResult(new List<Borrow>()));

            //Act
            var borrowsForLibrary = await borrowService.GetAllBorrowsForLibraryAsync(5);

            //Arrange
            Assert.Empty(borrowsForLibrary);
        }

        //David Matijanić
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetAllBorrowsForLibraryAsync_GivenAnExistingLibraryIdIsEntered_CorrectBorrowsRetrieved(int libraryId) {
            //Arrange
            A.CallTo(() => borrowRepository.GetAllBorrowsForLibraryAsync(libraryId)).Returns(Task.FromResult(borrows.Where(b => b.Book.Library_id == libraryId).ToList()));

            //Act
            var borrowsForLibrary = await borrowService.GetAllBorrowsForLibraryAsync(libraryId);

            //Arrange
            Assert.Equal(borrows.Where(b => b.Member.Library_id == libraryId).ToList(), borrowsForLibrary);
        }

        //David Matijanić
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetAllBorrowsForLibraryAsync_GivenAnExistingLibraryIsEntered_CorrectBorrowsRetrieved(int libraryId) {
            //Arrange
            var library = new Library { id = libraryId };
            A.CallTo(() => borrowRepository.GetAllBorrowsForLibraryAsync(library.id)).Returns(Task.FromResult(borrows.Where(b => b.Book.Library_id == library.id).ToList()));

            //Act
            var borrowsForLibrary = await borrowService.GetAllBorrowsForLibraryAsync(library.id);

            //Arrange
            Assert.Equal(borrows.Where(b => b.Member.Library_id == library.id).ToList(), borrowsForLibrary);
        }

        //David Matijanić
        [Fact]
        public async Task GetBorrowsForLibraryByStatusAsync_GivenThereAreNoBorrows_NoBorrowsRetrieved() {
            //Arrange
            A.CallTo(() => borrowRepository.GetBorrowsForLibraryByStatusAsync(5, BorrowStatus.Borrowed)).Returns(Task.FromResult(new List<Borrow>()));

            //Act
            var borrowsForLibrary = await borrowService.GetBorrowsForLibraryByStatusAsync(5, BorrowStatus.Borrowed);

            //Arrange
            Assert.Empty(borrowsForLibrary);
        }

        //David Matijanić
        [Theory]
        [InlineData(1, BorrowStatus.Waiting)]
        [InlineData(1, BorrowStatus.Borrowed)]
        [InlineData(1, BorrowStatus.Late)]
        [InlineData(2, BorrowStatus.Borrowed)]
        [InlineData(2, BorrowStatus.Late)]
        [InlineData(2, BorrowStatus.Returned)]
        [InlineData(3, BorrowStatus.Waiting)]
        [InlineData(3, BorrowStatus.Returned)]
        public async Task GetBorrowsForLibraryByStatusAsync_GivenAnExistingLibraryIdIsEntered_CorrectBorrowsRetrieved(int libraryId, BorrowStatus borrowStatus) {
            //Arrange
            A.CallTo(() => borrowRepository.GetBorrowsForLibraryByStatusAsync(libraryId, borrowStatus)).Returns(Task.FromResult(borrows.Where(b => b.Book.Library_id == libraryId && b.borrow_status == (int)borrowStatus).ToList()));

            //Act
            var borrowsForLibrary = await borrowService.GetBorrowsForLibraryByStatusAsync(libraryId, borrowStatus);

            //Arrange
            Assert.Equal(borrows.Where(b => b.Member.Library_id == libraryId && b.borrow_status == (int)borrowStatus).ToList(), borrowsForLibrary);
        }

        //David Matijanić
        [Theory]
        [InlineData(1, BorrowStatus.Waiting)]
        [InlineData(1, BorrowStatus.Borrowed)]
        [InlineData(1, BorrowStatus.Late)]
        [InlineData(2, BorrowStatus.Borrowed)]
        [InlineData(2, BorrowStatus.Late)]
        [InlineData(2, BorrowStatus.Returned)]
        [InlineData(3, BorrowStatus.Waiting)]
        [InlineData(3, BorrowStatus.Returned)]
        public async Task GetBorrowsForLibraryByStatusAsync_GivenAnExistingLibraryIsEntered_CorrectBorrowsRetrieved(int libraryId, BorrowStatus borrowStatus) {
            //Arrange
            var library = new Library { id = libraryId };
            A.CallTo(() => borrowRepository.GetBorrowsForLibraryByStatusAsync(library, borrowStatus)).Returns(Task.FromResult(borrows.Where(b => b.Book.Library_id == library.id && b.borrow_status == (int)borrowStatus).ToList()));

            //Act
            var borrowsForLibrary = await borrowService.GetBorrowsForLibraryByStatusAsync(library, borrowStatus);

            //Arrange
            Assert.Equal(borrows.Where(b => b.Member.Library_id == library.id && b.borrow_status == (int)borrowStatus).ToList(), borrowsForLibrary);
        }

        //David Matijanić
        [Fact]
        public void AddNewBorrow_BorrowHasStatusWaiting_BookIsBorrowed() {
            //Arrange
            var newBorrow = PrepareNewBorrow(BorrowStatus.Waiting);
            A.CallTo(() => borrowRepository.Add(newBorrow, true)).Invokes(call => {
                List<Borrow> borrowsWithAddedBorrow = borrows.ToList();
                borrowsWithAddedBorrow.Add(newBorrow);
                borrows = borrowsWithAddedBorrow.AsQueryable();
            }).Returns(1);

            //Act
            borrowService.AddNewBorrow(newBorrow);

            //Assert
            Assert.Contains(newBorrow, borrows);
        }

        //David Matijanić
        [Fact]
        public void AddNewBorrow_BorrowHasStatusBorrowed_BookHasOneLessCopies() {
            //Arrange
            var newBorrow = PrepareNewBorrow();

            IBookRepository bookRepository = A.Fake<IBookRepository>();
            A.CallTo(() => bookRepository.Update(newBorrow.Book, true)).Returns(1);
            BookServices bookService = new BookServices(bookRepository, null, null);
            A.CallTo(() => borrowRepository.Add(newBorrow, true)).Invokes(call => {
                List<Borrow> borrowsWithAddedBorrow = borrows.ToList();
                borrowsWithAddedBorrow.Add(newBorrow);
                borrows = borrowsWithAddedBorrow.AsQueryable();
            }).Returns(1);
            borrowService = new BorrowService(borrowRepository, null, bookService);

            //Act
            borrowService.AddNewBorrow(newBorrow);

            //Assert
            Action[] actions = {
                () => Assert.Contains(newBorrow, borrows),
                () => Assert.Equal(4, newBorrow.Book.current_copies)
            };
            Assert.Multiple(actions);
        }

        //David Matijanić
        [Fact]
        public void AddNewBorrow_BorrowHasStatusBorrowedAndBookHasNoMoreCopiesAndNoReservationExists_ThrowsNoMoreBookCopiesException() {
            //Arrange
            int libraryId = 20;
            Member member = new Member {
                id = 42,
                Library_id = libraryId
            };
            Book book = new Book {
                id = 68,
                Library_id = libraryId,
                current_copies = 0
            };
            var newBorrow = PrepareNewBorrow(member, book, BorrowStatus.Borrowed);

            borrowService = PrepareBorrowService(newBorrow, false);

            //Act & assert
            var exception = Assert.Throws<NoMoreBookCopiesException>(() => borrowService.AddNewBorrow(newBorrow));

            //Assert
            Assert.Equal("Odabrane knjige trenutno nema na stanju!", exception.Message);
        }

        //David Matijanić
        [Fact]
        public void AddNewBorrow_BorrowHasStatusBorrowedAndBookHasNoMoreCopiesAndReservationExists_BookIsBorrowed() {
            //Arrange
            int libraryId = 20;
            Member member = new Member {
                id = 42,
                Library_id = libraryId
            };
            Book book = new Book {
                id = 68,
                Library_id = libraryId,
                current_copies = 0
            };
            var newBorrow = PrepareNewBorrow(member, book, BorrowStatus.Borrowed);

            borrowService = PrepareBorrowService(newBorrow, true);

            //Act
            borrowService.AddNewBorrow(newBorrow);

            //Assert
            Assert.Contains(newBorrow, borrows);
        }

        //David Matijanić
        [Fact]
        public void AddNewBorrow_BorrowHasStatusBorrowedAndBookHasNoMoreCopiesAndReservationExists_NumberOfCopiesStaysAtZero() {
            //Arrange
            int libraryId = 20;
            Member member = new Member {
                id = 42,
                Library_id = libraryId
            };
            Book book = new Book {
                id = 68,
                Library_id = libraryId,
                current_copies = 0
            };
            var newBorrow = PrepareNewBorrow(member, book, BorrowStatus.Borrowed);

            borrowService = PrepareBorrowService(newBorrow, true);

            //Act
            borrowService.AddNewBorrow(newBorrow);

            //Assert
            Assert.Equal(0, newBorrow.Book.current_copies);
        }

        //David Matijanić
        [Fact]
        public void UpdateBorrow_WaitingBorrowDoesntExist_NoBorrowIsUpdated() {
            //Arrange
            Member member = new Member { id = 123, Library_id = 1 };
            Borrow borrow = new Borrow {
                idBorrow = 1,
                borrow_date = DateTime.Now,
                return_date = DateTime.Now.AddDays(7),
                borrow_status = (int)BorrowStatus.Waiting,
                Book_id = 300,
                Member_id = member.id,
                Employee_borrow_id = 56,
                Book = new Book { id = 3, Library_id = 1 },
                Employee = new Employee { id = 56 },
                Member = member
            };
            A.CallTo(() => borrowRepository.Update(borrow, true)).Returns(borrows.Contains(borrow) ? 1 : 0);

            //Act
            int updatedBorrowNumber = borrowService.UpdateBorrow(borrow);

            //Assert
            Assert.Equal(0, updatedBorrowNumber);
        }

        //David Matijanić
        [Fact]
        public void UpdateBorrow_BorrowExists_BorrowIsUpdated() {
            //Arrange
            Borrow borrow = borrows.First();
            borrow.borrow_status = (int)BorrowStatus.Late;
            A.CallTo(() => borrowRepository.Update(borrow, true)).Returns(borrows.Contains(borrow) ? 1 : 0);

            //Act
            int updatedBorrowNumber = borrowService.UpdateBorrow(borrow);

            //Assert
            Assert.Equal(1, updatedBorrowNumber);
        }

        //David Matijanić
        [Fact]
        public void UpdateBorrow_BorrowHasStatusReturned_OneCopyIsInserted() {
            //Arrange
            Borrow borrow = borrows.First();
            borrow.borrow_status = (int)BorrowStatus.Returned;
            int current_copies = (int)borrow.Book.current_copies;
            A.CallTo(() => borrowRepository.Update(borrow, true)).Returns(borrows.Contains(borrow) ? 1 : 0);

            IBookRepository bookRepository = A.Fake<IBookRepository>();
            A.CallTo(() => bookRepository.InsertOneCopy(borrow.Book, true)).Invokes(call => {
                borrow.Book.current_copies++;
            }).Returns(1);

            BookServices bookService = new BookServices(bookRepository, null, null);

            IReservationRepository reservationRepository = A.Fake<IReservationRepository>();
            A.CallTo(() => reservationRepository.EnterDateForReservation(borrow.Book)).Returns(true);

            ReservationService reservationService = new ReservationService(reservationRepository, bookService);

            borrowService = new BorrowService(borrowRepository, reservationService, null);

            //Act
            borrowService.UpdateBorrow(borrow);

            //Assert
            Assert.Equal(current_copies + 1, borrow.Book.current_copies);
        }

        //David Matijanić
        [Fact]
        public void UpdateBorrow_BorrowHasStatusBorrowedAndNoMoreCurrentCopies_ThrowsNoMoreBookCopiesException() {
            //Arrange
            Borrow borrow = new Borrow {
                idBorrow = 1,
                borrow_date = DateTime.Now,
                return_date = DateTime.Now.AddDays(7),
                borrow_status = (int)BorrowStatus.Borrowed,
                Book_id = 3,
                Member_id = 33,
                Employee_borrow_id = 56,
                Book = new Book { id = 3, Library_id = 1, current_copies = 0 },
                Employee = new Employee { id = 56 },
                Member = new Member {
                    id = 33,
                    Library_id = 1
                }
            };

            //Act & assert
            Assert.Throws<NoMoreBookCopiesException>(() => borrowService.UpdateBorrow(borrow));
        }

        //David Matijanić
        [Fact]
        public void UpdateBorrow_BorrowHasStatusBorrowedAndHasCopies_BorrowIsUpdated() {
            //Arrange
            Borrow borrow = borrows.First();
            borrow.borrow_status = (int)BorrowStatus.Borrowed;
            A.CallTo(() => borrowRepository.Update(borrow, true)).Returns(borrows.Contains(borrow) ? 1 : 0);

            IBookRepository bookRepository = A.Fake<IBookRepository>();
            A.CallTo(() => bookRepository.Update(borrow.Book, true)).Returns(1);

            BookServices bookService = new BookServices(bookRepository, null, null);

            borrowService = new BorrowService(borrowRepository, null, bookService);

            //Act
            int updatedBorrowNumber = borrowService.UpdateBorrow(borrow);

            //Assert
            Assert.Equal(1, updatedBorrowNumber);
        }

        //David Matijanić
        [Fact]
        public void UpdateBorrow_BorrowHasStatusBorrowedAndHasCopies_OneCopyIsRemoved() {
            //Arrange
            Borrow borrow = borrows.First();
            borrow.borrow_status = (int)BorrowStatus.Borrowed;
            int currentCopies = (int)borrow.Book.current_copies;
            A.CallTo(() => borrowRepository.Update(borrow, true)).Returns(borrows.Contains(borrow) ? 1 : 0);

            IBookRepository bookRepository = A.Fake<IBookRepository>();
            A.CallTo(() => bookRepository.Update(borrow.Book, true)).Returns(1);

            BookServices bookService = new BookServices(bookRepository, null, null);

            borrowService = new BorrowService(borrowRepository, null, bookService);

            //Act
            borrowService.UpdateBorrow(borrow);

            //Assert
            Assert.Equal(currentCopies - 1, borrow.Book.current_copies);
        }

        //David Matijanić
        [Fact]
        public void GetAllBorrowsForEmployee_GivenThereAreNoBorrows_NoBorrowsRetrieved() {
            //Arrange
            A.CallTo(() => borrowRepository.GetBorrowsForEmployee(1233)).Returns(new List<Borrow>().AsQueryable());

            //Act
            var borrowsForEmployee = borrowService.GetBorrowsForEmployee(1233);

            //Assert
            Assert.Equal(borrowsForEmployee, new List<Borrow>());
        }

        //David Matijanić
        [Theory]
        [InlineData(111)]
        [InlineData(222)]
        [InlineData(333)]
        [InlineData(444)]
        [InlineData(555)]
        [InlineData(6666)]
        public void GetAllBorrowsForEmployee_GivenTheCorrectEmployeeIdIsEntered_BorrowsRetrieved(int employeeId) {
            //Arrange
            A.CallTo(() => borrowRepository.GetBorrowsForEmployee(employeeId)).Returns(borrows.Where(b => b.Employee.id == employeeId || (b.Employee1 != null ? (b.Employee1.id == employeeId) : false)));

            //Act
            var borrowsForEmployee = borrowService.GetBorrowsForEmployee(employeeId);

            //Assert
            Assert.Equal(borrowsForEmployee, borrows.Where(b => b.Employee.id == employeeId || (b.Employee1 != null ? (b.Employee1.id == employeeId) : false)).ToList());
        }

        //David Matijanić
        [Fact]
        public void Dispose_CallsDisposeOnRepository() {
            //Act
            borrowService.Dispose();

            //Assert
            A.CallTo(() => borrowRepository.Dispose()).MustHaveHappened();
        }

        //David Matijanić
        private Borrow PrepareNewBorrow(BorrowStatus borrowStatus = BorrowStatus.Borrowed) {
            int libraryId = 20;
            Member member = new Member {
                id = 42,
                Library_id = libraryId
            };
            Book book = new Book {
                id = 68,
                Library_id = libraryId,
                current_copies = 5
            };
            Borrow newBorrow = new Borrow {
                idBorrow = 1234,
                Book_id = book.id,
                Book = book,
                Member_id = member.id,
                Member = member,
                borrow_date = DateTime.Now.AddDays(-2),
                return_date = DateTime.Now.AddDays(3),
                borrow_status = (int)borrowStatus,
            };

            return newBorrow;
        }

        //David Matijanić
        private Borrow PrepareNewBorrow(Member member, Book book, BorrowStatus borrowStatus = BorrowStatus.Borrowed) {
            Borrow newBorrow = new Borrow {
                idBorrow = 1234,
                Book_id = book.id,
                Book = book,
                Member_id = member.id,
                Member = member,
                borrow_date = DateTime.Now.AddDays(-2),
                return_date = DateTime.Now.AddDays(3),
                borrow_status = (int)borrowStatus,
            };

            return newBorrow;
        }

        //David Matijanić
        private BorrowService PrepareBorrowService(Borrow newBorrow, bool reservationExists) {
            IBookRepository bookRepository = A.Fake<IBookRepository>();
            A.CallTo(() => bookRepository.Update(newBorrow.Book, true)).Returns(1);

            IReservationRepository reservationRepository = A.Fake<IReservationRepository>();
            A.CallTo(() => reservationRepository.CheckValidReservationFroMember(newBorrow.Member.id, newBorrow.Book.id))
                .Returns(reservationExists ? new Reservation() : null);

            BookServices bookService = new BookServices(bookRepository, null, null);
            A.CallTo(() => borrowRepository.Add(newBorrow, true)).Invokes(call => {
                List<Borrow> borrowsWithAddedBorrow = borrows.ToList();
                borrowsWithAddedBorrow.Add(newBorrow);
                borrows = borrowsWithAddedBorrow.AsQueryable();
            }).Returns(1);

            return new BorrowService(borrowRepository, new ReservationService(reservationRepository, null), bookService);
        }

        //David Matijanić
        private BorrowService InitialiseBorrowService(IBorrowRepository repository) {
            var service = new BorrowService(repository, null, null);

            return service;
        }
    }
}
