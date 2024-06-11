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
    public class ArchiveServices_unitTest
    {
        readonly IArchiveRepository repo;
        readonly ArchiveServices service;

        public ArchiveServices_unitTest()
        {
            repo = A.Fake<IArchiveRepository>();
            service = new ArchiveServices(repo);
        }
        [Fact]
        public void GetArchive_GivenFunctionIsCalled_ReturnsArchiveList()
        {
            //Arrange
            var archivedBookInfos = new List<ArchivedBookInfo>
            {
                new ArchivedBookInfo { BookName = "Book1", EmployeeName = "Employee1", ArchiveDate = DateTime.Now },
                new ArchivedBookInfo { BookName = "Book2", EmployeeName = "Employee2", ArchiveDate = DateTime.Now },
            }.AsQueryable();

            A.CallTo(() => repo.GetArchive()).Returns(archivedBookInfos);

            //Act
            var result = service.GetArchive();

            //Assert
            Assert.Equal(archivedBookInfos, result);
        }
        [Fact]
        public void GetArchivesForEmployee_GivenFunctionIsCalled_ReturnsArchiveList()
        {
            //Arrange
            IQueryable<Archive> archives = new List<Archive>
            {
                new Archive { Book_id = 1, Employee_id = 1, arhive_date = DateTime.Now },
                new Archive { Book_id = 2, Employee_id = 1, arhive_date = DateTime.Now },
            }.AsQueryable();

            A.CallTo(() => repo.GetArchivesForEmployee(1)).Returns(archives);

            //Act
            var result = service.GetArchivesForEmployee(1);

            //Assert
            Assert.Equal(archives, result);
        }

        [Fact]
        public void Constructor_InitializesAuthorRepository()
        {
            //Arrange
            var testService = new ArchiveServices();
            //Act

            //Assert
            Assert.NotNull(testService.archiveRepository);
            Assert.IsType<ArchiveRepository>(testService.archiveRepository);
        }

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
