using BussinessLogicLayer.Exceptions;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services
{
    //Magdalena Markovinocić
    public class NotificationService
    {
        MemberService memberService = new MemberService();
        public List<Notification> GetAllNotificationByLibrary(int id)
        {
            using (var notificationsRepo = new NotificationsRepository())
            {
                return notificationsRepo.GetAllNotificationsForLibrary(id).ToList();
            }
        }
        public bool AddNewNotification(Notification notification)
        {
            using (var notificationsRepo = new NotificationsRepository())
            {
                var added = notificationsRepo.Add(notification);
                if (added != 0) return true;
            }
            return false;
        }
        public bool AddNotificationRead(Notification notification)
        {
            Member member = memberService.GetMemberByUsername(LoggedUser.Username);
            using (var notificationsRepo = new NotificationsRepository())
            {
                notificationsRepo.AddReadNotification(notification, member);
                return true;

            }
        }
        public bool EditNotification(Notification notification)
        {
            using (var notificationsRepo = new NotificationsRepository())
            {
                notificationsRepo.Update(notification);
                return true;
            }
        }

        public List<Notification> GetReadNotificationsForMember(Member member)
        {
            using (var notificationsRepo = new NotificationsRepository())
            {
                return notificationsRepo.GetReadNotificationsForMember(member).ToList();
            }
        }

        public int Remove(Notification notification, bool saveChanges = true) {
            using (var context = new NotificationsRepository()) {
                return context.Remove(notification, saveChanges);
            }
        }
    }
}
