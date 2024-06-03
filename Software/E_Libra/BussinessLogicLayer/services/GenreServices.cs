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
    public class GenreServices
    {
        public List<Genre> GetGenres()
        {
            using (var repo = new GenreRepository())
            {
                return repo.GetAll().ToList();
            }
        }
        public bool Add(Genre entity)
        {
            bool isSuccesful = false;
            using (var repo = new GenreRepository())
            {
                int affectedRows = repo.Add(entity);
                isSuccesful |= affectedRows > 0;
            }
            return isSuccesful;
        }
    }
}
