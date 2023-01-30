using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                //Start
                Console.WriteLine("Enter the parameter (or Press x and enter to exit): ");
                string parameter = Convert.ToString(Console.ReadLine().Replace(" ", ""));

                //Exit
                if (parameter.Equals("x", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                //Validate Parameter contains numbers and operators only.
                if (parameter == null || parameter == "")
                {
                    Console.WriteLine("(Blank)\n");
                }
                else
                {
                    List<string> validationlist = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", "(", ")", "+", "-", "*", "/" };
                    bool validation = true;
                    foreach (char p in parameter)
                    {
                        if (!validationlist.Contains(p.ToString()))
                        {
                            validation = false;
                        }
                    }

                    //Calculate or Reject
                    if (validation == true)
                    {
                        //double final = 0;
                        Program C = new Program();
                        Console.WriteLine("The answer is : " + C.Calculate(parameter) + "\n\n");
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid Input. Example: 10 - ( 2 + 3 * ( 7 - 5 )) \nTry again!\n");
                    }
                }
            }
        }
        //To perform the question inserted by user
        public double Calculate(string sum)
        {
            string postfix = "";
            Stack<double> numbers = new Stack<double>();
            Stack<char> operators = new Stack<char>();

            for (int i = 0; i < sum.Length; i++)
            {
                char c = sum[i];

                if (Char.IsDigit(c) || c.Equals('.'))
                {
                    postfix += c;
                }
                else if (c == '(')
                {
                    operators.Push(c);
                }
                else if (c == ')')
                {
                    
                    while (operators.Peek() != '(')
                    {
                        postfix += operators.Pop();
                    }
                    operators.Pop();
                }
                else if (isOperator(c))
                {
                    if (Char.IsDigit(postfix[postfix.Length - 1]))
                    {
                        postfix += ";";
                    }
                    while (operators.Count > 0 && precedence(c) <= precedence(operators.Peek()))
                    {
                        postfix += operators.Pop();
                    }
                    operators.Push(c);
                }
            }

            while (operators.Count > 0)
            {
                postfix += operators.Pop();
            }

            string numtostore = string.Empty;
            for (int i = 0; i < postfix.Length; i++)
            {
                char c = postfix[i];

                if (c.Equals(';') && numtostore!= "")
                {
                    numbers.Push(Convert.ToDouble(numtostore.Replace(";","")));
                    numtostore = string.Empty;
                }
                else if (isOperator(c))
                {
                    if (numtostore != string.Empty)
                    {
                        numbers.Push(Convert.ToDouble(numtostore.Replace(";", "")));
                        numtostore = string.Empty;
                    }
                    double num2 = numbers.Pop();
                    double num1 = numbers.Pop();

                    double result = performOperation(num1, num2, c);

                    numbers.Push(result);
                }
                else
                {
                    numtostore += c;
                }
            }
            if (numbers.Count == 0 && numtostore != string.Empty)
            {
                numbers.Push(Convert.ToDouble(numtostore.Replace(";", "")));
            }
            return numbers.Pop();
        }

        //To validate the operator priority
        static int precedence(char ch)
        {
            switch (ch)
            {
                case '+':
                case '-':
                    return 1;

                case '*':
                case '/':
                    return 2;

                case '^':
                    return 3;
            }
            return -1;
        }

        //To validate whether is an operator
        static bool isOperator(char ch)
        {
            return (ch == '+' || ch == '-' || ch == '*' || ch == '/');
        }

        //To perform the calculation
        static double performOperation(double num1, double num2, char operation)
        {
            switch (operation)
            {
                case '+':
                    return num1 + num2;
                case '-':
                    return num1 - num2;
                case '*':
                    return num1 * num2;
                case '/':
                    return num1 / num2;
                default:
                    return 0;
            }
        }
    }
}