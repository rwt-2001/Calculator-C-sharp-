using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public abstract class BinaryOperation : Operation
    {
        public BinaryOperation()
        {
            base.OperandCount = 2;
        }

    }
}