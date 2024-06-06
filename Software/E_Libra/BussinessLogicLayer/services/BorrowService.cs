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
    // David Matijanić: osim HasUserBorrowedBook
    public class BorrowService {
        IBorrowRepository borrowRepository { get; set; }
        ReservationService reservationService { get; set; }
        BookServices bookService { get; set; }

        public BorrowService(
            IBorrowRepository borrowRepository,
            ReservationService reservationService,
            BookServices bookService
        ) {
            this.borrowRepository = borrowRepository;
            this.reservationService = reservationService;
            this.bookService = bookService;
        }
        public BorrowService() : this(
            new BorrowRepository(),
            new ReservationService(),
            new BookServices()
        ) { }

        public List<Borrow> GetAllBorrowsForMember(int member_id, int library_id) {
            return borrowRepository.GetAllBorrowsForMember(member_id, library_id).ToList();
        }

        public List<Borrow> GetBorrowsForMemberByStatus(int member_id, int library_id, BorrowStatus status) {
            return borrowRepository.GetBorrowsForMemberByStatus(member_id, library_id, status).ToList();
        }

        public List<Borrow> GetBorrowsForMemberAndBook(int member_id, int book_id, int library_id) {
            return borrowRepository.GetBorrowsForMemberAndBook(member_id, book_id, library_id).ToList();
        }

        public async Task<List<Borrow>> GetAllBorrowsForLibraryAsync(Library library) {
            return await borrowRepository.GetAllBorrowsForLibraryAsync(library);
        }

        public async Task<List<Borrow>> GetAllBorrowsForLibraryAsync(int library_id) {
            return await borrowRepository.GetAllBorrowsForLibraryAsync(library_id);
        }

        public async Task<List<Borrow>> GetBorrowsForLibraryByStatusAsync(Library library, BorrowStatus status) {
            return await borrowRepository.GetBorrowsForLibraryByStatusAsync(library, status);
        }

        public async Task<List<Borrow>> GetBorrowsForLibraryByStatusAsync(int library_id, BorrowStatus status) {
            return await borrowRepository.GetBorrowsForLibraryByStatusAsync(library_id, status);
        }

        public int AddNewBorrow(Borrow borrow) {
            Book book = borrow.Book;
            bool reserved = false;
            if (borrow.borrow_status == (int)BorrowStatus.Borrowed) {
                if (book.current_copies < 1) {
                    Reservation existingReservation = reservationService.CheckValidReservationFroMember(borrow.Member.id, book.id);
                    if (existingReservation == null) {
                        throw new NoMoreBookCopiesException("Odabrane knjige trenutno nema na stanju!");
                    } else {
                        reserved = true;
                        reservationService.RemoveReservation(existingReservation);
                    }
                }

                if (!reserved) {
                    LowerBookCopies(book);
                }
            }

            return borrowRepository.Add(borrow);
        }

        public int UpdateBorrow(Borrow borrow) {
            Book book = borrow.Book;
            if (borrow.borrow_status == (int)BorrowStatus.Borrowed) {
                if (book.current_copies < 1) {
                    throw new NoMoreBookCopiesException("Odabrane knjige trenutno nema na stanju!");
                }
                LowerBookCopies(book);
            } else if (borrow.borrow_status == (int)BorrowStatus.Returned) {
                reservationService.ReturnBook(book);
            }

            return borrowRepository.Update(borrow);
        }

        public bool HasUserBorrowedBook(int userId, int bookId) {
            return borrowRepository.HasUserBorrowedBook(userId, bookId);
        }

        public List<Borrow> GetBorrowsForEmployee(int employeeId) {
            return borrowRepository.GetBorrowsForEmployee(employeeId).ToList();
        }

        private void LowerBookCopies(Book book) {
            book.current_copies--;
            bookService.UpdateBook(book);
        }
    }
}
