using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services
{
    //Viktor Lovrić
    //Magdalena Markovinivić, metoda: GetReservationsForMemberNormal
    public class ReservationService
    {
        public int CheckNumberOfReservations(int id)
        {
            using(var repo = new ReservationRepository())
            {
                return repo.CheckNumberOfReservations(id);
            }
        }
        public bool CheckExistingReservation(int bookId, int memberId)
        {
            using (var repo = new ReservationRepository())
            {
                return repo.CheckExistingReservation(bookId, memberId);
            }
        }
        public int AddReservation(Reservation reservation)
        {
            using (var repo = new ReservationRepository())
            {
                return repo.Add(reservation);
            }
        }
        public List<ReservationViewModel> GetReservationForMember(int memberId)
        {
            using (var repo = new ReservationRepository())
            {
                return repo.GetReservationsForMember(memberId).ToList();
            }
        }
        public List<Reservation> GetReservationsForMemberNormal(int memberId)
        {
            using (var repo = new ReservationRepository())
            {
                return repo.GetReservationsForMemberNormal(memberId).ToList();
            }
        }
        public int CountExistingReservations(int memberId)
        {
            using(var repo = new ReservationRepository())
            {
                return repo.CountExistingReservations(memberId);
            }
        }
        public bool EnterDateForReservation(Book book)
        {
            using (var repo = new ReservationRepository())
            {
                return repo.EnterDateForReservation(book);
            }
        }
        public void CheckReservationDates()
        {
            var repo = new ReservationRepository();
            var overdueReservations = repo.GetOverdueReservations();
            var bookService = new BookServices();
            foreach (var reservation in overdueReservations)
            {

                repo.Remove(reservation);
                bookService.InsertOneCopy(reservation.Book);
                repo.EnterDateForReservation(reservation.Book);
            }

        }
        public void ReturnBook(Book book)
        {
            var repo = new ReservationRepository();
            var bookServices = new BookServices();
            bookServices.InsertOneCopy(book);
            repo.EnterDateForReservation(book);
        }
        public int GetReservationId(int memberId, int bookId)
        {
            using (var repo = new ReservationRepository())
            {
                return repo.GetReservationId(memberId, bookId);
            }
        }
        public int GetReservationPosition(int reservationId, int bookId)
        {
            using (var repo = new ReservationRepository())
            {
                return repo.GetReservationPosition(reservationId, bookId);
            }
        }
        public string ShowExistingReservations()
        {
            using (var repo = new ReservationRepository())
            {
                return repo.ShowExistingReservations();
            }
        }
        public Reservation CheckValidReservationFroMember(int memberId, int bookId)
        {
            using(var repo = new ReservationRepository())
            {
                return repo.CheckValidReservationFroMember(memberId, bookId);
            }
        }

        public int RemoveReservation(Reservation reservation, bool saveChanges = true) {
            using (var repo = new ReservationRepository()) {
                return repo.Remove(reservation, saveChanges);
            }
        }
        public bool RemoveReservationFromList(int reservationId)
        {
            var repo = new ReservationRepository();
            var bookService = new BookServices();
            var reservation = repo.GetReservationById(reservationId);

            if (reservation.reservation_date == null)
            {
                repo.Remove(reservation);
                bookService.InsertOneCopy(reservation.Book);
            }
            else
            {
                repo.Remove(reservation);
                bookService.InsertOneCopy(reservation.Book);
                repo.EnterDateForReservation(reservation.Book);
            }
            return true;
        }
    }
}
