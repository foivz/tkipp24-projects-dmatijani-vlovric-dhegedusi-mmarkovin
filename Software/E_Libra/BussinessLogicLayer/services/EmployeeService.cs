using BussinessLogicLayer.Exceptions;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services {
    // David Matijanić: GetEmployeesByLibrary, AddEmployee, UpdateEmployee, DeleteEmployee, GetEmployeeByUsername
    public class EmployeeService {
        public List<Employee> GetEmployeesByLibrary(Library library) {
            using (var repository = new EmployeeRepository()) {
                return repository.GetEmployeesByLibrary(library).ToList();
            }
        }

        public int AddEmployee(Employee newEmployee) {
            using (var repository = new EmployeeRepository()) {
                var employeesWithId = repository.GetEmployeesById(newEmployee.id);
                if (employeesWithId.ToList().Count > 0) {
                    throw new EmployeeWithSameIDException("Zaposlenik sa istim ID već postoji!");
                }

                var employeesWithUsername = repository.GetEmployeesByUsername(newEmployee.username);
                if (employeesWithUsername.ToList().Count > 0) {
                    throw new EmployeeWithSameUsernameException("Zaposlenik sa istim korisničkim imenom već postoji!");
                }

                var employeesWithOIB = repository.GetEmployeesByOIB(newEmployee.OIB);
                if (employeesWithOIB.ToList().Count > 0) {
                    throw new EmployeeWithSameOIBException("Zaposlenik sa istim OIB već postoji!");
                }

                return repository.Add(newEmployee);
            }
        }

        public int UpdateEmployee(Employee employee) {
            using (var repository = new EmployeeRepository()) {
                var employeesWithId = repository.GetEmployeesById(employee.id);
                var otherEmployeesWithId = employeesWithId.ToList().FindAll(e => e.OIB != employee.OIB);
                if (otherEmployeesWithId.Count > 0) {
                    throw new EmployeeWithSameIDException("Zaposlenik sa tim ID već postoji!");
                }

                var employeesWithOIB = repository.GetEmployeesByOIB(employee.OIB);
                if (employeesWithOIB.ToList().Count == 0) {
                    throw new EmployeeWithSameOIBException("Ne postoji zaposlenik sa odabranim OIB!");
                }

                var employeesWithUsername = repository.GetEmployeesByUsername(employee.username);
                var otherEmployeesWithUsername = employeesWithUsername.ToList().FindAll(e => e.username != employee.username);
                if (otherEmployeesWithUsername.ToList().Count == 0) {
                    throw new EmployeeWithSameUsernameException("Zaposlenik sa tim korisničkim imenom već postoji!");
                }

                return repository.Update(employee);
            }
        }

        public int DeleteEmployee(Employee employee) {
            BorrowService borrowService = new BorrowService();
            if (borrowService.GetBorrowsForEmployee(employee.id).Count > 0) {
                throw new EmployeeException("Zaposlenik ima pohranjenih posudbi!");
            }

            ArchiveServices archiveService = new ArchiveServices();
            if (archiveService.GetArchivesForEmployee(employee.id).Count > 0) {
                throw new EmployeeException("Zaposlenik je arhivirao neke knjige!");
            }

            using (var repository = new EmployeeRepository()) {
                return repository.Remove(employee);
            }
        }

        public void CheckLoginCredentials(string username, string password) {
            using (var employeeRepo = new EmployeeRepository()) {
                var returned = employeeRepo.GetEmployeeLogin(username, password).ToList();

                if (returned.Count() == 1) {
                    LoggedUser.Username = username;
                    LoggedUser.UserType = Role.Employee;
                    LoggedUser.LibraryId = returned[0].Library_id;
                }
            }
        }

        public int GetEmployeeLibraryId(string username) {
            using (var employeeRepo = new EmployeeRepository()) {
                return employeeRepo.GetEmployeeLibraryId(username);
            }
        }

        public int GetEmployeeId(string username) {
            using (var employeeRepo = new EmployeeRepository()) {
                return employeeRepo.GetEmployeeId(username);
            }
        }

        public Employee GetEmployeeByUsername(string username) {
            using (var repository = new EmployeeRepository()) {
                return repository.GetEmployeesById(GetEmployeeId(username)).FirstOrDefault();
            }
        }
    }
}
