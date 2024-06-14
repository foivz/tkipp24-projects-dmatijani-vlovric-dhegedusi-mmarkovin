using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using BussinessLogicLayer.services;

namespace IntegrationTesting.Nove_funkcionalnosti.F13
{
    [Collection("Database collection")]
    public class AuthorService_intTest
    {
        readonly AuthorService service;
        readonly DatabaseFixture fixture;

        public AuthorService_intTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
            this.fixture.ResetDatabase();
            service = new AuthorService();
        }

    }
}
