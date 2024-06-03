using DataAccessLayer.Repositories;
using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services {
    // Domagoj Hegedušić
    public class StatisticsService {
        public int GetMemberCount(int Library_id) {
            using (var repo = new StatisticsRepository()) {
                var result = repo.GetMemberCount(Library_id);
                    return result;
            }
        }

        public List<MostPopularBooks> GetMostPopularBooks(int Library_id) {
            using (var repo = new StatisticsRepository()) {
                var result = repo.GetMostPopularBooks(Library_id);
                return result;
            }
        }

        public List<MostPopularGenres> GetMostPopularGenres(int Library_id) {
            using (var repo = new StatisticsRepository()) {
                var result = repo.GetMostPopularGenres(Library_id);
                return result;
            }
        }

        public List<ReviewStatistics> GetReviewCount(int Library_id) {
            using (var repo = new StatisticsRepository()) {
                var result = repo.GetReviewCount(Library_id);
                return result;
            }
        }

        public int CalculateTotalIncome(int Library_id) {
            using (var repo = new StatisticsRepository()) {
                var result = repo.GetMemberCount(Library_id);
                return result*12;
            }
        }

        public IncomeStatistics GetIncomeStatistics(int Library_id) {
            int memberCount = GetMemberCount(Library_id);
            int totalIncome = CalculateTotalIncome(Library_id);

            IncomeStatistics statistics = new IncomeStatistics {
                MemberCount = memberCount,
                TotalIncome = totalIncome
            };

            return statistics;
        }
    }
}
