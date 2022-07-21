

namespace Math.Operations
{
    public class Log : BinaryOperation
    {
        protected override double Calculate(double[] operands)
        {
            double number = operands[0];
            double @base = operands[1];
            double result = CalulateLog(number, @base);
            return result;
        }

        private double CalulateLog(double number, double @base)
        {
            return (number > @base - 1) ? 1 + CalulateLog(number/ @base, @base) : 0;
        }
    }
}