using System;
using RNAFoldingSim.Models;

namespace RNAFoldingSim.Models
{
    public class HullPoint
    {
        public double x, y;
        public HullPoint(Base b)
        {
            this.x = b.location.Key;
            this.y = b.location.Value;
        }
    }
}
