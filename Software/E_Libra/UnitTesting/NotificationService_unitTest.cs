using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using EntitiesLayer;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting
{ 
    //Magdalena markovinović
    public class NotificationService_unitTest
    {
        [Fact]
        public void GetAllNotificationByLibrary_GivenMemberIsFromLibraryOne_ReturnsAllNotificationsForLibraryOne()
        {
            // Arrange
            var notificationsRepository = A.Fake<INotificationsRepository>();
            var notificationService = new NotificationService(notificationsRepository);
            var expectedNotifications = new List<Notification>
        {
            new Notification
            {
                id = 1,
                title = "Naslov 1",
                description = "Opis 1",
                Library_id = 1,
            },
            new Notification
            {
                id = 2,
                title = "Naslov 2",
                description = "Opis 2",
                Library_id = 1,
            }
        };

            A.CallTo(() => notificationsRepository.GetAllNotificationsForLibrary(1)).Returns(expectedNotifications.AsQueryable());

            // Act
            var result = notificationService.GetAllNotificationByLibrary(1);

            // Assert
            Assert.Equal(expectedNotifications.Count, result.Count);
        }

        [Fact]
        public void AddNewNotification_GivenNotificationIsAdded_ReturnsTrue()
        {
            // Arrange
            INotificationsRepository notificationsRepository = A.Fake<INotificationsRepository>();
            var notificationService = new NotificationService(notificationsRepository);
            Notification notification = new Notification
            {
                id = 3,
                title = "Naslov 3",
                description = "Opis 3",
                Library_id = 1,
            };
            A.CallTo(() => notificationsRepository.Add(notification, true)).Returns(1);

            // Act
            var result = notificationService.AddNewNotification(notification);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public void AddNewNotification_GivenInvalidNotification_ReturnsFalse()
        {
            // Arrange
            var notificationsRepository = A.Fake<INotificationsRepository>();
            var notification = new Notification
            {
                id = 3,
                title = "Naslov 3",
                description = "Opis 3",
                Library_id = 1,
            };

            A.CallTo(() => notificationsRepository.Add(notification, false)).Returns(0);

            var notificationService = new NotificationService(notificationsRepository);

            // Act
            var result = notificationService.AddNewNotification(notification);

            // Assert
            Assert.False(result);
        }
    }
}
