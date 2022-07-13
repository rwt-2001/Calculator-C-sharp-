using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public abstract class BinaryOperation : IOperation
    {
        public int operands { get; set; }
        public virtual void Evaluate()
        {
            //Interface Overrided Function
        }
        BinaryOperation()
        {
            operands = 2;
        }
    }
}