using DataAccessLayer.Interfaces;
using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    /*Viktor Lovrić, metode: Add, GenerateBarcodeId, BarcodeExists, GetAll, GetNonArchivedBooks, InsertNewCopies, ArhiveBook, GetNonArchivedBooksByName,
     * SearchBooks, TransformDigital, GetBooksByGenre, GetBooksByAuthor, GetBooksByYear, GetWishlistBooksForMember, AddBookToWishlist, RemoveBookFromWishlist
     */
    // Domagoj Hegedušić, metode: GetMostPopularBookss
    // David Matijanić: GetBookByBarcodeId, Update, GetBookBarcode, GetBooksByLibrary

    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(): base(new DatabaseModel())
        {
            
        }
        
        public int Add(Book entity, Author selectedAuthor, bool saveChanges = true)
        {
            var genre = Context.Genres.FirstOrDefault(g => g.id == entity.Genre.id);
            var library = Context.Libraries.FirstOrDefault(l => l.id == entity.Library_id);

            var book = new Book
            {
                name = entity.name,
                description = entity.description,
                publish_date = entity.publish_date,
                pages_num = entity.pages_num,
                digital = entity.digital,
                url_digital = entity.url_digital,
                url_photo = entity.url_photo,
                total_copies = entity.total_copies,
                current_copies = entity.total_copies,
                barcode_id = GenerateBarcodeId(),
                Genre = genre,
                Library = library,
            };
            Context.Entry(book).State = EntityState.Added;

            
            Context.Entry(selectedAuthor).State = EntityState.Unchanged;

            Entities.Add(book);
            book.Authors.Add(selectedAuthor);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }

        }

        private string GenerateBarcodeId()
        {
            var rand = new Random();
            string res;
            do
            {
                res = rand.Next(10000000, 99999999).ToString();
            } while (BarcodeExists(res));
            return res;

        }

        public bool BarcodeExists(string barcode)
        {
            var sql = from b in Entities where b.barcode_id == barcode select b;
            return sql.Count() > 0;
        }

        public override IQueryable<Book> GetAll()
        {
            var sql = from b in Entities select b;
            return sql;
        }

        public IQueryable<Book> GetNonArchivedBooks(bool digital)
        {
            IQueryable<Book> sql;
            if (digital)
            {
                sql = from b in Context.Books
                          where !Context.Archives.Any(a => a.Book_id == b.id) && b.Library_id == LoggedUser.LibraryId
                          select b;
            }
            else
            {
                sql = from b in Context.Books
                          where !Context.Archives.Any(a => a.Book_id == b.id) && b.digital == 0 && b.Library_id == LoggedUser.LibraryId
                      select b;
            }
            

            return sql;
        }

        public int InsertOneCopy(Book passedBook, bool saveChanges = true)
        {
            int id = passedBook.id;
            var book = (from b in Entities where b.id == id select b).FirstOrDefault();
            book.current_copies++;
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        public int RemoveOneCopy(Book passedBook, bool saveChanges = true)
        {
            int id = passedBook.id;
            var book = (from b in Entities where b.id == id select b).FirstOrDefault();
            book.current_copies--;
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        public int InsertNewCopies(int number, Book passedBook, bool saveChanges = true)
        {
            int id = passedBook.id;
            var book = (from b in Entities where b.id == id select b).FirstOrDefault();
            book.current_copies += number;
            book.total_copies += number;
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        public int GetBookCurrentCopies(int id)
        {
            var book = (from b in Entities where b.id == id select b).FirstOrDefault();
            return (int)book.current_copies;
        }
        public int ArhiveBook(Book passedBook, Archive archive)
        {
            int id = passedBook.id;
            var book = (from b in Entities where b.id == id select b).FirstOrDefault();
            book.Archives.Add(archive);
            return SaveChanges();
        }

        public IQueryable<Book> GetNonArchivedBooksByName(string searchTerm)
        {
            var nonArchivedBooks = from book in GetNonArchivedBooks(false)
                                   where book.name.Contains(searchTerm)
                                   select book;

            return nonArchivedBooks;
        }

        public IQueryable<Book> GetBookByBarcodeId(string barcodeId) {
            var query = from b in Entities.Include("Library")
                        where b.barcode_id == barcodeId
                        select b;

            return query;
        }

        public override int Update(Book book, bool saveChanges = true) {
            var library = Context.Libraries.SingleOrDefault(l => l.id == book.Library_id);
            var genre = Context.Genres.SingleOrDefault(g => g.id == book.Genre_id);

            Book existingBook = Context.Books.SingleOrDefault(b => b.id == book.id);
            existingBook.name = book.name;
            existingBook.description = book.description;
            existingBook.publish_date = book.publish_date;
            existingBook.pages_num = book.pages_num;
            existingBook.digital = book.digital;
            existingBook.url_photo = book.url_photo;
            existingBook.url_digital = book.url_digital;
            existingBook.barcode_id = book.barcode_id;
            existingBook.total_copies = book.total_copies;
            existingBook.current_copies = book.current_copies;
            existingBook.Genre = genre;
            existingBook.Library = library;

            if (saveChanges) {
                return SaveChanges();
            } else {
                return 0;
            }
        }

        public IQueryable<BookViewModel> SearchBooks(string searchTerm, bool digital) {
            searchTerm = searchTerm.ToLower();

            var matchingBooks = from book in GetNonArchivedBooks(digital)
                                where book.name.ToLower().Contains(searchTerm) ||
                                      book.Authors.Any(author =>
                                          (author.name + " " + author.surname).ToLower().Contains(searchTerm) ||
                                          (author.surname + " " + author.name).ToLower().Contains(searchTerm)) ||
                                      book.Genre.name.ToLower().Contains(searchTerm) ||
                                      (book.publish_date != null && book.publish_date.Value.Year.ToString().Contains(searchTerm))
                                select new BookViewModel {
                                    Id = book.id,
                                    Name = book.name,
                                    PublishDate = book.publish_date,
                                    PublishDateDisplay = null,
                                    AuthorName = book.Authors.FirstOrDefault().name + " " + book.Authors.FirstOrDefault().surname,
                                    GenreName = book.Genre.name,
                                    Digital = book.digital.ToString(),
                                };

            var transformedBooks = matchingBooks.AsEnumerable().Select(book => new BookViewModel {
                Id = book.Id,
                Name = book.Name,
                PublishDate = book.PublishDate,
                PublishDateDisplay = book.PublishDate.HasValue ? book.PublishDate.Value.ToString("dd-MM-yyyy") : null,
                AuthorName = book.AuthorName,
                GenreName = book.GenreName,
                Digital = TransformDigital(book.Digital)
            });

            return transformedBooks.AsQueryable();
        }

        private string TransformDigital(string digital) {
            if (digital == "1") return "Da";
            return "Ne";
        }

        public IQueryable<BookViewModel> GetBooksByGenre(string genreName, bool digital) {
            genreName = genreName.ToLower();

            var booksByGenre = from book in GetNonArchivedBooks(digital)
                               where book.Genre.name.ToLower().Contains(genreName)
                               select new BookViewModel {
                                   Id = book.id,
                                   Name = book.name,
                                   PublishDate = book.publish_date,
                                   PublishDateDisplay = null,
                                   AuthorName = book.Authors.FirstOrDefault().name + " " + book.Authors.FirstOrDefault().surname,
                                   GenreName = book.Genre.name,
                                   Digital = book.digital.ToString(),
                               };

            var transformedBooks = booksByGenre.AsEnumerable().Select(book => new BookViewModel {
                Id = book.Id,
                Name = book.Name,
                PublishDate = book.PublishDate,
                PublishDateDisplay = book.PublishDate.HasValue ? book.PublishDate.Value.ToString("dd-MM-yyyy") : null,
                AuthorName = book.AuthorName,
                GenreName = book.GenreName,
                Digital = TransformDigital(book.Digital)
            });

            return transformedBooks.AsQueryable();
        }

        public IQueryable<BookViewModel> GetBooksByAuthor(string authorName, bool digital) {
            authorName = authorName.ToLower();

            var booksByAuthor = from book in GetNonArchivedBooks(digital)
                                where book.Authors.Any(author =>
                                    (author.name + " " + author.surname).ToLower().Contains(authorName) ||
                                    (author.surname + " " + author.name).ToLower().Contains(authorName))
                                select new BookViewModel {
                                    Id = book.id,
                                    Name = book.name,
                                    PublishDate = book.publish_date,
                                    PublishDateDisplay = null,
                                    AuthorName = book.Authors.FirstOrDefault().name + " " + book.Authors.FirstOrDefault().surname,
                                    GenreName = book.Genre.name,
                                    Digital = book.digital.ToString(),
                                };

            var transformedBooks = booksByAuthor.AsEnumerable().Select(book => new BookViewModel {
                Id = book.Id,
                Name = book.Name,
                PublishDate = book.PublishDate,
                PublishDateDisplay = book.PublishDate.HasValue ? book.PublishDate.Value.ToString("dd-MM-yyyy") : null,
                AuthorName = book.AuthorName,
                GenreName = book.GenreName,
                Digital = TransformDigital(book.Digital)
            });

            return transformedBooks.AsQueryable();
        }
        public IQueryable<BookViewModel> GetBooksByYear(int publicationYear, bool digital) {
            var booksByYear = from book in GetNonArchivedBooks(digital)
                              where book.publish_date != null && book.publish_date.Value.Year.ToString().Contains(publicationYear.ToString())
                              select new BookViewModel {
                                  Id = book.id,
                                  Name = book.name,
                                  PublishDate = book.publish_date,
                                  PublishDateDisplay = null,
                                  AuthorName = book.Authors.FirstOrDefault().name + " " + book.Authors.FirstOrDefault().surname,
                                  GenreName = book.Genre.name,
                                  Digital = book.digital.ToString(),
                              };

            return booksByYear;
        }
        public Book GetBookById(int id, bool digital = true) {
            var book = GetNonArchivedBooks(digital).FirstOrDefault(b => b.id == id);
            return book;
        }
        public IQueryable<BookViewModel> GetWishlistBooksForMember(string username) {
            var wishlistBooks = from book in Context.Books
                                from member in book.Members
                                where member.username == username
                                select new BookViewModel {
                                    Id = book.id,
                                    Name = book.name,
                                    PublishDate = book.publish_date,
                                    PublishDateDisplay = null,
                                    AuthorName = book.Authors.FirstOrDefault().name + " " + book.Authors.FirstOrDefault().surname,
                                    GenreName = book.Genre.name,
                                    Digital = book.digital.ToString(),
                                };

            var transformedBooks = wishlistBooks.AsEnumerable().Select(book => new BookViewModel {
                Id = book.Id,
                Name = book.Name,
                PublishDate = book.PublishDate,
                PublishDateDisplay = book.PublishDate.HasValue ? book.PublishDate.Value.ToString("dd-MM-yyyy") : null,
                AuthorName = book.AuthorName,
                GenreName = book.GenreName,
                Digital = TransformDigital(book.Digital)
            });

            return transformedBooks.AsQueryable();
        }
        public bool AddBookToWishlist(int memberId, int bookId) {
            var member = Context.Members.Find(memberId);
            var book = Context.Books.Find(bookId);

            if (!member.Books.Contains(book)) {
                member.Books.Add(book);
                Context.SaveChanges();
                return true;
            } else {
                return false;
            }
        }
        public bool RemoveBookFromWishlist(int memberId, int bookId) {
            var member = Context.Members.Find(memberId);
            var book = Context.Books.Find(bookId);

            if (member.Books.Contains(book)) {
                member.Books.Remove(book);
                Context.SaveChanges();
                return true;
            } else {
                return false;
            }
        }

        public class BookViewModel {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime? PublishDate { get; set; }
            public string PublishDateDisplay { get; set; }
            public string AuthorName { get; set; }
            public string GenreName { get; set; }
            public string Digital { get; set; }
        }

        public IEnumerable<MostPopularBooks> GetMostPopularBooks(int Library_id) {
            var query = from book in Entities
                        where book.Library_id == Library_id
                        let bookBorrows = book.Borrows
                        select new MostPopularBooks {
                            Book_Name = book.name,
                            Author_Name = book.Authors.Select(author => author.name + " " + author.surname).FirstOrDefault(),
                            Times_Borrowed = bookBorrows.Count()
                        };

            return query.OrderByDescending(book => book.Times_Borrowed).AsEnumerable();
        }

        public IQueryable<string> GetBookBarcode(int id) {
            var query = from b in Entities
                        where b.id == id
                        select b.barcode_id;

            return query;
        }

        public IQueryable<Book> GetBooksByLibrary(int libraryId) {
            var query = from e in Entities
                        where e.Library_id == libraryId
                        select e;

            return query;
        }

        public IEnumerable<MostPopularBooks> GetTopBooks(int Library_id) {
            var query = from book in Entities
                        where book.Library_id == Library_id
                        let bookBorrows = book.Borrows
                        select new MostPopularBooks {
                            Book_Name = book.name,
                            Author_Name = book.Authors.Select(author => author.name + " " + author.surname).FirstOrDefault(),
                            Times_Borrowed = bookBorrows.Count()
                        };

            return query.OrderByDescending(book => book.Times_Borrowed).Take(10).AsEnumerable();
        }

    }
}
