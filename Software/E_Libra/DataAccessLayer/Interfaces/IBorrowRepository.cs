using DataAccessLayer.Exceptions;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces {
    public interface IBorrowRepository : IDisposable {
        IQueryable<Borrow> GetAllBorrowsForMember(int member_id, int library_id);

        IQueryable<Borrow> GetBorrowsForMemberByStatus(int member_id, int library_id, BorrowStatus status);

        IQueryable<Borrow> GetBorrowsForMemberAndBook(int member_id, int book_id, int library_id);

        Task<List<Borrow>> GetAllBorrowsForLibraryAsync(Library library);

        Task<List<Borrow>> GetAllBorrowsForLibraryAsync(int library_id);

        Task<List<Borrow>> GetBorrowsForLibraryByStatusAsync(Library library, BorrowStatus status);

        Task<List<Borrow>> GetBorrowsForLibraryByStatusAsync(int library_id, BorrowStatus status);

        int Add(Borrow borrow, bool saveChanges = true);

        int Update(Borrow borrow, bool saveChanges = true);

        bool HasUserBorrowedBook(int userId, int bookId);

        IQueryable<Borrow> GetBorrowsForEmployee(int employeeId);
    }
}
