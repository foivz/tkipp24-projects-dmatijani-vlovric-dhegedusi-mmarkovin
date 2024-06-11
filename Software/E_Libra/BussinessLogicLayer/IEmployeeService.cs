using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployeesByLibrary(Library library);

        int AddEmployee(Employee newEmployee);

        int UpdateEmployee(Employee employee);

        int DeleteEmployee(Employee employee);

        void CheckLoginCredentials(string username, string password);

        int GetEmployeeLibraryId(string username);

        int GetEmployeeId(string username);

        Employee GetEmployeeByUsername(string username);
    }
}
