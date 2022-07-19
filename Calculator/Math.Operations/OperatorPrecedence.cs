using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Operations
{
    public class OperatorPrecedence
    {
        public Operation OperationType { get; set; }
        public int Precedence { get; set; }

        public OperatorPrecedence(Operation operationObject, int precedence)
        {
            OperationType = operationObject;
            Precedence = precedence;
        }
    }
}
