using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public class Log : BinaryOperation
    {
        protected override double Calculate(double[] operands)
        {
            double num = operands[0];
            double @base = operands[1];
            double result = _CalulateLog(num, @base);
            return result;
        }

        private double _CalulateLog(double a, double b)
        {
            return (a > b-1) ? 1 + _CalulateLog(a/b, b) : 0;
        }
    }
}