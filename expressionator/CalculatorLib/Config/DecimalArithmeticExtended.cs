using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Com.Rtwsq.Thom.Calculator.Validation;

namespace Com.Rtwsq.Thom.Calculator.Config
{
    /// <summary>
    /// Configuration for integer arithmetic using unary negative (-), multiplication (*), addition (+), and subtraction (-)
    /// as well as support for arbitarily nested parenthetical expressions
    /// </summary>
    public  class DecimalArithmeticExtended : IExpressionConfig
    {
        private readonly DecimalArithmeticBasic _decimalArithmeticBasic = new DecimalArithmeticBasic();
        public string Description => "Decimal Extended";
        /// <summary>
        /// Character set allowed in expressions - 0-9, +, -, *, ()
        /// </summary>
        public Regex ValidCharacters { get; } = new Regex(@"^[0-9\.\+\-\*\/\(\)\s]+$");

        public bool NumbersHaveValidFormat(string expression)
        {
            return _decimalArithmeticBasic.NumbersHaveValidFormat(expression);
        }

        /// <summary>
        /// Spots invalid combinations of operators - i.e. if there are two operators in a row, the second one must be a unary negative
        /// </summary>
        public Regex InvalidOperatorSequence { get; } = new Regex(@"[\+\-\*\/]\s*[\+\*\/]");

        /// <summary>
        /// Valid characters in a number - int this case, the digits 0-9
        /// </summary>
        public Regex Digits => _decimalArithmeticBasic.Digits ;

        /// <summary>
        /// Anything but 0-9, (, or ) - provides a quick way to extract all operators once and expression has been validated and minified
        /// </summary>
        public Regex NotDigitsOrParens => _decimalArithmeticBasic.NotDigitsOrParens;

        /// <summary>
        /// Returns the first character of an expression if it neither digits nor parentheses
        /// </summary>
        public Regex LeadingOperator => _decimalArithmeticBasic.LeadingOperator;

        /// <summary>
        /// All valid digits as an array of char
        /// </summary>
        public char[] DigitChars => _decimalArithmeticBasic.DigitChars;
        
        /// <summary>
        /// All valid operators as an array of char
        /// </summary>
        public char[] OpChars { get; } = { '+', '-', '*', '/' };
        
        /// <summary>
        /// All valid binary operators as an array of char
        /// </summary>
        public char[] BinaryOpChars { get; } = { '+', '-', '*', '/' };
        
        /// <summary>
        /// All valid unary operators as an array of char
        /// </summary>
        public char[] UnaryOpChars { get; } = { '-' };

        /// <summary>
        /// All valid operators grouped by precedence
        /// </summary>
        public char[][] OperatorsByPrecedence { get; } = { new[] { '*', '/' }, new[] { '-', '+' } };

        private Dictionary<string, Func<string, string, string>> _binaryOperations;
        /// <summary>
        /// Functions implementing the permitted binary operations (*, /, -, +)
        /// </summary>
        public Dictionary<string, Func<string, string, string>> BinaryOperations
        {
            get
            {
                if (_binaryOperations == null)
                {
                    _binaryOperations = _decimalArithmeticBasic.BinaryOperations;
                    _binaryOperations.Add("/", (dividend, divisor) => (decimal.Parse(dividend) / decimal.Parse(divisor)).ToString(CultureInfo.CurrentCulture));
                }
                return _binaryOperations;
            }
        }


        /// <summary>
        /// Functions implementing the permitted unary operations (-)
        /// </summary>
        public Dictionary<string, Func<string, string>> UnaryOperations => _decimalArithmeticBasic.UnaryOperations;

        public string RemoveWhiteSpace(string expression) => Regex.Replace(expression, @"\s+", "");

        public CharType GetCharType(char c)
        {
            return _decimalArithmeticBasic.GetCharType(c);
        }


        public  OpType GetOpType(char c)
        {
            return _decimalArithmeticBasic.GetOpType(c);
        }


        public  string[] ExtractOperators(string expression )
        {
          return NotDigitsOrParens.Matches(expression).Cast<Match>().Select(t => t.Value).ToArray();
        }

        public  string[] ExtractNumerands(string expression)
        {
            return Digits.Matches(expression).Cast<Match>().Select(t => t.Value).ToArray();
        }

        public string AllowedNumberTypes => "Decimal";
        private string _allowedOps;
        public  string AllowedOperators
        {
            get
            {
                if (string.IsNullOrEmpty(_allowedOps))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("Unary: (" + string.Join(",", UnaryOpChars) + "), Binary: ");
                    foreach (char[] ops in OperatorsByPrecedence)
                    {
                        stringBuilder.Append("(" + string.Join(",", ops) + ") ");
                    }
                    _allowedOps = stringBuilder.ToString();
                }
                return _allowedOps;
            }
        }
    }
}
