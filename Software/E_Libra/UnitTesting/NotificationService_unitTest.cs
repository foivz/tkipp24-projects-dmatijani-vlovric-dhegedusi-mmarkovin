using BussinessLogicLayer;
using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting
{ 
    //Magdalena markovinović
    public class NotificationService_unitTest
    {
        private NotificationService notificationService;
        private INotificationsRepository notificationsRepository;
        private IMembersRepository memberRepository;
        public NotificationService_unitTest()
        {
            notificationsRepository = A.Fake<INotificationsRepository>();
            memberRepository = A.Fake<IMembersRepository>();
            notificationService = new NotificationService(notificationsRepository);
        }

        [Fact]
        public void GetAllNotificationByLibrary_GivenMemberIsFromLibraryOne_ReturnsAllNotificationsForLibraryOne()
        {
            // Arrange
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
            var notification = new Notification
            {
                id = 3,
                title = "Naslov 3",
                description = "Opis 3",
                Library_id = 1,
            };

            A.CallTo(() => notificationsRepository.Add(notification, false)).Returns(0);


            // Act
            var result = notificationService.AddNewNotification(notification);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void EditNotification_GivenNotificationIsEdited_ReturnsTrue()
        {
            // Arrange
            var notificationService = new NotificationService(notificationsRepository);
            Notification notification = new Notification
            {
                id = 3,
                title = "Novi naslov 3",
                description = "Novi opis 3",
                Library_id = 1,
            };
            A.CallTo(() => notificationsRepository.Update(notification, true)).Returns(1);

            // Act
            var result = notificationService.EditNotification(notification);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public void EditNotification_GivenInvalidEditedNotification_ReturnsFalse()
        {
            // Arrange
            Notification notification = new Notification
            {
                id = 3,
                title = "Novi naslov 3",
                description = "Novi opis 3",
                Library_id = 1,
            };
            A.CallTo(() => notificationsRepository.Update(notification, false)).Returns(0);

            // Act
            var result = notificationService.EditNotification(notification);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Remove_NotificationExists_ReturnsOne()
        {
            // Arrange
            var notificationToRemove = new Notification();
            A.CallTo(() => notificationsRepository.Remove(notificationToRemove, true)).Returns(1);

            // Act
            var result = notificationService.Remove(notificationToRemove);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Remove_NotificationDoesNotExist_ReturnsZero()
        {
            // Arrange
            var notificationToRemove = new Notification();
            A.CallTo(() => notificationsRepository.Remove(notificationToRemove, true)).Returns(0);

            // Act
            var result = notificationService.Remove(notificationToRemove);

            // Assert
            Assert.Equal(0, result);
        }
        [Fact]
        public void AddNotificationRead_ValidNotification_ReturnsTrue()
        {
            // Arrange
            var members = new List<Member> { new Member { username = "testuser" } }.AsQueryable();
            var test1 = A.CallTo(() => memberRepository.GetMembersByUsername(A<string>._))
             .Returns(new List<Member> { new Member { username = "testuser" } }.AsQueryable());
            var test2 = A.CallTo(() => memberRepository.GetMembersByUsername(A<string>._))
                .Returns(members);

            var test3 = A.CallTo(() => notificationsRepository.AddReadNotification(A<Notification>._, A<Member>._, true))
                .Returns(1);

            // Act
            bool result = notificationService.AddNotificationRead(new Notification(), new Member { username = "testuser" });

            // Assert
            Assert.True(result);
        }
        [Fact]
        public void GetReadNotificationsForMember_ReturnsCorrectList()
        {
            // Arrange
            var member = new Member { username = "testuser" };
            var fakeNotifications = new List<Notification>
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
            }.AsQueryable();

            A.CallTo(() => notificationsRepository.GetReadNotificationsForMember(member))
           .Returns(fakeNotifications);

            // Act
            var result = notificationService.GetReadNotificationsForMember(member);

            // Assert
            Assert.Equal(fakeNotifications.Count(), result.Count);
            Assert.All(result, n => Assert.Contains(n, fakeNotifications));
        }

        [Fact]
        public void Dispose_GivenFunctionIsCalled_DisposeAll()
        {
            // Arrange
            var fakeMemberService = A.Fake<MemberService>();
            var fakeNotifRepo = A.Fake<INotificationsRepository>();
            var fakeNotifService = new NotificationService(fakeNotifRepo);
            // Act
            fakeNotifService.Dispose();

            // Assert
            A.CallTo(() => fakeNotifRepo.Dispose()).MustHaveHappened();
        }
    }
}
