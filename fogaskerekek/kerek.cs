using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fogaskerekek
{
    public class Kerek
    {
        public double Mn, Beta, X, Ra, B, D;
        public int Z;
        public Anyag Anyag;

        public Kerek(Anyag anyag, int z, double m, double beta, double x, double ra, double b, double d)
        {
            Anyag = anyag;
            X = x;
            Ra = ra;
            Z = z;
            Mn = m;
            Beta = beta;
            B = b;
            D = d;
        }
    }
}
