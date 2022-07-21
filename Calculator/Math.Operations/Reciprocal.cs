

namespace Math.Operations
{
    public class Reciprocal : UnaryOperation
    {
        protected override double Calculate(double[] operands)
        {
            double result = 1 / operands[0];
            return result;
        }
    }
}