using BussinessLogicLayer.services;
using DataAccessLayer.Interfaces;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    public class EmployeeService_unitTest
    {
        private IEmpoloyeeRepositroy empoloyeeRepositroy;
        private EmployeeService employeeService;
        public EmployeeService_unitTest()
        {
            empoloyeeRepositroy = A.Fake<IEmpoloyeeRepositroy>();
            employeeService = new EmployeeService(empoloyeeRepositroy, null, null);
        }
    }
}
