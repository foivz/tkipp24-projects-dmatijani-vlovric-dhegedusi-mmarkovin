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
    }
}
