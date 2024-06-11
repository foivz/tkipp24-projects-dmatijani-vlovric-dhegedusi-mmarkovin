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
    public class ArchiveServices : IDisposable
    {
        public IArchiveRepository archiveRepository { get; set; }
        public ArchiveServices(IArchiveRepository archiveRepository)
        {
            this.archiveRepository = archiveRepository;
        }
        public ArchiveServices() : this(new ArchiveRepository()){}

        ~ArchiveServices()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposing && archiveRepository != null)
            {
                archiveRepository.Dispose();
            }
        }

        public List<ArchivedBookInfo> GetArchive()
        {
            return archiveRepository.GetArchive().ToList();
        }

        public List<Archive> GetArchivesForEmployee(int employeeId) {

            return archiveRepository.GetArchivesForEmployee(employeeId).ToList();
        }
    }
}
