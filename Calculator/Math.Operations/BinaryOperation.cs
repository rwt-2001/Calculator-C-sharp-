using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public abstract class BinaryOperation : IOperation
    {
        public int operandCount { get; set; }
        public BinaryOperation()
        {
            operandCount = 2;
        }
        public void Evaluate(double[] operands)
        {
            if(operands.Length != operandCount)
            {
                throw new NotImplementedException();
            }

            return;
        }
        
    }
}