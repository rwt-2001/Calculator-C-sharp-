

namespace Math.Operations
{
    public class LogNatural :UnaryOperation
    {

        protected override double Calculate(double[] operands)
        {
            double result = System.Math.Log(operands[0]);
            return result;
        }
    }
}