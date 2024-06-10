using DataAccessLayer.Repositories;
using EntitiesLayer;
using EntitiesLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services {
    // Domagoj Hegedušić
    public class StatisticsService : IDisposable {
        public IStatisticsRepository statisticsRepository { get; set; }

        public StatisticsService(IStatisticsRepository statisticsRepository) {
            this.statisticsRepository = statisticsRepository;
        }
        public StatisticsService() : this(new StatisticsRepository()) {
        }


        public int GetMemberCount(int Library_id) {
                var result = statisticsRepository.GetMemberCount(Library_id);
                    return result;
        }

        public List<MostPopularBooks> GetMostPopularBooks(int Library_id) {
                var result = statisticsRepository.GetMostPopularBooks(Library_id);
                return result;
        }

        public List<MostPopularGenres> GetMostPopularGenres(int Library_id) {
                var result = statisticsRepository.GetMostPopularGenres(Library_id);
                return result;
        }

        public List<ReviewStatistics> GetReviewCount(int Library_id) {
                var result = statisticsRepository.GetReviewCount(Library_id);
                return result;
        }

        public int CalculateTotalIncome(int Library_id) {
                var result = statisticsRepository.GetMemberCount(Library_id);
                return result*12;
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

        ~StatisticsService() {
            Dispose(false);
        }

        private void Dispose(bool disposing) {
            if (disposing) {
                statisticsRepository?.Dispose();
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
