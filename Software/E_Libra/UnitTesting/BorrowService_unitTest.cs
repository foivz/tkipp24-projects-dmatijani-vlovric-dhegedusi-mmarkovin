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
        private BorrowService InitialiseBorrowService(IBorrowRepository repository) {
            var borrowService = new BorrowService(repository, null, null);

            return borrowService;
        }

        [Fact]
        public void GetAllBorrowsForMember_GivenTheCorrectMemberAndLibraryIdIsEntered_BorrowsRetrieved() {
            //Arrange
            var repo = A.Fake<IBorrowRepository>();
            Member member = new Member {
                id = 123
            };
            IQueryable<Borrow> borrows = new List<Borrow> {
                new Borrow {
                    idBorrow = 1,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now,
                    borrow_status = (int)BorrowStatus.Borrowed,
                    Book_id = 3,
                    Member_id = member.id,
                    Employee_borrow_id = 56,
                    Book = new Book(),
                    Employee = new Employee {
                        id = 56
                    },
                    Member = member
                },
                new Borrow {
                    idBorrow = 2,
                    borrow_date = DateTime.Now,
                    return_date = DateTime.Now,
                    borrow_status = (int)BorrowStatus.Late,
                    Book_id = 3,
                    Member_id = member.id,
                    Employee_borrow_id = 22,
                    Book = new Book(),
                    Employee = new Employee {
                        id = 22
                    },
                    Member = member
                }
            }.AsQueryable();
            A.CallTo(() => repo.GetAllBorrowsForMember(123, 456)).Returns(borrows);
            var borrowService = InitialiseBorrowService(repo);

            //Act
            var borrowsForMember = borrowService.GetAllBorrowsForMember(123, 456);

            //Assert
            Assert.Equal(borrowsForMember, borrows.ToList());
        }
    }
}
