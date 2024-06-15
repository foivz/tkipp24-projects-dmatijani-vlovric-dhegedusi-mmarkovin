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
        public void SetSystemMessage_MessageIsEmpty_Runs()
        {
            //Act
            var result = gptService.SetSystemMessage("");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void SetSystemMessage_Set_MessageShouldBeSet()
        {
            //Arrange
            string message = "Ti si pomoćnik u knjižnici.";

            //Act
            gptService.SetSystemMessage(message);

            //Assert
            Assert.Equal(message, gptService.systemMessage);
        }

        [Fact]
        public void SendSystemMessage_MessageSent_ShouldBeAbleToSend()
        {
            //Arrange
            string message = "Korisnik te pitao za pomoć oko knjige.";

            //Act
            gptService.SendSystemMessage(message);

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void SendUserMessage_MessageSent_ShouldBeAbleToSend()
        {
            //Arrange
            string message = "Korisnik te pitao za pomoć oko knjige.";

            //Act
            gptService.SendUserMessage(message);

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
        public void SendSystemMessage_MessageSent_RequestSenderShouldReceiveGPTRequestInstance()
        {
            //Arrange
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Returns("");

            //Act
            gptService.SendSystemMessage(message);

            //Assert
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void SendSystemMessage_MessageSent_RequestSenderGPTServiceShouldHaveMessageSet()
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
        public void SendSystemMessage_MessageSent_RequestSenderGPTServiceShouldHaveRoleSetToSystem()
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
        public void SendUserMessage_MessageSent_RequestSenderShouldReceiveGPTRequestInstance()
        {
            //Arrange
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Returns("");

            //Act
            gptService.SendUserMessage(message);

            //Assert
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void SendUserMessage_MessageSent_RequestSenderGPTServiceShouldHaveMessageSet()
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
        public void SendUserMessage_MessageSent_RequestSenderGPTServiceShouldHaveRoleSetToUser()
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
        public async Task SendSystemMessage_MessageSent_ShouldReturnString()
        {
            //Arrange
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Returns("");

            //Act
            var result = await gptService.SendSystemMessage(message);

            //Assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public void SendUserMessage_SystemMessageSet_FirstMessageShouldBeSystemAndSecondShouldBeTheSentMessage()
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


        [Fact]
        public void SendUserMessage_SystemMessageSet_FirstRoleShouldBeSystemAndSecondRoleShouldBeUser()
        {
            //Arrange
            string gottenSystemRole = "";
            string gottenSentRole = "";
            string systemMessage = "Ti si pomoćnik u knjižnici.";
            string message = "Korisnik te pitao za pomoć oko knjige.";
            gptService.SetSystemMessage(systemMessage);
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Invokes(call =>
            {
                GPTRequest request = call.GetArgument<GPTRequest>(0);
                gottenSystemRole = request.messages[0].role;
                gottenSentRole = request.messages[1].role;
            });

            //Act
            gptService.SendUserMessage(message);

            //Assert
            Assert.Equal("system", gottenSystemRole);
            Assert.Equal("user", gottenSentRole);
        }

        [Fact]
        public void SendSystemMessage_SystemMessageSet_BothRolesShouldBeSystem()
        {
            //Arrange
            string gottenSystemRole = "";
            string gottenSentRole = "";
            string systemMessage = "Ti si pomoćnik u knjižnici.";
            string message = "Korisnik te pitao za pomoć oko knjige.";
            gptService.SetSystemMessage(systemMessage);
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Invokes(call =>
            {
                GPTRequest request = call.GetArgument<GPTRequest>(0);
                gottenSystemRole = request.messages[0].role;
                gottenSentRole = request.messages[1].role;
            });

            //Act
            gptService.SendSystemMessage(message);

            //Assert
            Assert.Equal("system", gottenSystemRole);
            Assert.Equal("system", gottenSentRole);
        }

        [Fact]
        public void SendUserMessage_UserMessageSent_GPTRequestShouldHaveModelSet()
        {
           //Arrange
            string gottenModel = "";
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Invokes(call =>
            {
                GPTRequest request = call.GetArgument<GPTRequest>(0);
                gottenModel = request.model;
            });

            //Act
            gptService.SendUserMessage(message);

            //Assert
            Assert.Equal("gpt-3.5-turbo", gottenModel);
        }

        [Fact]
        public void SendUserMessage_UserMessageSentWithoutTemperature_GPTRequestShouldHaveTemperatureSetTo1()
        {
            //Arrange
            double gottenTemperature = 0;
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Invokes(call =>
            {
                GPTRequest request = call.GetArgument<GPTRequest>(0);
                gottenTemperature = request.temperature;
            });

            //Act
            gptService.SendUserMessage(message);

            //Assert
            Assert.Equal(1, gottenTemperature);
        }

        [Fact]
        public void SendUserMessage_UserMessageSentWithTemperature_GPTRequestShouldHaveThatTemperatureSet()
        {
            //Arrange
            double temperatureToSet = 1.3;
            double gottenTemperature = 0;
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Invokes(call =>
            {
                GPTRequest request = call.GetArgument<GPTRequest>(0);
                gottenTemperature = request.temperature;
            });

            //Act
            gptService.SendUserMessage(message, temperatureToSet);

            //Assert
            Assert.Equal(temperatureToSet, gottenTemperature);
        }

        [Fact]
        public void SendSystemMessage_SystemMessageSent_GPTRequestShouldHaveModelSet()
        {
            //Arrange
            string gottenModel = "";
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Invokes(call =>
            {
                GPTRequest request = call.GetArgument<GPTRequest>(0);
                gottenModel = request.model;
            });

            //Act
            gptService.SendSystemMessage(message);

            //Assert
            Assert.Equal("gpt-3.5-turbo", gottenModel);
        }

        [Fact]
        public void SendSystemMessage_SystemMessageSentWithoutTemperature_GPTRequestShouldHaveTemperatureSetTo1()
        {
            //Arrange
            double gottenTemperature = 0;
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Invokes(call =>
            {
                GPTRequest request = call.GetArgument<GPTRequest>(0);
                gottenTemperature = request.temperature;
            });

            //Act
            gptService.SendSystemMessage(message);

            //Assert
            Assert.Equal(1, gottenTemperature);
        }

        [Fact]
        public void SendSystemMessage_SystemMessageSentWithTemperature_GPTRequestShouldHaveThatTemperatureSet()
        {
            //Arrange
            double temperatureToSet = 1.3;
            double gottenTemperature = 0;
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Invokes(call =>
            {
                GPTRequest request = call.GetArgument<GPTRequest>(0);
                gottenTemperature = request.temperature;
            });

            //Act
            gptService.SendSystemMessage(message, temperatureToSet);

            //Assert
            Assert.Equal(temperatureToSet, gottenTemperature);
        }

        [Fact]
        public void SendUserMessage_UserMessageSentAndModelSet_GPTRequestShouldHaveModelSet()
        {
            //Arrange
            gptService = new GPTService(gptRequestSender, "gpt-4o");
            string gottenModel = "";
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Invokes(call =>
            {
                GPTRequest request = call.GetArgument<GPTRequest>(0);
                gottenModel = request.model;
            });

            //Act
            gptService.SendUserMessage(message);

            //Assert
            Assert.Equal("gpt-4o", gottenModel);
        }

        [Fact]
        public void SendSystemMessage_SystemMessageSentAndModelSet_GPTRequestShouldHaveModelSet()
        {
            //Arrange
            gptService = new GPTService(gptRequestSender, "gpt-4o");
            string gottenModel = "";
            string message = "Korisnik te pitao za pomoć oko knjige.";
            A.CallTo(() => gptRequestSender.SendRequest(A<GPTRequest>.Ignored)).Invokes(call =>
            {
                GPTRequest request = call.GetArgument<GPTRequest>(0);
                gottenModel = request.model;
            });

            //Act
            gptService.SendSystemMessage(message);

            //Assert
            Assert.Equal("gpt-4o", gottenModel);
        }
    }
}
