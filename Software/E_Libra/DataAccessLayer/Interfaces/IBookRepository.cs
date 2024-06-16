using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.Repositories.BookRepository;

namespace DataAccessLayer.Interfaces
{
    public interface IBookRepository : IDisposable
    {
        int Add(Book entity, Author selectedAuthor, bool saveChanges = true);
        bool BarcodeExists(string barcode);
        IQueryable<Book> GetAll();
        IQueryable<Book> GetNonArchivedBooks(bool digital);
        int InsertOneCopy(Book passedBook, bool saveChanges = true);
        int RemoveOneCopy(Book passedBook, bool saveChanges = true);
        int InsertNewCopies(int number, Book passedBook, bool saveChanges = true);
        int GetBookCurrentCopies(int id);
        int ArhiveBook(Book passedBook, Archive archive);
        IQueryable<Book> GetNonArchivedBooksByName(string searchTerm);
        IQueryable<Book> GetBookByBarcodeId(string barcodeId);
        int Update(Book book, bool saveChanges = true);
        IQueryable<BookViewModel> SearchBooks(string searchTerm, bool digital);
        IQueryable<BookViewModel> GetBooksByGenre(string genreName, bool digital);
        IQueryable<BookViewModel> GetBooksByAuthor(string authorName, bool digital);
        IQueryable<BookViewModel> GetBooksByYear(int publicationYear, bool digital);
        Book GetBookById(int id, bool digital = true);
        IQueryable<BookViewModel> GetWishlistBooksForMember(string username);
        bool AddBookToWishlist(int memberId, int bookId);
        bool RemoveBookFromWishlist(int memberId, int bookId);
        IEnumerable<MostPopularBooksViewModel> GetMostPopularBooks(int Library_id);
        IQueryable<string> GetBookBarcode(int id);
        IQueryable<Book> GetBooksByLibrary(int libraryId);
        IEnumerable<MostPopularBooksViewModel> GetTopBooks(int libraryId);
    }
}
