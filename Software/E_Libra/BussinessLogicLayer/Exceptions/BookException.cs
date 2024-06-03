using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Exceptions
{
    public class BookException: Exception
    {
        public string Poruka;
        public BookException(string poruka) : base(poruka) { 

            Poruka = poruka;

         }
    }
}
