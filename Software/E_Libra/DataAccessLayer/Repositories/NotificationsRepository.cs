 using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    //Magdalena Markovinocić
    public class NotificationsRepository : Repository<Notification>
    {
        public NotificationsRepository(): base(new DatabaseModel())
        {
            
        }
        public IQueryable<Notification> GetAllNotificationsForLibrary(int libraryID)
        {
            var query = from n in Entities
                        where n.Library_id == libraryID
                        select n;
            return query;
        }
        public int AddReadNotification(Notification notification, Member member, bool saveChanges = true)
        {
            Entities.Attach(notification);
            Context.Members.Attach(member);
            notification.Members.Add(member);
            if (saveChanges)
            {
                return SaveChanges();
            } else
            {
                return 0;
            }
        }
        public IQueryable<Notification> GetReadNotificationsForMember(Member member)
        {
            Context.Members.Attach(member);
            var query = from n in Entities
                        where n.Members.Any(m => m.id == member.id)
                        select n;
            return query;
        }
        public override int Add(Notification notification, bool saveChanges = true)
        {
            var newNotification = new Notification
            {
                title = notification.title,
                description = notification.description,
                Library_id = notification.Library_id
            };
            Entities.Add(newNotification);
            if (saveChanges)
            {
                return SaveChanges();
            } else
            {
                return 0;
            }
        } 
        public override int Update(Notification entity, bool saveChanges = true)
        {
            var existingNotif = Entities.SingleOrDefault(n => n.id == entity.id);
            existingNotif.title = entity.title;
            existingNotif.description = entity.description;
            if (saveChanges)
            {
                return SaveChanges();
            } else
            {
                return 0;
            }
        }
    }
}
