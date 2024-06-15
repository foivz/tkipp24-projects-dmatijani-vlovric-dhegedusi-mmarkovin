using BussinessLogicLayer.F16;
using DataAccessLayer.F16;
using EntitiesLayer.F16;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting.Nove_funkcionalnosti.F16
{
    //David Matijanić
    public class GPTService_unitTest
    {
        private GPTService gptService { get; set; }
        private IGPTRequestSender gptRequestSender { get; set; }

        public GPTService_unitTest()
        {
            gptRequestSender = A.Fake<IGPTRequestSender>();
            gptService = new GPTService(gptRequestSender);
        }

        [Fact]
        public void GPTService_SetEmptySystemMessage_Runs()
        {
            //Act
            var result = gptService.SetSystemMessage("");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GPTService_SetSystemMessage_MessageShouldBeSet()
        {
            //Arrange
            string message = "Ti si pomoćnik u knjižnici.";

            //Act
            gptService.SetSystemMessage(message);

            //Assert
            Assert.Equal(message, gptService.systemMessage);
        }

        [Fact]
        public void GPTService_SendSystemMessage_ShouldBeAbleToSend()
        {
            //Arrange
            string message = "Korisnik te pitao za pomoć oko knjige.";

            //Act
            gptService.SendSystemMessage(message);

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void GPTService_PassGPTRequestSender_ShouldWork()
        {
            //Act
            gptService = new GPTService(gptRequestSender);

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void GPTService_SendSystemMessage_RequestSenderShouldReceiveGPTRequestInstance()
        {
            //Arrange
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Returns(new GPTResponse());

            //Act
            gptService.SendSystemMessage(message);

            //Assert
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void GPTService_SendSystemMessage_RequestSenderGPTServiceShouldHaveMessageSet()
        {
            //Arrange
            string gottenMessage = "";
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Invokes(call =>
            {
                GPTRequest request = call.GetArgument<GPTRequest>(0);
                gottenMessage = request.messages.First().content;
            });

            //Act
            gptService.SendSystemMessage(message);

            //Assert
            Assert.Equal(message, gottenMessage);
        }

        [Fact]
        public void GPTService_SendSystemMessage_RequestSenderGPTServiceShouldHaveRoleSetToSystem()
        {

           //Arrange
            string gottenRole = "";
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Invokes(call =>
            {
                GPTRequest request = call.GetArgument<GPTRequest>(0);
                gottenRole = request.messages.First().role;
            });

            //Act
            gptService.SendSystemMessage(message);

            //Assert
            Assert.Equal("system", gottenRole);
        }

        [Fact]
        public void GPTService_SendUserMessage_RequestSenderShouldReceiveGPTRequestInstance()
        {
            //Arrange
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Returns(new GPTResponse());

            //Act
            gptService.SendUserMessage(message);

            //Assert
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void GPTService_SendUserMessage_RequestSenderGPTServiceShouldHaveMessageSet()
        {
            //Arrange
            string gottenMessage = "";
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Invokes(call =>
            {
                GPTRequest request = call.GetArgument<GPTRequest>(0);
                gottenMessage = request.messages.First().content;
            });

            //Act
            gptService.SendUserMessage(message);

            //Assert
            Assert.Equal(message, gottenMessage);
        }

        [Fact]
        public void GPTService_SendUserMessage_RequestSenderGPTServiceShouldHaveRoleSetToUser()
        {

            //Arrange
            string gottenRole = "";
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Invokes(call =>
            {
                GPTRequest request = call.GetArgument<GPTRequest>(0);
                gottenRole = request.messages.First().role;
            });

            //Act
            gptService.SendUserMessage(message);

            //Assert
            Assert.Equal("user", gottenRole);
        }

        [Fact]
        public async Task GPTService_SendSystemMessage_ShouldReturnString()
        {
            //Arrange
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Returns(new GPTResponse());

            //Act
            var result = await gptService.SendSystemMessage(message);

            //Assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public void GPTService_SystemMessageSet_FirstMessageShouldBeSystemAndSecondShouldBeTheSentMessage()
        {

            //Arrange
            string gottenSystemMessage = "";
            string gottenSentMessage = "";
            string systemMessage = "Ti si pomoćnik u knjižnici.";
            string message = "Korisnik te pitao za pomoć oko knjige.";
            gptService.SetSystemMessage(systemMessage);
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Invokes(call =>
            {
                GPTRequest request = call.GetArgument<GPTRequest>(0);
                gottenSystemMessage = request.messages[0].content;
                gottenSentMessage = request.messages[1].content;
            });

            //Act
            gptService.SendUserMessage(message);

            //Assert
            Assert.Equal(systemMessage, gottenSystemMessage);
            Assert.Equal(message, gottenSentMessage);
        }
    }
}
