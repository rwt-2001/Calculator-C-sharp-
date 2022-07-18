using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public class LogNatural :UnaryOperation
    {

        protected override double Calculate(double[] operands)
        {
            double result = CalculateLog(operands[0]);
            return result;
        }

        private double CalculateLog(double num)
        {
            return (num>1) ? 1 + CalculateLog(num/2) : 0; 
        }
    }
}