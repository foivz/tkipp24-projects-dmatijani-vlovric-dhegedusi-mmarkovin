using BussinessLogicLayer.services;
using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace IntegrationTesting.Nove_funkcionalnosti.F15 {
    [Collection("Database collection")]
    public class BookService_integrationTest {
        readonly BookServices _bookService;
        readonly DatabaseFixture _fixture;

        public BookService_integrationTest(DatabaseFixture fixture) {
            _bookService = new BookServices();
            this._fixture = fixture;
            this._fixture.ResetDatabase();
        }

        [Fact]
        public void GetTopBorrowedBooks_ReturnsCorrectBooks() {
            // Arrange
            InsertData();

            var expectedBooks = new List<MostPopularBooksViewModel>
            {
                new MostPopularBooksViewModel { Book_Name = "Hamlet", Author_Name = "William Shakespare", Times_Borrowed = 2, Url_Photo = "slika1", Order_Number = 1 },
                new MostPopularBooksViewModel { Book_Name = "Ilijada", Author_Name = "Homer Borovski", Times_Borrowed = 1, Url_Photo = "slika2", Order_Number = 2 }
            };

            // Act
            var result = _bookService.GetTopBorrowedBooks(1);

            // Assert
            result.Should().BeEquivalentTo(expectedBooks);
        }

        private void InsertData() {
            string sql =
                "INSERT [dbo].[Library] ([id], [name], [OIB], [phone], [email], [price_day_late], [address], [membership_duration]) VALUES (1, N'Knjiznica', 123, 331, N'email', 3, N'adresa', GETDATE());" +
                "INSERT [dbo].[Member] ([name], [surname], [username], [password], [OIB], [membership_date], [barcode_id], [Library_id]) VALUES (N'Ivo', N'Ivic', N'iivic', N'ivo123', '57647557445', GETDATE(), N'B001', 1);" +
                "INSERT [dbo].[Member] ([name], [surname], [username], [password], [OIB], [membership_date], [barcode_id], [Library_id]) VALUES (N'Ana', N'Anic', N'aanic', N'ana123', '64363434343', GETDATE(), N'B002', 1);" +
                "INSERT [dbo].[Genre] ([name]) VALUES (N'Tragedija');" +
                "INSERT [dbo].[Genre] ([name]) VALUES (N'Drama');" +
                "INSERT [dbo].[Author] ([idAuthor], [name], [surname], [birth_date]) VALUES (1, N'William', N'Shakespare', GETDATE());" +
                "INSERT [dbo].[Author] ([idAuthor], [name], [surname], [birth_date]) VALUES (2, N'Homer', N'Borovski', GETDATE());" +
                "INSERT [dbo].[Book] ([name], [description], [publish_date], [pages_num], [digital], [url_photo], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) VALUES (N'Hamlet', N'Nema opisa', GETDATE(), 300, 1, N'slika1', N'BC0011', 5, 3, 1, 1);" +
                "INSERT [dbo].[Book] ([name], [description], [publish_date], [pages_num], [digital], [url_photo], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) VALUES (N'Ilijada', N'Description 2', GETDATE(), 400, 0, N'slika2', N'BC0022', 10, 7, 2, 1);" +
                "INSERT [dbo].[Employee] ([name], [surname], [username], [password], [OIB], [Library_id]) VALUES (N'Darko', N'Daric', N'ddaric', N'jakalozinka', '11892593283', 1);" +
                "INSERT [dbo].[Employee] ([name], [surname], [username], [password], [OIB], [Library_id]) VALUES (N'Marina', N'Misic', N'mmisic', N'mypw', '85738923405', 1);" +
                "INSERT INTO [dbo].[Book_Author] ([Book_id], [Author_idAuthor]) VALUES (1, 1);" +
                "INSERT INTO [dbo].[Book_Author] ([Book_id], [Author_idAuthor]) VALUES (2, 2);" +
                "INSERT [dbo].[Borrow] ([Book_id], [Member_id], [borrow_status], [borrow_date], [return_date], [Employee_borrow_id], [Employee_return_id]) VALUES (1, 1, 0, GETDATE(), GETDATE(), 1, 1);" +
                "INSERT [dbo].[Borrow] ([Book_id], [Member_id], [borrow_status], [borrow_date], [return_date], [Employee_borrow_id], [Employee_return_id]) VALUES (2, 1, 0, GETDATE(), GETDATE(), 1, 1);" +
                "INSERT [dbo].[Borrow] ([Book_id], [Member_id], [borrow_status], [borrow_date], [return_date], [Employee_borrow_id], [Employee_return_id]) VALUES (1, 2, 0, GETDATE(), GETDATE(), 1, 1);";
            Helper.ExecuteCustomSql(sql);
        }
    }
}
