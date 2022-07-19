using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public class OperandMismatchException :Exception
    {

        public OperandMismatchException() : base("Operands given is not equal to number of operands reqired") { }
  
    }
}