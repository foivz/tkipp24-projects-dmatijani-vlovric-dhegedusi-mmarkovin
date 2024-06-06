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
        public void CheckLoginCredentials(string username, string password)
        {
            using (var adminRepo = new AdministratorRepository())
            {
                var returned = adminRepo.GetAdministratorLogin(username, password).ToList();

                if (returned.Count() == 1)
                {
                    LoggedUser.Username = username;
                    LoggedUser.UserType = Role.Admin;
                }
            }
        }
    }
}
