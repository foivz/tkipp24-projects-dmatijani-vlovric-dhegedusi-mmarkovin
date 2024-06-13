using BussinessLogicLayer.services;
using EntitiesLayer;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Data.SqlClient;


namespace IntegrationTesting
{
    [Collection("Database collection")]
    public class NotificationService_integrationTest
    {
        readonly NotificationService notifService;
        readonly MemberService memberService;
        readonly DatabaseFixture fixture;

        readonly string createLibrary =
        "INSERT [dbo].[Library] ([id], [name], [OIB], [phone], [email], [price_day_late], [address], [membership_duration]) " +
        "VALUES (1, N'LibraryB', 54321, 332, N'emailB', 2, N'addressB', GETDATE())";

        readonly string createNotifications =
        "INSERT [dbo].[Notification] ([Library_id], [title], [description]) " +
        "VALUES (1, N'Notification1', N'Notification1'); " +
        "INSERT [dbo].[Notification] ([Library_id], [title], [description]) " +
        "VALUES (1, N'Notification2', N'Notification2');";

        readonly string createMember = "INSERT [dbo].[Member] ([name], [surname], [OIB], [username], [password], [barcode_id], [membership_date], [Library_id]) " + "VALUES (N'name', N'surname', 12345123452, N'username', N'password', N'ea98ujjf', GETDATE(), 1);";

        public NotificationService_integrationTest(DatabaseFixture fixture)
        {
            notifService = new NotificationService();
            memberService = new MemberService();
            this.fixture = fixture;
            this.fixture.ResetDatabase();
        }
        //Magdalena Markovinović
        [Fact]

        public void GetAllNotificationByLibrary_GivenFunctionIsCalled_ReturnsListOfNotifications()
        {
            //Arrange
            Helper.ExecuteCustomSql(createLibrary);
            Helper.ExecuteCustomSql(createNotifications);

            var notificationList = new List<Notification>
            {
                new Notification { id = 1, Library_id = 1, title = "Notification1", description = "Notification1" },
                new Notification { id = 2, Library_id = 1, title = "Notification2", description = "Notification2" }
            };

            //Act
            var result = notifService.GetAllNotificationByLibrary(1);

            //Assert
            result.Should().BeEquivalentTo(notificationList, options => options.Excluding(n => n.Library));

        }

        //Magdalena Markovinović
        [Fact]
        private void AddNewNotification_GivenFunctionIsCalled_ReturnsTrue()
        {
            //Arrange
            Helper.ExecuteCustomSql(createLibrary);
            var notification = new Notification { id = 3, Library_id = 1, title = "Notification3", description = "Notification3" };

            //Act
            var result = notifService.AddNewNotification(notification);

            //Assert
            result.Should().BeTrue();
        }

        //Magdalena Markovinović
        [Fact]
        private void AddNotificationRead_GivenFunctionIsCalled_ReturnsTrue()
        {
            //Arrange
            Helper.ExecuteCustomSql(createLibrary);
            Helper.ExecuteCustomSql(createNotifications);
            Helper.ExecuteCustomSql(createMember);

            var member = memberService.GetMemberByUsername("username");
            memberService.Dispose();
            var notifications = notifService.GetAllNotificationByLibrary(1);

            //Act
            var result = notifService.AddNotificationRead(notifications[0], member);
            var readNotifications = notifService.GetReadNotificationsForMember(member);

            //Assert
            result.Should().BeTrue();
            readNotifications.Should().NotBeEmpty();
        }

        //Magdalena Markovinović
        [Fact]
        private void EditNotification_GivenFunctionIsCalled_ReturnsTrue()
        {
            //Arrange
            Helper.ExecuteCustomSql(createLibrary);
            Helper.ExecuteCustomSql(createNotifications);

            var notification = notifService.GetAllNotificationByLibrary(1)[0];
            notification.title = "Notification1Edited";
            notification.description = "Notification1Edited";

            //Act
            var result = notifService.EditNotification(notification);
            var editedNotification = notifService.GetAllNotificationByLibrary(1)[0];

            //Assert
            result.Should().BeTrue();
            editedNotification.Should().BeEquivalentTo(notification, options => options.Excluding(n => n.Library));
        }

        //Magdalena Markovinović
        [Fact]
        private void GetReadNotificationsForMember_GivenFunctionIsCalled_ReturnsTrue() 
        { 
            // Arrange
            Helper.ExecuteCustomSql(createLibrary);
            Helper.ExecuteCustomSql(createNotifications);
            Helper.ExecuteCustomSql(createMember);

            var member = memberService.GetMemberByUsername("username");
            memberService.Dispose();
            var notifications = notifService.GetAllNotificationByLibrary(1);
            var readNotifications = notifService.AddNotificationRead(notifications[0], member);

            // Act
            var result = notifService.GetReadNotificationsForMember(member);

            // Assert  
            result.Should().NotBeNull();
        }

        //Magdalena Markovinović
        [Fact]
        public void Remove_GivenFunctionIsCalled_ReturnsTrue()
        {
            //Arrange
            Helper.ExecuteCustomSql(createLibrary);
            Helper.ExecuteCustomSql(createNotifications);

            var notification = notifService.GetAllNotificationByLibrary(1)[0];

            //Act
            var result = notifService.Remove(notification);
            var notifications = notifService.GetAllNotificationByLibrary(1);

            //Assert
            result.Should().Be(1);
            notifications.Should().NotContain(notification);
        }
    }
}
