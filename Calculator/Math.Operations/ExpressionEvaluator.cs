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
        private List<object> ToInfixList(string expressionstr)
        {
            List<object> infix = new List<object>();
            int j;

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
                    string oprand = "";
                    while (j < expressionstr.Length && ((expressionstr[j] >= '0' && expressionstr[j] <= '9') || expressionstr[j] == '.') && expressionstr[j] != ' ')
                    {
                        oprand += expressionstr[j];
                        j++;
                    }
                    i = j - 1;


                    infix.Add(Convert.ToDouble(oprand));
                    

                }

                else
                {
                    /* 
                     * if their is a bracker just add it to the list
                     */
                    if (expressionstr[i].ToString() == "(" || expressionstr[i].ToString() == ")")
                    {
                        infix.Add(expressionstr[i].ToString());
                        Console.WriteLine(expressionstr[i]);
                    }
                    
                    else
                    {
                        string @operator = "";
                        j = i;
                        while (j < expressionstr.Length && !(expressionstr[j] >= 48 && expressionstr[j] <= 57) && expressionstr[j] != ' ')
                        {
                            @operator += expressionstr[j];
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
            return postfix;

        }

    }
}