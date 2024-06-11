using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IAuthorRepository : IDisposable
    {
        IQueryable<Author> GetAll();
        int Add(Author entity, bool saveChanges = true);
    }
}
