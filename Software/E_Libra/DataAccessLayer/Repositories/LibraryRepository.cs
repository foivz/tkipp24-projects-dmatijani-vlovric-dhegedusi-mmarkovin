using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories {
    // David Matijanić
    public class LibraryRepository : Repository<Library> {
        public LibraryRepository() : base(new DatabaseModel()) {

        }

        public override IQueryable<Library> GetAll() {
            var query = from e in Entities
                        select e;

            return query;
        }

        public async Task<List<Library>> GetAllLibrariesAsync() {
            var query = from e in Entities
                        select e;

            var libraries = await query.ToListAsync();
            return libraries;
        }

        public IQueryable<Library> GetLibrariesById(int libraryId) {
            var query = from l in Entities
                        where l.id == libraryId
                        select l;

            return query;
        }

        public IQueryable<Library> GetLibrariesByName(string libraryName) {
            var query = from l in Entities
                        where l.name == libraryName
                        select l;

            return query;
        }

        public IQueryable<Library> GetLibrariesByOIB(string libraryOIB) {
            var query = from l in Entities
                        where l.OIB == libraryOIB
                        select l;

            return query;
        }

        public override int Update(Library library, bool saveChanges = true) {
            var existingLibrary = Entities.SingleOrDefault(l => l.id == library.id);
            existingLibrary.name = library.name;
            existingLibrary.OIB = library.OIB;
            existingLibrary.phone = library.phone;
            existingLibrary.email = library.email;
            existingLibrary.price_day_late = library.price_day_late;
            existingLibrary.address = library.address;
            existingLibrary.membership_duration = library.membership_duration;

            if (saveChanges) {
                return SaveChanges();
            } else {
                return 0;
            }
        }

        public decimal GetLibraryPriceDayLate(int libraryId) {
            var query = from l in Entities
                        where l.id == libraryId
                        select l;

            if (query.Count() == 0) {
                return 0;
            }

            return query.FirstOrDefault().price_day_late;
        }

        public DateTime GetLibraryMembershipDuration(int libraryId) {
            var query = from l in Entities
                        where l.id == libraryId
                        select l.membership_duration;

            return query.FirstOrDefault();
        }
    }
}
