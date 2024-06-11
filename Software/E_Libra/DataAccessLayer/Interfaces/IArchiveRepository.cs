using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IArchiveRepository : IDisposable
    {
        IQueryable<ArchivedBookInfo> GetArchive();
        IQueryable<Archive> GetArchivesForEmployee(int employeeId);
    }
}
