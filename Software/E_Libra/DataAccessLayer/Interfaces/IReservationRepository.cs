using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IReservationRepository
    {
        int CheckNumberOfReservations(int id);
        int GetReservationPosition(int reservationId, int bookId);
        bool CheckExistingReservation(int bookId, int memberId);
        int Add(Reservation entity, bool saveChanges = true);
        List<ReservationViewModel> GetReservationsForMember(int memberId);
        IQueryable<Reservation> GetReservationsForMemberNormal(int memberId);
        Reservation GetReservationById(int reservationId);
        int CountExistingReservations(int memberId);
        bool EnterDateForReservation(Book book);
        IQueryable<Reservation> GetOverdueReservations();
        int GetReservationId(int memberId, int bookId);
        string ShowExistingReservations();
        Reservation CheckValidReservationFroMember(int memberId, int bookId);
        int Remove(Reservation reservation, bool saveChanges = true);
        int SaveChanges();
    }
}
