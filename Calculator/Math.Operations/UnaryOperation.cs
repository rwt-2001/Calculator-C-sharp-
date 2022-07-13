using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public abstract class UnaryOperation : IOperation
    {
        public int operands { get; set; }

        public void Evaluate()
        {

        }

        UnaryOperation()
        {
            operands = 1;
        }
    }
}