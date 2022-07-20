using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public class ImproperBracketsException : Exception
    {
        public ImproperBracketsException(): base(ExceptionMessages.IMPROPERBRACKETEXCEPTION)
        {

        }
    }
}