using BussinessLogicLayer.Exceptions;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services {
    // David Matijanić: GetEmployeesByLibrary, AddEmployee, UpdateEmployee, DeleteEmployee, GetEmployeeByUsername
    public class EmployeeService :  IDisposable{

        private IEmpoloyeeRepositroy employeeRepository;
        private BorrowService borrowService { get; set; }
        private ArchiveServices archiveService { get; set; }

        public EmployeeService(
            IEmpoloyeeRepositroy employeeRepository,
            BorrowService borrowService,
            ArchiveServices archiveService
        ) {
            this.employeeRepository = employeeRepository;
            this.borrowService = borrowService;
            this.archiveService = archiveService;
        }
        public EmployeeService(): this(
            new EmployeeRepository(),
            new BorrowService(),
            new ArchiveServices()
        ) {
            
        }
        public List<Employee> GetEmployeesByLibrary(Library library) {
                return employeeRepository.GetEmployeesByLibrary(library).ToList();

        }

        public int AddEmployee(Employee newEmployee) {
                var employeesWithId = employeeRepository.GetEmployeesById(newEmployee.id);
                if (employeesWithId.ToList().Count > 0) {
                    throw new EmployeeWithSameIDException("Zaposlenik sa istim ID već postoji!");
                }

                var employeesWithUsername = employeeRepository.GetEmployeesByUsername(newEmployee.username);
                if (employeesWithUsername.ToList().Count > 0) {
                    throw new EmployeeWithSameUsernameException("Zaposlenik sa istim korisničkim imenom već postoji!");
                }

                var employeesWithOIB = employeeRepository.GetEmployeesByOIB(newEmployee.OIB);
                if (employeesWithOIB.ToList().Count > 0) {
                    throw new EmployeeWithSameOIBException("Zaposlenik sa istim OIB već postoji!");
                }

            return employeeRepository.Add(newEmployee);
        }

        public int UpdateEmployee(Employee employee) { 
                var employeesWithId = employeeRepository.GetEmployeesById(employee.id);
                var otherEmployeesWithId = employeesWithId.ToList().FindAll(e => e.OIB != employee.OIB);
                if (otherEmployeesWithId.Count > 0) {
                    throw new EmployeeWithSameIDException("Zaposlenik sa tim ID već postoji!");
                }

                var employeesWithOIB = employeeRepository.GetEmployeesByOIB(employee.OIB);
                if (employeesWithOIB.ToList().Count == 0) {
                    throw new EmployeeWithSameOIBException("Ne postoji zaposlenik sa odabranim OIB!");
                }

                var employeesWithUsername = employeeRepository.GetEmployeesByUsername(employee.username);
                var otherEmployeesWithUsername = employeesWithUsername.ToList().FindAll(e => e.OIB != employee.OIB);
                if (otherEmployeesWithUsername.Count > 0) {
                    throw new EmployeeWithSameUsernameException("Zaposlenik sa tim korisničkim imenom već postoji!");
                }

                return employeeRepository.Update(employee);
        }

        public int DeleteEmployee(Employee employee) {
            if (borrowService.GetBorrowsForEmployee(employee.id).Count > 0) {
                throw new EmployeeException("Zaposlenik ima pohranjenih posudbi!");
            }

            if (archiveService.GetArchivesForEmployee(employee.id).Count > 0) {
                throw new EmployeeException("Zaposlenik je arhivirao neke knjige!");
            }

            return employeeRepository.Remove(employee);
            
        }

        public void CheckLoginCredentials(string username, string password) {
                var returned = employeeRepository.GetEmployeeLogin(username, password).ToList();

                if (returned.Count() == 1) {
                    LoggedUser.Username = username;
                    LoggedUser.UserType = Role.Employee;
                    LoggedUser.LibraryId = returned[0].Library_id;
                }
        }

        public int GetEmployeeLibraryId(string username) {
            return employeeRepository.GetEmployeeLibraryId(username);
        }

        public int GetEmployeeId(string username) {
            return employeeRepository.GetEmployeeId(username);
        }

        public Employee GetEmployeeByUsername(string username) {
            return employeeRepository.GetEmployeesById(GetEmployeeId(username)).FirstOrDefault();
        }

        ~EmployeeService()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                employeeRepository?.Dispose();
                borrowService?.Dispose();
                //archiveService?.Dispose(); jos nije implementirano
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
