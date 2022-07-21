

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
