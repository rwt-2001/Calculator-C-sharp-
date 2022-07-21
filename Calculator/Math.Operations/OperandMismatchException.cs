using System;


namespace Math.Operations
{
    public class OperandMismatchException :Exception
    {

        public OperandMismatchException() : base(ExceptionMessages.OPERANDMISMATCHEXCEPTION) { }
  
    }
}