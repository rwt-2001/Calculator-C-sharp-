using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public interface IOperation
    {
        int operands { get; set; }
        void Evaluate();
       
    }
}