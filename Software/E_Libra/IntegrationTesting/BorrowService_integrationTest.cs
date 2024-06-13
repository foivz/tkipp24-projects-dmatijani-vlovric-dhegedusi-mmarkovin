using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using BussinessLogicLayer.services;
using EntitiesLayer;
using BussinessLogicLayer.Exceptions;

namespace IntegrationTesting
{
    [Collection("Database collection")]
    public class BorrowService_integrationTest
    {
        private readonly BorrowService borrowService;
        private readonly DatabaseFixture fixture;

        private readonly Library library;
        private readonly List<Member> members;
        private readonly List<Genre> genres;
        private readonly List<Employee> employees;
        private readonly List<Book> books;
        private readonly List<Borrow> borrows;


        public BorrowService_integrationTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
            this.fixture.ResetDatabase();

            library = new Library
            {
                id = 123
            };
            InsertLibraryIntoDatabase();

            members = new List<Member>
            {
                new Member
                {
                    id = 1,
                    name = "Pero",
                    surname = "Peric",
                    username = "pperic",
                    password = "12345",
                    barcode_id = "c54sdf42",
                    OIB = "12345678901",
                    Library_id = library.id
                },
                new Member
                {
                    id = 2, 
                    name = "Vlaho",
                    surname = "Misic",
                    username = "vmisic",
                    password = "231321",
                    barcode_id = "dGV3312",
                    OIB = "11111222342",
                    Library_id = library.id
                },
                new Member
                {
                    id = 3,
                    name = "Ivan",
                    surname = "Ivic",
                    username = "iivic",
                    password = "123456",
                    barcode_id = "d3f3f3f3",
                    OIB = "12345678902",
                    Library_id = library.id
                },
                new Member
                {
                    id = 4,
                    name = "Marko",
                    surname = "Markic",
                    username = "mmarkic",
                    password = "strongpw",
                    barcode_id = "aaaaaaah",
                    OIB = "11119922883",
                    Library_id = library.id
                },
                new Member
                {
                    id = 5,
                    name = "Nemanja",
                    surname = "Posudbic",
                    username = "nposudbic",
                    password = "weakpw",
                    barcode_id = "asd3asd1",
                    OIB = "12321213223",
                    Library_id = library.id
                }
            };
            InsertMembersIntoDatabase(members);

            genres = new List<Genre>
            {
                new Genre { name = "Science fiction" },
                new Genre { name = "Fantasy" },
                new Genre { name = "Horror" }
            };
            InsertGenresIntoDatabase(genres);

            employees = new List<Employee>
            {
                new Employee {
                    id = 1,
                    name = "Darko",
                    surname = "Daric",
                    username = "ddaric",
                    password = "jakalozinka",
                    OIB = "11892593283",
                    Library_id = library.id
                },
                new Employee {
                    id = 2,
                    name = "Marina",
                    surname = "Misic",
                    username = "mmisic",
                    password = "mypw",
                    OIB = "85738923405",
                    Library_id = library.id
                },
                new Employee {
                    id = 3,
                    name = "Sven",
                    surname = "Sivic",
                    username = "ssivic",
                    password = "graygrey",
                    OIB = "23423423424",
                    Library_id = library.id
                },
                new Employee {
                    id = 4,
                    name = "Lea",
                    surname = "Lalic",
                    username = "llalic",
                    password = "jasamlea",
                    OIB = "22738449503",
                    Library_id = library.id
                },
                new Employee {
                    id = 5,
                    name = "Nemko",
                    surname = "Njemic",
                    username = "nnjemic",
                    password = "sad32asd",
                    OIB = "33433443312",
                    Library_id = library.id
                }
            };
            InsertEmployeesIntoDatabase(employees);

