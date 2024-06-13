using BussinessLogicLayer.services;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting
{
    [Collection("Database collection")]
    public class MemberService_integrationTest
    {
        readonly DatabaseFixture fixture;
        readonly MemberService memberService;

        readonly string createLibrary =
        "INSERT [dbo].[Library] ([id], [name], [OIB], [phone], [email], [price_day_late], [address], [membership_duration]) " +
        "VALUES (1, N'LibraryB', 54321, 332, N'emailB', 2, N'addressB', GETDATE())";

        readonly string createMember = "INSERT [dbo].[Member] ([name], [surname], [OIB], [username], [password], [barcode_id], [membership_date], [Library_id]) " + "VALUES (N'name', N'surname', 12345123452, N'username', N'password', N'ea98ujjf', GETDATE(), 1);";


        public MemberService_integrationTest(DatabaseFixture fixture)
        {
            memberService = new MemberService();
            this.fixture = fixture;
            this.fixture.ResetDatabase();

            InitializeDatabase();
        }
        private void InitializeDatabase()
        {
            Helper.ExecuteCustomSql(createLibrary);
            Helper.ExecuteCustomSql(createMember);
        }

        // Magdalena Markovinović
        [Fact]
        public void CheckLoginCredentials_GivenFunctionIsCalled_EqualsLoggedUser()
        {
            // Arrange
            string username = "username";
            string password = "password";

            // Act
            memberService.CheckLoginCredentials(username, password);

            // Assert
            Assert.Equal(username, LoggedUser.Username);
            Assert.Equal(Role.Member, LoggedUser.UserType);
            Assert.Equal(1, LoggedUser.LibraryId);
        }

        // Magdalena Markovinović
        [Fact]
        public void CheckMembershipDateLogin_GivenFunctionIsCalled_ReturnsFalse()
        {
            // Arrange
            string username = "username";
            string password = "password";

            // Act
            bool result = memberService.CheckMembershipDateLogin(username, password);

            // Assert
            Assert.False(result);
        }

        // Magdalena Markovinović
        [Fact]
        public void CheckExistingUsername_GivenFunctionIsCalled_ReturnsTrue()
        {
            // Arrange
            Member member = new Member
            {
                username = "username"
            };

            // Act
            bool result = memberService.CheckExistingUsername(member);

            // Assert
            Assert.True(result);
        }
    }
}
