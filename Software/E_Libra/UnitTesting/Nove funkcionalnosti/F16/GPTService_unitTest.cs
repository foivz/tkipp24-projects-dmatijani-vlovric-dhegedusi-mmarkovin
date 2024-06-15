﻿using BussinessLogicLayer.F16;
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

        public GPTService_unitTest()
        {
            gptService = new GPTService();
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
    }
}
