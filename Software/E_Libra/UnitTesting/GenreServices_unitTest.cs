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

namespace UnitTesting
{
    public class GenreServices_unitTest
    {

        [Fact]
        public void GetGenres_GivenFunctionIsCalled_ReturnsListOfGenres()
        {
            //Arrange
            var genreList = new List<Genre>
            {
                new Genre { name = "Genre1" },
                new Genre { name = "Genre2" }
            }.AsQueryable();

            var repo = A.Fake<IGenreRepository>();

            var service = new GenreServices(repo);
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
            var repo = A.Fake<IGenreRepository>();
            var genre = new Genre { name = "Genre1" };
            var service = new GenreServices(repo);

            A.CallTo(() => repo.Add(genre, true)).Returns(1);

            //Act
            var result = service.Add(genre);

            //Assert
            Assert.True(result);
        }
    }
}
