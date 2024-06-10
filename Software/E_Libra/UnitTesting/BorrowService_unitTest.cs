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
                new Member { id = 123 },
                new Member { id = 456 },
                new Member { id = 789 },
                new Member { id = 12 }
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
                    Book = new Book { id = 3, Library_id = 456 },
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
                    Book = new Book { id = 5, Library_id = 456 },
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
                    Book = new Book { id = 8, Library_id = 456 },
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
                    Book = new Book { id = 13, Library_id = 456 },
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
                    Book = new Book { id = 21, Library_id = 456 },
                    Employee = new Employee { id = 33 },
                    Member = members[2]
                },
                new Borrow {
                    idBorrow = 6,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now.AddDays(14),
                    borrow_status = (int)BorrowStatus.Returned,
                    Book_id = 13,
                    Member_id = members[2].id,
                    Employee_borrow_id = 22,
                    Employee_return_id = 33,
                    Book = new Book { id = 13, Library_id = 456 },
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
                    Book = new Book { id = 21, Library_id = 456 },
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
                    Book = new Book { id = 34, Library_id = 456 },
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
                    Book = new Book { id = 7, Library_id = 456 },
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
                    Book = new Book { id = 9, Library_id = 456 },
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
        [InlineData(123)]
        [InlineData(456)]
        [InlineData(789)]
        [InlineData(012)]
        [InlineData(0)]
        public void GetAllBorrowsForMember_GivenTheCorrectMemberAndLibraryIdIsEntered_BorrowsRetrieved(int memberId) {
            //Arrange
            A.CallTo(() => borrowRepository.GetAllBorrowsForMember(memberId, 456)).Returns(borrows.Where(b => b.Member.id == memberId));

            //Act
            var borrowsForMember = borrowService.GetAllBorrowsForMember(memberId, 456);

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
        [InlineData(123, BorrowStatus.Waiting)]
        [InlineData(123, BorrowStatus.Late)]
        [InlineData(123, BorrowStatus.Returned)]
        [InlineData(789, BorrowStatus.Waiting)]
        [InlineData(789, BorrowStatus.Borrowed)]
        [InlineData(789, BorrowStatus.Late)]
        [InlineData(0, BorrowStatus.Borrowed)]
        [InlineData(0, BorrowStatus.Returned)]
        public void GetBorrowsForMemberByStatus_GivenTheCorrectMemberAndBorrowStatusIsEntered_CorrectBorrowsRetrieved(int memberId, BorrowStatus borrowStatus) {
            //Arrange
            A.CallTo(() => borrowRepository.GetBorrowsForMemberByStatus(memberId, 456, borrowStatus)).Returns(borrows.Where(b => b.Member.id == memberId && b.borrow_status == (int)borrowStatus));

            //Act
            var borrowsForMember = borrowService.GetBorrowsForMemberByStatus(memberId, 456, borrowStatus);

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
        [InlineData(123, 3)]
        [InlineData(123, 5)]
        [InlineData(456, 8)]
        [InlineData(456, 13)]
        [InlineData(789, 21)]
        [InlineData(789, 13)]
        [InlineData(12, 21)]
        [InlineData(12, 34)]
        [InlineData(123, 7)]
        [InlineData(456, 9)]
        public void GetBorrowForMemberAndBook_GivenTheCorrectMemberAndBookIdIsEntered_CorrectBorrowIsRetrieved(int memberId, int bookId) {
            //Arrange
            A.CallTo(() => borrowRepository.GetBorrowsForMemberAndBook(memberId, 456, bookId)).Returns(borrows.Where(b => b.Member.id == memberId && b.Book.id == bookId));

            //Act
            var borrowsForMember = borrowService.GetBorrowsForMemberAndBook(memberId, 456, bookId);

            //Assert
            Assert.Equal(borrowsForMember, borrows.Where(b => b.Member.id == memberId && b.Book.id == bookId).ToList());
        }
    }
}
