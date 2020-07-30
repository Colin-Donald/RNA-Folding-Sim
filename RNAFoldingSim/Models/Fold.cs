using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using RNAFoldingSim.Models;

namespace RNAFoldingSim.Models
{
    public class Fold
    {
        public struct Point { public double x, y; }
        public struct HBond { public int left, right; }
        public struct Frame { public Point[] points; public HBond[] bonds; }

        public int id;
        public string baseString;
        public Frame[] animation;

        static List<Fold> defaultFolds()
        {
            List<Fold> folds = new List<Fold>();
	
            Fold df = new Fold();
            df.id = 0;
            df.baseString = "ACGU";
            df.animation = new Frame[]
            {
                new Frame
                {
                    points = new Point[]
                    {
                        new Point { x = 0.5, y = 0.5 },
                        new Point { x = 0.4, y = 0.4 },
                        new Point { x = 0.3, y = 0.3 },
                        new Point { x = 0.2, y = 0.2 }
                    },
                    bonds = new HBond[] {}
                },
                new Frame
                {
                    points = new Point[]
                    {
                        new Point { x = 0.35, y = 0.35 },
                        new Point { x = 0.35, y = 0.35 },
                        new Point { x = 0.35, y = 0.35 },
                        new Point { x = 0.35, y = 0.35 },
                    },
                    bonds = new HBond[] {}
                },
                new Frame
                {
                    points = new Point[]
                    {
                        new Point { x = 0.2, y = 0.2 },
                        new Point { x = 0.3, y = 0.3 },
                        new Point { x = 0.4, y = 0.4 },
                        new Point { x = 0.5, y = 0.5 }
                    },
                    bonds = new HBond[] {}
                }
            };

            folds.Add(df);

            df = new Fold();
            df.id = 1;
            df.baseString = "ACGU";
            df.animation = new Frame[]
            {
                new Frame
                {
                    points = new Point[]
                    {
                        new Point { x = 0.2, y = 0.5 },
                        new Point { x = 0.3, y = 0.5 },
                        new Point { x = 0.4, y = 0.5 },
                        new Point { x = 0.5, y = 0.5 }
                    },
                    bonds = new HBond[] {}
                },
                new Frame
                {
                    points = new Point[]
                    {
                        new Point { x = 0.2, y = 0.4 },
                        new Point { x = 0.3, y = 0.4 },
                        new Point { x = 0.4, y = 0.4 },
                        new Point { x = 0.5, y = 0.4 }
                    },
                    bonds = new HBond[] {}
                },
                new Frame
                {
                    points = new Point[]
                    {
                        new Point { x = 0.2, y = 0.3 },
                        new Point { x = 0.3, y = 0.3 },
                        new Point { x = 0.4, y = 0.3 },
                        new Point { x = 0.5, y = 0.3 }
                    },
                    bonds = new HBond[] {}
                }
            };
            folds.Add(df);

            df = new Fold();
            df.id = 2;
            df.baseString = "ACGU";
            df.animation = new Frame[]
            {
                new Frame
                { 
                    points = new Point[]
                    {
                        new Point { x = 0.2, y = 0.2 },
                        new Point { x = 0.5, y = 0.2 },
                        new Point { x = 0.5, y = 0.5 },
                        new Point { x = 0.2, y = 0.5 }
                    },
                    bonds = new HBond[] { new HBond { left=0, right=2 } }
                }
            };
            folds.Add(df);

            df = new Fold();
            df.id = 3;
            df.baseString = "ACGU";
            df.animation = new Frame[]
            {
                new Frame
                { 
                    points = new Point[]
                    {
                        new Point { x = 0.2, y = 0.2 },
                        new Point { x = 0.5, y = 0.2 },
                        new Point { x = 0.5, y = 0.5 },
                        new Point { x = 0.2, y = 0.5 }
                    },
                    bonds = new HBond[] { new HBond { left=0, right=2 } }
                },
                new Frame
                { 
                    points = new Point[]
                    {
                        new Point { x = 0.5, y = 0.2 },
                        new Point { x = 0.5, y = 0.5 },
                        new Point { x = 0.2, y = 0.5 },
                        new Point { x = 0.2, y = 0.2 }
                    },
                    bonds = new HBond[] { new HBond { left=0, right=2 } }
                },
                new Frame
                { 
                    points = new Point[]
                    {
                        new Point { x = 0.5, y = 0.5 },
                        new Point { x = 0.2, y = 0.5 },
                        new Point { x = 0.2, y = 0.2 },
                        new Point { x = 0.5, y = 0.2 }
                    },
                    bonds = new HBond[] { new HBond { left=0, right=2 } }
                },
                new Frame
                {
                    points = new Point[]
                    {
                        new Point { x = 0.2, y = 0.5 },
                        new Point { x = 0.2, y = 0.2 },
                        new Point { x = 0.5, y = 0.2 },
                        new Point { x = 0.5, y = 0.5 }
                    },
                    bonds = new HBond[] { new HBond { left=0, right=2 } }
                },
                new Frame
                { 
                    points = new Point[]
                    {
                        new Point { x = 0.2, y = 0.2 },
                        new Point { x = 0.5, y = 0.2 },
                        new Point { x = 0.5, y = 0.5 },
                        new Point { x = 0.2, y = 0.5 }
                    },
                    bonds = new HBond[] { new HBond { left=0, right=2 } }
                }
            };
            folds.Add(df);
            return folds;
        }

        public static string ValidateBaseString(string baseString)
        {
            var sb = new StringBuilder(baseString.Length);
            foreach (char c in baseString)
            {
                if (Char.IsWhiteSpace(c)) { continue; }
                else if (c == 'A' || c == 'C' || c == 'G' || c == 'U')
                {
                    sb.Append(c);
                }
                else if (c == 'a' || c == 'c' || c == 'g' || c == 'u')
                {
                    sb.Append(Char.ToUpper(c));
                }
                else
                {
                    return null;
                }   
            }
            return sb.ToString();
        }
        
        public static List<Fold> folds = defaultFolds();
    }
}
