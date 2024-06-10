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
    public class AuthorService_unitTest
    {
        [Fact]
        public void GetAllAuthors_GivenFunctionIsCalled_ReturnsListOfAuthors()
        {
            //Arrange
            var repo = A.Fake<IAuthorRepository>();
            var authors = new List<Author>
            {
                new Author { idAuthor = 1, name = "Author1", surname = "Surname1", birth_date = DateTime.Now },
                new Author { idAuthor = 2, name = "Author2", surname = "Surname2", birth_date = DateTime.Now },
            }.AsQueryable();
            var service = new AuthorService(repo);

            A.CallTo(() => repo.GetAll()).Returns(authors);

            //Act
            var result = service.GetAllAuthors();

            //Assert
            Assert.Equal(authors, result);
        }

        [Fact]
        public void AddAuthor_GivenAuthorIsPassed_ReturnsTrue()
        {
            //Arrange
            var repo = A.Fake<IAuthorRepository>();
            var author = new Author { idAuthor = 1, name = "Author1", surname = "Surname1", birth_date = DateTime.Now };
            var service = new AuthorService(repo);

            A.CallTo(() => repo.Add(author, true)).Returns(1);

            //Act
            var result = service.AddAuthor(author);

            //Assert
            Assert.True(result);
        }
    }
}
