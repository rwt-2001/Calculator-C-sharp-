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
            double result = _CalculateLog(operands[0]);
            return result;
        }

        private double _CalculateLog(double num)
        {
            return (num>1) ? 1 + _CalculateLog(num/2) : 0; 
        }
    }
}