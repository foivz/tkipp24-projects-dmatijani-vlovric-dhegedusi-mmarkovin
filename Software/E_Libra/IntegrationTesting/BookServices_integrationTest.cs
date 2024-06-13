using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using BussinessLogicLayer.services;
using EntitiesLayer;
using static DataAccessLayer.Repositories.BookRepository;

namespace IntegrationTesting
{
    [Collection("Database collection")]
    public class BookServices_integrationTest
    {
        readonly BookServices bookServices;
        readonly DatabaseFixture fixture;

        readonly string createLibrary =
             "INSERT [dbo].[Library] ([id], [name], [OIB], [phone], [email], [price_day_late], [address], [membership_duration]) " +
             "VALUES (1, N'Knjiznica', 12345, 331, N'email', 3, N'adresa', GETDATE())";

        readonly string createGenre =
            "INSERT [dbo].[Genre] ([name]) VALUES (N'zanr')";

        readonly Genre genre = new Genre { id = 1, name = "zanr" };

        readonly string createEmployee =
             "INSERT [dbo].[Employee] ([name], [surname], [username], [password], [OIB], [Library_id]) " +
            "VALUES (N'ime1', N'prezime1', N'Employee1', N'123', N'1234', 1);";

        private Author CreateAuthor()
        {
            Author author = new Author
            {
                idAuthor = 1,
                name = "AuthorName1",
                surname = "AuthorSurname1",
                birth_date = DateTime.Now.Date,
            };
            var formattedBirthDate = author.birth_date.HasValue ? author.birth_date.Value.ToString("yyyy-MM-dd") : "NULL";
            string sqlAuthor = $"INSERT INTO [dbo].[Author] ([idAuthor], [name], [surname], [birth_date]) " +
                   $"VALUES ({author.idAuthor}, '{author.name}', '{author.surname}', '{formattedBirthDate}');";

            Helper.ExecuteCustomSql(sqlAuthor);

            return author;
        }

        private Book ReturnSingleBook()
        {
            Book book = new Book
            {
                name = "BookName1",
                description = null,
                publish_date = null,
                pages_num = 10,
                digital = 0,
                url_digital = null,
                url_photo = null,
                total_copies = 10,
                current_copies = 10,
                Genre = genre,
                Library_id = 1
            };

            return book;
        }

        private List<Book> CreateTwoBooks()
        {
            List<Book> books = new List<Book>
            {
                new Book
                {
                    id = 1,
                    name = "BookName1",
                    description = null,
                    publish_date = null,
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12345",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = genre,
                    Library_id = 1,
                    Genre_id = genre.id
                },
                new Book
                {
                    id = 2,
                    name = "BookName2",
                    description = null,
                    publish_date = null,
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12346",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = genre,
                    Library_id = 1,
                    Genre_id = genre.id
                }
            };

            string sqlBook1 = $"INSERT INTO [dbo].[Book] ([name], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) " +
                   $"VALUES ('{books[0].name}', '{books[0].pages_num}', '{books[0].digital}', '{books[0].barcode_id}', '{books[0].total_copies}', '{books[0].current_copies}', '{books[0].Genre.id}', '{books[0].Library_id}');";
            string sqlBook2 = $"INSERT INTO [dbo].[Book] ([name], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) " +
                   $"VALUES ('{books[1].name}', '{books[1].pages_num}', '{books[1].digital}', '{books[1].barcode_id}', '{books[1].total_copies}', '{books[1].current_copies}', '{books[1].Genre.id}', '{books[1].Library_id}');";

            Helper.ExecuteCustomSql(sqlBook1);
            Helper.ExecuteCustomSql(sqlBook2);

            return books;
        }

