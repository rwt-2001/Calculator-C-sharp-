using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public abstract class BinaryOperation : IOperation
    {
       
        public BinaryOperation()
        {
            OperandCount = 2;
        }

        public int OperandCount { get; }

        /*
         Evaluate Method will validate the given operands against
         required operands. If the operands are same as required then it 
         will call the calculate method and return the answer
         */
        public double Evaluate(double[] operands)
        {
            if(operands.Length != OperandCount)
            {
                throw new NotImplementedException();
            }

            return Calculate(operands);

        }

        protected abstract double Calculate(double[] operands);
        
    }

}