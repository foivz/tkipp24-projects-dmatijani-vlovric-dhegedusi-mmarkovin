using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces {
    public interface ILibraryRepository : IDisposable {
        IQueryable<Library> GetAll();

        Task<List<Library>> GetAllLibrariesAsync();

        IQueryable<Library> GetLibrariesById(int libraryId);

        IQueryable<Library> GetLibrariesByName(string libraryName);

        IQueryable<Library> GetLibrariesByOIB(string libraryOIB);

        int Update(Library library, bool saveChanges = true);

        decimal GetLibraryPriceDayLate(int libraryId);

        DateTime GetLibraryMembershipDuration(int libraryId);
        int Add(Library newLibrary);
        int Remove(Library library);
    }
}
