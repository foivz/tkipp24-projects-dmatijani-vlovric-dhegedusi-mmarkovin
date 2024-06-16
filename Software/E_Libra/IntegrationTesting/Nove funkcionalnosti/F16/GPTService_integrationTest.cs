using BussinessLogicLayer.F16;
using DataAccessLayer.F16;
using EntitiesLayer.F16;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting.Nove_funkcionalnosti.F16
{
    //David Matijanić
    public class GPTService_integrationTest
    {
        private GPTService gptService { get; set; }

        public GPTService_integrationTest()
        {
            gptService = new GPTService(new GPTRequestSender("API-key"));
        }

        [Fact]
        public async Task SendSystemMessage_MessageSent_ShouldReturnString()
        {
            //Arrange
            string message = "Korisnik te pitao za pomoć oko knjige.";

            //Act
            var result = await gptService.SendSystemMessage(message);

            //Assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public async Task SendSystemMessage_SystemMessageSetAndMessageSent_ShouldReturnString()
        {
            //Arrange
            string message = "Korisnik te pitao za pomoć oko knjige.";
            gptService.SetSystemMessage("Ti si pomoćnik u knjižnici.");

            //Act
            var result = await gptService.SendSystemMessage(message);

            //Assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public async Task SendUserMessage_MessageSent_ShouldReturnString()
        {
            //Arrange
            string message = "Korisnik te pitao za pomoć oko knjige.";

            //Act
            var result = await gptService.SendUserMessage(message);

            //Assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public async Task SendUserMessage_SystemMessageSetAndMessageSent_ShouldReturnString()
        {
            //Arrange
            string message = "Korisnik te pitao za pomoć oko knjige.";
            gptService.SetSystemMessage("Ti si pomoćnik u knjižnici.");

            //Act
            var result = await gptService.SendUserMessage(message);

            //Assert
            Assert.IsType<string>(result);
        }
    }
}
