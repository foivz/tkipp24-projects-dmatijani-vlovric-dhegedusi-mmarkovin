using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using EntitiesLayer;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting {
    public class BorrowService_unitTest {
        private IQueryable<Borrow> borrows { get; set; }
        private readonly IBorrowRepository borrowRepository;
        private BorrowService borrowService { get; set; }

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
                    Employee_borrow_id = 56,
                    Book = new Book { id = 3, Library_id = 1 },
                    Employee = new Employee { id = 56 },
                    Member = members[0]
                },
                new Borrow {
                    idBorrow = 2,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(14),
                    borrow_status = (int)BorrowStatus.Late,
                    Book_id = 5,
                    Member_id = members[0].id,
                    Employee_borrow_id = 22,
                    Book = new Book { id = 5, Library_id = 1 },
                    Employee = new Employee { id = 22 },
                    Member = members[0]
                },
                new Borrow {
                    idBorrow = 3,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(10),
                    borrow_status = (int)BorrowStatus.Late,
                    Book_id = 8,
                    Member_id = members[1].id,
                    Employee_borrow_id = 22,
                    Book = new Book { id = 8, Library_id = 2 },
                    Employee = new Employee { id = 22 },
                    Member = members[1]
                },
                new Borrow {
                    idBorrow = 4,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(3),
                    borrow_status = (int)BorrowStatus.Returned,
                    Book_id = 13,
                    Member_id = members[1].id,
                    Employee_borrow_id = 22,
                    Employee_return_id = 33,
                    Book = new Book { id = 13, Library_id = 2 },
                    Employee = new Employee { id = 22 },
                    Employee1 = new Employee { id = 33 },
                    Member = members[1]
                },
                new Borrow {
                    idBorrow = 5,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(7),
                    borrow_status = (int)BorrowStatus.Waiting,
                    Book_id = 21,
                    Member_id = members[2].id,
                    Employee_borrow_id = 33,
                    Book = new Book { id = 21, Library_id = 3 },
                    Employee = new Employee { id = 33 },
                    Member = members[2]
                },
                new Borrow {
                    idBorrow = 6,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(14),
                    borrow_status = (int)BorrowStatus.Returned,
                    Book_id = 55,
                    Member_id = members[2].id,
                    Employee_borrow_id = 22,
                    Employee_return_id = 33,
                    Book = new Book { id = 13, Library_id = 3 },
                    Employee = new Employee { id = 22 },
                    Employee1 = new Employee { id = 33 },
                    Member = members[2]
                },
                new Borrow {
                    idBorrow = 7,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(21),
                    borrow_status = (int)BorrowStatus.Returned,
                    Book_id = 21,
                    Member_id = members[3].id,
                    Employee_borrow_id = 22,
                    Employee_return_id = 33,
                    Book = new Book { id = 21, Library_id = 3 },
                    Employee = new Employee { id = 22 },
                    Employee1 = new Employee { id = 33 },
                    Member = members[3]
                },
                new Borrow {
                    idBorrow = 8,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(7),
                    borrow_status = (int)BorrowStatus.Waiting,
                    Book_id = 34,
                    Member_id = members[3].id,
                    Employee_borrow_id = 22,
                    Book = new Book { id = 34, Library_id = 3 },
                    Employee = new Employee { id = 22 },
                    Member = members[3]
                },
                new Borrow {
                    idBorrow = 9,
                    borrow_date = DateTime.Now.AddDays(-1),
                    return_date = DateTime.Now.AddDays(6),
                    borrow_status = (int)BorrowStatus.Borrowed,
                    Book_id = 7,
                    Member_id = members[0].id,
                    Employee_borrow_id = 44,
                    Book = new Book { id = 7, Library_id = 1 },
                    Employee = new Employee { id = 44 },
                    Member = members[0]
                },
                new Borrow {
                    idBorrow = 10,
                    borrow_date = DateTime.Now.AddDays(-2),
                    return_date = DateTime.Now.AddDays(5),
                    borrow_status = (int)BorrowStatus.Borrowed,
                    Book_id = 9,
                    Member_id = members[1].id,
                    Employee_borrow_id = 45,
                    Book = new Book { id = 9, Library_id = 2 },
                    Employee = new Employee { id = 45 },
                    Member = members[1]
                }
            }.AsQueryable();
            borrowService = InitialiseBorrowService(borrowRepository);
        }

        private BorrowService InitialiseBorrowService(IBorrowRepository repository) {
            var service = new BorrowService(repository, null, null);

            return service;
        }

        [Fact]
        public void GetAllBorrowsForMember_GivenThereAreNoBorrows_NoBorrowsRetrieved() {
            //Arrange
            A.CallTo(() => borrowRepository.GetAllBorrowsForMember(1, 1)).Returns(new List<Borrow>().AsQueryable());

            //Act
            var borrowsForMember = borrowService.GetAllBorrowsForMember(1, 1);

            // Assert
            Assert.Equal(borrowsForMember, new List<Borrow>());
        }

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
            Assert.Equal(borrowsForMember, borrows.Where(b => b.Member.id == memberId).ToList());
        }

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

            // Assert
            Assert.Equal(borrowsForMember, new List<Borrow>());
        }

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
            Assert.Equal(borrowsForMember, borrows.Where(b => b.Member.id == memberId && b.borrow_status == (int)borrowStatus).ToList());
        }

        [Fact]
        public void GetBorrowsForMemberAndBook_GivenThereAreNoBorrows_NoBorrowsRetrieved() {
            //Arrange
            A.CallTo(() => borrowRepository.GetBorrowsForMemberAndBook(1, 1, 1)).Returns(new List<Borrow>().AsQueryable());

            //Act
            var borrowsForMember = borrowService.GetBorrowsForMemberAndBook(1, 1, 1);

            // Assert
            Assert.Equal(borrowsForMember, new List<Borrow>());
        }

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
            Assert.Equal(borrowsForMember, borrows.Where(b => b.Member.id == memberId && b.Book.id == bookId).ToList());
        }

        [Fact]
        public async Task GetAllBorrowsForLibraryAsync_GivenThereAreNoBorrows_NoBorrowsRetrieved() {
            //Arrange
            A.CallTo(() => borrowRepository.GetAllBorrowsForLibraryAsync(5)).Returns(Task.FromResult(new List<Borrow>()));

            //Act
            var borrowsForLibrary = await borrowService.GetAllBorrowsForLibraryAsync(5);

            //Arrange
            Assert.Equal(borrowsForLibrary, new List<Borrow>());
        }

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
            Assert.Equal(borrowsForLibrary, borrows.Where(b => b.Member.Library_id == libraryId).ToList());
        }

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
            Assert.Equal(borrowsForLibrary, borrows.Where(b => b.Member.Library_id == library.id).ToList());
        }
    }
}
