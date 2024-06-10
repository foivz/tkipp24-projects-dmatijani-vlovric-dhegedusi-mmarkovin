using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting
{
    public class GenreServices_unitTest
    {
        readonly IGenreRepository repo;
        readonly GenreServices service;

        public GenreServices_unitTest()
        {
            repo = A.Fake<IGenreRepository>();

            service = new GenreServices(repo);
        }

        [Fact]
        public void GetGenres_GivenFunctionIsCalled_ReturnsListOfGenres()
        {
            //Arrange
            var genreList = new List<Genre>
            {
                new Genre { name = "Genre1" },
                new Genre { name = "Genre2" }
            }.AsQueryable();

            A.CallTo(() => repo.GetAll()).Returns(genreList);

            //Act
            var result = service.GetGenres();

            //Assert
            Assert.Equal(genreList, result);
        }

        [Fact]
        public void Add_GivenFunctionIsCalled_ReturnsTrue()
        {
            //Arrange
            var genre = new Genre { name = "Genre1" };

            A.CallTo(() => repo.Add(genre, true)).Returns(1);

            //Act
            var result = service.Add(genre);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Constructor_InitializesAuthorRepository()
        {
            //Arrange
            var testService = new GenreServices();
            //Act

            //Assert
            Assert.NotNull(testService.genreRepository);
            Assert.IsType<GenreRepository>(testService.genreRepository);
        }
    }
}
