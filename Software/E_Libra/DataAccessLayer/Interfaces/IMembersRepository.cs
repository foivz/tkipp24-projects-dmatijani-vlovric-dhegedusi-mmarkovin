using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IMembersRepository
    {
        IQueryable<Member> GetMemberLogin(string username, string password);
        IQueryable<Member> GetMembersByUsername(string username);
        IQueryable<Member> GetAllMembersByFilter(string name, string surname);
        int UpdateMembershipDate(Member member, DateTime dateNow, bool saveChanges = true);
        int DeleteMember(int memberId, bool saveChanges = true);
        IQueryable<Member> GetMembersByLibrary(int libraryID);
        int GetMemberLibraryId(string username);
        int Update(Member entity, bool saveChanges = true);
        int GetMemberId(string username);
        IQueryable<string> GetMemberNameSurname(int memberId);
        int GetMemberCount(int Library_id);
        IQueryable<Member> GetMemberByBarcodeId(string barcodeId);
        IQueryable<string> GetMemberBarcode(int id);
        IQueryable<Member> GetAll();
        int Add(Member member);
    }
}
