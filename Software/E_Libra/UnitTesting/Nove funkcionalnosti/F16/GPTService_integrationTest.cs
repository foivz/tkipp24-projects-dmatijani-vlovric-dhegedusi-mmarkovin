using BussinessLogicLayer.F16;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting.Nove_funkcionalnosti.F16
{
    //David Matijanić
    public class GPTService_integrationTest
    {
        [Fact]
        public void GPTService_SetEmptySystemMessage_Runs()
        {
            //Arrange
            var gptService = new GPTService();

            //Act
            var result = gptService.SetSystemMessage("");

            //Assert
            Assert.True(result);
        }
    }
}
