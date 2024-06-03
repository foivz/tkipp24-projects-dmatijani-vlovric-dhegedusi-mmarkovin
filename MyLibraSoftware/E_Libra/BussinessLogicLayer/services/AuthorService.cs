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
    public class AuthorService
    {
        public List<Author> GetAllAuthors()
        {
            using (var repo = new AuthorRepository())
            {
                return repo.GetAll().ToList();
            }
        }
        public bool AddAuthor(Author author)
        {
            bool isSuccesful = false;
            using(var repo = new AuthorRepository())
            {
                int affectedRows = repo.Add(author);
                isSuccesful |= affectedRows > 0;
            }
            return isSuccesful;
        }
    }
}
