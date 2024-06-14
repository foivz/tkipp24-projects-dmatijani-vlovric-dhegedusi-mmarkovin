﻿using BussinessLogicLayer.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting.Nove_funkcionalnosti.F13
{
    [Collection("Database collection")]
    public class GenreServices_intTest
    {
        readonly GenreServices services;
        readonly DatabaseFixture fixture;

        public GenreServices_intTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
            this.fixture.ResetDatabase();
            services = new GenreServices();
        }


    }
}
