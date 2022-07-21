

namespace Math.Operations
{
    public class LogNatural :UnaryOperation
    {

        protected override double Calculate(double[] operands)
        {
            double result = CalculateLog(operands[0]);
            return result;
        }

        private double CalculateLog(double number)
        {
            return (number>1) ? 1 + CalculateLog(number/2) : 0; 
        }
    }
}