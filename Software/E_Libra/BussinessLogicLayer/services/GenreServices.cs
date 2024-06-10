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
    public class GenreServices
    {
        public IGenreRepository genreRepository { get; set; }
        public GenreServices(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }
        public GenreServices() : this(new GenreRepository()) { }

        public List<Genre> GetGenres()
        {
            return genreRepository.GetAll().ToList();
        }
        public bool Add(Genre entity)
        {
            bool isSuccesful = false;

            int affectedRows = genreRepository.Add(entity);
            isSuccesful |= affectedRows > 0;

            return isSuccesful;
        }
    }
}
