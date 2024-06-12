using BussinessLogicLayer.services;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting
{
    public class Proba
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            Member member = new Member
            {
                username = "anabol1",
            };
            MemberService memberService = new MemberService();
            //Act
            var result = memberService.CheckExistingUsername(member);

            //Assert
            Assert.True(result);
        }
    }
}
