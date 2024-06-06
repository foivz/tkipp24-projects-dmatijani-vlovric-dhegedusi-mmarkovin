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
    // David Matijanić
    public class LibraryService {

        private ILibraryRepository libraryRepository { get; set; }
        private EmployeeService employeeService { get; set; }
        private MemberService memberService { get; set; }
        private BookServices bookService { get; set; }
        private NotificationService notificationService { get; set; }

        public LibraryService(
            ILibraryRepository libraryRepository,
            EmployeeService employeeService,
            MemberService memberService,
            BookServices bookService,
            NotificationService notificationService
        ) {
            this.libraryRepository = libraryRepository;
            this.employeeService = employeeService;
            this.memberService = memberService;
            this.bookService = bookService;
            this.notificationService = notificationService;
        }

        public LibraryService(): this(
            new LibraryRepository(),
            new EmployeeService(),
            new MemberService(),
            new BookServices(),
            new NotificationService()
        ) { }

        public List<Library> GetAllLibraries() {
            return libraryRepository.GetAll().ToList();
        }

        public async Task<List<Library>> GetAllLibrariesAsync() {
            return await libraryRepository.GetAllLibrariesAsync();
        }

        public int AddLibrary(Library newLibrary) {
            var librariesWithId = libraryRepository.GetLibrariesById(newLibrary.id);
            if (librariesWithId.ToList().Count > 0) {
                throw new LibraryWithSameIDException("Knjižnica sa istim ID već postoji!");
            }

            var librariesWithOIB = libraryRepository.GetLibrariesByOIB(newLibrary.OIB);
            if (librariesWithOIB.ToList().Count > 0) {
                throw new LibraryWithSameOIBException("Knjižnica sa istim OIB već postoji!");
            }

            return libraryRepository.Add(newLibrary);
        }

        public int DeleteLibrary(Library library) {
            if (employeeService.GetEmployeesByLibrary(library).Count > 0) {
                throw new LibraryHasEmployeesException("Odabrana knjižnica ima zaposlenike!");
            }

            if (memberService.GetMembersByLibrary(library.id).Count > 0) {
                throw new LibraryHasMembersException("Odabrana knjižnica ima članove!");
            }

            if (bookService.GetBooksByLibrary(library.id).Count > 0) {
                throw new LibraryHasBooksException("Odabrana knjižnica ima knjige!");
            }

            if (notificationService.GetAllNotificationByLibrary(library.id).Count > 0) {
                throw new LibraryException("Odabrana knjižnica ima notifikacije!");
            }

            return libraryRepository.Remove(library);
        }

        public int UpdateLibrary(Library library) {
            var librariesWithId = libraryRepository.GetLibrariesById(library.id);
            if (librariesWithId.ToList().Count == 0) {
                throw new LibraryWithSameIDException("Knjižnica sa tim ID ne postoji!");
            }

            var librariesWithOIB = libraryRepository.GetLibrariesByOIB(library.OIB);
            List<Library> otherLibrariesWithOIB = librariesWithOIB.ToList().FindAll(l => l.id != library.id);
            if (otherLibrariesWithOIB.Count > 0) {
                throw new LibraryWithSameOIBException("Druga knjižnica već ima taj OIB!");
            }

            return libraryRepository.Update(library);
        }

        public decimal GetLibraryPriceDayLate(Library library) {
            return libraryRepository.GetLibraryPriceDayLate(library.id);
        }

        public decimal GetLibraryPriceDayLate(int libraryId) {
            return libraryRepository.GetLibraryPriceDayLate(libraryId);
        }

        public decimal GetLibraryMembershipDuration(int libraryId) {
            DateTime returnedValue = libraryRepository.GetLibraryMembershipDuration(libraryId);
            return CalculateMembershipDurationFromDate(returnedValue);
        }

        private decimal CalculateMembershipDurationFromDate(DateTime date) {
            return (date - new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Unspecified)).Days + 1;
        }
    }
}
