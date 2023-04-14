using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika_OOP_2
{
    internal class Punkt1
    {
        public static int FindGCDEuclid(int a, int b)
        {
            if (a == 0) { return b; }
            while (b != 0)
            {
                if (a > b)
                {
                    a -= b;
                }
                else
                {
                    b -= a;
                }
            }
            return a;
        }
    }
}
