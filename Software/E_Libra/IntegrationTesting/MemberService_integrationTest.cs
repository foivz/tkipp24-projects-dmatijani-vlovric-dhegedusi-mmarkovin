using BussinessLogicLayer.Exceptions;
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
        readonly EmployeeService employeeService;

        readonly string createLibrary =
        "INSERT [dbo].[Library] ([id], [name], [OIB], [phone], [email], [price_day_late], [address], [membership_duration]) " +
        "VALUES (1, N'LibraryB', 54321, 332, N'emailB', 2, N'addressB', '2024-01-30')";

        readonly string createEmployee = "INSERT[dbo].[Employee] ([name], [surname], [OIB], [username], [password],[Library_id]) " +
        "VALUES (N'employee', N'employee', 23453456555, N'employee', N'employee', 1)";

        readonly string createMember =
        "INSERT [dbo].[Member] ([name], [surname], [OIB], [username], [password], [barcode_id], [membership_date], [Library_id]) " +
        "VALUES (N'name', N'surname', 12345123452, N'username', N'password', N'ea98ujjf', GETDATE(), 1);" +
        "INSERT [dbo].[Member] ([name], [surname], [OIB], [username], [password], [barcode_id], [membership_date], [Library_id]) " +
        "VALUES (N'anotherName', N'anotherSurname', 98765432109, N'anotherUsername', N'anotherPassword', N'bc92klmn', GETDATE(), 1);";

        readonly string createGenre =
            "INSERT [dbo].[Genre] ([name]) VALUES (N'zanr')";

        string createBook = "INSERT INTO [dbo].[Book] " +
        "([name], [description], [publish_date], [pages_num], [digital], [url_digital], [url_photo], [barcode_id], [total_copies], [current_copies], [Genre_id], [Library_id]) " +
        "VALUES " +
        "('NewBookName', 'New Description', '2023-04-01', 300, 0, NULL, NULL,'jdk89sjz', 20, 20, 1, 1);";


        public MemberService_integrationTest(DatabaseFixture fixture)
        {
            memberService = new MemberService();
            employeeService = new EmployeeService();
            this.fixture = fixture;
            this.fixture.ResetDatabase();

            InitializeDatabase();

            LoggedUser.Username = null;
            LoggedUser.LibraryId = 0;
        }
        private void InitializeDatabase()
        {
            Helper.ExecuteCustomSql(createLibrary);
            Helper.ExecuteCustomSql(createEmployee);
            Helper.ExecuteCustomSql(createMember);
            Helper.ExecuteCustomSql(createGenre);
            Helper.ExecuteCustomSql(createBook);
        }

        private Member CreateMemberWithExpiredMembership()
        {
            string expiredMembershipDate = "2023-01-01";
            string createMemberWithExpiredDate =
                "INSERT [dbo].[Member] ([name], [surname], [OIB], [username], [password], [barcode_id], [membership_date], [Library_id]) " +
                $"VALUES (N'name', N'surname', 44444123452, N'expiredUser', N'password123', N'ssde45f5', '{expiredMembershipDate}', 1);";
            Helper.ExecuteCustomSql(createMemberWithExpiredDate);
            return memberService.GetMemberByUsername("expiredUser");
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
            LoggedUser.Username = null;
            LoggedUser.UserType = Role.Employee;
            LoggedUser.LibraryId = 0;

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
            CreateMemberWithExpiredMembership();

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
        public void DeleteMember_GivenMemberHasNoReservationsAndBorrows_ReturnsTrue()
        {
            // Arrange
            Member memberToDelete = memberService.GetMemberByUsername("username");

            // Act
            bool result = memberService.DeleteMember(memberToDelete);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteMember_GivenMemberHasBorrows_ReturnsFalse()
        {
            // Arrange
            string sqlInsertBorrow = $"INSERT [dbo].[Borrow] ([Book_id], [Member_id], [borrow_status], [borrow_date], [return_date], [Employee_borrow_id], [Employee_return_id]) VALUES (1, 2, 2, '2023-04-01', GETDATE(), 1, 1);";

            Helper.ExecuteCustomSql(sqlInsertBorrow);
            Member memberToDelete = memberService.GetMemberByUsername("anotherUsername");

            // Act
            bool result = memberService.DeleteMember(memberToDelete);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void DeleteMember_GivenMemberHasReservations_ReturnsFalse()
        {
            // Arrange
            string sqlInsertReservation = "INSERT INTO [dbo].[Reservation] ([reservation_date], [Member_id], [Book_id]) VALUES ('2024-06-11', 2, 1);";
            Helper.ExecuteCustomSql(sqlInsertReservation);
            Member memberToDelete = memberService.GetMemberByUsername("anotherUsername");

            // Act
            bool result = memberService.DeleteMember(memberToDelete);

            // Assert
            Assert.False(result);
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
        // Magdalena markovinović
        [Fact]
        public void GetMemberLibraryId_MemberExists_ReturnsLibraryId()
        {
            // Arrange
            string username = "username";

            // Act
            var result = memberService.GetMemberLibraryId(username);

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

        // Magdalena Markovinović
        [Fact]
        public void GetAllMembersByLybrary_MembersExist_ReturnsListOfMembers()
        {
            // Arrange
            var libraryMembers = new List<Member>
            {
                new Member { id = 1, name = "name", surname = "surname", OIB = "12345123452", username = "username", password = "password", barcode_id = "ea98ujjf", membership_date = DateTime.Now, Library_id = 1 },
                new Member { id = 2, name = "anotherName", surname = "anotherSurname", OIB = "98765432109", username = "anotherUsername", password = "anotherPassword", barcode_id = "bc92klmn", membership_date = DateTime.Now, Library_id = 1 }
            };
            Employee loggedEmployee = employeeService.GetEmployeeByUsername("employee");
            LoggedUser.Username = loggedEmployee.name;
            LoggedUser.LibraryId = loggedEmployee.Library_id;

            // Act
            var result = memberService.GetAllMembersByLybrary();

            // Assert
            result[0].Should().BeEquivalentTo(libraryMembers[0], options => options
            .Excluding(n => n.Library)
            .Excluding(n => n.membership_date));
            result[1].Should().BeEquivalentTo(libraryMembers[1], options => options
            .Excluding(n => n.Library)
            .Excluding(n => n.membership_date));
        }

        // Magdalena Markovinović
        [Fact]
        public void GetAllMembersByLybrary_MembersDoNotExist_ReturnsEmptyList()
        {
            // Arrange
            var sqlDeleteMember = "DELETE FROM [dbo].[Member];";
            Helper.ExecuteCustomSql(sqlDeleteMember);
            Employee loggedEmployee = employeeService.GetEmployeeByUsername("employee");
            LoggedUser.Username = loggedEmployee.name;
            LoggedUser.LibraryId = loggedEmployee.Library_id;

            // Act
            var result = memberService.GetAllMembersByLybrary();

            // Assert
            result.Should().BeEmpty();
        }

        // Magdalena markovinović
        [Fact]
        public void RestoreMembership_GivenMembershipExpired_ReturnsTrue()
        {
            // Arrange
            Member member = CreateMemberWithExpiredMembership();

            // Act
            bool result = memberService.RestoreMembership(member);

            // Assert
            Assert.True(result);
        }

        // Magdalena markovinović
        [Fact]
        public void RestoreMembership_GivenMembershipDidNotExpire_ReturnsFalse()
        {
            // Arrange
            Member member = memberService.GetMemberByUsername("username");

            // Act
            bool result = memberService.RestoreMembership(member);

            // Assert
            Assert.False(result);
        }

        // Magdalena Markovinović
        [Fact]
        public void RandomCodeGenerator_GivenFunctionIsCalled_ReturnsRandomCode()
        {
            // Arrange
            string code = memberService.RandomCodeGenerator();

            // Act
            bool result = code.Length == 8;

            // Assert
            Assert.True(result);
        }

        //Magdalena markovinović
        [Fact]
        public void GetMemberByBarcodeId_MemberExists_ReturnsMember()
        {
            // Arrange
            int libraryId = 1;
            string barcodeId = "ea98ujjf";

            // Act
            var result = memberService.GetMemberByBarcodeId(libraryId, barcodeId);

            // Assert
            result.barcode_id.Should().BeEquivalentTo(barcodeId);
        }

        //Magdalena markovinović
        [Fact]
        public void GetMemberByBarcodeId_MemberDoesNotExist_ThrowsException()
        {
            // Arrange
            int libraryId = 1;
            string barcodeId = "invalidBarcode";

            // Act
            Action act = () => memberService.GetMemberByBarcodeId(libraryId, barcodeId);

            // Assert
            act.Should().Throw<MemberNotFoundException>();
        }

        //Magdalena markovinović
        [Fact]
        public void GetMemberByBarcodeId_MemberFromDifferentLibrary_ThrowsException()
        {
            // Arrange
            int libraryId = 2;
            string barcodeId = "ea98ujjf";

            // Act
            Action act = () => memberService.GetMemberByBarcodeId(libraryId, barcodeId);

            // Assert
            act.Should().Throw<WrongLibraryException>();
        }

        //Magdalena markovinović

        [Fact]
        public void GetMemberBarcode_MemberExists_ReturnsBarcode()
        {
            // Arrange
            int id = 1;
            string barcodeId = "ea98ujjf";

            // Act
            var result = memberService.GetMemberBarcode(id);

            // Assert
            result.Should().BeEquivalentTo(barcodeId);
        }

        // Magdalena Markovinović
        [Fact]
        public void GetMembersByLibrary_MembersExist_ReturnsListOfMembers()
        {
            // Arrange
            int libraryId = 1;
            var libraryMembers = new List<Member>
            {
                new Member { id = 1, name = "name", surname = "surname", OIB = "12345123452", username = "username", password = "password", barcode_id = "ea98ujjf", membership_date = DateTime.Now, Library_id = 1 },
                new Member { id = 2, name = "anotherName", surname = "anotherSurname", OIB = "98765432109", username = "anotherUsername", password = "anotherPassword", barcode_id = "bc92klmn", membership_date = DateTime.Now, Library_id = 1 }
            };

            // Act
            var result = memberService.GetMembersByLibrary(libraryId);

            // Assert
            result[0].Should().BeEquivalentTo(libraryMembers[0], options => options
            .Excluding(n => n.Library)
            .Excluding(n => n.membership_date));
            result[1].Should().BeEquivalentTo(libraryMembers[1], options => options
            .Excluding(n => n.Library)
            .Excluding(n => n.membership_date));
        }

        // Magdalena Markovinović
        [Fact]
        public void GetMembersByLibrary_MembersDoNotExist_ReturnsEmptyList()
        {
            // Arrange
            int libraryId = 2;

            // Act
            var result = memberService.GetMembersByLibrary(libraryId);

            // Assert
            result.Should().BeEmpty();
        }

        // Magdalena Markovinović
        [Fact]
        public void GetLibraryMembershipDuration_MembershipDurationExists_ReturnsDuration()
        {
            // Arrange
            int libraryId = 1;

            // Act
            var result = memberService.GetLibraryMembershipDuration(libraryId);

            // Assert
            result.Should().Be(30);
        }
    }
}
