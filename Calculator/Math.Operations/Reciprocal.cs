using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public class Reciprocal : UnaryOperation
    {
        protected override double Calculate(double[] operands)
        {
            double result = 1 / operands[0];
            return result;
        }
    }
}