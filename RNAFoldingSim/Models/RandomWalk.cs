using System;
using System.Collections.Generic;
using RNAFoldingSim.Models;

namespace RNAFoldingSim.Models
{
    public class RandomWalk
    {
        public HullPoint rightMost;
        public List<StepPoint> steps;
        private Random rng = new Random();
        private double max = 0.05;
        private double min = -0.05;
        public List<List<Coords>> walk = new List<List<Coords>>();
        private List<Base> bases;
        private double desiredLength = 0.05;
        private double initialDistanceApart = 20;
        private int numStepsAlongConvexHull = 0;
        private StepPoint fivePrimeStart;
        private int numStepsToFinalPosition;
        private ConvexHull convexHull;

        public RandomWalk(List<Base> bases)
        {
            this.bases = bases;
            this.convexHull = new ConvexHull(bases);
            this.steps = new List<StepPoint>();
            createSteps(convexHull);
            fivePrimeThreePrime();
            Walk();
        }

        public void updateRadius(double radius)
        {
            max = radius;
            min = -radius;
        }

        public struct Coords
        {
            public double x, y;

            public Coords(double p1, double p2)
            {
                x = p1;
                y = p2;
            }
        }

        public void createSteps(ConvexHull hull)
        {
            List<HullPoint> list = hull.hull;

            for (int i = 0; i < list.Count; i++)
            {
                Coords c;
                if (i == list.Count - 1)
                {
                    c = new Coords(list[0].x - list[i].x,
                                   list[0].y - list[i].y);
                }
                else
                {
                    c = new Coords(list[i+1].x - list[i].x,
                                   list[i+1].y - list[i].y);
                }

                double hullLength = Math.Sqrt(c.x*c.x + c.y*c.y);
                double stepCount = Math.Ceiling(hullLength / desiredLength);
                //Console.WriteLine("hull length " + hullLength);

                // TODO: Find out if this is important?
                //hull.rightMost.x + c.x * (1 / stepCount);

                for (int k = 0; k < stepCount; k++)
                {
                    var x = new StepPoint(list[i].x + c.x * (k / stepCount),
                                          list[i].y + c.y * (k / stepCount));
                    steps.Add(x);
                    numStepsAlongConvexHull += 1;
                    //Console.WriteLine("steps count " + steps.Count);
                }
            }
        }

        public void createFinalSteps(Base b,double x, double y, double finalX, double finalY)
        {
            List<HullPoint> list = convexHull.hull;

            Coords c = new Coords(x - finalX, y - finalY);

            double hullLength = Math.Sqrt(c.x*c.x + c.y*c.y);
            double stepCount = Math.Ceiling(hullLength / desiredLength);

            // TODO: Find out if this is important?
            //hull.rightMost.x + c.x * (1 / stepCount);
            Console.WriteLine(stepCount + " " + list.Count);
            for (int k = 0; k < stepCount; k++)
            {
                var s = new StepPoint(x - c.x * (k / stepCount),
                                      y - c.y * (k / stepCount));
                b.steps.Add(s);
                numStepsToFinalPosition += 1;
            }
        }


        public void fivePrimeThreePrime()
        {
            StepPoint closestPoint = new StepPoint(0,0);
            Base b = bases[0];
            int closestPointIndex = 0;

            for (int i = 0; i < steps.Count; i++)
            {
                if ((Math.Abs(steps[i].x - b.location.Key) <= Math.Abs(closestPoint.x - b.location.Key)) 
                    && (Math.Abs(steps[i].y - b.location.Value) <= Math.Abs(closestPoint.y - b.location.Value)))
                {
                    closestPoint = steps[i];
                    closestPointIndex = i;
                }
            }

            //fivePrime = closestPoint;
            if (closestPointIndex == (steps.Count - 1))
            {
                fivePrimeStart = steps[0];
            }
            else if(closestPointIndex < steps.Count)
            {
                fivePrimeStart = steps[closestPointIndex + 1];
            }
            if (closestPointIndex != steps.Count-1)
            {
                ReorgList(closestPointIndex);
            }
            
        }

        public void ReorgList(int closestPointIndex)
        {
            var temp = new List<StepPoint>();

            for (int i = 0; i < steps.Count; i++)
            {
                temp.Add(steps[(i+closestPointIndex+1) % steps.Count]);
            }
            
            steps = temp;
        }

        public void Walk()
        {
            int finalSteps = 0;
            int maxBasesInsideConvexHull = 1;
            int stepCount = numStepsAlongConvexHull;
            int baseLength = bases.Count;
            var finalFrameInsideHull = new List<Coords>();
            for (int i = 0; i < stepCount; i++)
            {
                int j;
                //Console.WriteLine("walk count " + walk.Count);
                var frame = new List<Coords>();
                for (j = 0; j < i + 1 && j < bases.Count; j++)
                {
                    double x = steps[i-j].x + (rng.NextDouble() * (max - min) + min);
                    double y = steps[i-j].y + (rng.NextDouble() * (max - min) + min);
                    Coords c = new Coords(x, y);
                    frame.Add(c);
                }
                //if (maxBasesInsideConvexHull < bases.Count)
                //k = maxBasesInsideConvexHull;
                for (; j < bases.Count; j++)
                    {
                        double x = frame[j-1].x + (rng.NextDouble() * (max - min) + min);
                        double y = frame[j-1].y;
                        Coords c = new Coords(x, y);
                        frame.Add(c);
                    }
                walk.Add(frame);
            }
            finalFrameInsideHull = walk[walk.Count - 1];
            for (int j = 0; j < walk.Count; j++) { Console.WriteLine(j + ": " + walk[j].Count); }
            for (int a = 0; a < finalFrameInsideHull.Count; a++)
            {
                Console.WriteLine("bases: " + bases.Count);
                createFinalSteps(bases[a],
                    finalFrameInsideHull[a].x, 
                    finalFrameInsideHull[a].y, 
                    bases[a].location.Key, 
                    bases[a].location.Value);
            }
            stepCount = numStepsToFinalPosition;
             //int k = 0;
            for(int l = 0;l < stepCount; l++)
            {
                Coords c;
                var sublist = new List<Coords>();
                foreach (Base b in bases)
                {
                    if (l >= b.steps.Count)
                    {
                        c = new Coords(b.location.Key, b.location.Value);
                    }
                    else
                    {
                        c = new Coords(b.steps[l].x, b.steps[l].y);
                    }
                    sublist.Add(c);
                }
                walk.Add(sublist);
            }
        }
    }
}
