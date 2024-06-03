using EntitiesLayer;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Repositories;
using System;
using EntitiesLayer.Entities;

namespace BussinessLogicLayer.services {
    // Domagoj Hegedušić
    public class ReviewService {
        public List<ReviewInfo> GetReviewsForBook(int book_id) {
            using (var repo = new ReviewRepository()) {
                return repo.GetReviewsForBook(book_id).ToList();
            }
        }

        public int AddReview(Review newReview) {
            using (var repo = new ReviewRepository()) {
                return repo.Add(newReview);
            }
        }

        public int DeleteReview(int reviewId, int bookId) {
            using (var repo = new ReviewRepository()) {
                return repo.Remove(reviewId, bookId);
            }
        }

        public bool HasUserReviewedBook(int memberId, int bookId) {
                List<Review> userReviews = GetReviewsForMemberAndBook(memberId, bookId);
                return userReviews.Any();
            }

        public List<Review> GetReviewsForMemberAndBook(int memberId, int bookId) {
            using (var repo = new ReviewRepository()) {
                return repo.GetReviewsForMemberAndBook(memberId, bookId).ToList();
            }
        }
    }

}

