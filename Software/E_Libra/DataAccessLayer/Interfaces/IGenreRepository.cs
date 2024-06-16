using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IGenreRepository : IDisposable
    {
        IQueryable<Genre> GetAll();
        int Add(Genre entity, bool saveChanges = true);
        IQueryable<MostPopularGenres> GetMostPopularGenres(int Library_id);
        List<Genre> SearchGenre(string search);
    }
}
