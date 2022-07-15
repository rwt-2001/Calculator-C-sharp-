using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public class Multiplication : BinaryOperation
    {
        public new double Evaluate(double[] operands)
        {
            base.Evaluate(operands);
            
            double result = operands[0] * operands[1];
            return result;
        }
    }
}