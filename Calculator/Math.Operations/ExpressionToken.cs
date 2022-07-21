

namespace Math.Operations
{
    public enum Token
    {
        Operator, Operand
    }

    public class ExpressionToken
    {

        public Token TokenType { get; private set; }
        public string Value;

        public ExpressionToken(Token tokenType, string value)
        {
            TokenType = tokenType;
            Value = value;
        }
       
    }
}