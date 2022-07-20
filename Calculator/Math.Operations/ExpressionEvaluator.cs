using System;
using System.Collections.Generic;


namespace Math.Operations
{
    public class ExpressionEvaluator
    {
        private Dictionary<string, OperatorPrecedence> _operationDictionary = new Dictionary<string, OperatorPrecedence>();
        
        /*Constructor to Initialize the dictionary for basic operations*/
        public ExpressionEvaluator()
        {
            RegisterOperation(OperatorAndPrecedence.ADD_OPERATOR, new Addition(), Convert.ToInt32(OperatorAndPrecedence.ADD_PRECEDENCE));
            RegisterOperation(OperatorAndPrecedence.SUBTRACT_OPERATOR, new Subtraction(), Convert.ToInt32(OperatorAndPrecedence.SUBTRACT_PRECEDENCE));
            RegisterOperation(OperatorAndPrecedence.MULTIPLY_OPERATOR, new Multiplication(), Convert.ToInt32(OperatorAndPrecedence.MULTIPLY_PRECEDENCE));
            RegisterOperation(OperatorAndPrecedence.DIVIDE_OPERATOR, new Divide(), Convert.ToInt32(OperatorAndPrecedence.DIVIDE_PRECEDENCE));
            RegisterOperation(OperatorAndPrecedence.FACTORIAL_OPERATOR, new Factorial(), Convert.ToInt32(OperatorAndPrecedence.FACTORIAL_PRECEDENCE));
            RegisterOperation(OperatorAndPrecedence.POWER_OPERATOR, new Power(), Convert.ToInt32(OperatorAndPrecedence.POWER_PRECEDENCE));
            RegisterOperation(OperatorAndPrecedence.PERCENTAGE_OPERATOR, new Precentage(), Convert.ToInt32(OperatorAndPrecedence.PERCENTAGE_PRECEDENCE));
        }
        
        /*Method to evaluate the expression given by user*/
        public double Evaluate(string expression)
        {
            
            List<ExpressionToken> infix= GenerateTokens(expression);
            List<ExpressionToken> postfix = InfixToPostfix(infix);
            double result;
            try
            { 
                result = GetResult(postfix);
            }
            catch (DivideByZeroException ex)
            {
                throw ex;
            }
            catch (UndefinedOperationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new InvalidExpressionException();
            }
            return result;
        }

        /* Method to register new operation*/
        public void RegisterOperation(string @operator, Operation operationObject, int operationPriority)
        {
            
            _operationDictionary.
                Add(@operator,  new OperatorPrecedence(operationObject,operationPriority));

        }

        /* Method to remove an operation*/
        public void RemoveOperation(string @operator)
        {
            
            if (_operationDictionary.ContainsKey(@operator))
                _operationDictionary.Remove(@operator);
            
        }

        /* Methd to change precedence of an operation */
        public void ChangePrecedence(string @operator, int operationPrecedence)
        {
            if(_operationDictionary.ContainsKey(@operator))
            _operationDictionary[@operator].Precedence = operationPrecedence;
            

        }

        
        /* Method to Generate Tokens in infix order */
        private List<ExpressionToken> GenerateTokens(string expression)
        {

            List<ExpressionToken> infix = new List<ExpressionToken>();
            int j;
            int trackBrackets = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == ' ') continue;
                if ((expression[i] >= '0' && expression[i] <= '9'))
                {
                    j = i;
                    string operand = "";
                    while (j < expression.Length && ((expression[j] >= '0' && expression[j] <= '9') || expression[j] == '.') && expression[j] != ' ')
                    {
                        operand += expression[j];
                        j++;
                    }
                    i = j - 1;



                    infix.Add(new ExpressionToken(Token.Operand, operand));
                }

                else
                {

                    if (expression[i].ToString() == "(" || expression[i].ToString() == ")")
                    {
                        if(expression[i].ToString() == "(") trackBrackets++;
                        else trackBrackets--;
                        infix.Add(new ExpressionToken(Token.Operator, expression[i].ToString()));
                    }

                    else
                    {
                        string @operator = "";
                        j = i;
                        while (j < expression.Length && !(expression[j] >= '0' && expression[j] <= '9') && expression[j] != ' ' && expression[j] != ')' && expression[j] != '(')
                        {
                            @operator += expression[j];
                            j++;
                        }
                        infix.Add(new ExpressionToken(Token.Operator, @operator));
                        i = j - 1;
                    }
                }
            }
            if (trackBrackets != 0)
                throw new ImproperBracketsException();
            return infix;
        }
     

        /* Method to convert infix to postfix expression */
        private List<ExpressionToken> InfixToPostfix(List<ExpressionToken> infix)
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
                        if (operatorStk.Count == 0 || operatorStk.Peek().Value == "(" || infix[i].Value == "(")
                        {

                            operatorStk.Push(infix[i]);

                        }

                        else
                        {
                            //If the priority of operator at top of stack if equal to or greater than incoming operator then pop the stack and 
                            while (operatorStk.Count != 0 && operatorStk.Peek().Value != "(" && _operationDictionary[operatorStk.Peek().Value].Precedence >= _operationDictionary[infix[i].Value].Precedence)
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

        /* Method to evaluate the result from the postfix expression */
        private double GetResult (List<ExpressionToken> postfix) 
        {
            Stack<double> result = new Stack<double>();
            for (int i = 0; i < postfix.Count; i++)
            {
                if (postfix[i].TokenType == Token.Operand)
                {
                    result.Push(Convert.ToDouble(postfix[i].Value));
                }

                else
                {

                    Operation newOperator;
                    try
                    {
                        newOperator = _operationDictionary[postfix[i].Value].OperationType;
                    }
                    catch(Exception e) 
                    {
                        throw new UndefinedOperationException(postfix[i].Value);
                    }
                    int operandsCount = newOperator.OperandCount;
                    double[] operands = new double[operandsCount];

                    for (int j = operandsCount - 1; j >= 0; j--)
                    {
                        operands[j] = result.Peek();
                        result.Pop();
                    }
                    if (newOperator.GetType() == typeof(Precentage))
                    {
                        result.Push(operands[0]);
                    }
                    result.Push(newOperator.Evaluate(operands));
                 
                }


            }
            if (result.Count != 1) throw new InvalidExpressionException();

            return result.Peek();


        }
    }
}