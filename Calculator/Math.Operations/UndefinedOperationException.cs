using System;

namespace Math.Operations
{
    public class UndefinedOperationException : Exception
    {
        public UndefinedOperationException(string @operator) : base(ExceptionMessages.UNDEFINEDOPERATIONEXCEPTION + @operator)
        {

        }
    }
}