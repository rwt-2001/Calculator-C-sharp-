// See https://aka.ms/new-console-template for more information
using System;
using System.Collections;
using System.Text.RegularExpressions;

//class ExpressionOperation
//{
//    Dictionary<char, int> priority = new Dictionary<char, int>();
//    ExpressionOperation()
//    { 
//        priority.Add('+', 1);
//        priority.Add('-', 1);
//        priority.Add('*', 10);
//        priority.Add('/', 10);
//    }


//    //TODO implement infix to postfix conversion
//    public string InfixToPostfix(string expression)
//    {
//        return "";
//    }
//}



////For conversion of infix to postfix
//string postfix(string s)
//{
//    string output = "";
//    Stack<char> @operator = new Stack<char>();
//    for(int i = 0; i < s.Length; i++)
//    {
//        if (s[i] >= '0' && s[i] <= '9')
//        {
//            output += s[i];
//        }
//        else {
//            if(@operator.Count > 0)
//            {
//                while(priority[@operator.Peek()] >= priority['j'])
//                {

//                }
//            }
//        }


//    }

//    return output;
//}
//abstract class Binary
//{
//    public double Validate(double[] args)
//    {
//        if (args.Length != 2)
//        {
//            Console.Write("Operands Error");
//            return -1.0;
//        }

//        return Calculate(args);
//    }

//    protected abstract double Calculate(double[] args);
//}
//class Addition : Binary
//{

//    protected override double Calculate(double[] args)
//    {
//        double result = args[0] + args[1];
//        return result;
//    }
//}
class Practice
{
    static void Main()
    {
        string expressionstr = "2 + 3.6 + 4 + 7 / 2 * 8 - 94 ln 8";
     

        List<object> infix = GetInfixList(expressionstr);
        
        Console.WriteLine("Expression in infix :");

        for(int i = 0; i < infix.Count; i++)
        {
            Console.Write(infix[i]);
        }



        Dictionary<string, int> priority = new Dictionary<string, int>();
        
            priority.Add("+", 1);
            priority.Add("-", 1);
            priority.Add("/", 10);
            priority.Add("*", 10);
            priority.Add("ln", 20);




        //TODO : Change list infix to postfix -> Completed

        List<object> postfix = new List<object>();
        Stack<string> operatorStk = new Stack<string>();

        for(int i = 0; i < infix.Count; i++)
        {
            if(infix[i].GetType() == typeof(double))
            {
                postfix.Add(infix[i]);
            }
            else
            {
                if(operatorStk.Count == 0)
                {
                    operatorStk.Push(infix[i].ToString());
                }
                else
                {
                    if (priority[operatorStk.Peek()] < priority[infix[i].ToString()])
                    {
                        operatorStk.Push(infix[i].ToString());
                    }
                    else
                    {
                        //If the priority of operator at top of stack if equal to or greater than incoming operator then pop the stack and 
                        while(operatorStk.Count != 0 && priority[operatorStk.Peek()] >= priority[infix[i].ToString()])
                        {
                            postfix.Add( operatorStk.Peek());
                            operatorStk.Pop();
                        }
                        operatorStk.Push(infix[i].ToString());
                    }

                }

            }
        }
        while(operatorStk.Count > 0)
        {
            postfix.Add(operatorStk.Peek());
            operatorStk.Pop();
        }
        Console.WriteLine("\n\nPostfix Expression : ");
        for (int i = 0; i < postfix.Count; i++)
        {
            Console.Write(postfix[i]);
        }

        Console.WriteLine("\n\n");


    }

    //Method to form infix list of type object
    static public List<object> GetInfixList(string expressionstr)
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


    //Method to convert
}