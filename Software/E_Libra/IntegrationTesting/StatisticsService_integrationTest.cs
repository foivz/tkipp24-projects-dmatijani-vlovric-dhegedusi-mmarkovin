using BussinessLogicLayer.services;
using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting {

    [Collection("Database collection")]
    public class StatisticsService_integrationTest {

        readonly StatisticsService statisticsService;
        readonly DatabaseFixture fixture;


        public StatisticsService_integrationTest(DatabaseFixture fixture) {
            statisticsService = new StatisticsService();
            this.fixture = fixture;
            this.fixture.ResetDatabase();


            var library = new Library {
                id = 1,
            };
            InsertLibraryIntoDatabase();


            var members = new List<Member>
            {
                new Member {
                    name = "Ivo",
                    surname = "Ivic",
                    username = "iivic",
                    password = "ivo123",
                    OIB = "57647557445",
                    barcode_id = "B001",
                    Library_id = library.id
                },
                new Member {
                    name = "Ana",
                    surname = "Anic",
                    username = "aanic",
                    password = "ana123",
                    OIB = "64363434343",
                    barcode_id = "B002",
                    Library_id = library.id
                }
            };
            InsertMemberIntoDatabase(members);


            var genres = new List<Genre>
            {
                new Genre {
                    name = "Tragedija"
                },
                new Genre {
                    name = "Drama"
                }
            };
            InsertGenreIntoDatabase(genres);


            var authors = new List<Author>
            {
                new Author {
                    idAuthor = 1,
                    name = "William",
                    surname = "Shakespare"},
                new Author {
                    idAuthor = 2,
                    name = "Cecilije",
                    surname = "Borovski"}
            };
            InsertAuthorIntoDatabase(authors);


            var books = new List<Book>
           {
                new Book {
                    name = "Hamlet",
                    description = "Nema opisa",
                    pages_num = 300,
                    digital = 1,
                    url_photo = "slika1",
                    barcode_id = "BC0011",
                    total_copies = 5,
                    current_copies = 3,
                    Genre_id = 1,
                    Library_id = library.id },
                new Book {
                    name = "Romeo i Julija",
                    description = "Description 2",
                    pages_num = 400,
                    digital = 0,
                    url_photo = "slika2",
                    barcode_id = "BC0022",
                    total_copies = 10,
                    current_copies = 7,
                    Genre_id = 2,
                    Library_id = library.id }
            };
            InsertBookIntoDatabase(books);

            var employees = new List<Employee>
            {
                new Employee {
                    name = "Darko",
                    surname = "Daric",
                    username = "ddaric",
                    password = "jakalozinka",
                    OIB = "11892593283",
                    Library = library,
                    Library_id = library.id
                },
                new Employee {
                    name = "Marina",
                    surname = "Misic",
                    username = "mmisic",
                    password = "mypw",
                    OIB = "85738923405",
                    Library = library,
                    Library_id = library.id
                }
            };
            InsertEmployeeIntoDatabase(employees);

            var borrows = new List<Borrow> {
                new Borrow {
                    Book_id = 1,
                    Member_id = 1,
                    borrow_status = (int)BorrowStatus.Waiting,
                    Employee_borrow_id = 1
                },
                new Borrow {
                    Book_id = 2,
                    Member_id = 1,
                    borrow_status = (int)BorrowStatus.Waiting,
                    Employee_borrow_id = 1
                },
                new Borrow {
                    Book_id = 1,
                    Member_id = 2,
                    borrow_status = (int)BorrowStatus.Waiting,
                    Employee_borrow_id = 1
                }
            };
            InsertBorrowIntoDatabase(borrows);


            var reviews = new List<Review>{
                new Review {
                    Member_id = 1,
                    Book_id = 1,
                    comment = "Odlicna knjiga!",
                    rating = 5
                },
                new Review {
                    Member_id = 1,
                    Book_id = 2,
                    comment = "Preporucujem",
                    rating = 5
                },
                new Review {
                    Member_id = 2,
                    Book_id = 1,
                    comment = "Knjiga mi se ne svidja.",
                    rating = 2
                }
            };
            InsertReviewIntoDatabase(reviews);

           
        }

        private void InsertEmployeeIntoDatabase(List<Employee> employees) {
            foreach (var employee in employees) {
                string sqlInsertEmployee = $"INSERT [dbo].[Employee] ([name], [surname], [username], [password], [OIB], [Library_id]) VALUES ('{employee.name}', '{employee.surname}', '{employee.username}', '{employee.password}', '{employee.OIB}', {employee.Library_id});";
                Helper.ExecuteCustomSql(sqlInsertEmployee);
            }
        }

        private void InsertBorrowIntoDatabase(List<Borrow> borrows) {
            foreach (var borrow in borrows) {
                string sqlInsertBorrow = $"INSERT [dbo].[Borrow] ([Book_id], [Member_id], [borrow_status], [borrow_date], [return_date], [Employee_borrow_id], [Employee_return_id]) VALUES ({borrow.Book_id}, {borrow.Member_id}, {borrow.borrow_status}, GETDATE(), GETDATE(), {borrow.Employee_borrow_id}, NULL);";
                Helper.ExecuteCustomSql(sqlInsertBorrow);
            }
        }

        private void InsertReviewIntoDatabase(List<Review> reviews) {
            foreach (var review in reviews) {
                string sqlInsertReview = $"INSERT INTO [dbo].[Review] ([Member_id], [Book_id], [comment], [rating], [date]) VALUES ({review.Member_id}, {review.Book_id}, '{review.comment}', {review.rating}, GETDATE());";
                Helper.ExecuteCustomSql(sqlInsertReview);
            }
        }

        private void InsertBookIntoDatabase(List<Book> books) {
            foreach (var book in books) {
                string sqlInsertBook = $"INSERT [dbo].[Book] ([name], [description], [publish_date], [pages_num], [digital], [url_photo], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) VALUES ('{book.name}', '{book.description}', GETDATE(), {book.pages_num}, '{book.digital}', '{book.url_photo}', '{book.barcode_id}', {book.total_copies}, {book.current_copies}, {book.Genre_id}, {book.Library_id});";
                Helper.ExecuteCustomSql(sqlInsertBook);
            }
        }

        private void InsertAuthorIntoDatabase(List<Author> authors) {
            foreach (var author in authors) {
                string sqlInsertAuthor = $"INSERT [dbo].[Author] ([idAuthor], [name], [surname], [birth_date]) VALUES ('{author.idAuthor}', '{author.name}', '{author.surname}', GETDATE());";
                Helper.ExecuteCustomSql(sqlInsertAuthor);
            }
        }

        private void InsertGenreIntoDatabase(List<Genre> genres) {
            foreach (var genre in genres) {
                string sqlInsertGenre = $"INSERT [dbo].[Genre] ([name]) VALUES ('{genre.name}');";
                Helper.ExecuteCustomSql(sqlInsertGenre);
            }
        }

        private void InsertMemberIntoDatabase(List<Member> members) {
            foreach (var member in members) {
                string InsertMember = $"INSERT INTO [dbo].[Member] ([name], [surname], [username], [password], [OIB], [membership_date], [barcode_id], [Library_id]) VALUES ('{member.name}', '{member.surname}', '{member.username}', '{member.password}', '{member.OIB}', GETDATE(), '{member.barcode_id}', {member.Library_id});";
                Helper.ExecuteCustomSql(InsertMember);
            }
        }

        private void InsertLibraryIntoDatabase() {
            string createLibrary =
            "INSERT [dbo].[Library] ([id], [name], [OIB], [phone], [email], [price_day_late], [address], [membership_duration]) " +
            "VALUES (1, N'Knjiznica', 123, 331, N'email', 3, N'adresa', GETDATE())";
            Helper.ExecuteCustomSql(createLibrary);
        }

        [Fact]
        public void MostPopularBooks_WithBorrowedBooks_ShouldShowBooksInDgv() {
            // Arrange
            int libraryId = 1;

            var expectedBooks = new List<MostPopularBooks>
            {
                 new MostPopularBooks { Book_Name = "Hamlet", Author_Name = "William Shakespare", Times_Borrowed = 2 },
                 new MostPopularBooks { Book_Name = "Romeo i Julija", Author_Name = "Cecilije Borovski", Times_Borrowed = 1 }
            };

            // Act
            var actualBooks = statisticsService.GetMostPopularBooks(libraryId);

           //  Assert
            Assert.Equal(expectedBooks.Count, actualBooks.Count);

            for (int i = 0; i < expectedBooks.Count; i++) {
                Assert.Equal(expectedBooks[i].Book_Name, actualBooks[i].Book_Name);
                Assert.Equal(expectedBooks[i].Times_Borrowed, actualBooks[i].Times_Borrowed);
            }
        }

        [Fact]
        public void MostPopularBooks_NoBorrowedBooks_ShouldReturnEmptyList() {
            // Arrange
            int libraryId = 1;

            string sqlDeleteBorrows = "DELETE FROM [dbo].[Borrow]";
            Helper.ExecuteCustomSql(sqlDeleteBorrows);

            var expectedBooks = new List<MostPopularBooks>
            {
                new MostPopularBooks { Book_Name = "Hamlet", Author_Name = null, Times_Borrowed = 0 },
                new MostPopularBooks { Book_Name = "Romeo i Julija", Author_Name = null, Times_Borrowed = 0 }
            };

            // Act
            var actualBooks = statisticsService.GetMostPopularBooks(libraryId);

            // Assert
            Assert.Equal(expectedBooks.Count, actualBooks.Count);

            for (int i = 0; i < expectedBooks.Count; i++) {
                Assert.Equal(expectedBooks[i].Book_Name, actualBooks[i].Book_Name);
                Assert.Equal(expectedBooks[i].Times_Borrowed, actualBooks[i].Times_Borrowed);
            }
        }

        [Fact]
        public void GetReviewCount_WithThreeReviews_ShouldReturnCorrectStatistics() {
            // Arrange
            int libraryId = 1;

            var expectedReviewStatistics = new List<ReviewStatistics>
            {
                new ReviewStatistics { Grade = "5", Number_Count = 2 },
                new ReviewStatistics { Grade = "2", Number_Count = 1 }
    };

            // Act
            var actualReviewStatistics = statisticsService.GetReviewCount(libraryId);

            // Assert
            Assert.Equal(expectedReviewStatistics.Count, actualReviewStatistics.Count);

            foreach (var expected in expectedReviewStatistics) {
                var actual = actualReviewStatistics.FirstOrDefault(r => r.Grade == expected.Grade);
                Assert.Equal(expected.Grade, actual.Grade);
                Assert.Equal(expected.Number_Count, actual.Number_Count);
            }
        }

        [Fact]
        public void GetReviewCount_NoReviews_ShouldReturnEmptyList() {
            // Arrange
            int libraryId = 1;

            string sqlDeleteReviews = "DELETE FROM [dbo].[Review]";
            Helper.ExecuteCustomSql(sqlDeleteReviews);

            var expectedReviewStatistics = new List<ReviewStatistics>();

            // Act
            var actualReviewStatistics = statisticsService.GetReviewCount(libraryId);

            // Assert
            Assert.Equal(expectedReviewStatistics.Count, actualReviewStatistics.Count);
        }

        [Fact]
        public void GetMostPopularGenres_WithBooks_ShouldReturnCorrectStatistics() {
            // Arrange
            int libraryId = 1;

            var expectedGenreStatistics = new List<MostPopularGenres>
            {
                new MostPopularGenres { Genre_name = "Tragedija", Times_Borrowed = 2 },
                new MostPopularGenres { Genre_name = "Drama", Times_Borrowed = 1 }
    };

            // Act
            var actualGenreStatistics = statisticsService.GetMostPopularGenres(libraryId);

            // Assert
            Assert.Equal(expectedGenreStatistics.Count, actualGenreStatistics.Count);

            for (int i = 0; i < expectedGenreStatistics.Count; i++) {
                Assert.Equal(expectedGenreStatistics[i].Genre_name, actualGenreStatistics[i].Genre_name);
                Assert.Equal(expectedGenreStatistics[i].Times_Borrowed, actualGenreStatistics[i].Times_Borrowed);
            }
        }

        [Fact]
        public void GetMostPopularGenres_NoGenres_ShouldReturnEmptyList() {
            // Arrange
            int libraryId = 1;

            string sqlDeleteGenres = "DELETE FROM [dbo].[Genre]";
            Helper.ExecuteCustomSql(sqlDeleteGenres);

            var expectedGenreStatistics = new List<MostPopularGenres>();

            // Act
            var actualGenreStatistics = statisticsService.GetMostPopularGenres(libraryId);

            // Assert
            Assert.Equal(expectedGenreStatistics.Count, actualGenreStatistics.Count);
        }

    }
}