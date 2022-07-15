using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public abstract class UnaryOperation : IOperation
    {
        public int operandCount { get; set; }
        public UnaryOperation()
        {
            operandCount = 1;
        }
        public void Evaluate(double[] operands)
        {
           if(operandCount != operands.Length)
            {

                throw new NotImplementedException();
            }
            return;
        }

        

        
    }
}