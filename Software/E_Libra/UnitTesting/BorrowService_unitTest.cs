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
                new Member { id = 012 }
            };
            borrows = new List<Borrow> {
                new Borrow {
                    idBorrow = 1,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now,
                    borrow_status = (int)BorrowStatus.Borrowed,
                    Book_id = 3,
                    Member_id = members[0].id,
                    Employee_borrow_id = 56,
                    Book = new Book(),
                    Employee = new Employee {
                        id = 56
                    },
                    Member = members[0]
                },
                new Borrow {
                    idBorrow = 2,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now,
                    borrow_status = (int)BorrowStatus.Late,
                    Book_id = 5,
                    Member_id = members[0].id,
                    Employee_borrow_id = 22,
                    Book = new Book(),
                    Employee = new Employee {
                        id = 22
                    },
                    Member = members[0]
                },
                new Borrow {
                    idBorrow = 2,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now,
                    borrow_status = (int)BorrowStatus.Late,
                    Book_id = 8,
                    Member_id = members[1].id,
                    Employee_borrow_id = 22,
                    Book = new Book(),
                    Employee = new Employee {
                        id = 22
                    },
                    Member = members[1]
                },
                new Borrow {
                    idBorrow = 2,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now,
                    borrow_status = (int)BorrowStatus.Returned,
                    Book_id = 13,
                    Member_id = members[1].id,
                    Employee_borrow_id = 22,
                    Employee_return_id = 33,
                    Book = new Book(),
                    Employee = new Employee {
                        id = 22
                    },
                    Employee1 = new Employee {
                        id = 33
                    },
                    Member = members[1]
                },
                new Borrow {
                    idBorrow = 2,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now,
                    borrow_status = (int)BorrowStatus.Waiting,
                    Book_id = 21,
                    Member_id = members[2].id,
                    Employee_borrow_id = 33,
                    Book = new Book(),
                    Employee = new Employee {
                        id = 33
                    },
                    Member = members[2]
                },
                new Borrow {
                    idBorrow = 2,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now,
                    borrow_status = (int)BorrowStatus.Returned,
                    Book_id = 13,
                    Member_id = members[2].id,
                    Employee_borrow_id = 22,
                    Employee_return_id = 33,
                    Book = new Book(),
                    Employee = new Employee {
                        id = 22
                    },
                    Employee1 = new Employee {
                        id = 33
                    },
                    Member = members[2]
                },
                new Borrow {
                    idBorrow = 2,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now,
                    borrow_status = (int)BorrowStatus.Returned,
                    Book_id = 21,
                    Member_id = members[3].id,
                    Employee_borrow_id = 22,
                    Employee_return_id = 33,
                    Book = new Book(),
                    Employee = new Employee {
                        id = 22
                    },
                    Employee1 = new Employee {
                        id = 33
                    },
                    Member = members[3]
                },
                new Borrow {
                    idBorrow = 2,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now,
                    borrow_status = (int)BorrowStatus.Waiting,
                    Book_id = 34,
                    Member_id = members[3].id,
                    Employee_borrow_id = 22,
                    Book = new Book(),
                    Employee = new Employee {
                        id = 22
                    },
                    Member = members[3]
                }
            }.AsQueryable();
            borrowService = InitialiseBorrowService(borrowRepository);
        }

        private BorrowService InitialiseBorrowService(IBorrowRepository repository) {
            var service = new BorrowService(repository, null, null);

            return service;
        }

        [Theory]
        [InlineData(123)]
        [InlineData(456)]
        [InlineData(789)]
        [InlineData(012)]
        [InlineData(0)]
        public void GetAllBorrowsForMember_GivenTheCorrectMemberAndLibraryIdIsEntered_BorrowsRetrieved(int memberId) {
            //Arrange
            A.CallTo(() => borrowRepository.GetAllBorrowsForMember(memberId, 456)).Returns(borrows.Where(b => b.Member_id == memberId));

            //Act
            var borrowsForMember = borrowService.GetAllBorrowsForMember(memberId, 456);

            //Assert
            Assert.Equal(borrowsForMember, borrows.Where(b => b.Member_id == memberId).ToList());
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
            A.CallTo(() => borrowRepository.GetBorrowsForMemberByStatus(memberId, 456, borrowStatus)).Returns(borrows.Where(b => b.Member_id == memberId && b.borrow_status == (int)borrowStatus));

            //Act
            var borrowsForMember = borrowService.GetBorrowsForMemberByStatus(memberId, 456, borrowStatus);

            //Assert
            Assert.Equal(borrowsForMember, borrows.Where(b => b.Member_id == memberId && b.borrow_status == (int)borrowStatus).ToList());
        }
    }
}
