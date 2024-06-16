using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using EntitiesLayer;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting.Nove_funkcionalnosti.F13
{
    public class GenreServices_Test
    {
        readonly IGenreRepository repo;
        readonly GenreServices service;

        public GenreServices_Test()
        {
            repo = A.Fake<IGenreRepository>();
            service = new GenreServices(repo);
        }

        //Viktor Lovrić
        [Fact]
        public void SearchGenres_GivenStringIsPassed_ReturnsGenres()
        {
            //Arrange
            var genres = new List<Genre>
            {
                new Genre { name = "Genre1" },
                new Genre { name = "Genre2" }
            };

            A.CallTo(() => repo.SearchGenre("Genre")).Returns(genres);

            //Act
            var result = service.SearchGenres("Genre");

            //Assert
            Assert.Equal(genres, result);
        }
    }
}
