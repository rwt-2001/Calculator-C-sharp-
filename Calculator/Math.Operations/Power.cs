using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public class Power : BinaryOperation
    {
        protected override double Calculate(double[] operands)
        {

            double result = operands[0];
            for (int i = 2; i <= operands[1]; i++)
                result *= operands[0];
            return result;
        }
    }
}