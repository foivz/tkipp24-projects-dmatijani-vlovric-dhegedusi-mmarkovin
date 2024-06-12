using BussinessLogicLayer.services;
using DataAccessLayer;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace IntegrationTesting
{
    public class Proba
    {
        public Proba()
        {
            Helper.ResetDatabase();
        }

        [Fact]
        public void Test1()
        {
            //Arrange
            Genre genre = new Genre
            {
                name = "TestGenre"
            };
            GenreServices genreService = new GenreServices();
            //Act
            var result = genreService.Add(genre);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Test2()
        {
            Author author = new Author
            {
                name = "TestAuthor"
            };
            AuthorService authorService = new AuthorService();

            var result = authorService.AddAuthor(author);

            Assert.True(result);
        }

        [Fact]
        public void Test3()
        {
            //Arrange
            string sql =
                "INSERT [dbo].[Genre] ([name]) VALUES (N'TestGenre1');" +
                "INSERT [dbo].[Genre] ([name]) VALUES (N'TestGenre2');";

            Helper.ExecuteCustomSql(sql);

            List<Genre> genres = new List<Genre>
            {
                new Genre
                {
                    id = 1,
                    name = "TestGenre1"
                },
                new Genre
                {
                    id = 2,
                    name = "TestGenre2"
                }
            };
            GenreServices genreService = new GenreServices();

            //Act

            var result = genreService.GetGenres();

            //Assert
            result.Should().BeEquivalentTo(genres);
        }
    }
}