        private List<Book> CreateThreeBooks()
        {
            List<Book> books = new List<Book>
            {
                new Book
                {
                    id = 1,
                    name = "BookName1",
                    description = null,
                    publish_date = null,
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12345",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = genre,
                    Library_id = 1,
                    Genre_id = genre.id
                },
                new Book
                {
                    id = 2,
                    name = "BookName2",
                    description = null,
                    publish_date = null,
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12346",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = genre,
                    Library_id = 1,
                    Genre_id = genre.id
                },
                new Book
                {
                    id = 3,
                    name = "BookName3",
                    description = null,
                    publish_date = null,
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12346",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = genre,
                    Library_id = 1,
                    Genre_id = genre.id
                }
            };

            string sqlBook1 = $"INSERT INTO [dbo].[Book] ([name], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) " +
                   $"VALUES ('{books[0].name}', '{books[0].pages_num}', '{books[0].digital}', '{books[0].barcode_id}', '{books[0].total_copies}', '{books[0].current_copies}', '{books[0].Genre.id}', '{books[0].Library_id}');";
            string sqlBook2 = $"INSERT INTO [dbo].[Book] ([name], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) " +
                   $"VALUES ('{books[1].name}', '{books[1].pages_num}', '{books[1].digital}', '{books[1].barcode_id}', '{books[1].total_copies}', '{books[1].current_copies}', '{books[1].Genre.id}', '{books[1].Library_id}');";
            string sqlBook3 = $"INSERT INTO [dbo].[Book] ([name], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) " +
                   $"VALUES ('{books[2].name}', '{books[2].pages_num}', '{books[2].digital}', '{books[2].barcode_id}', '{books[2].total_copies}', '{books[2].current_copies}', '{books[2].Genre.id}', '{books[2].Library_id}');";

            Helper.ExecuteCustomSql(sqlBook1);
            Helper.ExecuteCustomSql(sqlBook2);
            Helper.ExecuteCustomSql(sqlBook3);

            return books;
        }

        private Book CreateSingleBook()
        {
            Book book = new Book
            {
                id = 1,
                name = "BookName1",
                description = null,
                publish_date = null,
                pages_num = 10,
                digital = 0,
                url_digital = null,
                barcode_id = "12345",
                url_photo = null,
                total_copies = 10,
                current_copies = 10,
                Genre = genre,
                Library_id = 1,
                Genre_id = genre.id
            };

            string sqlBook = $"INSERT INTO [dbo].[Book] ([name], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) " +
                   $"VALUES ('{book.name}', '{book.pages_num}', '{book.digital}', '{book.barcode_id}', '{book.total_copies}', '{book.current_copies}', '{book.Genre.id}', '{book.Library_id}');";

            Helper.ExecuteCustomSql(sqlBook);

            return book;
        }

