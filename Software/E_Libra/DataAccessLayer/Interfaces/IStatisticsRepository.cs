using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces {
    public interface IStatisticsRepository : IDisposable {
        List<MostPopularBooks> GetMostPopularBooks(int Library_id);
        List<MostPopularGenres> GetMostPopularGenres(int Library_id);
        List<ReviewStatistics> GetReviewCount(int Library_id);
        int GetMemberCount(int Library_id);
    }
}
