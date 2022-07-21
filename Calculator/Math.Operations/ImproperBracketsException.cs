using System;


namespace Math.Operations
{
    public class ImproperBracketsException : Exception
    {   
        /* Exception, thrown if the brackets in expression are not in proper format */ 
        public ImproperBracketsException(): base(ExceptionMessages.IMPROPERBRACKETEXCEPTION)
        {

        }
    }
}