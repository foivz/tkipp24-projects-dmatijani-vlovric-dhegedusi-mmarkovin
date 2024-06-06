using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface INotificationsRepository
    {
        IQueryable<Notification> GetAllNotificationsForLibrary(int libraryID);

        int AddReadNotification(Notification notification, Member member, bool saveChanges = true);

        IQueryable<Notification> GetReadNotificationsForMember(Member member);

        int Add(Notification notification, bool saveChanges = true);

        int Update(Notification entity, bool saveChanges = true);

    }
}
