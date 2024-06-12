using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IEmpoloyeeRepositroy: IDisposable
    {
        IQueryable<Employee> GetEmployeesByLibrary(Library library);
        IQueryable<Employee> GetEmployeesById(int employeeId);
        IQueryable<Employee> GetEmployeesByUsername(string employeeUsername);
        IQueryable<Employee> GetEmployeesByOIB(string employeeOIB);
        int Add(Employee employee, bool saveChanges = true);
        int Update(Employee employee, bool saveChanges = true);
        IQueryable<Employee> GetEmployeeLogin(string username, string password);
        int GetEmployeeLibraryId(string username);
        int GetEmployeeId(string username);
        int Remove(Employee employee);
    }
}
