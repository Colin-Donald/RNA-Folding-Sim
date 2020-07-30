using System;
using System.Collections.Generic;
using RNAFoldingSim.Models;

namespace RNAFoldingSim.Models
{
    public class Animator
    {
        string rnaString;
        List<Base> bases;
        List<List<RandomWalk.Coords>> walk;

        public Animator() {}

        public void Animate(String rnaString)
        {
            bases = new List<Base>();

            this.rnaString = rnaString;

            var n = new Nussinov();
            n.nussinovAlgorithm(rnaString);
            string st = n.traceBack(0, rnaString.Length - 1);

            var s = new SecondaryStructure();
            s.DrawStructure(st);

            for (int i = 0; i < s.bases.Count; i++) { bases.Add(s.bases[i]); }

            var r = new RandomWalk(bases);
            walk = r.walk;
        }

        public Fold Fold()
        {
            var f = new Fold();
            f.baseString = rnaString;
            f.animation = new Fold.Frame[walk.Count];
            for (int i = 0; i < walk.Count; i++)
            {
                f.animation[i] = new Fold.Frame();
                f.animation[i].points = new Fold.Point[rnaString.Length];
            }
            for (int i = 0; i < walk.Count; i++)
            {
                for (int j = 0; j < walk[i].Count; j++)
                {
                    f.animation[i].points[j].x = walk[i][j].x;
                    f.animation[i].points[j].y = walk[i][j].y;
                }
            }
            // I want hbonds represented by { index1, index2 }.
            // I only have an array of bases linked by references, so I
            // reverse that (dictionary of bases to indexes) and use both
            // to make my pairs.
            for (int i = 0; i < f.animation.Length - 1; i++)
            {
                f.animation[i].bonds = new Fold.HBond[0];
            }
            var bh = new Dictionary<Base, int>();
            var t = new List<Fold.HBond>();
            for (int i = 0; i < bases.Count; i++) { bh.Add(bases[i], i); }
            for (int i = 0; i < bases.Count; i++)
            {
                Base bp = bases[i].myBasePair;
                if (bp != null && bh[bp] > i) 
                {
                    t.Add(new Fold.HBond {left=i, right=bh[bp]});
                }
            }
            f.animation[f.animation.Length - 1].bonds = t.ToArray();
            return f;
        }
    }
}
