
namespace Math.Operations
{
    public class Precentage : BinaryOperation
    {
        protected override double Calculate(double[] operands)
        {
            double result = operands[0] * (operands[1] / 100);
            return result;
        }
    }
}