            books = new List<Book>
            {
                new Book
                {
                    id = 1,
                    name = "Dune",
                    pages_num = 412,
                    digital = 0,
                    barcode_id = "dune123",
                    total_copies = 5,
                    current_copies = 5,
                    Genre_id = 1,
                    Library_id = library.id
                },
                new Book
                {
                    id = 2,
                    name = "Cool book",
                    pages_num = 223,
                    digital = 0,
                    barcode_id = "coolbookxd",
                    total_copies = 17,
                    current_copies = 6,
                    Genre_id = 2,
                    Library_id = library.id
                },
                new Book
                {
                    id = 3,
                    name = "Scary book",
                    pages_num = 123,
                    digital = 0,
                    barcode_id = "scarybook",
                    total_copies = 34,
                    current_copies = 31,
                    Genre_id = 3,
                    Library_id = library.id
                },
                new Book
                {
                    id = 4,
                    name = "Programiranje 2",
                    pages_num = 0,
                    digital = 1,
                    barcode_id = "prog2",
                    total_copies = 33,
                    current_copies = 33,
                    Genre_id = 1,
                    Library_id = library.id
                }
            };
            InsertBooksIntoDatabase(books);

            borrows = new List<Borrow>
            {
                new Borrow
                {
                    Book_id = 1,
                    Member_id = 1,
                    borrow_status = (int)BorrowStatus.Waiting,
                    borrow_date = DateTime.Now.AddDays(-3),
                    return_date = DateTime.Now.AddDays(2),
                    Employee_borrow_id = 1,
                    Employee_return_id = null
                },
                new Borrow
                {
                    Book_id = 2,
                    Member_id = 2,
                    borrow_status = (int)BorrowStatus.Waiting,
                    borrow_date = DateTime.Now.AddDays(-3),
                    return_date = DateTime.Now.AddDays(2),
                    Employee_borrow_id = 1,
                    Employee_return_id = null
                },
                new Borrow
                {
                    Book_id = 3,
                    Member_id = 2,
                    borrow_status = (int)BorrowStatus.Borrowed,
                    borrow_date = DateTime.Now.AddDays(-17),
                    return_date = DateTime.Now.AddDays(13),
                    Employee_borrow_id = 1,
                    Employee_return_id = null
                },
                new Borrow
                {
                    Book_id = 3,
                    Member_id = 3,
                    borrow_status = (int)BorrowStatus.Borrowed,
                    borrow_date = DateTime.Now.AddDays(-8),
                    return_date = DateTime.Now.AddDays(24),
                    Employee_borrow_id = 1,
                    Employee_return_id = null
                },
                new Borrow
                {
                    Book_id = 4,
                    Member_id = 1,
                    borrow_status = (int)BorrowStatus.Late,
                    borrow_date = DateTime.Now.AddDays(-35),
                    return_date = DateTime.Now.AddDays(-5),
                    Employee_borrow_id = 2,
                    Employee_return_id = null
                },
                new Borrow
                {
                    Book_id = 4,
                    Member_id = 2,
                    borrow_status = (int)BorrowStatus.Late,
                    borrow_date = DateTime.Now.AddDays(-64),
                    return_date = DateTime.Now.AddDays(-31),
                    Employee_borrow_id = 3,
                    Employee_return_id = null
                },
                new Borrow
                {
                    Book_id = 4,
                    Member_id = 3,
                    borrow_status = (int)BorrowStatus.Returned,
                    borrow_date = DateTime.Now.AddDays(-3),
                    return_date = DateTime.Now,
                    Employee_borrow_id = 2,
                    Employee_return_id = 4
                },
                new Borrow
                {
                    Book_id = 4,
                    Member_id = 4,
                    borrow_status = (int)BorrowStatus.Returned,
                    borrow_date = DateTime.Now.AddDays(-3),
                    return_date = DateTime.Now,
                    Employee_borrow_id = 3,
                    Employee_return_id = 4
                },
            };
            InsertBorrowsIntoDatabase(borrows);

            borrowService = new BorrowService();
        }

        //David Matijanić
        [Fact]
        public void GetAllBorrowsForMember_GivenThereAreNoBorrows_NoBorrowsRetrieved()
        {
            //Act
            var borrowsForMember = borrowService.GetAllBorrowsForMember(5, library.id);

            //Assert
            Assert.Empty(borrowsForMember);
        }

