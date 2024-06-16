using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces {
    public interface IReviewRepository : IDisposable {

        IQueryable<ReviewInfo> GetReviewsForBook(int bookId);

        int Add(Review review, bool saveChanges = true);

        int Remove(int memberId, int bookId, bool saveChanges = true);

        int Update(Review review, bool saveChanges = true);

        IQueryable<Review> GetReviewsForMemberAndBook(int memberId, int bookId);

        List<ReviewStatistics> GetReviewCount(int library_id);
    }
}