        private List<BookViewModel> CreateSearchableBooks()
        {
            string createNewGenre = "INSERT [dbo].[Genre] ([name]) VALUES (N'drugi')";
            Genre newGenre = new Genre { id = 2, name = "drugi" };

            Helper.ExecuteCustomSql(createNewGenre);

            List<Book> books = new List<Book>
            {
                new Book
                {
                    id = 1,
                    name = "Prva",
                    description = null,
                    publish_date = DateTime.ParseExact("01-01-1995", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12341",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = genre,
                    Library_id = 1,
                    Genre_id = genre.id
                },
                new Book
                {
                    id = 2,
                    name = "DrugaPrva",
                    description = null,
                    publish_date = DateTime.ParseExact("01-01-1960", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12342",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = genre,
                    Library_id = 1,
                    Genre_id = genre.id
                },
                new Book
                {
                    id = 3,
                    name = "Treca",
                    description = null,
                    publish_date = DateTime.ParseExact("01-01-2010", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12343",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = genre,
                    Library_id = 1,
                    Genre_id = genre.id
                },
                new Book
                {
                    id = 4,
                    name = "Cetvrta",
                    description = null,
                    publish_date = DateTime.ParseExact("01-01-2015", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12342",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = newGenre,
                    Library_id = 1,
                    Genre_id = newGenre.id
                },
                new Book
                {
                    id = 5,
                    name = "Peta",
                    description = null,
                    publish_date = DateTime.ParseExact("01-01-2001", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12342",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = newGenre,
                    Library_id = 1,
                    Genre_id = newGenre.id
                }
            };
            foreach(var book in books)
            {
                string sqlBook = $"INSERT INTO [dbo].[Book] ([name], [publish_date], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) " +
                   $"VALUES ('{book.name}', '{book.publish_date:yyyy-MM-dd}', '{book.pages_num}', '{book.digital}', '{book.barcode_id}', '{book.total_copies}', '{book.current_copies}', '{book.Genre.id}', '{book.Library_id}');";
                Helper.ExecuteCustomSql(sqlBook);
            }
            List<Author> authors = new List<Author>
            {
                new Author { idAuthor = 1, name = "Author1", surname = "Surname1", birth_date = DateTime.Now.Date },
                new Author { idAuthor = 2, name = "Author2", surname = "Surname2", birth_date = DateTime.Now.Date }
            };
            
            foreach(var author in authors)
            {
                var formattedBirthDate = author.birth_date.HasValue ? author.birth_date.Value.ToString("yyyy-MM-dd") : "NULL";
                Helper.ExecuteCustomSql($"INSERT INTO dbo.Author (idAuthor, name, surname, birth_date) VALUES ({author.idAuthor}, '{author.name}', '{author.surname}', '{formattedBirthDate}')");
            }

            List<string> strings = new List<string>
            {
                "INSERT INTO [dbo].[Book_Author] ([Author_idAuthor], [Book_id]) VALUES (1, 1);",
                "INSERT INTO [dbo].[Book_Author] ([Author_idAuthor], [Book_id]) VALUES (1, 2);",
                "INSERT INTO [dbo].[Book_Author] ([Author_idAuthor], [Book_id]) VALUES (2, 3);",
                "INSERT INTO [dbo].[Book_Author] ([Author_idAuthor], [Book_id]) VALUES (2, 4);",
                "INSERT INTO [dbo].[Book_Author] ([Author_idAuthor], [Book_id]) VALUES (2, 5);"
            };

            foreach(var str in strings){
                Helper.ExecuteCustomSql(str);
            }


            List<BookViewModel> bookViewModels = new List<BookViewModel>
            {
                new BookViewModel
                {
                    Id = 1,
                    Name = "Prva",
                    PublishDate = new DateTime(1995, 1, 1),
                    PublishDateDisplay = "1995-01-01",
                    AuthorName = authors[0].name + " " + authors[0].surname,
                    GenreName = genre.name,
                    Digital = "Ne",
                },
                new BookViewModel
                {
                    Id = 2,
                    Name = "DrugaPrva",
                    PublishDate = new DateTime(1960, 1, 1),
                    PublishDateDisplay = "1960-01-01",
                    AuthorName = authors[0].name + " " + authors[0].surname,
                    GenreName = genre.name,
                    Digital = "Ne",
                },
                new BookViewModel
                {
                    Id = 3,
                    Name = "Treca",
                    PublishDate = new DateTime(2010, 1, 1),
                    PublishDateDisplay = "2010-01-01",
                    AuthorName = authors[1].name + " " + authors[1].surname,
                    GenreName = genre.name,
                    Digital = "Ne",
                },
                new BookViewModel
                {
                    Id = 4,
                    Name = "Cetvrta",
                    PublishDate = new DateTime(2015, 1, 1),
                    PublishDateDisplay = "2015-01-01",
                    AuthorName = authors[1].name + " " + authors[1].surname,
                    GenreName = newGenre.name,
                    Digital = "Ne",
                },
                new BookViewModel
                {
                    Id = 5,
                    Name = "Peta",
                    PublishDate = new DateTime(2001, 1, 1),
                    PublishDateDisplay = "2001-01-01",
                    AuthorName = authors[1].name + " " + authors[1].surname,
                    GenreName = newGenre.name,
                    Digital = "Ne",
                },

            };

            return bookViewModels;

        }


        public BookServices_integrationTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
            this.fixture.ResetDatabase();
            bookServices = new BookServices();

            Helper.ExecuteCustomSql(createLibrary);
            Helper.ExecuteCustomSql(createGenre);
            Helper.ExecuteCustomSql(createEmployee);

            LoggedUser.LibraryId = 1;
        }
        //Viktor Lovrić
        [Fact]
        public void AddBook_GivenBookAndAuthor_ReturnsTrue()
        {
            //Arrange
            var author = CreateAuthor();
            var book = ReturnSingleBook();
            
            //Act
            var result = bookServices.AddBook(book, author);

            //Assert
            result.Should().BeTrue();
        }

        //Viktor Lovrić
        [Fact]
        public void GetAllBooks_GivenFunctionIsCalled_ReturnsListOfBooks()
        {
            //Arrange
            var books = CreateTwoBooks();

            //Act
            var result = bookServices.GetAllBooks();

            //Assert
            result.Should().BeEquivalentTo(books, options => options
            .Excluding(e => e.Library)
            .Excluding(e => e.Genre.Books)
            );
        }

        //Viktor Lovrić
        [Fact]
        public void GetNonArchivedBooks_GivenFunctionIsCalled_ReturnsListOfBooks()
        {
            //Arrange
            //LoggedUser.LibraryId = 1;
            var books = CreateThreeBooks();
            

            string sqlArchive = "INSERT [dbo].[Archive] ([Book_id], [Employee_id], [arhive_date]) " +
            "VALUES (2, 1, GETDATE());";
            Helper.ExecuteCustomSql(sqlArchive);

            List<Book> expectedBooks = new List<Book>
            {
                new Book
                {
                    id = 1,
                    name = "BookName1",
                    description = null,
                    publish_date = null,
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12345",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = genre,
                    Library_id = 1,
                    Genre_id = genre.id
                },
                new Book
                {
                    id = 3,
                    name = "BookName3",
                    description = null,
                    publish_date = null,
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12346",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = genre,
                    Library_id = 1,
                    Genre_id = genre.id
                }
            };

            //Act
            var result = bookServices.GetNonArchivedBooks(false);

            //Assert
            result.Should().BeEquivalentTo(expectedBooks, options => options
            .Excluding(e => e.Library)
            .Excluding(e => e.Genre.Books)
            );
        }

        //Viktor Lovrić
        [Fact]
        public void InsertOneCopy_GivenBook_ReturnsTrue()
        {
            //Arrange
            var book = CreateSingleBook();

            //Act
            var result = bookServices.InsertOneCopy(book);

            //Assert
            result.Should().BeTrue();
        }

        //Viktor Lovrić
        [Fact]
        public void RemoveOneCopy_GivenBook_ReturnsTrue()
        {
            //Arrange
            var book = CreateSingleBook();

            //Act
            var result = bookServices.RemoveOneCopy(book);

            //Assert
            result.Should().BeTrue();
        }

        //Viktor Lovrić
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void InsertNewCopies_GivenNumberOfCopies_AddsToCopies(int currCopies)
        {
            //Arrange
            var book = CreateSingleBook();

            //Act
            var result = bookServices.InsertNewCopies(currCopies, book);

            //Assert
            result.Should().BeTrue();
        }

        //Viktor Lovrić
        [Fact]
        public void ArchiveBook_GivenBookAndArchive_ReturnsTrue()
        {
            //Arrange
            var books = CreateTwoBooks();

            var archive = new Archive
            {
                Book_id = 1,
                Employee_id = 1,
                arhive_date = DateTime.Now.Date,
            };

            //Act
            var result = bookServices.ArchiveBook(books[0], archive);

            //Assert
            result.Should().BeTrue();
        }

        //Viktor Lovrić
        [Fact]
        public void GetNonArchivedBooksByName_GivenSearchTerm_ReturnsListOfBooks()
        {
            //Arrange
            List<Book> books = new List<Book>
            {
                new Book
                {
                    id = 1,
                    name = "Prvo",
                    description = null,
                    publish_date = null,
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12345",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = genre,
                    Library_id = 1,
                    Genre_id = genre.id
                },
                new Book
                {
                    id = 2,
                    name = "Drugo",
                    description = null,
                    publish_date = null,
                    pages_num = 10,
                    digital = 0,
                    url_digital = null,
                    barcode_id = "12346",
                    url_photo = null,
                    total_copies = 10,
                    current_copies = 10,
                    Genre = genre,
                    Library_id = 1,
                    Genre_id = genre.id
                }
            };

            string sqlBook1 = $"INSERT INTO [dbo].[Book] ([name], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) " +
                   $"VALUES ('{books[0].name}', '{books[0].pages_num}', '{books[0].digital}', '{books[0].barcode_id}', '{books[0].total_copies}', '{books[0].current_copies}', '{books[0].Genre.id}', '{books[0].Library_id}');";
            string sqlBook2 = $"INSERT INTO [dbo].[Book] ([name], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) " +
                   $"VALUES ('{books[1].name}', '{books[1].pages_num}', '{books[1].digital}', '{books[1].barcode_id}', '{books[1].total_copies}', '{books[1].current_copies}', '{books[1].Genre.id}', '{books[1].Library_id}');";

            Helper.ExecuteCustomSql(sqlBook1);
            Helper.ExecuteCustomSql(sqlBook2);

            List<Book> expectedBooks = new List<Book> { books[0] };

            //Act
            var result = bookServices.GetNonArchivedBooksByName("Prv");

            //Assert
            result.Should().BeEquivalentTo(expectedBooks, options => options
            .Excluding(e => e.Library)
            .Excluding(e => e.Genre.Books)
            );
        }

        //Viktor Lovrić
        [Fact]
        public void SearchBooks_GivenSearchTermAndDigital_ReturnsListOfBooks()
        {
            //Arrange
            var books = CreateSearchableBooks();
            List<BookViewModel> expectedBooks = new List<BookViewModel> { books[0], books[1] };

            //Act
            var result = bookServices.SearchBooks("Prv", false);

            //Assert
            result.Should().BeEquivalentTo(expectedBooks, options => options
            .Excluding(e => e.PublishDateDisplay)
            );
        }

        //Viktor Lovrić
        [Fact]
        public void GetBooksByGenre_GivenGenreNameAndDigital_ReturnsListOfBooks()
        {
            //Arrange
            var books = CreateSearchableBooks();
            List<BookViewModel> expectedBooks = new List<BookViewModel> { books[0], books[1], books[2] };

            //Act
            var result = bookServices.GetBooksByGenre("zanr", false);

            //Assert
            result.Should().BeEquivalentTo(expectedBooks, options => options
            .Excluding(e => e.PublishDateDisplay)
            );
        }

        //Viktor Lovrić
        [Fact]
        public void GetBooksByAuthor_GivenAuthorNameAndDigital_ReturnsListOfBooks()
        {
            //Arrange
            var books = CreateSearchableBooks();
            List<BookViewModel> expectedBooks = new List<BookViewModel> { books[2], books[3], books[4] };

            //Act
            var result = bookServices.GetBooksByAuthor("Author2", false);

            //Assert
            result.Should().BeEquivalentTo(expectedBooks, options => options
            .Excluding(e => e.PublishDateDisplay)
            );
        }

        //Viktor Lovrić
        [Fact]
        public void GetBooksByYear_GivenYearAndDigital_ReturnsListOfBooks()
        {
            //Arrange
            var books = CreateSearchableBooks();
            List<BookViewModel> expectedBooks = new List<BookViewModel> { books[0], books[1] };

            //Act
            var result = bookServices.GetBooksByYear(19, false);

            //Assert
            result.Should().BeEquivalentTo(expectedBooks, options => options
            .Excluding(e => e.PublishDateDisplay)
            .Excluding(e => e.Digital)
            );
        }

        //Viktor Lovrić
        [Fact]
        public void GetBookById_GivenId_ReturnsBook()
        {
            //Arrange
            var books = CreateTwoBooks();
            var expectedBook = books[1];

            //Act
            var result = bookServices.GetBookById(2);

            //Assert
            result.Should().BeEquivalentTo(expectedBook, options => options
            .Excluding(e => e.Library)
            .Excluding(e => e.Genre.Books)
            );
        }


        //Viktor Lovrić
        [Fact]
        public void GetWishlistBooksForMember_GivenFunctionIsCalled_ReturnsListOfBooks()
        {
            //Arrange
            
            //Act

            //Assert
        }

        //Viktor Lovrić
        [Fact]
        public void AddBookToWishlist_GivenBookId_ReturnsTrue()
        {
            //Arrange

            //Act

            //Assert
        }

        //Viktor Lovrić
        [Fact]
        public void RemoveBookFromWishlist_GivenBookId_ReturnsTrue()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}
