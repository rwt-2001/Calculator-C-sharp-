using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public class ExpressionEvaluator
    {
        private Dictionary<string, Tuple<IOperation, int>> _operationDictionary;

        /*Constructor to Initialize the dictionary for basic operations*/
        public ExpressionEvaluator()
        {
            RegisterOperation("*", 1, new Multiplication());
            RegisterOperation("/", 1, new Divide());
            RegisterOperation("+", 2, new Addition());
            RegisterOperation("-", 2, new Subtraction());
            RegisterOperation("!", 1, new Factorial());
            RegisterOperation("^", 1, new Power());
            RegisterOperation("lg", 1, new Log());
            RegisterOperation("ln", 1, new LogNatural());

        }
        
        /*Method to evaluate the expression given by user*/
        public double Evaluate(string expression)
        {
            //TODO: Implement expression parsing and Exception Handling
            List<object> infix= _ToInfixList(expression);
            double result = 0;
            return result;
        }

        /* Method to register new operation*/
        public void RegisterOperation(string @operator,int priority, IOperation operation)
        {
            _operationDictionary.
                Add(@operator, new Tuple<IOperation, int>( operation, priority ));
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

        

        /*
         * TODO : Convert string expression to List<object> (separating operands and operators)
         */
        private List<object> _ToInfixList(string expressionstr)
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

                    Console.WriteLine(oprand);
                    infix.Add(Convert.ToDouble(oprand));
                    continue;

                }

                string @operator = "";
                j = i;
                while (j < expressionstr.Length && !(expressionstr[j] >= 48 && expressionstr[j] <= 57) && expressionstr[j] != ' ')
                {
                    @operator += expressionstr[j];
                    j++;
                }
                infix.Add(@operator);
                i = j - 1;

            }
            return infix;
        }
        /*
         * TODO : Infix to postfix conversion
         */
        private List<object> _InfixToPostfix(List<object> infix)
        {
            List<object> postfix = new List<object>();
            return postfix;
        }

    }
}