using BussinessLogicLayer.Exceptions;
using DataAccessLayer.Interfaces;
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
        public IBookRepository bookRepository { get; set; }
        private IReservationRepository reservationRepository { get; set; }
        private IMembersRepository memberRepository { get; set; }
        public BookServices(
            IBookRepository bookRepository,
            IReservationRepository reservationRepository,
            IMembersRepository memberRepository
        )
        {
            this.bookRepository = bookRepository;
            this.reservationRepository = reservationRepository;
            this.memberRepository = memberRepository;
        }
        public BookServices() : this(
            new BookRepository(),
            new ReservationRepository(),
            new MemberRepository()
        )
        {}

        public bool AddBook(Book book, Author author)
        {
            bool isSuccesful;
            int affectedRows = bookRepository.Add(book, author);
            isSuccesful = affectedRows > 0;
            return isSuccesful;
        }

        public List<Book> GetAllBooks()
        {
            return bookRepository.GetAll().ToList();
        }

        public List<Book> GetNonArchivedBooks(bool digital)
        {
            return bookRepository.GetNonArchivedBooks(digital).ToList();
        }
  
        public bool InsertOneCopy(Book book)
        {
            bool isSuccesful;
            int affectedRows = bookRepository.InsertOneCopy(book);
            isSuccesful = affectedRows > 0;
            return isSuccesful;
        }
        public bool RemoveOneCopy(Book book)
        {
            bool isSuccesful;
            int affectedRows = bookRepository.RemoveOneCopy(book);
            isSuccesful = affectedRows > 0;
            return isSuccesful;
        }
        public bool InsertNewCopies(int number, Book book)
        {
            var numCopies = number;

            int currentCopies = bookRepository.GetBookCurrentCopies(book.id);
            bool imaRezervacije;
            if (currentCopies < 0) //ako ima rezervacija
            {
                do
                {
                    imaRezervacije = reservationRepository.EnterDateForReservation(book); //daj osobi datum na rezervaciju
                    if (imaRezervacije)
                    {
                        bookRepository.InsertNewCopies(1, book);
                        numCopies--;
                    }
                } while (imaRezervacije && numCopies > 0);
                if (numCopies > 0) //ako je ostalo jos kopija nakon popunjavanja svih rezervacija
                {
                    bookRepository.InsertNewCopies(numCopies, book); //dodaj ih
                }
            }
            else
            {
                bookRepository.InsertNewCopies(number, book); //samo dodajem current i total copies
            }
            return true;
        }
        public bool ArchiveBook(Book book, Archive archive)
        {
            bool isSuccesful;
            int affectedRows = bookRepository.ArhiveBook(book, archive);
            isSuccesful = affectedRows > 0;
            return isSuccesful;
        }
        public List<Book> GetNonArchivedBooksByName(string searchTerm)
        {
            return bookRepository.GetNonArchivedBooksByName(searchTerm).ToList();
        }

        public List<BookViewModel> SearchBooks(string searchTerm, bool digital) {
            return bookRepository.SearchBooks(searchTerm, digital).ToList();
        }
        public List<BookViewModel> GetBooksByGenre(string genreName, bool digital) {
            return bookRepository.GetBooksByGenre(genreName, digital).ToList();
        }
        public List<BookViewModel> GetBooksByAuthor(string authorName, bool digital) {
            return bookRepository.GetBooksByAuthor(authorName, digital).ToList();
        }
        public List<BookViewModel> GetBooksByYear(int year, bool digital) {
            return bookRepository.GetBooksByYear(year, digital).ToList();
        }
        public Book GetBookById(int id) {
            return bookRepository.GetBookById(id);
        }
        public List<BookViewModel> GetWishlistedBooks() {
            return bookRepository.GetWishlistBooksForMember(LoggedUser.Username).ToList();
        }
        public bool AddBookToWishlist(int bookId) {
            int userId = memberRepository.GetMemberId(LoggedUser.Username);

            return bookRepository.AddBookToWishlist(userId, bookId);
        }
        public bool RemoveBookFromWishlist(int bookId) {
            int userId = memberRepository.GetMemberId(LoggedUser.Username);

            return bookRepository.RemoveBookFromWishlist(userId, bookId);
        }

        public Book GetBookByBarcodeId(int libraryId, string barcodeId) {
            List<Book> returned = bookRepository.GetBookByBarcodeId(barcodeId).ToList();

            if (returned.Count == 0)
            {
                throw new BookNotFoundException("Knjiga s tim barkodom ne postoji!");
            }

            Book book = returned.FirstOrDefault();

            if (book.Library.id != libraryId)
            {
                throw new WrongLibraryException("Knjiga s tim barkodom ovdje ne postoji!");
            }

            return book;
        }

        public int UpdateBook(Book book, bool saveChanges = true) {
            return bookRepository.Update(book, saveChanges);
        }

        public string GetBookBarcode(int id) {
            return bookRepository.GetBookBarcode(id).FirstOrDefault();
        }

        public List<Book> GetBooksByLibrary(int libraryId) {
            return bookRepository.GetBooksByLibrary(libraryId).ToList();
        }
    }
}
