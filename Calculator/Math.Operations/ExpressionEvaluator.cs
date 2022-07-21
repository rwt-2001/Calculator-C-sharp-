using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Math.Operations
{
    public class ExpressionEvaluator
    {
        private Dictionary<string, OperatorPrecedence> _operationDictionary = new Dictionary<string, OperatorPrecedence>();
        private Dictionary<string, Operator> _operatorSymbolPrecedenceDictionary = new Dictionary<string, Operator>();
        private const string _configurationFilePath = "./operatorsConfiguration.json";

        /*Constructor to Initialize the dictionary for basic operations*/
        public ExpressionEvaluator()
        {
            /* fileContent holds data of json file as a string */
            string fileContent = ReadJsonFile();

            /* Deserialize fileContent into dictionary */
            _operatorSymbolPrecedenceDictionary = JsonSerializer.Deserialize<Dictionary<string, Operator>>(fileContent);
            RegisterOperation(_operatorSymbolPrecedenceDictionary["Addition"].OperatorSymbol, new Addition(), _operatorSymbolPrecedenceDictionary["Addition"].OperatorPrecedence);
            RegisterOperation(_operatorSymbolPrecedenceDictionary["Subtraction"].OperatorSymbol, new Subtraction(), _operatorSymbolPrecedenceDictionary["Subtraction"].OperatorPrecedence);
            RegisterOperation(_operatorSymbolPrecedenceDictionary["Multiplication"].OperatorSymbol, new Multiplication(), _operatorSymbolPrecedenceDictionary["Multiplication"].OperatorPrecedence);
            RegisterOperation(_operatorSymbolPrecedenceDictionary["Divide"].OperatorSymbol, new Divide(), _operatorSymbolPrecedenceDictionary["Divide"].OperatorPrecedence);
            RegisterOperation(_operatorSymbolPrecedenceDictionary["Factorial"].OperatorSymbol , new Factorial(), _operatorSymbolPrecedenceDictionary["Factorial"].OperatorPrecedence);
            RegisterOperation(_operatorSymbolPrecedenceDictionary["Power"].OperatorSymbol, new Power(), _operatorSymbolPrecedenceDictionary["Power"].OperatorPrecedence);
            RegisterOperation(_operatorSymbolPrecedenceDictionary["Precentage"].OperatorSymbol, new Precentage(), _operatorSymbolPrecedenceDictionary["Precentage"].OperatorPrecedence);
        }
        

        /*Method to Reads JSON file and returns content in string */
        private string ReadJsonFile()
        {
            try
            {
                StreamReader readFile = new StreamReader(_configurationFilePath);
                return readFile.ReadToEnd();
            }
            catch
            {
                throw new Exception("OperatorsConfiguration.json is not present");
            }
            
            
        }

        /* Method added newly added operatorsymbol and precedence to json file */
        private void AddNewOperator()
        {
            string content = JsonSerializer.Serialize(_operatorSymbolPrecedenceDictionary);
            File.WriteAllText(_configurationFilePath, content);
        }

        /*Method to evaluate the expression given by user*/
        public double Evaluate(string expression)
        {
            
            List<ExpressionToken> infixExpression= GenerateTokens(expression);
            List<ExpressionToken> postfixExpression = InfixToPostfix(infixExpression);
            double result = GetResult(postfixExpression);
            return result;
        }

        /* Method to register new operation*/
        public void RegisterOperation(string @operator, Operation  operationObject, int operationPriority)
        {

            if (_operationDictionary.ContainsKey(@operator))
            {
                throw new Exception("Operator Already present");
            }
            else if(operationObject == null)
            {
                throw new Exception("Operation type object can't be null");
            }
            else if(operationPriority <0 || operationPriority > 3)
            {
                throw new Exception("Operation priority must be within 1 to 3");
            }

            _operationDictionary.Add(@operator, new OperatorPrecedence(operationObject,operationPriority));
            
            Operator newOperator = new Operator();
            newOperator.OperatorPrecedence = operationPriority;
            newOperator.OperatorSymbol = @operator;

            _operatorSymbolPrecedenceDictionary.Add(@operator, newOperator);


        }

        /* Method to remove an operation*/
        public void RemoveOperation(string @operator)
        {
            
            if (_operationDictionary.ContainsKey(@operator))
                _operationDictionary.Remove(@operator);
            else{
                throw new Exception("Operator not present");
            }
            
        }

        /* Method to change precedence of an operation */
        public void ChangePrecedence(string @operator, int operationPrecedence)
        {
            if(_operationDictionary.ContainsKey(@operator))
            _operationDictionary[@operator].Precedence = operationPrecedence;
            

        }

        
        /* Method to Generate Tokens in infix order */
        private List<ExpressionToken> GenerateTokens(string expression)
        {

            List<ExpressionToken> infixExpression = new List<ExpressionToken>();
            int currentindex;
            int trackBrackets = 0;

            for (int index = 0; index < expression.Length; index++)
            {
                if (expression[index] == ' ') continue;
                if ((expression[index] >= '0' && expression[index] <= '9'))
                {
                    currentindex = index;
                    string operand = "";
                    while (currentindex < expression.Length && ((expression[currentindex] >= '0' && expression[currentindex] <= '9') || expression[currentindex] == '.') && expression[currentindex] != ' ')
                    {
                        operand += expression[currentindex];
                        currentindex++;
                    }
                    index = currentindex - 1;



                    infixExpression.Add(new ExpressionToken(Token.Operand, operand));
                }

                else
                {

                    if (expression[index].ToString() == "(" || expression[index].ToString() == ")")
                    {
                        if(expression[index].ToString() == "(") trackBrackets++;
                        else trackBrackets--;
                        infixExpression.Add(new ExpressionToken(Token.Operator, expression[index].ToString()));
                    }

                    else
                    {
                        string @operator = "";
                        currentindex = index;
                        while (currentindex < expression.Length && !(expression[currentindex] >= '0' && expression[currentindex] <= '9') && expression[currentindex] != ' ' && expression[currentindex] != ')' && expression[currentindex] != '(')
                        {
                            @operator += expression[currentindex];
                            currentindex++;
                        }
                        infixExpression.Add(new ExpressionToken(Token.Operator, @operator));
                        index = currentindex - 1;
                    }
                }
            }
            if (trackBrackets != 0)
                throw new ImproperBracketsException();
            return infixExpression;
        }
     

        /* Method to convert infix to postfix expression */
        private List<ExpressionToken> InfixToPostfix(List<ExpressionToken> infix)
        {
            List<ExpressionToken> postfixExpression = new List<ExpressionToken>();
            Stack<ExpressionToken> operatorStk = new Stack<ExpressionToken>();

            for (int i = 0; i < infix.Count; i++)
            {
                if (infix[i].TokenType == Token.Operand)
                {
                    postfixExpression.Add(infix[i]);
                }
                else
                {
                    if (infix[i].Value == ")")
                    {
                        while (operatorStk.Peek().Value != "(")
                        {
                            postfixExpression.Add(operatorStk.Peek());
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
                            //If the priority of operator at top of stack if equal to or greater than incoming operator then pop the stack until the stack is empty or the operator at top of stack has less priority */
                            while (operatorStk.Count != 0 && operatorStk.Peek().Value != "(" && _operationDictionary[operatorStk.Peek().Value].Precedence >= _operationDictionary[infix[i].Value].Precedence)
                            {

                                postfixExpression.Add(operatorStk.Peek());
                                operatorStk.Pop();
                            }
                            operatorStk.Push(infix[i]);
                        }

                    }

                }
            }
            while (operatorStk.Count > 0)
            {
                postfixExpression.Add(operatorStk.Peek());
                operatorStk.Pop();
            }

            return postfixExpression;

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
                    catch 
                    {
                        throw new UndefinedOperationException(postfix[i].Value);
                    }
                    int operandsCount = newOperator.OperandCount;
                    double[] operands = new double[operandsCount];

                    for (int j = operandsCount - 1; j >= 0; j--)
                    {
                        try
                        {
                            operands[j] = result.Peek();
                        }
                        catch
                        {
                            throw new InvalidExpressionException();
                        }
                        
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