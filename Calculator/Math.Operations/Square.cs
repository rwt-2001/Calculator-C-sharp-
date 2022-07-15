using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public class Square : UnaryOperation
    {
        protected override double Calculate(double[] operands)
        {
            double result = operands[0]*operands[0];
            return result;
        }

    }
}