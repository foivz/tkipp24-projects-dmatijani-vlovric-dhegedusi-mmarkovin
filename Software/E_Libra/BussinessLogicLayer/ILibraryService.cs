using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer
{
    using EntitiesLayer;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILibraryService : IDisposable
    {
        List<Library> GetAllLibraries();
        Task<List<Library>> GetAllLibrariesAsync();
        int AddLibrary(Library newLibrary);
        int DeleteLibrary(Library library);
        int UpdateLibrary(Library library);
        decimal GetLibraryPriceDayLate(Library library);
        decimal GetLibraryPriceDayLate(int libraryId);
        decimal GetLibraryMembershipDuration(int libraryId);
    }

}
