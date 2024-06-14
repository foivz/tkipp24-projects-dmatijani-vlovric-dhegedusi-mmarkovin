using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using EntitiesLayer;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.Nove_funkcionalnosti.F14
{
    public class F14_unitTest
    {
        private MemberService memberService;
        private IMembersRepository membersRepository;

        public F14_unitTest()
        {
            membersRepository = A.Fake<IMembersRepository>();
            memberService = new MemberService(membersRepository, null, null, null, null);

            LoggedUser.Username = null;
            LoggedUser.UserType = null;
            LoggedUser.LibraryId = 0;
        }
    }
}
