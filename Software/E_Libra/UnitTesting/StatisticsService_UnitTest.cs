using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using EntitiesLayer.Entities;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting {
    public class StatisticsService_UnitTest {

        readonly IStatisticsRepository repo;
        readonly StatisticsService service;

        public StatisticsService_UnitTest() {
            repo = A.Fake<IStatisticsRepository>();
            service = new StatisticsService(repo);
        }


        [Fact]
        public void GetMemberCount_GivenLibraryId_ReturnsMemberCount() {
            // Arrange
            int libraryId = 1;
            int expectedMemberCount = 100;

            A.CallTo(() => repo.GetMemberCount(libraryId)).Returns(expectedMemberCount);

            // Act
            int result = service.GetMemberCount(libraryId);

            // Assert
            Assert.Equal(expectedMemberCount, result);
        }

        [Fact]
        public void CalculateTotalIncome_GivenLibraryId_ReturnsTotalIncome() {
            // Arrange
            int libraryId = 1;
            int memberCount = 100;
            int expectedTotalIncome = memberCount * 12;

            A.CallTo(() => repo.GetMemberCount(libraryId)).Returns(memberCount);

            // Act
            int result = service.CalculateTotalIncome(libraryId);

            // Assert
            Assert.Equal(expectedTotalIncome, result);
        }

        [Fact]
        public void GetIncomeStatistics_GivenLibraryId_ReturnsIncomeStatistics() {
            // Arrange
            int libraryId = 1;
            int memberCount = 100;
            int expectedTotalIncome = memberCount * 12;
            var expectedStatistics = new IncomeStatistics { MemberCount = memberCount, TotalIncome = expectedTotalIncome };

            A.CallTo(() => repo.GetMemberCount(libraryId)).Returns(memberCount);

            // Act
            var result = service.GetIncomeStatistics(libraryId);

            // Assert
            Assert.Equal(expectedStatistics.MemberCount, result.MemberCount);
            Assert.Equal(expectedStatistics.TotalIncome, result.TotalIncome);
        }

        [Fact]
        public void GetReviewCount_GivenLibraryId_ReturnsReviewCount() {
            // Arrange
            int libraryId = 1;
            var expectedReviews = new List<ReviewStatistics>
            {
                new ReviewStatistics { Grade = "5", Number_Count = 10 },
                new ReviewStatistics { Grade = "4", Number_Count = 8 },
                new ReviewStatistics { Grade = "3", Number_Count = 2 },
                new ReviewStatistics { Grade = "1", Number_Count = 4 }
            };

            A.CallTo(() => repo.GetReviewCount(libraryId)).Returns(expectedReviews);

            // Act
            var result = service.GetReviewCount(libraryId);

            // Assert
            Assert.Equal(expectedReviews, result);
        }

        [Fact]
        public void GetMostPopularGenres_GivenLibraryId_ReturnsMostPopularGenres() {
            // Arrange
            int libraryId = 1;
            var expectedGenres = new List<MostPopularGenres>
            {
                new MostPopularGenres { Genre_name = "Roman", Times_Borrowed = 20 },
                new MostPopularGenres { Genre_name = "Science-Fiction", Times_Borrowed = 15 },
                new MostPopularGenres { Genre_name = "Drama", Times_Borrowed = 37 },
                new MostPopularGenres { Genre_name = "Poezija", Times_Borrowed = 6 }
            };

            A.CallTo(() => repo.GetMostPopularGenres(libraryId)).Returns(expectedGenres);

            // Act
            var result = service.GetMostPopularGenres(libraryId);

            // Assert
            Assert.Equal(expectedGenres, result);
        }

        [Fact]
        public void GetMostPopularBooks_GivenLibraryId_ReturnsMostPopularBooks() {
            // Arrange
            int libraryId = 1;
            var expectedBooks = new List<MostPopularBooks>
            {
                new MostPopularBooks { Book_Name = "Hamlet", Author_Name = "William Shakespeare", Times_Borrowed = 10 },
                new MostPopularBooks { Book_Name = "Romeo i Julija", Author_Name = "William Shakespeare", Times_Borrowed = 8 },
                new MostPopularBooks { Book_Name = "Srce Ratnika", Author_Name = "Alexander Stone", Times_Borrowed = 0 }
            };

            A.CallTo(() => repo.GetMostPopularBooks(libraryId)).Returns(expectedBooks);

            // Act
            var result = service.GetMostPopularBooks(libraryId);

            // Assert
            Assert.Equal(expectedBooks, result);
        }

        [Fact]
        public void Dispose_CallsDisposeOnRepository() {
            // Act
            service.Dispose();

            // Assert
            A.CallTo(() => repo.Dispose()).MustHaveHappened();
        }


    }
}
