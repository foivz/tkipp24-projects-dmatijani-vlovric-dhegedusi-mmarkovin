using BussinessLogicLayer.Exceptions;
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
        public List<Borrow> GetAllBorrowsForMember(int member_id, int library_id) {
            using (var context = new BorrowRepository()) {
                return context.GetAllBorrowsForMember(member_id, library_id).ToList();
            }
        }

        public List<Borrow> GetBorrowsForMemberByStatus(int member_id, int library_id, BorrowStatus status) {
            using (var context = new BorrowRepository()) {
                return context.GetBorrowsForMemberByStatus(member_id, library_id, status).ToList();
            }
        }

        public List<Borrow> GetBorrowsForMemberAndBook(int member_id, int book_id, int library_id) {
            using (var context = new BorrowRepository()) {
                return context.GetBorrowsForMemberAndBook(member_id, book_id, library_id).ToList();
            }
        }

        public async Task<List<Borrow>> GetAllBorrowsForLibraryAsync(Library library) {
            using (var context = new BorrowRepository()) {
                return await context.GetAllBorrowsForLibraryAsync(library);
            }
        }

        public async Task<List<Borrow>> GetAllBorrowsForLibraryAsync(int library_id) {
            using (var context = new BorrowRepository()) {
                return await context.GetAllBorrowsForLibraryAsync(library_id);
            }
        }

        public async Task<List<Borrow>> GetBorrowsForLibraryByStatusAsync(Library library, BorrowStatus status) {
            using (var context = new BorrowRepository()) {
                return await context.GetBorrowsForLibraryByStatusAsync(library, status);
            }
        }

        public async Task<List<Borrow>> GetBorrowsForLibraryByStatusAsync(int library_id, BorrowStatus status) {
            using (var context = new BorrowRepository()) {
                return await context.GetBorrowsForLibraryByStatusAsync(library_id, status);
            }
        }

        public int AddNewBorrow(Borrow borrow) {
            Book book = borrow.Book;
            bool reserved = false;
            if (borrow.borrow_status == (int)BorrowStatus.Borrowed) {
                if (book.current_copies < 1) {
                    ReservationService reservationService = new ReservationService();
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

            using (var context = new BorrowRepository()) {
                return context.Add(borrow);
            }
        }

        public int UpdateBorrow(Borrow borrow) {
            Book book = borrow.Book;
            if (borrow.borrow_status == (int)BorrowStatus.Borrowed) {
                if (book.current_copies < 1) {
                    throw new NoMoreBookCopiesException("Odabrane knjige trenutno nema na stanju!");
                }
                LowerBookCopies(book);
            } else if (borrow.borrow_status == (int)BorrowStatus.Returned) {
                ReservationService reservationService = new ReservationService();
                reservationService.ReturnBook(book);
            }

            using (var context = new BorrowRepository()) {
                return context.Update(borrow);
            }
        }

        public bool HasUserBorrowedBook(int userId, int bookId) {
            using (var context = new BorrowRepository()) {
                return context.HasUserBorrowedBook(userId, bookId);
            }
        }

        public List<Borrow> GetBorrowsForEmployee(int employeeId) {
            using (var context = new BorrowRepository()) {
                return context.GetBorrowsForEmployee(employeeId).ToList();
            }
        }

        private void LowerBookCopies(Book book) {
            BookServices bookService = new BookServices();
            book.current_copies--;
            bookService.UpdateBook(book);
        }
    }
}
