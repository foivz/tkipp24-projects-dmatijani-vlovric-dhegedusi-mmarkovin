using BussinessLogicLayer.services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting.Nove_funkcionalnosti.F13
{
    [Collection("Database collection")]
    public class GenreServices_intTest
    {
        readonly GenreServices services;
        readonly DatabaseFixture fixture;

        public GenreServices_intTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
            this.fixture.ResetDatabase();
            services = new GenreServices();
        }

        //Viktor Lovrić
        [Fact]
        public void SearchGenres_GivenStringIsPassed_ReturnsGenres()
        {
            //Arrange
            string sql =
                "INSERT [dbo].[Genre] ([name]) VALUES (N'zanr1') " +
                "INSERT [dbo].[Genre] ([name]) VALUES (N'zanr2') " +
                "INSERT [dbo].[Genre] ([name]) VALUES (N'Drugaciji') ";
            Helper.ExecuteCustomSql(sql);

            var genres = new List<EntitiesLayer.Genre>
            {
                new EntitiesLayer.Genre { id = 1, name = "zanr1" },
                new EntitiesLayer.Genre { id = 2, name = "zanr2" }
            };

            //Act
            var result = services.SearchGenres("zanr");

            //Assert
            result.Should().BeEquivalentTo(genres);
        }


    }
}
