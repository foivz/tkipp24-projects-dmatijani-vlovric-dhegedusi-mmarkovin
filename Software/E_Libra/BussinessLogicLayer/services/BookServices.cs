using BussinessLogicLayer.Exceptions;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.Repositories.BookRepository;

namespace BussinessLogicLayer.services
{
    //Viktor Lovrić
    // David Matijanić: GetBookByBarcodeId
    public class BookServices
    {
        public bool AddBook(Book book, Author author)
        {
            bool isSuccesful = false;
            using(var repo = new BookRepository())
            {
                int affectedRows = repo.Add(book, author);
                isSuccesful = affectedRows > 0;
            }
            return isSuccesful;
        }

        public List<Book> GetAllBooks()
        {
            using (var repo = new BookRepository())
            {
                return repo.GetAll().ToList();
            }
        }

        public List<Book> GetNonArchivedBooks(bool digital)
        {
            using(var repo = new BookRepository())
            {
                return repo.GetNonArchivedBooks(digital).ToList();
            }
        }
  
        public bool InsertOneCopy(Book book)
        {
            bool isSuccesful = false;
            using (var repo = new BookRepository())
            {
                int affectedRows = repo.InsertOneCopy(book);
                isSuccesful = affectedRows > 0;
            }
            return isSuccesful;
        }
        public bool RemoveOneCopy(Book book)
        {
            bool isSuccesful = false;
            using (var repo = new BookRepository())
            {
                int affectedRows = repo.RemoveOneCopy(book);
                isSuccesful = affectedRows > 0;
            }
            return isSuccesful;
        }
        public bool InsertNewCopies(int number, Book book)
        {
            var numCopies = number;
            var reservationService = new ReservationService();
            using (var repo = new BookRepository())
            {
                int currentCopies = repo.GetBookCurrentCopies(book.id);
                bool imaRezervacije;
                if (currentCopies < 0) //ako ima rezervacija
                {
                    do
                    {
                        imaRezervacije = reservationService.EnterDateForReservation(book); //daj osobi datum na rezervaciju
                        if (imaRezervacije)
                        {
                            repo.InsertNewCopies(1, book);
                            numCopies--;
                        }
                    } while (imaRezervacije && numCopies > 0);
                    if (numCopies > 0) //ako je ostalo jos kopija nakon popunjavanja svih rezervacija
                    {
                        repo.InsertNewCopies(numCopies, book); //dodaj ih
                    }
                }
                else
                {
                    repo.InsertNewCopies(number, book); //samo dodajem current i total copies
                }
                return true;
            }
        }
        public bool ArchiveBook(Book book, Archive archive)
        {
            bool isSuccesful = false;
            using(var repo = new BookRepository())
            {
                int affectedRows = repo.ArhiveBook(book, archive);
                isSuccesful = affectedRows > 0;
            }
            return isSuccesful;
        }
        public List<Book> GetNonArchivedBooksByName(string searchTerm)
        {
            using (var repo = new BookRepository())
            {
                return repo.GetNonArchivedBooksByName(searchTerm).ToList();
            }
        }

        public List<BookViewModel> SearchBooks(string searchTerm, bool digital) {
            using (var repo = new BookRepository()) {
                return repo.SearchBooks(searchTerm, digital).ToList();
            }
        }
        public List<BookViewModel> GetBooksByGenre(string genreName, bool digital) {
            using (var repo = new BookRepository()) {
                return repo.GetBooksByGenre(genreName, digital).ToList();
            }
        }
        public List<BookViewModel> GetBooksByAuthor(string authorName, bool digital) {
            using (var repo = new BookRepository()) {
                return repo.GetBooksByAuthor(authorName, digital).ToList();
            }
        }
        public List<BookViewModel> GetBooksByYear(int year, bool digital) {
            using (var repo = new BookRepository()) {
                return repo.GetBooksByYear(year, digital).ToList();
            }
        }
        public Book GetBookById(int id) {
            using (var repo = new BookRepository()) {
                return repo.GetBookById(id);
            }
        }
        public List<BookViewModel> GetWishlistedBooks() {
            using (var repo = new BookRepository()) {
                return repo.GetWishlistBooksForMember(LoggedUser.Username).ToList();
            }
        }
        public bool AddBookToWishlist(int bookId) {
            MemberRepository memberRepository = new MemberRepository();
            int userId = memberRepository.GetMemberId(LoggedUser.Username);

            using (var repo = new BookRepository()) {
                return repo.AddBookToWishlist(userId, bookId);
            }
        }
        public bool RemoveBookFromWishlist(int bookId) {
            MemberRepository memberRepository = new MemberRepository();
            int userId = memberRepository.GetMemberId(LoggedUser.Username);

            using (var repo = new BookRepository()) {
                return repo.RemoveBookFromWishlist(userId, bookId);
            }
        }

        public Book GetBookByBarcodeId(int libraryId, string barcodeId) {
            using (var repository = new BookRepository()) {
                List<Book> returned = repository.GetBookByBarcodeId(barcodeId).ToList();

                if (returned.Count == 0) {
                    throw new BookNotFoundException("Knjiga s tim barkodom ne postoji!");
                }

                Book book = returned.FirstOrDefault();

                if (book.Library.id != libraryId) {
                    throw new WrongLibraryException("Knjiga s tim barkodom ovdje ne postoji!");
                }

                return book;
            }
        }

        public int UpdateBook(Book book, bool saveChanges = true) {
            using (var context = new BookRepository()) {
                return context.Update(book, saveChanges);
            }
        }

        public string GetBookBarcode(int id) {
            using (var repository = new BookRepository()) {
                return repository.GetBookBarcode(id).FirstOrDefault();
            }
        }

        public List<Book> GetBooksByLibrary(int libraryId) {
            using (var repository = new BookRepository()) {
                return repository.GetBooksByLibrary(libraryId).ToList();
            }
        }
    }
}
