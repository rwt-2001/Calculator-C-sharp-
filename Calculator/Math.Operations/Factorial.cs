

namespace Math.Operations
{
    public class Factorial : UnaryOperation
    {

        protected override double Calculate(double[] operands)
        {
            if(operands[0] > 170)
                throw new InvalidExpressionException(ExceptionMessages.OVERFLOWEXCEPTION);
            
            double result = 1;
            for (int i = 2; i <= operands[0]; i++)
                result *= i;
            return result;
        }
    }
}