using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using BussinessLogicLayer.services;
using EntitiesLayer;

namespace IntegrationTesting
{
    public class AuthorService_integrationTest
    {
        AuthorService service;
        public AuthorService_integrationTest()
        {
            Helper.ResetDatabase();
            service = new AuthorService();
        }

        [Fact]
        public void GetAllAuthors_GivenFunctionIsCalled_ReturnsListOfAuthors()
        {
            //Arrange
            var authors = new List<Author>
            {
                new Author { idAuthor = 1, name = "Author1", surname = "Surname1", birth_date = DateTime.Now.Date },
                new Author { idAuthor = 2, name = "Author2", surname = "Surname2", birth_date = DateTime.Now.Date },
            };

            foreach (var author in authors)
            {
                var formattedBirthDate = author.birth_date.HasValue ? author.birth_date.Value.ToString("yyyy-MM-dd") : "NULL";
                Helper.ExecuteCustomSql($"INSERT INTO dbo.Author (idAuthor, name, surname, birth_date) VALUES ({author.idAuthor}, '{author.name}', '{author.surname}', '{formattedBirthDate}')");
            }

            //Act
            var result = service.GetAllAuthors();

            //Assert
            result.Should().BeEquivalentTo(authors);
        }
    }
}
