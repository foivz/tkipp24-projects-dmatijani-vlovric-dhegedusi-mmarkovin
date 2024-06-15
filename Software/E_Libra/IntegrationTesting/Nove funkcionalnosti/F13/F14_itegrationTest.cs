using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting.Nove_funkcionalnosti.F13
{
    [Collection("Database collection")]
    public class F14_itegrationTest
    {
        readonly DatabaseFixture fixture;
        readonly MemberService memberService;
        readonly EmployeeService employeeService;

        readonly string createLibrary =
           "INSERT [dbo].[Library] ([id], [name], [OIB], [phone], [email], [price_day_late], [address], [membership_duration]) " +
           "VALUES (1, N'LibraryB', 54321, 332, N'emailB', 2, N'addressB', '2024-06-18')";

        readonly string createMember =
            "INSERT [dbo].[Member] ([name], [surname], [OIB], [username], [password], [barcode_id], [membership_date], [Library_id]) " +
            "VALUES (N'name', N'surname', 12345123452, N'username', N'password', N'ea98ujjf', GETDATE(), 1);";

        public F14_itegrationTest(DatabaseFixture fixture)
        {
            memberService = new MemberService();
            this.fixture = fixture;
            this.fixture.ResetDatabase();

            InitializeDatabase();

            LoggedUser.Username = null;
            LoggedUser.LibraryId = 0;
        }

        private void InitializeDatabase()
        {
            Helper.ExecuteCustomSql(createLibrary);
            Helper.ExecuteCustomSql(createMember);
        }

        private void UpdateLibraryMembershipDuration(int daysUntilExpiration)
        {
            string newMembershipDurationDate = DateTime.Today.AddDays(daysUntilExpiration).ToString("yyyy-MM-dd");
            string updateLibrary =
               $"UPDATE [dbo].[Library] SET [membership_duration] = '{newMembershipDurationDate}' WHERE [id] = 1";
            Helper.ExecuteCustomSql(updateLibrary);
        }


        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void CalculateDaysUntilExpiration_GivenMembershipExpiresInSomeDays_ReturnDaysUntilexpiration(int daysUntilExpiration)
        {
            // Arrange
            LoggedUser.Username = "username";
            LoggedUser.LibraryId = 1;
            UpdateLibraryMembershipDuration(daysUntilExpiration);

            // Act 
            var result = memberService.CalculateDaysUntilExpiration();

            // Assert
            Assert.Equal(daysUntilExpiration, result);
        }

        [Fact]
        public void CalculateDaysUntilExpiration_GivenMembershipExpiresIn6days_EqualsZero()
        {
            // Arrange
            LoggedUser.Username = "username";
            LoggedUser.LibraryId = 1;
            UpdateLibraryMembershipDuration(6);

            // Act 
            var result = memberService.CalculateDaysUntilExpiration();

            // Assert
            Assert.Equal(result, 0);
        }

        [Fact]
        public void CalculateDaysUntilExpiration_GivenUnexistingUser_ReturnsNull()
        {
            // Arrange
            string unexistingUsername = "unexistingUser";
            LoggedUser.Username = unexistingUsername;
            LoggedUser.LibraryId = 1;

            // Act 
            var result = memberService.CalculateDaysUntilExpiration();

            // Assert
            Assert.Equal(result, 0);
        }



    }
}
