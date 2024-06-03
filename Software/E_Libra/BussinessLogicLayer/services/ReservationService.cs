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
        public int Add(Reservation reservation)
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
        public bool RemoveReservation(int reservationId)
        {
            using (var repo = new ReservationRepository())
            {
                int res = repo.RemoveReservation(reservationId);
                if (res == 1)
                {
                    return true;
                }
                else return false;
            }
        }
        public int CountExistingReservations(int memberId)
        {
            using(var repo = new ReservationRepository())
            {
                return repo.CountExistingReservations(memberId);
            }
        }
        public void SetReservationEndDateAndAddCopies(Book book, int current, int received)
        {
            using (var repo = new ReservationRepository())
            {
                repo.SetReservationEndDateAndAddCopies(book, current, received);
            }
        }
        public void CheckReservationDates()
        {
            using (var repo = new ReservationRepository())
            {
                repo.CheckReservationDates();
            }
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
    }
}
