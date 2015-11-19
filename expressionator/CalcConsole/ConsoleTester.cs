using System;
using Com.Rtwsq.Thom.Calculator;
using Com.Rtwsq.Thom.Calculator.Config;
using Com.Rtwsq.Thom.Calculator.Validation;

namespace CalcConsole
{
    /// <summary>
    /// Noddy util class to exercise validation logic 
    /// </summary>
    public static class ConsoleTester
    {
        private static readonly IExpressionValidator ExpressionValidator = new ExpressionValidator(new IntegerArithmeticBasic());
        static void TestExpression(string expression, int? expected)
        {

            ExpressionValidationResult expressionValidationResult = ExpressionValidator.Validate(expression);

            if (expressionValidationResult.HasValidationErrors)
            {
                System.Console.WriteLine("{0}", expression);
                foreach (string validationError in expressionValidationResult.ValidationErrors)
                {
                    Console.WriteLine(validationError);
                }
            }
            else
            {
                //var expessionElement = ExpressionProcessor.Process(expressionValidationResult.MinifiedExpression);
                var arithmeticExpression = new ArithmeticExpression(expressionValidationResult.MinifiedExpression);

                if (arithmeticExpression.HasValue)
                {
                    var itsValue = arithmeticExpression.ItsValue;
                    string result = (Int32.Parse(itsValue) == expected) ? "Equal" : "NOT Equal";
                    Console.WriteLine("{0} -> {1} {2} {3}", arithmeticExpression.MinifiedExpression, itsValue, result, expected);
                }
                else
                    Console.WriteLine("{0} -> {1} [{2}]", arithmeticExpression.MinifiedExpression, arithmeticExpression.ProcessingException.Message, arithmeticExpression.ProcessingException.InnerException.Message);
            }
        }

        private static void RunTests1()
        {
            TestExpression("1+2-3*4-5+6", -8);
            TestExpression("-1+2-3*4-5+6", -10);
            TestExpression("--1+2-3*4-5+6", null);
            TestExpression("-1 1+2-3*4-5+6", null);
            TestExpression("(1+2-3*4-5+6)", -8);
            TestExpression("((1+2)-3*4-5+6)", -8);
            TestExpression("((1+2-3)*4-5+6)", 1);

            TestExpression("(1+2-3*(4-5+6))", -12);
            TestExpression("(1+2-(3*(4-5+6)))", -12);
            TestExpression("((1+2)-((3*4)-(5+6))", null);
            TestExpression("(((((1+2)-3)*4)-5)+6)", 1);

            TestExpression("-((-((-(1+2)-3)*4)-5)+6)", -25);

            TestExpression("1 + 2 * 3 - 4 +-5 *-6", 33);

        }

        private static void RunTests2()
        {
            TestExpression("2+3", 5);
            TestExpression("2+3*4", 14);
            TestExpression("2*3+4", 10);



            TestExpression("2+3*(2+3*4)", 44);

            TestExpression("(2+3)*(2+3*4)", 70);

            TestExpression("(2+3)*2+3*4", 22);
            TestExpression("((2+3)*2+3)*4", 52);
            TestExpression("(-(-1 + -2)) * -(-3 - -4) + -(-5 * 6)", 27);
            // NOTE: javascript errors on this ["(9--10)" is illegal], but excel & this parser accepts it
            TestExpression("(((1+(2+3)*-(4*5))+6)+7*8+(9--10))+11", -7);
            TestExpression("(((1+(2+3)*-(4*5))+6)+7*8+(9-10))+11", -27);
            TestExpression("(3+(4*5)+6)", 29);
            TestExpression("-((3+(4*5)+6)+(3+(4*5)+6)*(3+(4*5)+6))", -870);
            TestExpression("(-(-1 + -2)) * -(-3 - -4) + -(-5 * 6)", 27);
            TestExpression("((-1 + -2)) * (-3 - -4) + (-5 * 6)", -33);
            TestExpression("(((1+(2*(3+(4*5)+6)+7)*8)+9)-10)+11", 531);
            TestExpression("(((2*3)+(4*5))*((6+7)*(8+9)+(10*11))+((12+13)+(14*15)+(16+17)*(18+19)))", 10062);
            TestExpression("((-(2*3)+(4*5))*-((6+7)*(8+9)+(10*11))+(-(12+13)+(14*15)+-(16+17)*(18+19)))", -5670);
        }
    }
}
