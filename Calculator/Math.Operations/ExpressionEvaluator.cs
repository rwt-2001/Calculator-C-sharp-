using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public class ExpressionEvaluator
    {
        private Dictionary<string, KeyValuePair<Operation, int>> _operationDictionary;

      

        /*Constructor to Initialize the dictionary for basic operations*/
        public ExpressionEvaluator()
        {
            RegisterOperation("+", new Addition(), 1);
            RegisterOperation("-", new Subtraction(), 1);
            RegisterOperation("*", new Multiplication(), 10);
            RegisterOperation("/", new Divide(), 10);
            RegisterOperation("^", new Square(), 15);
            RegisterOperation("!", new Factorial(),20);
            RegisterOperation("^", new Power(),15);
        }
        
        /*Method to evaluate the expression given by user*/
        public double Evaluate(string expression)
        {
            //TODO: Implement expression parsing and Exception Handling
            List<object> infix= ToInfixList(expression);
            List<object> postfix = InfixToPostfix(infix);
            /*
             * TODO: Evaluate postfix expression
             */

            double result = 0;
            return result;
        }

        /* Method to register new operation*/
        public void RegisterOperation(string @operator, Operation operationObject, int operationPriority)
        {
            _operationDictionary.
                Add(@operator, new KeyValuePair<Operation, int>(operationObject, operationPriority));

        }

        /* Method to remove an operation*/
        public void RemoveOperation(string @operator)
        {
            /*Check Whether a key exist or not*/
            bool keyExists = _operationDictionary.ContainsKey(@operator);

            /*Remove the @operator from dictionary if its is present*/
            if (keyExists)
                _operationDictionary.Remove(@operator);
            else
                Console.WriteLine("Operator Not Found");
        }

        /* Methd to change precedence of an operation */
        public void ChangePrecedence(string @operator, int operationPrecedence)
        {
            Operation key = _operationDictionary[@operator].Key;
            _operationDictionary[@operator] = new KeyValuePair<Operation, int>(key, operationPrecedence);

        }

        /*
         * TODO : Convert string expression to List<object> (separating operands and operators)
         */
        private List<object> ToInfixList(string expression)
        {
            List<object> infix = new List<object>();
            int j;

            /*
             * 
             *TODO:  Convert expression into list infix of type dynamic
             *
             */
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == ' ') continue;
                if ((expression[i] >= '0' && expression[i] <= '9'))
                {
                    j = i;
                    string oprand = "";

                    while (
                        j < expression.Length && 
                        ((expression[j] >= '0' && expression[j] <= '9') || expression[j] == '.') 
                        && expression[j] != ' '
                        )
                    {
                        oprand += expression[j];
                        j++;
                    }
                    i = j - 1;


                    infix.Add(Convert.ToDouble(oprand));


                }

                else
                {
                 

                    if (expression[i].ToString() == "(" || expression[i].ToString() == ")")
                    {
                        infix.Add(expression[i].ToString());
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
                        Console.WriteLine(@operator);
                        infix.Add(@operator);
                        i = j - 1;
                    }
                }



            }
            return infix;
        }

        /*
         * TODO : Infix to postfix conversion
         */
        private List<object> InfixToPostfix(List<object> infix)
        {
            List<object> postfix = new List<object>();
            Stack<string> operatorStk = new Stack<string>();

            for (int i = 0; i < infix.Count; i++)
            {
                if (infix[i].GetType() == typeof(double))
                {
                    postfix.Add(infix[i]);
                }
                else
                {
                    if (infix[i].ToString() == ")")
                    {
                        while (operatorStk.Peek() != "(")
                        {
                            postfix.Add(operatorStk.Peek());
                            operatorStk.Pop();
                        }
                        operatorStk.Pop();
                    }


                    else
                    {
                        if (operatorStk.Count == 0 || operatorStk.Peek() == "(" || infix[i].ToString() == "(")
                        {

                            operatorStk.Push(infix[i].ToString());

                        }

                        else
                        {
                            //If the priority of operator at top of stack if equal to or greater than incoming operator then pop the stack and 
                            while (
                                operatorStk.Peek() != "(" 
                                && operatorStk.Count != 0 
                                && _operationDictionary[operatorStk.Peek()].Value >= _operationDictionary[infix[i].ToString()].Value
                                )
                            {
                                postfix.Add(operatorStk.Peek());
                                operatorStk.Pop();
                            }
                            operatorStk.Push(infix[i].ToString());
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


        private void GetResult(List<object> postfix)
        {
            /*
             * TODO: Apply exception handling here
             */
           Stack<object> result = new Stack<object>();
            foreach (object item in postfix)
            {
                if(item.GetType() == typeof(double))
                    result.Push((double)item);


                
                else
                {
                    string currentOperator = item.ToString();
                    int operandsRequired = _operationDictionary[currentOperator].Value;
                    Operation @operation = _operationDictionary[currentOperator].Key;
                    double[] operands = new double[operandsRequired];
                    for (int i = operandsRequired - 1;i >= 0;i--)
                    {
                        operands[i] = (double)result.Peek();
                        result.Pop();
                    }

                    double newResult = @operation.Evaluate(operands);
                    result.Push(newResult);
                }
            }

        }
    }
}