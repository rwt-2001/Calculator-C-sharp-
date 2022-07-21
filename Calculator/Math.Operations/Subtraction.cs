

namespace Math.Operations
{
    public class Subtraction : BinaryOperation
    {
        protected override double Calculate(double[] operands)
        {
            double result = operands[0] - operands[1];
            return result;
        }
    }
}