using BussinessLogicLayer.services;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting {

    [Collection("Database collection")]
    public class StatisticsService_integrationTest {

        readonly StatisticsService statisticsService;
        readonly DatabaseFixture fixture;

        readonly Library library;

        public StatisticsService_integrationTest(DatabaseFixture fixture)
        {
            statisticsService = new StatisticsService();
            this.fixture = fixture;
            this.fixture.ResetDatabase();

            library = new Library {
                id = 123,
                name = "Testna knjiznica",
                OIB = "11112222333",
                price_day_late = 3.5m,
                membership_duration = new DateTime(2024, 6, 23)
            };
            InsertLibraryIntoDatabase(library);

        }

        private void InsertLibraryIntoDatabase(Library library) {
            var formattedMembershipDuration = library.membership_duration.ToString("yyyy-MM-dd");
            string sqlInsertLibrary = $"INSERT [dbo].[Library] ([id], [name], [OIB], [price_day_late], [membership_duration]) VALUES ('{library.id}', '{library.name}', '{library.OIB}', {library.price_day_late}, '{formattedMembershipDuration}');";
            Helper.ExecuteCustomSql(sqlInsertLibrary);
        }
    }
}
