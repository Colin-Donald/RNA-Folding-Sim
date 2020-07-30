using System;
namespace RNAFoldingSim.Models
{
    public class StepPoint
    {
        public double x, y;
        public StepPoint(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public StepPoint(Base b)
        {
            this.x = b.location.Key;
            this.y = b.location.Value;
        }
    }
}
