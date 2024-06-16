using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using EntitiesLayer.Entities;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting {
    public class ReviewService_UnitTest {

        readonly IReviewRepository repo;
        readonly ReviewService service;

        public ReviewService_UnitTest() {
            repo = A.Fake<IReviewRepository>();
            service = new ReviewService(repo);
        }

        // Domagoj Hegedušić
        [Fact]
        public void GetReviewsForBook_GivenBookId_ReturnsReviews()
        {
            // Arrange
            int bookId = 1;
            var existingReviews = new List<ReviewInfo>
            {
                new ReviewInfo { Member_Name = "Martina Pranjic", Rating = 5, Comment = "Svidja mi se knjiga", Date = DateTime.Now },
                new ReviewInfo { Member_Name = "Marko Ivanić", Rating = 3, Comment = "Kraj je prilično loš.", Date = DateTime.Now }
            }.AsQueryable();

            A.CallTo(() => repo.GetReviewsForBook(bookId)).Returns(existingReviews);

            // Act
            var result = service.GetReviewsForBook(bookId);

            // Assert
            Assert.Equal(existingReviews, result);
        }
        // Domagoj Hegedušić

        [Fact]
        public void AddReview_GivenNewReview_ReturnsReviewId() {
            // Arrange
            var newReview = new Review { Book_id = 1, comment = "Knjiga je odlicna!", rating = 5, date = DateTime.Now };
            int expectedReviewId = 1;

            A.CallTo(() => repo.Add(newReview, true)).Returns(expectedReviewId);

            // Act
            var result = service.AddReview(newReview);

            // Assert
            Assert.Equal(expectedReviewId, result);
        }
        // Domagoj Hegedušić

        [Fact]
        public void DeleteReview_GivenReviewIdAndBookId_ReturnsResult() {
            // Arrange
            int memberId = 1;
            int bookId = 1;
            int expectedResult = 1;

            A.CallTo(() => repo.Remove(memberId, bookId, true)).Returns(expectedResult);

            // Act
            var result = service.DeleteReview(memberId, bookId);

            // Assert
            Assert.Equal(expectedResult, result);
        }
        // Domagoj Hegedušić

        [Fact]
        public void HasUserReviewedBook_GivenMemberIdAndBookId_ReturnsTrueIfReviewed() {
            // Arrange
            int memberId = 1;
            int bookId = 1;
            var userReviews = new List<Review>
            {
                new Review { Member_id = memberId, Book_id = bookId, comment = "Knjiga je odlicna!", rating = 5, date = DateTime.Now }
            }.AsQueryable();

            A.CallTo(() => repo.GetReviewsForMemberAndBook(memberId, bookId)).Returns(userReviews);

            // Act
            var result = service.HasUserReviewedBook(memberId, bookId);

            // Assert
            Assert.True(result);
        }
        // Domagoj Hegedušić

        [Fact]
        public void GetReviewsForMemberAndBook_GivenMemberIdAndBookId_ReturnsReviews() {
            // Arrange
            int memberId = 1;
            int bookId = 1;
            var expectedReviews = new List<Review>
            {
                new Review { Member_id = memberId, Book_id = bookId, comment = "Svidja mi se knjiga!", rating = 5, date = DateTime.Now },
                new Review { Member_id = memberId, Book_id = bookId, comment = "Kraj je los.", rating = 3, date = DateTime.Now }
            }.AsQueryable();

            A.CallTo(() => repo.GetReviewsForMemberAndBook(memberId, bookId)).Returns(expectedReviews);

            // Act
            var result = service.GetReviewsForMemberAndBook(memberId, bookId);

            // Assert
            Assert.Equal(expectedReviews, result);
        }
        // Domagoj Hegedušić

        [Fact]
        public void Dispose_CallsDisposeOnRepository() {
            // Act
            service.Dispose();

            // Assert
            A.CallTo(() => repo.Dispose()).MustHaveHappened();
        }
        // Domagoj Hegedušić

        [Fact]
        public void DefaultConstructor_InitializesStatisticsRepository() {
            // Act
            var realService = new ReviewService();

            // Assert
            Assert.NotNull(realService.reviewRepository);
            Assert.IsType<ReviewRepository>(realService.reviewRepository);
        }
    }
}
