using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories {
    // David Matijanić: sve osim HasUserBorrowedBook
    public class BorrowRepository : Repository<Borrow> {
        public BorrowRepository() : base(new DatabaseModel()) {

        }

        public IQueryable<Borrow> GetAllBorrowsForMember(int member_id, int library_id) {
            UpdateBorrowStatus(library_id);

            var query = from b in Entities.Include("Member").Include("Book")
                        where b.Member.id == member_id
                        select b;

            return query;
        }

        public IQueryable<Borrow> GetBorrowsForMemberByStatus(int member_id, int library_id, BorrowStatus status) {
            UpdateBorrowStatus(library_id);

            var query = from b in Entities.Include("Member").Include("Book")
                        where b.Member.id == member_id && b.borrow_status == (int)status
                        select b;

            return query;
        }

        public IQueryable<Borrow> GetBorrowsForMemberAndBook(int member_id, int book_id, int library_id) {
            UpdateBorrowStatus(library_id);

            var query = from b in Entities.Include("Member").Include("Book").Include("Employee")
                        where b.Member.id == member_id && b.Book.id == book_id
                        select b;

            return query;
        }

        public async Task<List<Borrow>> GetAllBorrowsForLibraryAsync(Library library) {
            await UpdateBorrowStatusAsync(library.id);

            var query = from b in Entities.Include("Member").Include("Book").Include("Employee")
                        where b.Book.Library_id == library.id
                        select b;

            return await query.ToListAsync();
        }

        public async Task<List<Borrow>> GetAllBorrowsForLibraryAsync(int library_id) {
            await UpdateBorrowStatusAsync(library_id);

            var query = from b in Entities.Include("Member").Include("Book").Include("Employee")
                        where b.Book.Library_id == library_id
                        select b;

            return await query.ToListAsync();
        }

        public async Task<List<Borrow>> GetBorrowsForLibraryByStatusAsync(Library library, BorrowStatus status) {
            await UpdateBorrowStatusAsync(library.id);

            var query = from b in Entities.Include("Member").Include("Book").Include("Employee")
                        where b.Book.Library_id == library.id && b.borrow_status == (int)status
                        select b;

            return await query.ToListAsync();
        }

        public async Task<List<Borrow>> GetBorrowsForLibraryByStatusAsync(int library_id, BorrowStatus status) {
            await UpdateBorrowStatusAsync(library_id);

            var query = from b in Entities.Include("Member").Include("Book").Include("Employee")
                        where b.Book.Library_id == library_id && b.borrow_status == (int)status
                        select b;

            return await query.ToListAsync();
        }

        private async Task UpdateBorrowStatusAsync(int library_id) {
            var allBorrows = from b in Entities.Include("Book")
                             where b.Book.Library_id == library_id
                             select b;

            List<Borrow> borrowsToRemove = new List<Borrow>();
            List<Borrow> borrowsToUpdate = new List<Borrow>();

            foreach (Borrow borrow in allBorrows) {
                switch (borrow.borrow_status) {
                    case ((int)BorrowStatus.Waiting):
                        if (borrow.return_date < DateTime.Now) {
                            borrowsToRemove.Add(borrow);
                        }
                        break;

                    case ((int)BorrowStatus.Borrowed):
                        if (borrow.return_date < DateTime.Now) {
                            borrow.borrow_status = (int)BorrowStatus.Late;
                            borrowsToUpdate.Add(borrow);
                        }
                        break;
                }
            }

            foreach (Borrow borrow in borrowsToRemove) {
                Remove(borrow);
            }

            foreach (Borrow borrow in borrowsToUpdate) {
                Update(borrow);
            }

            await Task.CompletedTask;
        }

        private void UpdateBorrowStatus(int library_id) {
            var allBorrows = from b in Entities.Include("Book")
                             where b.Book.Library_id == library_id
                             select b;

            List<Borrow> borrowsToRemove = new List<Borrow>();
            List<Borrow> borrowsToUpdate = new List<Borrow>();

            foreach (Borrow borrow in allBorrows) {
                switch (borrow.borrow_status) {
                    case ((int)BorrowStatus.Waiting):
                        if (borrow.return_date < DateTime.Now) {
                            borrowsToRemove.Add(borrow);
                        }
                        break;

                    case ((int)BorrowStatus.Borrowed):
                        if (borrow.return_date < DateTime.Now) {
                            borrow.borrow_status = (int)BorrowStatus.Late;
                            borrowsToUpdate.Add(borrow);
                        }
                        break;
                }
            }

            foreach (Borrow borrow in borrowsToRemove) {
                Remove(borrow);
            }

            foreach (Borrow borrow in borrowsToUpdate) {
                Update(borrow);
            }
        }

        public override int Add(Borrow borrow, bool saveChanges = true) {
            var book = Context.Books.SingleOrDefault(b => b.id == borrow.Book.id);
            var member = Context.Members.SingleOrDefault(m => m.id == borrow.Member.id);
            
            var newBorrow = new Borrow {
                Book = book,
                Member = member,
                borrow_date = borrow.borrow_date,
                return_date = borrow.return_date,
                borrow_status = borrow.borrow_status
            };
            
            if (borrow.Employee != null) {
                var employee = Context.Employees.SingleOrDefault(e => e.id == borrow.Employee.id);
                newBorrow.Employee = employee;
            } else {
                Employee employee = Context.Employees.Where(e => e.Library_id == member.Library_id).First();
                newBorrow.Employee = employee;
            }

            if (borrow.Employee1 != null) {
                var employeeReturn = Context.Employees.SingleOrDefault(e => e.id == borrow.Employee1.id);
                newBorrow.Employee1 = employeeReturn;
            }

            Entities.Add(newBorrow);
            if (saveChanges) {
                return SaveChanges();
            } else {
                return 0;
            }
        }

        public override int Update(Borrow borrow, bool saveChanges = true) {
            var book = Context.Books.SingleOrDefault(b => b.id == borrow.Book.id);
            var member = Context.Members.SingleOrDefault(m => m.id == borrow.Member.id);
            var employee = Context.Employees.SingleOrDefault(e => e.id == borrow.Employee.id);

            var existingBorrow = Context.Borrows.SingleOrDefault(b => b.idBorrow == borrow.idBorrow);
            existingBorrow.borrow_date = borrow.borrow_date;
            existingBorrow.return_date = borrow.return_date;
            existingBorrow.borrow_status = borrow.borrow_status;
            existingBorrow.Book = book;
            existingBorrow.Member = member;
            existingBorrow.Employee = employee;
            if (borrow.Employee1 != null) {
                var employeeReturn = Context.Employees.SingleOrDefault(e => e.id == borrow.Employee1.id);
                existingBorrow.Employee1 = employeeReturn;
            }

            if (saveChanges) {
                return SaveChanges();
            } else {
                return 0;
            }
        }

        public bool HasUserBorrowedBook(int userId, int bookId) {
            var result = Entities
                .Where(b => b.Member_id == userId && b.Book_id == bookId && b.borrow_status != 0)
                .FirstOrDefault();

            return result != null;
        }

        public IQueryable<Borrow> GetBorrowsForEmployee(int employeeId) {
            var query = from b in Entities.Include("Employee")
                        where b.Employee_borrow_id == employeeId || b.Employee_return_id == employeeId
                        select b;

            return query;
        }
    }
}
