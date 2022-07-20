using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public class UndefinedOperationException : Exception
    {
        public UndefinedOperationException(string @operator) : base(ExceptionMessages.UNDEFINEDOPERATIONEXCEPTION + @operator)
        {

        }
    }
}