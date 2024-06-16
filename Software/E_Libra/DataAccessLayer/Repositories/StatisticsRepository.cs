using DataAccessLayer.Interfaces;
using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories {
    // Domagoj Hegedušić
    public class StatisticsRepository : IDisposable, IStatisticsRepository {
        protected DatabaseModel Context { get; set; }
        public StatisticsRepository() {
            Context = new DatabaseModel();
        }
        public List<MostPopularBooksViewModel> GetMostPopularBooks(int Library_id) {
            using (var repo = new BookRepository()) {
                return repo.GetMostPopularBooks(Library_id).ToList();
            }
        }

        public List<MostPopularGenres> GetMostPopularGenres(int Library_id) {
            using (var repo = new GenreRepository()) {
                return repo.GetMostPopularGenres(Library_id).ToList();
            }
        }

        public List<ReviewStatistics> GetReviewCount(int Library_id) {
            using (var repo = new ReviewRepository()) {
                return repo.GetReviewCount(Library_id).ToList();
            }
        }


        public int GetMemberCount(int Library_id) {
            using (var repo = new MemberRepository()) {
                return repo.GetMemberCount(Library_id);
            }
        }

        public void Dispose() {
            if (Context != null) {
                Context.Dispose();
            }
        }
    }
}
