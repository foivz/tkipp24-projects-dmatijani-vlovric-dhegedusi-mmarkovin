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
    public class GenreServices_integrationTest
    {
        readonly GenreServices genreServices;
        public GenreServices_integrationTest()
        {
            Helper.ResetDatabase();
            genreServices = new GenreServices();
        }
        //Viktor Lovrić
        [Fact]
        public void GetGenres_GivenFunctionIsCalled_ReturnsListOfGenres()
        {
            //Arrange
            string sql =
                "INSERT [dbo].[Genre] ([name]) VALUES (N'zanr1') " +
                "INSERT [dbo].[Genre] ([name]) VALUES (N'zanr2') ";
            Helper.ExecuteCustomSql(sql);

            var genreList = new List<Genre>
            {
                new Genre { id = 1, name = "zanr1" },
                new Genre { id = 2, name = "zanr2" }
            };

            //Act
            var result = genreServices.GetGenres();

            //Assert
            result.Should().BeEquivalentTo(genreList);
        }
        //Viktor Lovrić
        [Fact]
        public void Add_GivenFunctionIsCalled_ReturnsTrue()
        {
            //Arrange
            var genre = new Genre { name = "zanr1" };

            //Act
            var result = genreServices.Add(genre);

            //Assert
            result.Should().BeTrue();
        }
    }
}
