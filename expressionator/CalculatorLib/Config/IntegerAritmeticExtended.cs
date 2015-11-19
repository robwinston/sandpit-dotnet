using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Com.Rtwsq.Thom.Calculator.Validation;

namespace Com.Rtwsq.Thom.Calculator.Config
{
    /// <summary>
    /// Configuration for integer arithmetic using unary negative (-), multiplication (*), modulo (%), addition (+), and subtraction (-)
    /// (as well as support for nested parenthetical expressions)
    /// </summary>
    public class IntegerAritmeticExtended : IExpressionConfig
    {
        // Use IntegerArithmeticBasic config if it's the same
        // Use composition instead of inheritance to keep things loosely coupled
        private readonly IntegerArithmeticBasic _integerArithmeticBasic = new IntegerArithmeticBasic();
        /// <summary>
        /// Character set allowed in expressions - 0-9, +, -, *, %, ()
        /// </summary>
        public Regex ValidCharacters { get; } = new Regex(@"^[0-9\+\-\*\%\(\)\s]+$");

        /// <summary>
        /// Spots successive numbers missing an operator or parentheses
        /// </summary>
        public Regex BadWhiteSpace => _integerArithmeticBasic.BadWhiteSpace;

        /// <summary>
        /// Spots invalid combinations of operators - i.e. if there are two operators in a row, the second one must be a unary negative
        /// </summary>
        public Regex InvalidOperatorSequence { get; } = new Regex(@"[\+\-\*\%]\s*[\+\*\%]");

        /// <summary>
        /// Valid characters in a number - int this case, the digits 0-9
        /// </summary>
        public Regex Digits => _integerArithmeticBasic.Digits;
        
        /// <summary>
        /// Anything but 0-9, (, or ) - provides a quick way to extract all operators once and expression has been validated and minified
        /// </summary>
        public Regex NotDigitsOrParens => _integerArithmeticBasic.NotDigitsOrParens;

        /// <summary>
        /// Returns the first character of an expression if it neither digits nor parentheses
        /// </summary>
        public Regex LeadingOperator => _integerArithmeticBasic.LeadingOperator;

        /// <summary>
        /// All valid digits as an array of char
        /// </summary>
        public char[] DigitChars => _integerArithmeticBasic.DigitChars;

        /// <summary>
        /// All valid operators as an array of char
        /// </summary>
        public char[] OpChars { get; } = { '+', '-', '*', '%' };

        /// <summary>
        /// All valid binary operators as an array of char
        /// </summary>
        public char[] BinaryOpChars { get; } = { '+', '-', '*', '%' };

        /// <summary>
        /// All valid unary operators as an array of char
        /// </summary>
        public char[] UnaryOpChars => _integerArithmeticBasic.UnaryOpChars;

        /// <summary>
        /// All valid operators grouped by precedence
        /// </summary>
        public char[][] OperatorsByPrecedence { get; } = { new[] { '*', '%' }, new[] { '-', '+' } };


        private Dictionary<string, Func<string, string, string>> _binaryOperations;
        /// <summary>
        /// Functions implementing the permitted binary operations (*, %, -, +)
        /// </summary>
        public Dictionary<string, Func<string, string, string>> BinaryOperations
        {
            get
            {
                if (_binaryOperations == null)
                {
                    _binaryOperations = _integerArithmeticBasic.BinaryOperations;
                    _binaryOperations.Add( "%", (dividend, divisor) => (int.Parse(dividend) % int.Parse(divisor)).ToString());
                }
                return _binaryOperations;
            }
        }

        public Dictionary<string, Func<string, string>> UnaryOperations => _integerArithmeticBasic.UnaryOperations;

        public string RemoveWhiteSpace(string expression)
        {
            return _integerArithmeticBasic.RemoveWhiteSpace(expression);
        }

        public CharType GetCharType(char c)
        {
            return _integerArithmeticBasic.GetCharType(c);
        }

        public OpType GetOpType(char c)
        {
            return _integerArithmeticBasic.GetOpType(c);
        }

        public string[] ExtractOperators(string expression)
        {
            return _integerArithmeticBasic.ExtractOperators(expression);
        }

        public string[] ExtractNumerands(string expression)
        {
            return _integerArithmeticBasic.ExtractNumerands(expression);
        }

        private string _allowedOps;
        public string AllowedOperators
        {
            get
            {
                if (string.IsNullOrEmpty(_allowedOps))
                {
                    StringBuilder stringBuilder = new StringBuilder("Integer arithmetic using - ");
                    stringBuilder.Append("Unary: (" + string.Join(",", UnaryOpChars) + "), Binary: ");
                    foreach (char[] ops in OperatorsByPrecedence)
                    {
                        stringBuilder.Append(" (" + string.Join(",", ops) + ") ");
                    }
                    _allowedOps = stringBuilder.ToString();
                }
                return _allowedOps;
            }
        }
    }
}