        //David Matijanić
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void GetAllBorrowsForMember_GivenCorrectMemberAndLibraryIdIsEntered_BorrowsRetrieved(int memberId)
        {
            //Act
            var borrowsForMember = borrowService.GetAllBorrowsForMember(memberId, library.id);

            //Assert
            borrowsForMember.Should().BeEquivalentTo(borrows.Where(b => b.Member_id == memberId), option => option
                .Excluding(b => b.Book)
                .Excluding(b => b.Employee)
                .Excluding(b => b.Employee1)
                .Excluding(b => b.Member)
                .Excluding(b => b.idBorrow)
                .Excluding(b => b.borrow_date)
                .Excluding(b => b.return_date)
            );
        }

        //David Matijanić
        [Theory]
        [InlineData(BorrowStatus.Waiting)]
        [InlineData(BorrowStatus.Borrowed)]
        [InlineData(BorrowStatus.Late)]
        [InlineData(BorrowStatus.Returned)]
        public void GetBorrowsForMemberByStatus_GivenThereAreNoBorrows_NoBorrowsRetrieved(BorrowStatus borrowStatus)
        {
            //Act
            var borrowsForMember = borrowService.GetBorrowsForMemberByStatus(5, library.id, borrowStatus);

            //Assert
            Assert.Empty(borrowsForMember);
        }

        //David Matijanić
        [Theory]
        [InlineData(1, BorrowStatus.Waiting)]
        [InlineData(1, BorrowStatus.Late)]
        [InlineData(1, BorrowStatus.Returned)]
        [InlineData(2, BorrowStatus.Waiting)]
        [InlineData(2, BorrowStatus.Borrowed)]
        [InlineData(2, BorrowStatus.Returned)]
        [InlineData(3, BorrowStatus.Waiting)]
        [InlineData(3, BorrowStatus.Borrowed)]
        [InlineData(3, BorrowStatus.Late)]
        [InlineData(4, BorrowStatus.Borrowed)]
        [InlineData(4, BorrowStatus.Late)]
        [InlineData(4, BorrowStatus.Returned)]
        [InlineData(5, BorrowStatus.Waiting)]
        [InlineData(5, BorrowStatus.Late)]
        private void GetBorrowsForMemberByStatus_GivenTheCorrectMemberAndBorrowStatusIsEntered_CorrectBorrowsRetrieved(int memberId, BorrowStatus borrowStatus)
        {
            //Act
            var borrowsForMember = borrowService.GetBorrowsForMemberByStatus(memberId, library.id, borrowStatus);

            //Assert
            borrowsForMember.Should().BeEquivalentTo(borrows.Where(b => b.Member_id == memberId && b.borrow_status == (int)borrowStatus), option => option
                .Excluding(b => b.Book)
                .Excluding(b => b.Employee)
                .Excluding(b => b.Employee1)
                .Excluding(b => b.Member)
                .Excluding(b => b.idBorrow)
                .Excluding(b => b.borrow_date)
                .Excluding(b => b.return_date)
            );
        }

        //David Matijanić
        [Fact]
        public void GetBorrowsForMemberAndBook_GivenThereAreNoBorrows_NoBorrowsRetrieved()
        {
            //Act
            var borrowsForMember = borrowService.GetBorrowsForMemberAndBook(5, 1, library.id);

            //Assert
            Assert.Empty(borrowsForMember);
        }

