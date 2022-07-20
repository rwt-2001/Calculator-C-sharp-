// See https://aka.ms/new-console-template for more information
using Practice;

public enum Token
{
    Operator, Operand
}

abstract class Operation
{
    public int OperandCount
    {
        get;
        protected set;
    }
    public double Evaluate(double[] operands)
    {
        if (operands == null || operands.Length != OperandCount)
        {
            throw new NotImplementedException();
        }

        return Calculate(operands);
    }


    protected abstract double Calculate(double[] operands);
}

class ExpressionToken
{
    public string Value;
    public Token TokenType { get; private set; }

    public ExpressionToken(Token tokenType, string value)
    {
        this.TokenType = tokenType;
        this.Value = value;
    }
}
abstract class BinaryOperation : Operation
{
    public BinaryOperation()
    {
        base.OperandCount = 2;
    }

}

class Subtraction : BinaryOperation
{
    protected override double Calculate(double[] operands)
    {
        double result = operands[0] - operands[1];
        return result;
    }
}

class Divide : BinaryOperation
 {
    protected override double Calculate(double[] operands)
    {
        double result = operands[0] / operands[1];
        return result;
    }
}
class Multiplication : BinaryOperation
{
    protected override double Calculate(double[] operands)
    {

        double result = operands[0] * operands[1];
        return result;

    }
}
class Addition : BinaryOperation
{
    protected override double Calculate(double[] operands)
    {
        double result = operands[0] + operands[1];
        return result;
    }
}

abstract class Unary : Operation
{
    public Unary()
    {
        base.OperandCount = 1;
    }
}

class Square : Unary
{
    protected override double Calculate(double[] operands)
    {
        double result = operands[0] * operands[0];
        return result;
    }
}
class Factorial : Unary
{
    protected override double Calculate(double[] operands)
    {
        double result = 1;
        for (int i = 2; i <= operands[0]; i++)
            result *= i;
        return result;
    }
}

class OperatorPrecedence
{
    public int Precedence;
    public Operation OperationType;

    public OperatorPrecedence(Operation operationType, int precedence)
    {
        Precedence = precedence;
        OperationType = operationType;
    }
}

class Percentage : BinaryOperation
{
    protected override double Calculate(double[] operands)
    {
        double result = operands[0] * (operands[1] / 100);
        return result;
    }
}
class ClassPractice
{
    static void Main()
    {
        // Mathematical Expression
        string expressionstr = "50 + 10% * 2";
     

        List<ExpressionToken> infix = GetInfixList(expressionstr);
        
        Console.WriteLine("Expression in infix :");

        for(int i = 0; i < infix.Count; i++)
        {
            Console.Write(infix[i].Value);
        }


        Dictionary<string, OperatorPrecedence> operationDictionary = new Dictionary<string, OperatorPrecedence>();

        operationDictionary.Add(Resource1.ADD_OPERATOR, new OperatorPrecedence(new Addition(), Convert.ToInt32(Resource1.ADD_PRECEDENCE)));
        operationDictionary.Add(Resource1.SUBTRACT_OPERATOR, new OperatorPrecedence(new Subtraction(), Convert.ToInt32(Resource1.SUBTRACT_PRECEDENCE)));
        operationDictionary.Add(Resource1.DIVIDE_OPERATOR, new OperatorPrecedence(new Divide(), Convert.ToInt32(Resource1.DIVIDE_PRECEDENCE)));
        operationDictionary.Add(Resource1.MULTIPLY_OPERATOR, new OperatorPrecedence(new Multiplication(), Convert.ToInt32(Resource1.MULTIPLY_PRECEDENCE)));
        operationDictionary.Add(Resource1.PRECENTAGE_OPERATOR, new OperatorPrecedence(new Percentage(), Convert.ToInt32(Resource1.PRECENTAGE_PRECEDENCE)));



        List<ExpressionToken> postfix = InfixToPostfix(infix, operationDictionary); 

     


        Console.WriteLine("\n\nPostfix Expression : ");
        for (int i = 0; i < postfix.Count; i++)
        {
            Console.Write(postfix[i].Value);
        }

        Console.WriteLine("\n\n");

         GetResult(postfix, operationDictionary);


    }

    //Method to form infix list of type object


