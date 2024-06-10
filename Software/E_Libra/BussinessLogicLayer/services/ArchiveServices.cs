using DataAccessLayer.Interfaces;
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
        public IArchiveRepository archiveRepository { get; set; }
        public ArchiveServices(IArchiveRepository archiveRepository)
        {
            this.archiveRepository = archiveRepository;
        }
        public ArchiveServices() : this(new ArchiveRepository()){}


        public List<ArchivedBookInfo> GetArchive()
        {
            return archiveRepository.GetArchive().ToList();
        }

        public List<Archive> GetArchivesForEmployee(int employeeId) {

            return archiveRepository.GetArchivesForEmployee(employeeId).ToList();
        }
    }
}
