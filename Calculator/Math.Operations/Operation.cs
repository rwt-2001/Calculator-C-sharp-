

namespace Math.Operations
{
    public abstract class Operation
    {
        public int OperandCount
        {
            get;
            protected set;
        }
        
        public double Evaluate(double[] operands)
        {
            if (operands == null || operands.Length != OperandCount)
            {

                throw new InvalidExpressionException(ExceptionMessages.OPERANDMISMATCH);

            }

            return Calculate(operands);
        }


        protected abstract double Calculate(double[] operands);

    }
}