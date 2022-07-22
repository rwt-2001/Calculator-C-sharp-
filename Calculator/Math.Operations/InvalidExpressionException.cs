using System;


namespace Math.Operations
{
    public class InvalidExpressionException :Exception
    {
        /* Exception that will be thrown if the given expression is not valid */
        public InvalidExpressionException() { }
        public InvalidExpressionException(string message) : base(message) { }

        

    }
}