    static public List<ExpressionToken> GetInfixList(string expressionstr)
    {

        List<ExpressionToken> infix = new List<ExpressionToken>();
        int j;
        int trackBrackets = 0;

        /*
         * 
         *TODO:  Convert expression into list infix of type dynamic
         *
         */
        for (int i = 0; i < expressionstr.Length; i++)
        {
            if (expressionstr[i] == ' ') continue;
            if ((expressionstr[i] >= '0' && expressionstr[i] <= '9'))
            {
                j = i;
                string operand = "";
                while (j < expressionstr.Length && ((expressionstr[j] >= '0' && expressionstr[j] <= '9') || expressionstr[j] == '.') && expressionstr[j] != ' ')
                {
                    operand += expressionstr[j];
                    j++;
                }
                i = j - 1;


                
                infix.Add(new ExpressionToken(Token.Operand, operand));
            }

            else
            {
                /* 
                 * if their is a bracket just add it to the list
                 */
                if (expressionstr[i].ToString() == "(" || expressionstr[i].ToString() == ")")
                {
                    if (expressionstr[i].ToString() == "(") trackBrackets++;
                    else trackBrackets--;
                    infix.Add(new ExpressionToken(Token.Operator, expressionstr[i].ToString()));
                    Console.WriteLine(expressionstr[i]);
                }

                else
                {
                    string @operator = "";
                    j = i;
                    while (j < expressionstr.Length && !(expressionstr[j] >= '0' && expressionstr[j] <= '9') && expressionstr[j] != ' ' && expressionstr[j] != ')' && expressionstr[j] !='(')
                    {
                        @operator += expressionstr[j];
                        j++;
                    }
                    Console.WriteLine(@operator);
                    infix.Add(new ExpressionToken(Token.Operator, @operator));
                    i = j - 1;
                }
            }
        }
        if (trackBrackets != 0)
            throw new Exception("All brackets are not closed");
        return infix;
    }


    static public List<ExpressionToken> InfixToPostfix(List<ExpressionToken> infix, Dictionary<string, OperatorPrecedence> operationDictionary)
    {
    

        List<ExpressionToken> postfix = new List<ExpressionToken>();
        Stack<ExpressionToken> operatorStk = new Stack<ExpressionToken>();

        for (int i = 0; i < infix.Count; i++)
        {
            if (infix[i].TokenType == Token.Operand)
            {
                postfix.Add(infix[i]);
            }
            else
            {
                if (infix[i].Value == ")")
                {
                    while (operatorStk.Peek().Value != "(")
                    {
                        postfix.Add(operatorStk.Peek());
                        operatorStk.Pop();
                    }
                    operatorStk.Pop();
                }

                
                else
                {
                    if (operatorStk.Count == 0 || operatorStk.Peek().Value == "(" || infix[i].Value=="(")
                    {

                        operatorStk.Push(infix[i]);

                    }

                    else
                    {
                        //If the priority of operator at top of stack if equal to or greater than incoming operator then pop the stack and 
                        while (operatorStk.Count != 0 && operatorStk.Peek().Value != "(" && operationDictionary[operatorStk.Peek().Value].Precedence >= operationDictionary[infix[i].Value].Precedence)
                        {
                            
                            postfix.Add(operatorStk.Peek());
                            operatorStk.Pop();
                        }
                        operatorStk.Push(infix[i]);
                    }

                }

            }
        }
        while (operatorStk.Count > 0)
        {
            postfix.Add(operatorStk.Peek());
            operatorStk.Pop();
        }
        return postfix;
    }





    static public void GetResult(List<ExpressionToken> postfix , Dictionary<string, OperatorPrecedence> operationDictionary)
    {
        Stack <double> result = new Stack <double>();
        for (int i = 0; i < postfix.Count; i++)
        {
            if (postfix[i].TokenType == Token.Operand)
            {
                /* If it is operand then push it into stack */
                result.Push(Convert.ToDouble(postfix[i].Value));
            }

            else
            {
                
                Operation newOperator = operationDictionary[postfix[i].Value].OperationType;

                int operandsCount = newOperator.OperandCount;
                double[] operands = new double[operandsCount];
                
                for(int j = operandsCount-1;j >= 0; j--)
                {
                    operands[j] = result.Peek();
                    result.Pop();
                }
                if (newOperator.GetType() == typeof(Percentage))
                {
                    result.Push(operands[0]);
                }
                result.Push(newOperator.Evaluate(operands));
                
                
            }
        

        }       


        Console.WriteLine("Answer : " + Math.Round(result.Peek(),3));
        
    }
}