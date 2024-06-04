using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer {
    public partial class Member {
        public override string ToString() {
            return barcode_id + " - " + name + " " + surname;
        }
    }
}
