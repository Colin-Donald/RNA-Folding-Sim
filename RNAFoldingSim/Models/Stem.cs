using System;
using System.Collections.Generic;
using System.Text;
using RNAFoldingSim.Models;

namespace RNAFoldingSim
{
    class Stem
    {
        public Loop parentLoop;
        public Loop childLoop;
        public List<Base> bases = new List<Base>();
        public double pdLength;
    }
}
