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
    public class AuthorService_unitTest
    {
        readonly IAuthorRepository repo;
        readonly AuthorService service;

        public AuthorService_unitTest()
        {
            repo = A.Fake<IAuthorRepository>();
            service = new AuthorService(repo);
        }
        //Viktor Lovrić
        [Fact]
        public void GetAllAuthors_GivenFunctionIsCalled_ReturnsListOfAuthors()
        {
            //Arrange
            var authors = new List<Author>
            {
                new Author { idAuthor = 1, name = "Author1", surname = "Surname1", birth_date = DateTime.Now },
                new Author { idAuthor = 2, name = "Author2", surname = "Surname2", birth_date = DateTime.Now },
            }.AsQueryable();

            A.CallTo(() => repo.GetAll()).Returns(authors);

            //Act
            var result = service.GetAllAuthors();

            //Assert
            Assert.Equal(authors, result);
        }
        //Viktor Lovrić
        [Fact]
        public void AddAuthor_GivenAuthorIsPassed_ReturnsTrue()
        {
            //Arrange
            var author = new Author { idAuthor = 1, name = "Author1", surname = "Surname1", birth_date = DateTime.Now };

            A.CallTo(() => repo.Add(author, true)).Returns(1);

            //Act
            var result = service.AddAuthor(author);

            //Assert
            Assert.True(result);
        }
        //Viktor Lovrić
        [Fact]
        public void Constructor_InitializesAuthorRepository()
        {
            //Arrange
            var testService = new AuthorService();
            //Act

            //Assert
            Assert.NotNull(testService.authorRepository);
            Assert.IsType<AuthorRepository>(testService.authorRepository);
        }
        //Viktor Lovrić
        [Fact]
        public void Dispose_GivenFunctionIsCalled_ReturnsNothing()
        {
            //Arrange

            //Act
            service.Dispose();

            //Assert
            A.CallTo(() => repo.Dispose()).MustHaveHappened();
        }
    }
}
