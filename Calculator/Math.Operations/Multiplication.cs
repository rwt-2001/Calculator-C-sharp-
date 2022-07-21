

namespace Math.Operations
{
    public class Multiplication : BinaryOperation
    {
        protected override double Calculate(double[] operands)
        {
            
            double result = operands[0] * operands[1];
            return result;

        }
    }
}