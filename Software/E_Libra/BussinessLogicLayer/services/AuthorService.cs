using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services
{
    //Viktor Lovrić
    public class AuthorService : IDisposable
    {
        public IAuthorRepository authorRepository { get; set; }
        public AuthorService(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        public AuthorService() : this(new AuthorRepository()) { }

        ~AuthorService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposing && authorRepository != null)
            {
                authorRepository.Dispose();
            }
        }
        public List<Author> GetAllAuthors() 
        {
            return authorRepository.GetAll().ToList();
        }
        public bool AddAuthor(Author author)
        {
            bool isSuccesful = false;

            int affectedRows = authorRepository.Add(author);
            isSuccesful |= affectedRows > 0;

            return isSuccesful;
        }

        public List<Author> SearchAuthors(string search)
        {
            return authorRepository.SearchAuthor(search);
        }
    }
}
