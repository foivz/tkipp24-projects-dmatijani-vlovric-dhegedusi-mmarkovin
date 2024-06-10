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
        [Fact]
        public void GetArchive_GivenFunctionIsCalled_ReturnsArchiveList()
        {
            //Arrange
            IArchiveRepository repo = A.Fake<IArchiveRepository>();
            IQueryable<ArchivedBookInfo> archivedBookInfos = new List<ArchivedBookInfo>
            {
                new ArchivedBookInfo { BookName = "Book1", EmployeeName = "Employee1", ArchiveDate = DateTime.Now },
                new ArchivedBookInfo { BookName = "Book2", EmployeeName = "Employee2", ArchiveDate = DateTime.Now },
            }.AsQueryable();
            var service = new ArchiveServices(repo);

            A.CallTo(() => repo.GetArchive()).Returns(archivedBookInfos);

            //Act
            var act = service.GetArchive();

            //Assert
            Assert.Equal(archivedBookInfos, act);
        }
        [Fact]
        public void GetArchivesForEmployee_GivenFunctionIsCalled_ReturnsArchiveList()
        {
            //Arrange
            IArchiveRepository repo = A.Fake<IArchiveRepository>();
            IQueryable<Archive> archives = new List<Archive>
            {
                new Archive { Book_id = 1, Employee_id = 1, arhive_date = DateTime.Now },
                new Archive { Book_id = 2, Employee_id = 1, arhive_date = DateTime.Now },
            }.AsQueryable();
            var service = new ArchiveServices(repo);

            A.CallTo(() => repo.GetArchivesForEmployee(1)).Returns(archives);

            //Act
            var act = service.GetArchivesForEmployee(1);

            //Assert
            Assert.Equal(archives, act);
        }   
    }
}
