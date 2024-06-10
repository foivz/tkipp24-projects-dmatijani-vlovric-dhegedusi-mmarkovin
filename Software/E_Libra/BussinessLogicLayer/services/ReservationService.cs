using DataAccessLayer.Interfaces;
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
        public IReservationRepository reservationRepository { get; set; }
        public BookServices bookService { get; set; }
        public ReservationService(
            IReservationRepository reservationRepository,
            BookServices bookServices
        )
        {
            this.reservationRepository = reservationRepository;
            this.bookService = bookServices;
        }
        public ReservationService() : this(
            new ReservationRepository(),
            new BookServices()
        ) { }


        public int CheckNumberOfReservations(int id)
        {
            return reservationRepository.CheckNumberOfReservations(id);
        }
        public bool CheckExistingReservation(int bookId, int memberId)
        {
            return reservationRepository.CheckExistingReservation(bookId, memberId);
        }
        public int AddReservation(Reservation reservation)
        {
            return reservationRepository.Add(reservation);
        }
        public List<ReservationViewModel> GetReservationForMember(int memberId)
        {
            return reservationRepository.GetReservationsForMember(memberId).ToList();
        }
        public List<Reservation> GetReservationsForMemberNormal(int memberId)
        {
            return reservationRepository.GetReservationsForMemberNormal(memberId).ToList();
        }
        public int CountExistingReservations(int memberId)
        {
            return reservationRepository.CountExistingReservations(memberId);
        }
        public bool EnterDateForReservation(Book book)
        {
            return reservationRepository.EnterDateForReservation(book);
        }
        public void CheckReservationDates()
        {
            var overdueReservations = reservationRepository.GetOverdueReservations();
            foreach (var reservation in overdueReservations)
            {
                bookService.InsertOneCopy(reservation.Book);
                reservationRepository.EnterDateForReservation(reservation.Book);
                reservationRepository.Remove(reservation, false);
            }
            reservationRepository.SaveChanges();
        }
        public void ReturnBook(Book book)
        {
            bookService.InsertOneCopy(book);
            reservationRepository.EnterDateForReservation(book);
        }
        public int GetReservationId(int memberId, int bookId)
        {
            return reservationRepository.GetReservationId(memberId, bookId);
        }
        public int GetReservationPosition(int reservationId, int bookId)
        {
            return reservationRepository.GetReservationPosition(reservationId, bookId);
        }
        public string ShowExistingReservations()
        {
            return reservationRepository.ShowExistingReservations();
        }
        public Reservation CheckValidReservationFroMember(int memberId, int bookId)
        {
            return reservationRepository.CheckValidReservationFroMember(memberId, bookId);
        }

        public int RemoveReservation(Reservation reservation, bool saveChanges = true)
        {
            return reservationRepository.Remove(reservation, saveChanges);
        }
        public bool RemoveReservationFromList(int reservationId)
        {
            var reservation = reservationRepository.GetReservationById(reservationId);
            Book book = reservation.Book;
            if (reservation.reservation_date == null)
            {
                reservationRepository.Remove(reservation);
                bookService.InsertOneCopy(book);
            }
            else
            {
                reservationRepository.Remove(reservation);
                bookService.InsertOneCopy(book);
                reservationRepository.EnterDateForReservation(book);
            }
            return true;
        }
    }
}
