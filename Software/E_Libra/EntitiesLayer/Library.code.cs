﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer {
    public partial class Library {
        public override string ToString() {
            return id + " - " + name;
        }
    }
}
