using DataAccessLayer.Interfaces;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class AdministratorRepository: Repository<Administrator>, IAdministratorRepository
    {
        public DbSet<Administrator> Administrator{ get; set; }
        public AdministratorRepository() :base(new DatabaseModel())
        {
            Administrator = Context.Set<Administrator>();
        }
        public IQueryable<Administrator> GetAdministratorLogin(string username, string password)
        {
            var sql = from a in Administrator
                      where a.username == username && a.password == password
                      select a;
            return sql;
        }

        public override int Update(Administrator entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
