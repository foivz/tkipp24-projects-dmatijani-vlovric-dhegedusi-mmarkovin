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
    public class GenreServices : IDisposable
    {
        public IGenreRepository genreRepository { get; set; }
        public GenreServices(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }
        public GenreServices() : this(new GenreRepository()) { }

        ~GenreServices()
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
            if (disposing && genreRepository != null)
            {
                genreRepository.Dispose();
            }
        }

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

        public List<Genre> SearchGenres(string search)
        {
            return genreRepository.SearchGenre(search);
        }
    }
}
