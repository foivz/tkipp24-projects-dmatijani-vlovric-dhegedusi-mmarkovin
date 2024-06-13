using BussinessLogicLayer.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTesting
{
    public class MemberService_integrationTest
    {
        readonly DatabaseFixture fixture;
        readonly MemberService memberService;

        public MemberService_integrationTest(DatabaseFixture fixture)
        {
            memberService = new MemberService();
            this.fixture = fixture;
            this.fixture.ResetDatabase();
        }
    }
}
