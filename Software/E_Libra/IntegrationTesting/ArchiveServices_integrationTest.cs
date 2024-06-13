using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DataAccessLayer.Repositories;
using BussinessLogicLayer.services;

namespace IntegrationTesting
{
    [Collection("Database collection")]
    public class ArchiveServices_integrationTest
    {
        readonly ArchiveServices service;
        readonly DatabaseFixture fixture;

        readonly string createLibrary =
            "INSERT [dbo].[Library] ([id], [name], [OIB], [phone], [email], [price_day_late], [address], [membership_duration]) " +
            "VALUES (1, N'Knjiznica', 12345, 331, N'email', 3, N'adresa', GETDATE())";

        readonly string createBooks =
            "INSERT [dbo].[Book] ([name], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id],  [Library_id]) " +
            "VALUES (N'Book1', 10, 0, 12345, 10, 10, 1, 1); " +
            "INSERT [dbo].[Book] ([name], [pages_num], [digital], [barcode_id], [total_copies], [current_copies], [Genre_id],  [Library_id]) " +
            "VALUES (N'Book2', 10, 0, 12346, 10, 10, 1, 1); ";
       
        readonly string createEmployees =
             "INSERT [dbo].[Employee] ([name], [surname], [username], [password], [OIB], [Library_id]) " +
            "VALUES (N'ime1', N'prezime1', N'Employee1', N'123', N'1234', 1) " +
             "INSERT [dbo].[Employee] ([name], [surname], [username], [password], [OIB], [Library_id]) " +
            "VALUES (N'ime2', N'prezime2', N'Employee2', N'123', N'1235', 1) ";

        readonly string createGenres =
            "INSERT [dbo].[Genre] ([name]) VALUES (N'zanr')";
        
        readonly string createArchives =
            "INSERT [dbo].[Archive] ([Book_id], [Employee_id], [arhive_date]) " +
            "VALUES (1, 1, GETDATE()); " +
            "INSERT [dbo].[Archive] ([Book_id], [Employee_id], [arhive_date]) " +
            "VALUES (2, 2, GETDATE()); ";

        readonly List<ArchivedBookInfo> archivedBookInfos = new List<ArchivedBookInfo>
            {
                new ArchivedBookInfo { BookName = "Book1", EmployeeName = "ime1 prezime1", ArchiveDate = DateTime.Now.Date },
                new ArchivedBookInfo { BookName = "Book2", EmployeeName = "ime2 prezime2", ArchiveDate = DateTime.Now.Date },
            };

        public ArchiveServices_integrationTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
            this.fixture.ResetDatabase();
            
            Helper.ExecuteCustomSql(createLibrary);
            Helper.ExecuteCustomSql(createGenres);
            Helper.ExecuteCustomSql(createBooks);
            Helper.ExecuteCustomSql(createEmployees);
            Helper.ExecuteCustomSql(createArchives);
            service = new ArchiveServices();

        }

        //Viktor Lovrić
        [Fact]
        public void GetArchive_GivenFunctionIsCalled_ReturnsArchiveList()
        {
            //Arrange
            
            //Act
            var result = service.GetArchive();


            //Assert
            result.Should().BeEquivalentTo(archivedBookInfos);
        }

        //Viktor Lovrić
        [Fact]
        public void GetArchivesForEmployee_GivenFunctionIsCalled_ReturnsArchiveList()
        {
            //Arrange

            //Act
            var result = service.GetArchivesForEmployee(1);

            //Assert
            result.Should().HaveCount(1);
        }
    }
}
