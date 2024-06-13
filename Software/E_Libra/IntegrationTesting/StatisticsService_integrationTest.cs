using BussinessLogicLayer.services;
using EntitiesLayer;
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


        public StatisticsService_integrationTest(DatabaseFixture fixture)
        {
            statisticsService = new StatisticsService();
            this.fixture = fixture;
            this.fixture.ResetDatabase();


            var library = new Library{
                id = 123,
                name = "Testna knjiznica",
                OIB = "11112222333",
                price_day_late = 3.5m,
                membership_duration = new DateTime(2024, 6, 23)
            };
            InsertLibraryIntoDatabase(library);


            var members = new List<Member>
            {
                new Member { 
                    id = 1, 
                    name = "Ivo", 
                    surname = "Ivic", 
                    username = "iivic", 
                    password = "ivo123", 
                    OIB = "57647557445", 
                    membership_date = new DateTime(2024, 28, 8), 
                    barcode_id = "B001", 
                    Library_id = library.id 
                },
                new Member { 
                    id = 2, 
                    name = "Ana", 
                    surname = "Anic", 
                    username = "aanic", 
                    password = "ana123", 
                    OIB = "64363434343", 
                    membership_date = new DateTime(2024, 23, 6), 
                    barcode_id = "B002", 
                    Library_id = library.id 
                }
            };
            InsertMemberIntoDatabase(members);


            var genres = new List<Genre>
            {
                new Genre { 
                    id = 1, 
                    name = "Tragedija" 
                },
                new Genre { 
                    id = 2, 
                    name = "Drama" 
                }
            };
            InsertGenreIntoDatabase(genres);


            var authors = new List<Author>
            {
                new Author { 
                    idAuthor = 1, 
                    name = "William", 
                    surname = "Shakespare", 
                    birth_date = new DateTime(1570, 11, 6) },
                new Author { 
                    idAuthor = 2, 
                    name = "Cecilije", 
                    surname = "Borovski", 
                    birth_date = new DateTime(1980, 3, 5) }
            };
            InsertAuthorIntoDatabase(authors);


            var books = new List<Book>
           {
                new Book { 
                    id = 1, 
                    name = "Hamlet", 
                    description = "Nema opisa", 
                    publish_date = new DateTime(1620, 24, 8), 
                    pages_num = 300, 
                    digital = 1, 
                    url_photo = "slika1", 
                    barcode_id = "BC0011", 
                    total_copies = 5, 
                    current_copies = 3, 
                    Genre_id = 1, 
                    Library_id = library.id },
                new Book { id = 2, 
                    name = "Romeo i Julija", 
                    description = "Description 2", 
                    publish_date = new DateTime(1600, 2, 9), 
                    pages_num = 400, 
                    digital = 0, 
                    url_photo = "slika2", 
                    barcode_id = "BC0022", 
                    total_copies = 10, 
                    current_copies = 7, 
                    Genre_id = 2, 
                    Library_id = library.id }
            };
            InsertBookIntoDatabse(books);
        }

        private void InsertBookIntoDatabse(List<Book> books) {
            foreach (var book in books) {
                string sqlInsertBook = $"INSERT [dbo].[Book] ([id], [name], [description], [publish_date], [pages_num], [digital], [photo], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) VALUES ('{book.id}', '{book.name}', '{book.description}', '{book.publish_date}', {book.pages_num}, '{book.digital}', '{book.url_photo}', '{book.barcode_id}', {book.total_copies}, {book.current_copies}, {book.Genre_id}, {book.Library_id});";
                Helper.ExecuteCustomSql(sqlInsertBook);
            }
        }

        private void InsertAuthorIntoDatabase(List<Author> authors) {
            foreach (var author in authors) {
                string sqlInsertAuthor = $"INSERT [dbo].[Author] ([id], [name], [surname], [birth_date]) VALUES ('{author.idAuthor}', '{author.name}', '{author.surname}', '{author.birth_date}');";
                Helper.ExecuteCustomSql(sqlInsertAuthor);
            }
        }

        private void InsertGenreIntoDatabase(List<Genre> genres) {
            foreach (var genre in genres) {
                string sqlInsertGenre = $"INSERT [dbo].[Genre] ([id], [name]) VALUES ('{genre.id}', '{genre.name}');";
                Helper.ExecuteCustomSql(sqlInsertGenre);
            }
        }

        private void InsertMemberIntoDatabase(List<Member> members) {
            foreach (var member in members) {
                string InsertMember = $"INSERT INTO [dbo].[Member] ([id], [name], [surname], [username], [password], [OIB], [membership_date], [barcode_id], [Library_id]) VALUES ({member.id}, '{member.name}', '{member.surname}', '{member.username}', '{member.password}', '{member.OIB}', '{member.membership_date}', '{member.barcode_id}', {member.Library_id});";
                Helper.ExecuteCustomSql(InsertMember);
            }
        }


        private void InsertLibraryIntoDatabase(Library library) {
            string sqlInsertLibrary = $"INSERT [dbo].[Library] ([id], [name], [OIB], [price_day_late], [membership_duration]) VALUES ('{library.id}', '{library.name}', '{library.OIB}', {library.price_day_late}, '{library.membership_duration}');";
            Helper.ExecuteCustomSql(sqlInsertLibrary);
        }


    }
}
