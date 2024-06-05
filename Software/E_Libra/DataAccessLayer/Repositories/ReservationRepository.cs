using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    //Viktor Lovrić
    //Magdalena markovinović, metoda: GetReservationsForMemberNormal
    public class ReservationRepository: Repository<Reservation>
    {
        public ReservationRepository() : base(new DatabaseModel())
        {

        }
        
        public override int Update(Reservation entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public int CheckNumberOfReservations(int id)
        {
            var count = Entities.Count(r => r.Book_id == id);
            return count;
        }
        public int GetReservationPosition(int reservationId, int bookId)
        {
            var query = Entities.Count(r => r.idReservation <= reservationId && r.Book_id == bookId);
            return query;
        }
        public bool CheckExistingReservation(int bookId, int memberId)
        {
            var query = (from r in Entities where r.Book_id == bookId && r.Member_id == memberId select r).Any();
            return query;
        }
        public override int Add(Reservation entity, bool saveChanges = true)
        {
            var reservation = new Reservation
            {
                Member_id = entity.Member_id,
                Book_id = entity.Book_id,
            };
            Entities.Add(reservation);
            if(saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        public List<ReservationViewModel> GetReservationsForMember(int memberId)
        {
            var reservations = from r in Entities
                               where r.Member_id == memberId
                               join b in Context.Books on r.Book_id equals b.id
                               select new ReservationViewModel
                               {
                                   ReservationId = r.idReservation,
                                   BookName = b.name,
                                   Date = r.reservation_date
                               };

            return reservations.ToList();
        }
        public IQueryable<Reservation> GetReservationsForMemberNormal(int memberId)
        {
            var query = from r in Entities
                        where r.Member_id == memberId
                        select r;
            return query;
        }
        public Reservation GetReservationById(int reservationId)
        {
            var query = from e in Entities
                        where e.idReservation == reservationId
                        select e;
            return query.FirstOrDefault();
        }
        public int CountExistingReservations(int memberId)
        {
            var count = Entities.Count(r => r.Member_id == memberId);
            return count;
        }
        public bool EnterDateForReservation(Book book)
        {
            Book thisBook = Context.Books.Find(book.id);
            var reservation = Entities.Where(r => r.Book_id == thisBook.id && r.reservation_date == null)
                .OrderBy(r => r.idReservation)
                .FirstOrDefault();
            if (reservation != null)
            {
                reservation.reservation_date = DateTime.Now.AddDays(3);
                SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public IQueryable<Reservation> GetOverdueReservations()
        {
            var now = DateTime.Now;

            var overdueReservations = from r in Entities
                                      where r.reservation_date.HasValue && r.reservation_date.Value < now
                                      select r;
            return overdueReservations;
        }

        public int GetReservationId(int memberId, int bookId)
        {
            var query = (from r in Entities where r.Member_id == memberId && r.Book_id == bookId select r.idReservation).FirstOrDefault();
            return query;
        }
        public string ShowExistingReservations()
        {
            MemberRepository memberRepository = new MemberRepository();
            int memberId = memberRepository.GetMemberId(LoggedUser.Username);
            BookRepository bookRepository = new BookRepository();

            var userReservations = Entities
                .Where(r => r.Member_id == memberId && r.reservation_date != null)
                .ToList();

            if (userReservations.Count > 0)
            {
                StringBuilder sb = new StringBuilder("Podignite sljedeće rezervacije do prikazanog datuma:\n");

                foreach (var reservation in userReservations)
                {
                    sb.AppendLine($"Knjiga: {reservation.Book.name}, Datum: {reservation.reservation_date}");
                }
                return sb.ToString();
            }
            else return null;
        }
        public Reservation CheckValidReservationFroMember(int memberId, int bookId)
        {
            var query = (from r in Entities where (r.Member_id == memberId && r.Book_id == bookId && r.reservation_date != null) select r).FirstOrDefault();
            return query;
        }
    }
}
