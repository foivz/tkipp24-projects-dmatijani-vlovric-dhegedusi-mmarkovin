using DataAccessLayer.Interfaces;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories {
    //Magdalena Markovinović, metode: GetEmployeeLogin, 
    // David Matijanić: GetEmployeesByLibrary, GetEmployeesById, GetEmployeesByUsername, GetEmployeesByOIB,
    // Add, Update, GetEmployeeLibraryId, GetEmployeeId
    public class EmployeeRepository : Repository<Employee>, IEmpoloyeeRepositroy{
        public EmployeeRepository() : base(new DatabaseModel()) {

        }

        public IQueryable<Employee> GetEmployeesByLibrary(Library library) {
            var query = from e in Entities.Include("Library")
                        where e.Library_id == library.id
                        select e;

            return query;
        }

        public IQueryable<Employee> GetEmployeesById(int employeeId) {
            var query = from e in Entities.Include("Library")
                        where e.id == employeeId
                        select e;

            return query;
        }

        public IQueryable<Employee> GetEmployeesByUsername(string employeeUsername) {
            var query = from e in Entities.Include("Library")
                        where e.username == employeeUsername
                        select e;

            return query;
        }

        public IQueryable<Employee> GetEmployeesByOIB(string employeeOIB) {
            var query = from e in Entities.Include("Library")
                        where e.OIB == employeeOIB
                        select e;

            return query;
        }

        public override int Add(Employee employee, bool saveChanges = true) {
            var library = Context.Libraries.SingleOrDefault(l => l.id == employee.Library.id);

            var newEmployee = new Employee {
                id = employee.id,
                name = employee.name,
                surname = employee.surname,
                username = employee.username,
                password = employee.password,
                OIB = employee.OIB,
                Library = library
            };

            Entities.Add(newEmployee);
            if (saveChanges) {
                return SaveChanges();
            } else {
                return 0;
            }
        }

        public override int Update(Employee employee, bool saveChanges = true) {
            var library = Context.Libraries.SingleOrDefault(l => l.id == employee.Library.id);

            var existingEmployee = Entities.SingleOrDefault(e => e.OIB == employee.OIB);
            existingEmployee.name = employee.name;
            existingEmployee.surname = employee.surname;
            existingEmployee.username = employee.username;
            existingEmployee.password = employee.password;
            existingEmployee.Library = library;

            if (saveChanges) {
                return SaveChanges();
            } else {
                return 0;
            }
        }

        public IQueryable<Employee> GetEmployeeLogin(string username, string password) {
            var sql = from e in Entities.Include("Library")
                      where e.username == username && e.password == password
                      select e;
            return sql;
        }

        public int GetEmployeeLibraryId(string username) {
            var sql = (from e in Entities.Include("Library") where e.username == username select e.Library_id).FirstOrDefault();
            return sql;
        }

        public int GetEmployeeId(string username) {
            var sql = (from e in Entities.Include("Library") where e.username == username select e.id).FirstOrDefault();
            return sql;
        }

        public int Remove(Employee employee)
        {
            return base.Remove(employee);
        }
    }
}
