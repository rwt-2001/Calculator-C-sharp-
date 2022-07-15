using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public class Reciprocal : UnaryOperation
    {
        public new double Evaluate(double[] operands)
        {
            base.Evaluate(operands);

            double result = 1 / operands[0];
            return result;
        }
    }
}