using DataAccessLayer.Interfaces;
using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    //Viktor Lovrić, metode: GetAll, Add
    //Domagoj Hegedušić, metode: GetMostPopularGenres
    public class GenreRepository: Repository<Genre>, IGenreRepository
    {
        public GenreRepository(): base(new DatabaseModel())
        {
            
        }

        public override int Update(Genre entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<Genre> GetAll()
        {
            var query = from g in Entities select g;
            return query;
        }
        public override int Add(Genre entity, bool saveChanges = true)
        {
            var genre = new Genre
            {
                name = entity.name,
            };
            Entities.Add(genre);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public IQueryable<MostPopularGenres> GetMostPopularGenres(int Library_id) {
            var query = from borrow in Context.Borrows
                        join book in Context.Books on borrow.Book_id equals book.id
                        join genre in Context.Genres on book.Genre_id equals genre.id
                        where book.Library_id == Library_id
                        group genre.name by genre into genreGroup
                        select new MostPopularGenres {
                            Genre_name = genreGroup.Key.name,
                            Times_Borrowed = genreGroup.Count()
                        };

            query = query.OrderByDescending(genre => genre.Times_Borrowed);
            return query;
        }


    }
}
