using System;
using System.Collections.Generic;
using RNAFoldingSim.Models;

namespace RNAFoldingSim.Models
{
    public class ConvexHull
    {
        public Base rightMost;
        public List<HullPoint> hull;
        public List<Base> bases;
        public ConvexHull(List<Base> bases)
        {
            this.bases = bases;
            hull = new List<HullPoint>();
            createHull();
        }

        private void findRightMost()
        {
            Base rightMost = bases[0];
            foreach (Base b in bases)
            {
                if (b.location.Key == rightMost.location.Key && b.location.Value > rightMost.location.Value)
                {
                    rightMost = b;
                }
                else if (b.location.Key > rightMost.location.Key)
                {
                    rightMost = b;
                }
            }
            this.rightMost = rightMost;
        }

        public void createHull()
        {
            findRightMost();
            Base currentBase = rightMost;
            double currentDirection = Math.PI / 2;
            double nextDirection, nextAngle;
            double angle;
            double difference;
            Base nextBase;
            nextDirection = 0;
            Console.WriteLine("location: " + rightMost.location.Key + " " + rightMost.location.Value);
            do
            {
                nextAngle = 2 * Math.PI;
                nextBase = null;
                foreach (Base b in bases)
                {
                    if (b == currentBase) { continue; }
                    //find angle
                    angle = Math.Atan2(b.location.Value - currentBase.location.Value, b.location.Key - currentBase.location.Key);
                    if (angle < 0) { angle = 2 * Math.PI + angle; }
                    if (angle < currentDirection) { angle += 2 * Math.PI; }
                    difference = Math.Abs((currentDirection - angle) % (Math.PI * 2));
                    if (difference < nextAngle)
                    {
                        Console.WriteLine("angle: " + angle);
                        Console.WriteLine("difference: " + difference);
                        nextAngle = difference;
                        nextBase = b;
                        nextDirection = angle;
                    }
                }
                Console.WriteLine("location: " + nextBase.location.Key + " " + nextBase.location.Value);
                hull.Add(new HullPoint(nextBase));
                currentBase = nextBase;
                currentDirection = nextDirection;
            } while (currentBase != rightMost);
        }
    }
}
