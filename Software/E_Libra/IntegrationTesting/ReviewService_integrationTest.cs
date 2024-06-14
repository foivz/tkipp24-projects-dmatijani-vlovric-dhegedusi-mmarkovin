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
    public class ReviewService_integrationTest {

        readonly ReviewService reviewService;
        readonly DatabaseFixture fixture;
        public ReviewService_integrationTest(DatabaseFixture fixture)
        {
            reviewService = new ReviewService();
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
        public void GetReviewsForBook_WithReviews_ShouldReturnCorrectReviews() {
            // Arrange
            int bookId = 1;

            var expectedReviews = new List<ReviewInfo>
            {
                new ReviewInfo { Member_Name = "Ivo Ivic", Rating = 5, Comment = "Odlicna knjiga!", Date = DateTime.Now },
                new ReviewInfo { Member_Name = "Ana Anic", Rating = 2, Comment = "Knjiga mi se ne svidja.", Date = DateTime.Now }
            };

            // Act
            var actualReviews = reviewService.GetReviewsForBook(bookId);

            // Assert
            Assert.Equal(expectedReviews.Count, actualReviews.Count);

            for (int i = 0; i < expectedReviews.Count; i++) {
                Assert.Equal(expectedReviews[i].Member_Name, actualReviews[i].Member_Name);
                Assert.Equal(expectedReviews[i].Rating, actualReviews[i].Rating);
                Assert.Equal(expectedReviews[i].Comment, actualReviews[i].Comment);
            }
        }

        [Fact]
        public void AddReview_ShouldAddReview() {
            // Arrange
            var newReview = new Review {
                Member_id = 2,
                Book_id = 2,
                comment = "Knjiga nije losa",
                rating = 4,
                date = DateTime.Now
            };

            // Act
            var reviewId = reviewService.AddReview(newReview);

            // Assert
            var addedReview = reviewService.GetReviewsForBook(2).FirstOrDefault(r => r.Comment == newReview.comment && r.Rating == newReview.rating);
            Assert.NotNull(addedReview);
            Assert.Equal("Ana Anic", addedReview.Member_Name);
            Assert.Equal(newReview.rating, addedReview.Rating);
            Assert.Equal(newReview.comment, addedReview.Comment);
            Assert.Equal(newReview.date.Date, addedReview.Date.Date);
        }

        [Fact]
        public void DeleteReview_ShouldDeleteReview() {
            // Arrange
            int reviewId = 1;
            int bookId = 1;

            // Act
            var result = reviewService.DeleteReview(reviewId, bookId);

            // Assert
            var deletedReview = reviewService.GetReviewsForBook(bookId).FirstOrDefault(r => r.Comment == "Odlicna knjiga!");
            Assert.Null(deletedReview);
            Assert.Equal(0, result);
        }

        [Fact]
        public void HasUserReviewedBook_WithExistingReview_ShouldReturnTrue() {
            // Arrange
            int memberId = 2;
            int bookId = 1;

            // Act
            var hasReviewed = reviewService.HasUserReviewedBook(memberId, bookId);

            // Assert
            Assert.True(hasReviewed);
        }

        [Fact]
        public void HasUserReviewedBook_WithNoReview_ShouldReturnFalse() {
            // Arrange
            int memberId = 1;
            int bookId = 1;

            // Act
            var hasReviewed = reviewService.HasUserReviewedBook(memberId, bookId);

            // Assert
            Assert.False(hasReviewed);
        }




    }
}
