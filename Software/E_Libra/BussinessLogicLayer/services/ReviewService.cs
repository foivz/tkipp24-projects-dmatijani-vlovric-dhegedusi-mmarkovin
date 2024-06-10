using EntitiesLayer;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Repositories;
using System;
using EntitiesLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BussinessLogicLayer.services {
    // Domagoj Hegedušić
    public class ReviewService : IDisposable {
        public IReviewRepository reviewRepository { get; set; }

        public ReviewService(IReviewRepository reviewRepository){
            this.reviewRepository = reviewRepository;
        }
        public ReviewService (): this(new ReviewRepository()) {

        }


        public List<ReviewInfo> GetReviewsForBook(int book_id) {
                return reviewRepository.GetReviewsForBook(book_id).ToList();
        }

        public int AddReview(Review newReview) {
                return reviewRepository.Add(newReview);
        }

        public int DeleteReview(int reviewId, int bookId) {
                return reviewRepository.Remove(reviewId, bookId);
        }

        public bool HasUserReviewedBook(int memberId, int bookId) {
                List<Review> userReviews = GetReviewsForMemberAndBook(memberId, bookId);
                return userReviews.Any();
            }

        public List<Review> GetReviewsForMemberAndBook(int memberId, int bookId) {
                return reviewRepository.GetReviewsForMemberAndBook(memberId, bookId).ToList();
        }


        ~ReviewService() {
            Dispose(false);
        }

        private void Dispose(bool disposing) {
            if (disposing) {
                reviewRepository?.Dispose();
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

}

