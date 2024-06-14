using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using BussinessLogicLayer.services;
using EntitiesLayer;

namespace IntegrationTesting.Nove_funkcionalnosti.F13
{
    [Collection("Database collection")]
    public class AuthorService_intTest
    {
        readonly AuthorService service;
        readonly DatabaseFixture fixture;

        public AuthorService_intTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
            this.fixture.ResetDatabase();
            service = new AuthorService();
        }


        //Viktor Lovrić
        [Fact]
        public void SearchAuthors_GivenStringIsPassed_ReturnsAuthors()
        {
            //Arrange
            string sql =
                "INSERT [dbo].[Author] ([name], [surname], [birth_date]) VALUES (N'Author1', N'Surname1', '2021-06-01') " +
                "INSERT [dbo].[Author] ([name], [surname], [birth_date]) VALUES (N'Author2', N'Surname2', '2021-06-01') " +
                "INSERT [dbo].[Author] ([name], [surname], [birth_date]) VALUES (N'Drugaciji', N'Oddrugih', '2021-06-01') ";
            Helper.ExecuteCustomSql(sql);

            var authors = new List<Author>
            {
                new Author { idAuthor = 1, name = "Drugaciji", surname = "Oddrugih", birth_date = DateTime.Parse("2021-06-01") }
            };

            //Act
            var result = service.SearchAuthors("Drug");

            //Assert
            result.Should().BeEquivalentTo(authors);
        }
    }
}
