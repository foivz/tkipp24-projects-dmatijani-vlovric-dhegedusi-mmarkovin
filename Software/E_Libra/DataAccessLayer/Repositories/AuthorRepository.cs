using DataAccessLayer.Interfaces;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    //Viktor Lovrić
    public class AuthorRepository: Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(): base(new DatabaseModel())
        {
            
        }

        public override int Update(Author entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<Author> GetAll()
        {
            var sql = from a in Entities select a;
            return sql;
        }

        public override int Add(Author entity, bool saveChanges = true)
        {
            var author = new Author
            {
                idAuthor = GetAuthorNextId(),
                name = entity.name,
                surname = entity.surname,
                birth_date = entity.birth_date,
            };
            Entities.Add(author);
            if(saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        private int GetAuthorNextId()
        {
            int lastId = Entities.OrderByDescending(a => a.idAuthor).Select(a => a.idAuthor).FirstOrDefault();
            return lastId + 1;

        }

        public List<Author> SearchAuthor(string search)
        {
            var sql = from a in Entities
                      where a.name.Contains(search) || a.surname.Contains(search)
                      select a;
            return sql.ToList();
        }
    }
}
