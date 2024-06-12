using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer
{
    public interface IMemberService : IDisposable
    {
        void CheckLoginCredentials(string username, string password);
        bool CheckMembershipDateLogin(string username, string password);
        bool CheckExistingUsername(Member member);
        bool CheckBarcodeUnique(Member member);
        bool CheckOibUnique(Member member);
        bool AddNewMember(Member member);
        bool UpdateMember(Member member);
        bool DeleteMember(Member member);
        List<Member> GetAllMembersByFilter(string name, string surname);
        int GetMemberId(string username);
        IQueryable<string> GetMemberNameSurname(int memberId);
        int GetMemberLibraryId(string username);
        Member GetMemberByUsername(string username);
        List<Member> GetAllMembersByLibrary();
        bool RestoreMembership(Member member);
        string RandomCodeGenerator();
        Member GetMemberByBarcodeId(int libraryId, string barcodeId);
        string GetMemberBarcode(int id);
        List<Member> GetMembersByLibrary(int libraryId);
        decimal GetLibraryMembershipDuration(int libraryId);
    }
}
