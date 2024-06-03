using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services
{
    //Viktor Lovrić
    public class ArchiveServices
    {
        public List<ArchivedBookInfo> GetArchive()
        {
            using (var repo = new ArchiveRepository())
            {
                return repo.GetArchive().ToList();
            }
        }

        public List<Archive> GetArchivesForEmployee(int employeeId) {
            using (var repo = new ArchiveRepository()) {
                return repo.GetArchivesForEmployee(employeeId).ToList();
            }
        }
    }
}
