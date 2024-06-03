using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public class ReservationViewModel
    {
        public int ReservationId {  get; set; }
        public string BookName { get; set; }
        public DateTime? Date { get; set; }
    }
}
