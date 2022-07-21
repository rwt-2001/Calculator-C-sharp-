using System;

namespace Math.Operations
{
    public class Divide : BinaryOperation
    {
     

        protected override double Calculate(double[] operands)
        {
            if(operands[1]==0) throw new DivideByZeroException();
            double result = operands[0] / operands[1];
            return result;
        }
    }
}