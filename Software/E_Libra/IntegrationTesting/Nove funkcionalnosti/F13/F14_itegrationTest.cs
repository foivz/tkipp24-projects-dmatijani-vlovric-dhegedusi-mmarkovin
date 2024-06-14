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
           "VALUES (1, N'LibraryB', 54321, 332, N'emailB', 2, N'addressB', '2024-01-30')";

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

        [Fact]
        public void MembershipExpieringSoon_GivenMembershipExpiresInSomeDays_ReturnDaysUntilexpiration()
        {
            // Arrange
            LoggedUser.Username = "username";
            LoggedUser.LibraryId = 1;
            DateTime? expirationDate = DateTime.Today.AddDays(5);

            // Act
            var daysUntilExpiration = memberService.MembershipExpieringSoon();

            // Assert
            Assert.Equal(5, daysUntilExpiration);
        }
    }
}
