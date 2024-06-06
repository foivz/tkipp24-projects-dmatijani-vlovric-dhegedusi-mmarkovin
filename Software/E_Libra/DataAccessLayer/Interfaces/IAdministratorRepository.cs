using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces {
    public interface IAdministratorRepository {
        IQueryable<Administrator> GetAdministratorLogin(string username, string password);

        int Update(Administrator entity, bool saveChanges = true);
    }
}
