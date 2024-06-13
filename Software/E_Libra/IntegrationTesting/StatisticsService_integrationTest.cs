using BussinessLogicLayer.services;
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

        public StatisticsService_integrationTest(DatabaseFixture fixture)
        {
            statisticsService = new StatisticsService();
            this.fixture = fixture;
            this.fixture.ResetDatabase();
        }
    }
}
