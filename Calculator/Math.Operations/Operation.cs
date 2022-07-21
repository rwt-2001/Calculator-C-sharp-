

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

                throw new OperandMismatchException();

            }

            return Calculate(operands);
        }


        protected abstract double Calculate(double[] operands);

    }
}