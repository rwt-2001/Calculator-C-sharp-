using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public class Factorial : UnaryOperation
    {

        public new double Evaluate(double[] operands)
        {

            base.Evaluate(operands);

            double result = 0;
            for (int i = 1; i <= operands[0]; i++)
                result *= i;

            return result;
            
        }
    }
}