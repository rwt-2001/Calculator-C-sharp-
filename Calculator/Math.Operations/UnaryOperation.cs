using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public abstract class UnaryOperation : IOperation
    {
        public int OperandCount { get; set; }
        public UnaryOperation()
        {
            OperandCount = 1;
        }
        public double Evaluate(double[] operands)
        {
           if(OperandCount != operands.Length)
            {

                throw new NotImplementedException();
            }
            return Calculate(operands);
        }

        protected abstract double Calculate(double[] operands);

        
    }
}