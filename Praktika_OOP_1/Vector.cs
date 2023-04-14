using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika_OOP_1
{
    internal class Vector
    {
        protected float x, y;
        public Vector(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public static explicit operator int[](Vector param)
        {
            int[] output = { (int)param.x, (int)param.y };
            return output;
        }
        public static implicit operator float[](Vector param)
        {
            float[] output = { param.x, param.y };
            return output;
        }
    }
}