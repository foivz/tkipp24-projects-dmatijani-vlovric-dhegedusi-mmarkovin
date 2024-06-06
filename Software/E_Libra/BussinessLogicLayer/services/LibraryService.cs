using BussinessLogicLayer.Exceptions;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services {
    // David Matijanić
    public class LibraryService {
        public List<Library> GetAllLibraries() {
            using (var repository = new LibraryRepository()) {
                return repository.GetAll().ToList();
            }
        }

        public async Task<List<Library>> GetAllLibrariesAsync() {
            using (var repository = new LibraryRepository()) {
                return await repository.GetAllLibrariesAsync();
            }
        }

        public int AddLibrary(Library newLibrary) {
            using (var repository = new LibraryRepository()) {
                var librariesWithId = repository.GetLibrariesById(newLibrary.id);
                if (librariesWithId.ToList().Count > 0) {
                    throw new LibraryWithSameIDException("Knjižnica sa istim ID već postoji!");
                }

                var librariesWithOIB = repository.GetLibrariesByOIB(newLibrary.OIB);
                if (librariesWithOIB.ToList().Count > 0) {
                    throw new LibraryWithSameOIBException("Knjižnica sa istim OIB već postoji!");
                }

                return repository.Add(newLibrary);
            }
        }

        public int DeleteLibrary(Library library) {
            using (var repository = new LibraryRepository()) {
                EmployeeService employeeService = new EmployeeService();
                if (employeeService.GetEmployeesByLibrary(library).Count > 0) {
                    throw new LibraryHasEmployeesException("Odabrana knjižnica ima zaposlenike!");
                }

                MemberService memberService = new MemberService();
                if (memberService.GetMembersByLibrary(library.id).Count > 0) {
                    throw new LibraryHasMembersException("Odabrana knjižnica ima članove!");
                }

                BookServices bookService = new BookServices();
                if (bookService.GetBooksByLibrary(library.id).Count > 0) {
                    throw new LibraryHasBooksException("Odabrana knjižnica ima knjige!");
                }

                NotificationService notificationService = new NotificationService();
                if (notificationService.GetAllNotificationByLibrary(library.id).Count > 0) {
                    throw new LibraryException("Odabrana knjižnica ima notifikacije!");
                }

                return repository.Remove(library);
            }
        }

        public int UpdateLibrary(Library library) {
            using (var repository = new LibraryRepository()) {
                var librariesWithId = repository.GetLibrariesById(library.id);
                if (librariesWithId.ToList().Count == 0) {
                    throw new LibraryWithSameIDException("Knjižnica sa tim ID ne postoji!");
                }

                var librariesWithOIB = repository.GetLibrariesByOIB(library.OIB);
                List<Library> otherLibrariesWithOIB = librariesWithOIB.ToList().FindAll(l => l.id != library.id);
                if (otherLibrariesWithOIB.Count > 0) {
                    throw new LibraryWithSameOIBException("Druga knjižnica već ima taj OIB!");
                }

                return repository.Update(library);
            }
        }

        public decimal GetLibraryPriceDayLate(Library library) {
            using (var repository = new LibraryRepository()) {
                return repository.GetLibraryPriceDayLate(library.id);
            } 
        }

        public decimal GetLibraryPriceDayLate(int libraryId) {
            using (var repository = new LibraryRepository()) {
                return repository.GetLibraryPriceDayLate(libraryId);
            }
        }

        public decimal GetLibraryMembershipDuration(int libraryId) {
            using (var repository = new LibraryRepository()) {
                DateTime returnedValue = repository.GetLibraryMembershipDuration(libraryId);
                return CalculateMembershipDurationFromDate(returnedValue);
            }
        }

        private decimal CalculateMembershipDurationFromDate(DateTime date) {
            return (date - new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Unspecified)).Days + 1;
        }
    }
}
