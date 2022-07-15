using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public class ExpressionEvaluator
    {
        private Dictionary<string, KeyValuePair<IOperation, int>> _operationDictionary;

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
            double result = 0;
            return result;
        }

        /* Method to register new operation*/
        public void RegisterOperation(string @operator,int priority, IOperation operation)
        {
            _operationDictionary.
                Add(@operator, new KeyValuePair<IOperation, int>( operation, priority ));
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
    }
}