        //David Matijanić
        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(1, 4)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(2, 3)]
        [InlineData(3, 2)]
        [InlineData(3, 4)]
        [InlineData(4, 3)]
        [InlineData(5, 1)]
        [InlineData(5, 2)]
        public void GetBorrowsForMemberAndBook_GivenTheCorrectMemberAndBookIdIsEntered_CorrectBorrowIsRetrieved(int memberId, int bookId)
        {
            //Act
            var borrowsForMember = borrowService.GetBorrowsForMemberAndBook(memberId, bookId, library.id);

            //Assert
            borrowsForMember.Should().BeEquivalentTo(borrows.Where(b => b.Member_id == memberId && b.Book_id == bookId), option => option
                .Excluding(b => b.Book)
                .Excluding(b => b.Employee)
                .Excluding(b => b.Employee1)
                .Excluding(b => b.Member)
                .Excluding(b => b.idBorrow)
                .Excluding(b => b.borrow_date)
                .Excluding(b => b.return_date)
            );
        }

        //David Matijanić
        [Fact]
        public async Task GetAllBorrowsForLibraryAsync_GivenAnExistingLibraryIsEntered_CorrectBorrowsRetrieved()
        {
            //Act
            var borrowsForLibrary = await borrowService.GetAllBorrowsForLibraryAsync(library);

            //Assert
            borrowsForLibrary.Should().BeEquivalentTo(borrows, option => option
                .Excluding(b => b.Book)
                .Excluding(b => b.Employee)
                .Excluding(b => b.Employee1)
                .Excluding(b => b.Member)
                .Excluding(b => b.idBorrow)
                .Excluding(b => b.borrow_date)
                .Excluding(b => b.return_date)
            );
        }

        //David Matijanić
        [Theory]
        [InlineData(BorrowStatus.Waiting)]
        [InlineData(BorrowStatus.Borrowed)]
        [InlineData(BorrowStatus.Late)]
        [InlineData(BorrowStatus.Returned)]
        public async Task GetAllBorrowsForLibraryByStatusAsync_GivenAnExistingLibraryIsEntered_CorrectBorrowsRetrieved(BorrowStatus borrowStatus)
        {
            //Act
            var borrowsForLibrary = await borrowService.GetBorrowsForLibraryByStatusAsync(library, borrowStatus);

            //Assert
            borrowsForLibrary.Should().BeEquivalentTo(borrows.Where(b => b.borrow_status == (int)borrowStatus), option => option
                .Excluding(b => b.Book)
                .Excluding(b => b.Employee)
                .Excluding(b => b.Employee1)
                .Excluding(b => b.Member)
                .Excluding(b => b.idBorrow)
                .Excluding(b => b.borrow_date)
                .Excluding(b => b.return_date)
            );
        }

        //David Matijanić
        [Fact]
        public void GetBorrowsForEmployee_EmployeeHasNoBorrows_NoBorrowsReturned()
        {
            //Arrange
            int employeeId = employees[4].id;

            //Act
            var borrowsForEmployee = borrowService.GetBorrowsForEmployee(employeeId);

            //Assert
            Assert.Empty(borrowsForEmployee);
        }

        //David Matijanić
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void GetBorrowsForEmployee_GivenTheCorrectEmployeeIdIsEntered_BorrowsRetrieved(int employeeId)
        {
            //Act
            var borrowsForEmployee = borrowService.GetBorrowsForEmployee(employeeId);

            //Assert
            borrowsForEmployee.Should().BeEquivalentTo(borrows.Where(b => b.Employee_borrow_id == employeeId || b.Employee_return_id == employeeId), option => option
                .Excluding(b => b.Book)
                .Excluding(b => b.Employee)
                .Excluding(b => b.Employee1)
                .Excluding(b => b.Member)
                .Excluding(b => b.idBorrow)
                .Excluding(b => b.borrow_date)
                .Excluding(b => b.return_date)
            );
        }

        //David Matijanić
        [Fact]
        public void AddNewBorrow_BookHasNoCopies_ThrowsNoMoreBookCopiesException()
        {
            //Arrange
            Book book = books[0];
            book.current_copies = 0;

            Borrow borrow = new Borrow
            {
                Book = book,
                Member = members[0],
                borrow_status = (int)BorrowStatus.Borrowed,
                borrow_date = DateTime.Now,
                return_date = DateTime.Now.AddDays(7),
                Employee_borrow_id = employees[0].id,
                Employee_return_id = null
            };

            //Act
            Action act = () => borrowService.AddNewBorrow(borrow);

            //Assert
            act.Should().Throw<NoMoreBookCopiesException>().WithMessage("Odabrane knjige trenutno nema na stanju!");
        }

        //David Matijanić
        [Fact]
        public void AddNewBorrow_BorrowHasStatusBorrowedAndHasCopiesAndNoReservationExists_BorrowIsAdded()
        {
            //Arrange
            Borrow borrow = new Borrow
            {
                Book = books[2],
                Member = members[0],
                borrow_status = (int)BorrowStatus.Borrowed,
                borrow_date = DateTime.Now,
                return_date = DateTime.Now.AddDays(7),
                Employee_borrow_id = employees[0].id,
                Employee_return_id = null
            };

            //Act
            int borrowed = borrowService.AddNewBorrow(borrow);

            //Assert
            Assert.Equal(1, borrowed);
        }

        private void InsertLibraryIntoDatabase()
        {
            string createLibrary =
            "INSERT [dbo].[Library] ([id], [name], [OIB], [phone], [email], [price_day_late], [address], [membership_duration]) " +
            "VALUES (123, N'Knjiznica', 123, 331, N'email', 3, N'adresa', GETDATE())";
            Helper.ExecuteCustomSql(createLibrary);
        }

        private void InsertEmployeesIntoDatabase(List<Employee> employeeList)
        {
            foreach (Employee employee in employeeList)
            {
                InsertEmployeeIntoDatabase(employee);
            }
        }

        private void InsertEmployeeIntoDatabase(Employee employee)
        {
            string sqlInsertEmployee = $"INSERT [dbo].[Employee] ([name], [surname], [username], [password], [OIB], [Library_id]) VALUES ('{employee.name}', '{employee.surname}', '{employee.username}', '{employee.password}', '{employee.OIB}', {employee.Library_id});";
            Helper.ExecuteCustomSql(sqlInsertEmployee);
        }

        private void InsertGenresIntoDatabase(List<Genre> genreList)
        {
            foreach(Genre genre in genreList)
            {
                InsertGenreIntoDatabase(genre);
            }
        }

        private void InsertGenreIntoDatabase(Genre genre)
        {
            string sqlInsertGenre = $"INSERT [dbo].[Genre] ([name]) VALUES ('{genre.name}')";
            Helper.ExecuteCustomSql(sqlInsertGenre);
        }

        private void InsertBooksIntoDatabase(List<Book> bookList)
        {
            foreach(Book book in bookList)
            {
                InsertBookIntoDatabase(book);
            }
        }

        private void InsertBookIntoDatabase(Book book)
        {
            string sqlInsertBook = $"INSERT [dbo].[Book] ([name], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) VALUES ('{book.name}', {book.pages_num}, {book.digital}, '{book.barcode_id}', {book.total_copies}, {book.current_copies}, {book.Genre_id}, {library.id});";
            Helper.ExecuteCustomSql(sqlInsertBook);
        }

        private void InsertMembersIntoDatabase(List<Member> memberList)
        {
            foreach(Member member in memberList)
            {
                InsertMemberIntoDatabase(member);
            }
        }

        private void InsertMemberIntoDatabase(Member member)
        {
            string sqlInsertMember = $"INSERT [dbo].[Member] ([name], [surname], [username], [password], [OIB], [Library_id], [barcode_id]) VALUES ('{member.name}', '{member.surname}', '{member.username}', '{member.password}', '{member.OIB}', {library.id}, '{member.barcode_id}');";
            Helper.ExecuteCustomSql(sqlInsertMember);
        }

        private void InsertBorrowsIntoDatabase(List<Borrow> borrowList)
        {
            foreach(Borrow borrow in borrowList)
            {
                InsertBorrowIntoDatabase(borrow);
            }
        }

        private void InsertBorrowIntoDatabase(Borrow borrow)
        {
            string borrowReturnDate = "NULL";
            if (borrow.return_date != null) {
                borrowReturnDate = "'" + ParseDateForSql((DateTime)borrow.return_date) +"'";
            }

            string employeeReturnId = "NULL";
            if (borrow.Employee_return_id != null)
            {
                employeeReturnId = borrow.Employee_return_id.ToString();
            }
            
            string sqlInsertBorrow = $"INSERT [dbo].[Borrow] ([Book_id], [Member_id], [borrow_status], [borrow_date], [return_date], [Employee_borrow_id], [Employee_return_id]) VALUES ({borrow.Book_id}, {borrow.Member_id}, {borrow.borrow_status}, '{ParseDateForSql(borrow.borrow_date)}', {borrowReturnDate}, {borrow.Employee_borrow_id}, {employeeReturnId});";
            Helper.ExecuteCustomSql(sqlInsertBorrow);
        }

        private DateTime GetDateFromMembershipDuration(decimal duration)
        {
            DateTime startDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
            DateTime durationDate = startDate.AddDays(Convert.ToDouble(duration) - 1);

            return durationDate;
        }

        private string ParseDateForSql(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}
