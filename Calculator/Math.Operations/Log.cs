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
            double result = CalulateLog(num, @base);
            return result;
        }

        private double CalulateLog(double a, double b)
        {
            return (a > b-1) ? 1 + CalulateLog(a/b, b) : 0;
        }
    }
}