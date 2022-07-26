using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
namespace Math.Operations
{
    public class ExpressionEvaluator
    {
        private Dictionary<string, OperatorPrecedence> _operationDictionary = new Dictionary<string, OperatorPrecedence>();
        private Dictionary<string, OperatorInfo> _operatorSymbolPrecedenceDictionary = new Dictionary<string, OperatorInfo>();
        private string _configurationFilePath = Strings.OPERATOR_FILE_PATH;

        /*Constructor to Initialize the dictionary for basic operations*/
        public ExpressionEvaluator()
        {
            /* fileContent holds data of json file as a string */
            string fileContent = ReadJsonFile();

            /* Deserialize fileContent into dictionary */
            _operatorSymbolPrecedenceDictionary = JsonConvert.DeserializeObject<Dictionary<string, OperatorInfo>>(fileContent);
            RegisterOperation(_operatorSymbolPrecedenceDictionary[Strings.ADDITION].OperatorSymbol, new Addition(), _operatorSymbolPrecedenceDictionary[Strings.ADDITION].OperatorPrecedence);
            RegisterOperation(_operatorSymbolPrecedenceDictionary[Strings.SUBTRACTION].OperatorSymbol, new Subtraction(), _operatorSymbolPrecedenceDictionary[Strings.SUBTRACTION].OperatorPrecedence);
            RegisterOperation(_operatorSymbolPrecedenceDictionary[Strings.MULTIPICATION].OperatorSymbol, new Multiplication(), _operatorSymbolPrecedenceDictionary[Strings.MULTIPICATION].OperatorPrecedence);
            RegisterOperation(_operatorSymbolPrecedenceDictionary[Strings.DIVIDE].OperatorSymbol, new Divide(), _operatorSymbolPrecedenceDictionary[Strings.DIVIDE].OperatorPrecedence);
            RegisterOperation(_operatorSymbolPrecedenceDictionary[Strings.FACTORIAL].OperatorSymbol , new Factorial(), _operatorSymbolPrecedenceDictionary[Strings.FACTORIAL].OperatorPrecedence);
            RegisterOperation(_operatorSymbolPrecedenceDictionary[Strings.POWER].OperatorSymbol, new Power(), _operatorSymbolPrecedenceDictionary[Strings.POWER].OperatorPrecedence);
            RegisterOperation(_operatorSymbolPrecedenceDictionary[Strings.PRECENTAGE].OperatorSymbol, new Precentage(), _operatorSymbolPrecedenceDictionary[Strings.PRECENTAGE].OperatorPrecedence);
            RegisterOperation(_operatorSymbolPrecedenceDictionary[Strings.LogN].OperatorSymbol, new LogNatural(), _operatorSymbolPrecedenceDictionary[Strings.LogN].OperatorPrecedence);
            RegisterOperation(_operatorSymbolPrecedenceDictionary[Strings.RECIPROCAL].OperatorSymbol, new Reciprocal(), _operatorSymbolPrecedenceDictionary[Strings.RECIPROCAL].OperatorPrecedence);  
        }
        

        /*Method to Reads JSON file and returns content in string */
        private string ReadJsonFile()
        {
            try
            {
                string content = File.ReadAllText(_configurationFilePath);
                return content;
            }
            catch
            {
                /* Throw error if file is not present in the given path */
                throw new Exception(ExceptionMessages.FILENOTPRESENT);
            }
            
            
        }

        /* Method adds newly changed precedence to json file */
        private void ChangeOperatorsData()
        {
            string dataToWrite = JsonConvert.SerializeObject(_operatorSymbolPrecedenceDictionary);
            File.WriteAllText(_configurationFilePath, dataToWrite);
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
                throw new Exception(ExceptionMessages.OPERATORPRESENTEXCEPTION);
            }
            else if(operationObject == null)
            {
                throw new Exception(ExceptionMessages.NULLOBJECTEXCEPTION);
            }
            else if(operationPriority <0 || operationPriority > 3)
            {
                throw new Exception(ExceptionMessages.INVALIDPRIORITYEXCEPTION);
            }

            _operationDictionary.Add(@operator, new OperatorPrecedence(operationObject,operationPriority));
            /* If the operation and its precedence already present in configuration file then just return otherwise add
             * that new operator into operatorcongifuration file
             */
            

        }

        /* Method to remove an operation*/
        public void RemoveOperation(string @operator)
        {
            
            if (_operationDictionary.ContainsKey(@operator))
                _operationDictionary.Remove(@operator);
            else{
                throw new Exception(ExceptionMessages.OPERATORNOTPRESENT);
            }
            
        }

        /* Method to change precedence of an operation */
        public bool ChangePrecedence(string operationName, int newOperationPrecedence)
        {
            string @operatorSymbol = _operatorSymbolPrecedenceDictionary[operationName].OperatorSymbol;
            if (!_operationDictionary.ContainsKey(@operatorSymbol)) throw new Exception(ExceptionMessages.OPERATORNOTPRESENT);
           
            _operationDictionary[@operatorSymbol].Precedence = newOperationPrecedence;
            _operatorSymbolPrecedenceDictionary[operationName].OperatorPrecedence = newOperationPrecedence; 

            ChangeOperatorsData();
            return true;
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
                    string operand = String.Empty;
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
                        string @operator = String.Empty;
                        currentindex = index;
                        while (currentindex < expression.Length 
                            && !(expression[currentindex] >= '0' 
                            && expression[currentindex] <= '9') 
                            && expression[currentindex] != ' ' 
                            && expression[currentindex] != ')' 
                            && expression[currentindex] != '(')
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
                throw new InvalidExpressionException(ExceptionMessages.IMPROPERBRACKET);
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
                        throw new InvalidExpressionException(ExceptionMessages.UNDEFINEDOPERATION + postfix[i].Value);
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
                            throw new InvalidExpressionException(ExceptionMessages.INVALIDEXPRESSION);
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
            if (result.Count != 1) throw new InvalidExpressionException(ExceptionMessages.INVALIDEXPRESSION);

            return result.Peek();


        }
    }
}