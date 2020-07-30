using System;
using System.Collections.Generic;
using System.Text;

namespace RNAFoldingSim.Models
{
    class Slice
    {
        public List<Base> bases = new List<Base>();
        public double pdLength;
        public double pdAngle;
    }
}
