using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories {
    // Domagoj Hegedušić
    public class ReviewRepository : Repository<Review> {
        public ReviewRepository() : base(new DatabaseModel()) {
        }

        public IQueryable<ReviewInfo> GetReviewsForBook(int bookId) {
            var query = from review in Entities
                        where review.Book_id == bookId
                        select new ReviewInfo {
                            Member_Name = review.Member.name + " " + review.Member.surname,
                            Rating = review.rating,
                            Comment = review.comment,
                            Date = review.date
                        };

            return query;
        }

        public override int Add(Review review, bool saveChanges = true) {

            var newReview = new Review() {
                Member_id = review.Member_id,
                Book_id = review.Book_id,
                comment = review.comment,
                rating = review.rating,
                date = review.date
            };

            Entities.Add(newReview);
            if (saveChanges) {
                return SaveChanges();
            } else {
                return 0;
            }
        }

        public int Remove(int memberId, int bookId, bool saveChanges = true) {
            var reviewToRemove = Entities.SingleOrDefault(r => r.Member_id == memberId && r.Book_id == bookId);

            if (reviewToRemove != null) {
                Entities.Remove(reviewToRemove);

                if (saveChanges) {
                    return SaveChanges();
                } else {
                    return 0;
                }
            }

            return 0;
        }
        public override int Update(Review review, bool saveChanges = true) {
            var existingReview = Entities.SingleOrDefault(r => r.Member_id == review.Member_id && r.Book_id == review.Book_id);

            if (existingReview != null) {
                existingReview.rating = review.rating;
                existingReview.comment = review.comment;
                existingReview.date = review.date;

                if (saveChanges) {
                    return SaveChanges();
                }
            }

            return 0;
        }

        public IQueryable<Review> GetReviewsForMemberAndBook(int memberId, int bookId) {
            var query = from r in Entities
                        where r.Member_id == memberId && r.Book_id == bookId
                        select r;

            return query;
        }

        public List<ReviewStatistics> GetReviewCount(int library_id) {
            var query = from r in Entities
                        where r.Book.Library_id == library_id
                        group r by r.rating into g
                        select new ReviewStatistics {
                            Grade = g.Key.ToString(),
                            Number_Count = g.Count()
                        };

            return query.ToList();
        }
    }
}
