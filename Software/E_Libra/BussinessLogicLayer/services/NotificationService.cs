using BussinessLogicLayer.Exceptions;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services
{
    //Magdalena Markovinocić
    public class NotificationService
    {
        private INotificationsRepository notificationsRepository;

        public NotificationService(INotificationsRepository notificationsRepo)
        {
            this.notificationsRepository = notificationsRepo;
        }
        public NotificationService() : this(new NotificationsRepository())
        {
        }

        public List<Notification> GetAllNotificationByLibrary(int id)
        {
            return notificationsRepository.GetAllNotificationsForLibrary(id).ToList();
        }
        public bool AddNewNotification(Notification notification)
        {
            var added = notificationsRepository.Add(notification);
            if (added != 0) return true;
            return false;
        }
        public bool AddNotificationRead(Notification notification, Member loggedMember)
        {
            var added = notificationsRepository.AddReadNotification(notification, loggedMember);
            if (added != 0) return true;
            return false;
        }
        public bool EditNotification(Notification notification)
        {
            var updated = notificationsRepository.Update(notification);
            if (updated != 0) return true;
            else return false;
        }

        public List<Notification> GetReadNotificationsForMember(Member member)
        {
            return notificationsRepository.GetReadNotificationsForMember(member).ToList();
        }

        public int Remove(Notification notification, bool saveChanges = true) {
            return notificationsRepository.Remove(notification, saveChanges);
        }
        ~NotificationService()
        {
            Dispose(false);
        }
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                notificationsRepository?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
