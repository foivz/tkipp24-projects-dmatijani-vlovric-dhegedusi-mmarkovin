using DataAccessLayer.Interfaces;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    //Viktor Lovrić
    public class ArchiveRepository : Repository<Archive>, IArchiveRepository
    {
        public ArchiveRepository(): base(new DatabaseModel())
        {
            
        }

        public override int Update(Archive entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ArchivedBookInfo> GetArchive()
        {
            var query = from archive in Entities
                        join book in Context.Books on archive.Book_id equals book.id
                        join employee in Context.Employees on archive.Employee_id equals employee.id
                        select new ArchivedBookInfo
                        {
                            BookName = book.name,
                            EmployeeName = employee.name + " " + employee.surname,
                            ArchiveDate = archive.arhive_date
                        };
            return query;
        }

        public IQueryable<Archive> GetArchivesForEmployee(int employeeId) {
            var query = from a in Entities.Include("Employee")
                        where a.Employee_id == employeeId
                        select a;

            return query;
        }
    }
    public class ArchivedBookInfo
    {
        public string BookName { get; set;}
        public string EmployeeName { get; set;}
        public DateTime ArchiveDate { get; set;}
    }
}
