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

namespace UnitTesting.Nove_funkcionalnosti.F13 {
    public class AuthorService_Test
    {
        readonly IAuthorRepository repo;
        readonly AuthorService service;

        public AuthorService_Test()
        {
            repo = A.Fake<IAuthorRepository>();
            service = new AuthorService(repo);
        }

        //Viktor Lovrić
        [Fact]
        public void SearchAuthors_GivenStringIsPassed_ReturnsAuthors()
        {
            //Arrange
            var authors = new List<Author>
            {
                new Author { idAuthor = 1, name = "Author1", surname = "Surname1", birth_date = DateTime.Now },
                new Author { idAuthor = 2, name = "Author2", surname = "Surname2", birth_date = DateTime.Now },
            };

            A.CallTo(() => repo.SearchAuthor("Author")).Returns(authors);

            //Act
            var result = service.SearchAuthors("Author");

            //Assert
            Assert.Equal(authors, result);
        }
    }
}
