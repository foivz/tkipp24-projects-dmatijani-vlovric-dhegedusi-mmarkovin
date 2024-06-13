using BussinessLogicLayer.services;
using EntitiesLayer;
using FluentAssertions;
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
        public void CheckLoginCredentials_GivenFunctionIsCalled_ReturnsSuccesfullLogin()
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

        [Fact]
        public void CheckLoginCredentials_GivenFunctionIsCalled_ReturnsUnsucessfulLogin()
        {
            // Arrange
            string username = "invalidUsername";
            string password = "password123";

            // Act
            memberService.CheckLoginCredentials(username, password);

            // Assert
            Assert.NotEqual(username, LoggedUser.Username);
            Assert.NotEqual(Role.Member, LoggedUser.UserType);
            Assert.NotEqual(1, LoggedUser.LibraryId);
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
        public void CheckMembershipDateLogin_WithExpiredMembership_ReturnsTrue()
        {
            // Arrange
            string expiredMembershipDate = "2023-01-01";
            string createMemberWithExpiredDate =
                "INSERT [dbo].[Member] ([name], [surname], [OIB], [username], [password], [barcode_id], [membership_date], [Library_id]) " +
                $"VALUES (N'name', N'surname', 44444123452, N'expiredUser', N'password123', N'sd98ujjf', '{expiredMembershipDate}', 1);";
            Helper.ExecuteCustomSql(createMemberWithExpiredDate);

            string username = "expiredUser";
            string password = "password123";

            // Act
            bool result = memberService.CheckMembershipDateLogin(username, password);

            // Assert
            Assert.True(result);
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

        // Magdalena Markovinović
        [Fact]
        public void CheckExistingUsername_GivenFunctionIsCalled_ReturnsFalse()
        {
            // Arrange
            Member member = new Member
            {
                username = "username1"
            };

            // Act
            bool result = memberService.CheckExistingUsername(member);

            // Assert
            Assert.False(result);
        }

        // Magdalena Markovinović
        [Fact]
        public void CheckBarcodeUnoque_GivenFunctionIsCalled_ReturnsTrue()
        {
            // Arrange
            Member member = new Member
            {
                barcode_id = "ea98ujjf"
            };

            // Act
            bool result = memberService.CheckBarcodeUnoque(member);

            // Assert
            Assert.True(result);
        }

        public void CheckBarcodeUnoque_GivenFunctionIsCalled_ReturnsFalse()
        {
            // Arrange
            Member member = new Member
            {
                barcode_id = "sd98ujjf"
            };

            // Act
            bool result = memberService.CheckBarcodeUnoque(member);

            // Assert
            Assert.False(result);
        }

        // Magdalena Markovinović
        [Fact]
        private void CheckOibUnoque_GivenFunctionIsCalled_ReturnsTrue()
        {
            // Arrange
            Member member = new Member
            {
                OIB = "12345123452"
            };

            // Act
            bool result = memberService.CheckOibUnoque(member);

            // Assert
            Assert.True(result);
        }

        // Magdalena Markovinović
        [Fact]
        private void CheckOibUnoque_GivenFunctionIsCalled_ReturnsFalse()
        {
            // Arrange
            Member member = new Member
            {
                OIB = "33335123111"
            };

            // Act
            bool result = memberService.CheckOibUnoque(member);

            // Assert
            Assert.False(result);
        }

        // Magdalena Markovinović
        [Fact]
        private void AddNewMember_GivenFunctionIsCalled_ReturnsTrue()
        {
            // Arrange
            Member member = new Member
            {
                name = "name",
                surname = "surname",
                OIB = "44444444444",
                username = "newUser",
                password = "password111",
                barcode_id = "jas67ahg",
                membership_date = DateTime.Now,
                Library_id = 1
            };

            // Act
            bool result = memberService.AddNewMember(member);

            // Assert
            Assert.True(result);
        }

        // Magdalena Markovinović
        [Fact]
        public void UpdateMember_GivenMemberExists_ReturnsTrue()
        {
            // Arrange
            Member memberToEdit = memberService.GetMemberByUsername("username");
            memberToEdit.name = "newName";
            memberToEdit.surname = "newSurname";

            // Act
            bool result = memberService.UpdateMember(memberToEdit);

            // Assert
            Assert.True(result);
        }

        // Magdalena Markovinović
        [Fact]
        public void DeleteMember_GivenFunctionIsCalled_ReturnsTrue()
        {
            // Arrange
            Member memberToDelete = memberService.GetMemberByUsername("username");

            // Act
            bool result = memberService.DeleteMember(memberToDelete);

            // Assert
            Assert.True(result);
        }

        // Magdalena Markovinović
        [Fact]
        public void GetAllMembersByFilter_MemberExists_ReturnsListOfMembers()
        {
            // Arrange
            var filterMember = new List<Member>
            {
                new Member { id = 1, name = "name", surname = "surname", OIB = "12345123452", username = "username", password = "password", barcode_id = "ea98ujjf", membership_date = DateTime.Now, Library_id = 1 }
            };

            // Act
            var result = memberService.GetAllMembersByFilter(filterMember[0].name, filterMember[0].surname);

            // Assert
            result[0].name.Should().BeEquivalentTo(filterMember[0].name);
            result[0].surname.Should().BeEquivalentTo(filterMember[0].surname);
        }

        [Fact]
        public void GetAllMembersByFilter_MemberDontExist_ReturnsNull()
        {
            // Arrange
            var filterMember = new List<Member>
            {
                new Member { id = 1, name = "noName", surname = "NoSurename", OIB = "12345123452", username = "username", password = "password", barcode_id = "ea98ujjf", membership_date = DateTime.Now, Library_id = 1 }
            };

            // Act
            var result = memberService.GetAllMembersByFilter(filterMember[0].name, filterMember[0].surname);

            // Assert
            result.Should().BeEmpty();
        }
        // Magdalena Markovinović
        [Fact]
        public void GetMemberId_MemberExists_ReturnsMember()
        {
            // Arrange
            string username = "username";

            // Act
            var result = memberService.GetMemberId(username);

            // Assert
            result.Should().Be(1);
        }

        // Magdalena Markovinović
        [Fact]
        public void GetMemberByUsername_MemberExists_ReturnsMember()
        {
            // Arrange
            string username = "username";

            // Act
            var result = memberService.GetMemberByUsername(username);

            // Assert
            result.username.Should().BeEquivalentTo(username);
        }
    }
}
