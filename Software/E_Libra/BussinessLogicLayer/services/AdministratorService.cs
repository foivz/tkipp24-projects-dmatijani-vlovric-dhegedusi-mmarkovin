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
    public class AdministratorService
    {
        private IAdministratorRepository administratorRepository { get; set; }

        public AdministratorService(IAdministratorRepository administratorRepository) {
            this.administratorRepository = administratorRepository;
        }
        public AdministratorService() : this(new AdministratorRepository()) {
        }

        public void CheckLoginCredentials(string username, string password)
        {
            var returned = administratorRepository.GetAdministratorLogin(username, password).ToList();

            if (returned.Count() == 1)
            {
                LoggedUser.Username = username;
                LoggedUser.UserType = Role.Admin;
            }
        }
    }
}
