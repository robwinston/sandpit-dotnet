using System;
using System.Collections.Generic;
using Com.Rtwsq.Thom.Calculator;
using Com.Rtwsq.Thom.Calculator.Config;
using Com.Rtwsq.Thom.Calculator.Validation;

namespace CalcConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            while (SetExpressionType())
            {
                Console.WriteLine(ArithmeticExpression.AllowedOps);
                while (true)
                {
                    Console.WriteLine("Enter an expression or q:");
                    string expression = Console.ReadLine();
                    if (!string.IsNullOrEmpty(expression))
                    {
                        if (expression.ToLower().StartsWith("q"))
                        {
                            break;
                        }
                        Console.WriteLine(EvaluateExpression(expression));
                    }
                }
            }
        }

        private static bool SetExpressionType()
        {
            bool gotOne = false;
            do
            {

                Console.WriteLine("Select Expression Type:");
                foreach (KeyValuePair<string, IExpressionConfig> keyValuePair in Choices)
                {
                    Console.WriteLine("{0} : {1}", keyValuePair.Key, keyValuePair.Value.AllowedOperators);
                }
                Console.WriteLine("q) Quit");

                var choice = Console.ReadLine() ?? string.Empty;
                if (choice.ToLower().StartsWith("q")) return false;
                if (!Choices.ContainsKey(choice)) continue;
                ArithmeticExpression.ExpressionConfig = Choices[choice];
                gotOne = true;
            } while (!gotOne);
            return true;
        }

        private static Dictionary<string, IExpressionConfig> Choices { get; } = new Dictionary
            <string, IExpressionConfig>()
        {
            {"1", new IntegerArithmeticBasic()},
            {"2", new IntegerAritmeticExtended()}
        };

        private static string EvaluateExpression(string expression)
        {
            return new ArithmeticExpression(expression).Result;
        }
    }
}

