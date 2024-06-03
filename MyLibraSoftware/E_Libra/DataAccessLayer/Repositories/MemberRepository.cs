using EntitiesLayer;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    // Viktor Lovrić, metode: GetMemberId
    // Magdalena Markovinović, metode: GetMemberByBarcodeId, GetMemberBarcode, GetAllMembersByFilter, UpdateMembershipDate, DeleteMember, GetMemberLogin
    // Domagoj Hegedušić, metode: GetMemberCount
    public class MemberRepository : Repository<Member>
    {
        public DbSet<Member> Member { get; set; }
        public MemberRepository() : base(new DatabaseModel())
        {
            Member = Context.Set<Member>();
        }
        public IQueryable<Member> GetMemberLogin(string username, string password)
        {
            var sql = from m in Member
                      where m.username == username && m.password == password
                      select m;
            return sql;
        }
        public IQueryable<Member> GetMembersByUsername(string username)
        {
            var query = from m in Entities
                        where m.username == username
                        select m;

            return query;
        }
        public IQueryable<Member> GetAllMembersByFilter(string name, string surname)
        {
            var query = from m in Entities
                        where m.name == name || m.surname == surname
                        select m;

            return query;
        }
        public int UpdateMembershipDate(Member member, DateTime dateNow, bool saveChanges = true)
        {
            var existingMembeer = Entities.SingleOrDefault(m => m.id == member.id);
            existingMembeer.membership_date = dateNow;
            if (saveChanges)
            {
                return SaveChanges();
            } else
            {
                return 0;
            }
        }

        public int DeleteMember(int memberId, bool saveChanges = true)
        {
            var m = Entities.Find(memberId);
            Context.Members.Attach(m);
            m.Notifications.Clear();
            m.Reviews.Clear();
            m.Books.Clear();
            m.Borrows.Clear();
            m.Reservations.Clear();
            Context.Members.Remove(m);
            if (saveChanges)
            {
                return SaveChanges();
            } else
            {
                return 0;
            }
        }

        public IQueryable<Member> GetMembersByLibrary(int libraryID)
        {
            var query = from m in Entities
                        where m.Library_id == libraryID
                        select m;

            return query;
        }
        public int GetMemberLibraryId(string username)
        {
            var sql = (from m in Entities.Include("Library") where m.username == username select m.Library_id).FirstOrDefault();
            return sql;
        }
        public override int Update(Member entity, bool saveChanges = true)
        {
            var existingMembeer = Entities.SingleOrDefault(m => m.id == entity.id);
            existingMembeer.name = entity.name;
            existingMembeer.surname = entity.surname;
            existingMembeer.password = entity.password;
            if (saveChanges)
            {
                return SaveChanges();
            } else
            {
                return 0;
            }
        }
        public int GetMemberId(string username)
        {
            var sql = from m in Member where m.username == username select m.id;
            return sql.FirstOrDefault();
        }

        public IQueryable<string> GetMemberNameSurname(int memberId)
        {
            var sql = from m in Member
                      where m.id == memberId
                      select $"{m.name} {m.surname}";
            return sql;
        }

        public int GetMemberCount(int Library_id)
        {
            var sql = Member.Count(m => m.Library_id == Library_id);
            return sql;
        }

        public IQueryable<Member> GetMemberByBarcodeId(string barcodeId)
        {
            var query = from m in Member.Include("Library")
                        where m.barcode_id == barcodeId
                        select m;

            return query;
        }

        public IQueryable<string> GetMemberBarcode(int id) {
            var query = from m in Member
                        where m.id == id
                        select m.barcode_id;

            return query;
        }
    }
}