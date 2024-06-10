using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting
{
    public class ReservationService_unitTest
    {
        readonly IReservationRepository reservationRepo;
        readonly ReservationService reservationService;

        readonly IBookRepository bookRepo;
        readonly BookServices bookServices;

        readonly IMembersRepository membersRepo;




        public ReservationService_unitTest()
        {
            reservationRepo = A.Fake<IReservationRepository>();
            bookRepo = A.Fake<IBookRepository>();
            membersRepo = A.Fake<IMembersRepository>();
            bookServices = new BookServices(bookRepo, reservationRepo, membersRepo);
            reservationService = new ReservationService(reservationRepo, bookServices);
        }

        [Fact]
        public void 
    }
}
