﻿using System;
using System.Collections.Generic;
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
    public  class IntegerArithmeticBasic : IExpressionConfig
    {
        public string Description => "Integer Basic";
        /// <summary>
        /// Character set allowed in expressions - 0-9, +, -, *, ()
        /// </summary>
        public Regex ValidCharacters { get; } = new Regex(@"^[0-9\+\-\*\(\)\s]+$");

        /// <summary>
        /// Spots successive numbers missing an operator or parentheses
        /// </summary>
        private Regex EmbeddedWhiteSpace { get; } = new Regex(@"[0-9]+\s+[0-9]+");

        public bool NumbersHaveValidFormat(string expression)
        {
            return EmbeddedWhiteSpace.Matches(expression).Count == 0;
        }

        /// <summary>
        /// Spots invalid combinations of operators - i.e. if there are two operators in a row, the second one must be a unary negative
        /// </summary>
        public Regex InvalidOperatorSequence { get; } = new Regex(@"[\+\-\*]\s*[\+\*]");
        
        /// <summary>
        /// Valid characters in a number - int this case, the digits 0-9
        /// </summary>
        public Regex Digits { get; } = new Regex("[0-9]+");
        
        /// <summary>
        /// Anything but 0-9, (, or ) - provides a quick way to extract all operators once and expression has been validated and minified
        /// </summary>
        public Regex NotDigitsOrParens { get; } = new Regex("[^()0-9]+");

        /// <summary>
        /// Returns the first character of an expression if it neither digits nor parentheses
        /// </summary>
        public Regex LeadingOperator { get; } = new Regex("^[^()0-9]+");
        
        /// <summary>
        /// All valid digits as an array of char
        /// </summary>
        public char[] DigitChars { get; } = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
        
        /// <summary>
        /// All valid operators as an array of char
        /// </summary>
        public char[] OpChars { get; } = { '+', '-', '*' };
        
        /// <summary>
        /// All valid binary operators as an array of char
        /// </summary>
        public char[] BinaryOpChars { get; } = { '+', '-', '*' };
        
        /// <summary>
        /// All valid unary operators as an array of char
        /// </summary>
        public char[] UnaryOpChars { get; } = { '-' };

        /// <summary>
        /// All valid operators grouped by precedence
        /// </summary>
        public char[][] OperatorsByPrecedence { get; } = { new[] { '*' }, new[] { '-', '+' } };

        /// <summary>
        /// Functions implementing the permitted binary operations
        /// </summary>
        public Dictionary<string, Func<string, string, string>> BinaryOperations { get; } = new Dictionary<string, Func<string, string, string>>()
        {
            {"*", (multiplicand, multiplier) => (int.Parse(multiplier)* int.Parse(multiplicand)).ToString()},
            {"+", (addend1, addend2) => (int.Parse(addend1) + int.Parse(addend2)).ToString()},
            {"-", (minuend, subtrahend) => (int.Parse(minuend) - int.Parse(subtrahend)).ToString()},

        };
        
        /// <summary>
        /// Functions implementing the permitted unary operations (-)
        /// </summary>
        public Dictionary<string, Func<string, string>> UnaryOperations { get; } = new Dictionary<string, Func<string, string>>()
        {
            {"-", (numerand) => (-int.Parse(numerand)).ToString()},

        };

        public string RemoveWhiteSpace(string expression) => Regex.Replace(expression, @"\s+", "");

        public CharType GetCharType(char c)
        {
            if (DigitChars.Contains(c))
                return CharType.Number;
            if (OpChars.Contains(c))
                return CharType.Operator;
            if (c == ')') return CharType.RightParen;
            if (c == '(') return CharType.LeftParen;

            return CharType.None;
        }


        public  OpType GetOpType(char c)
        {
            if (BinaryOpChars.Contains(c) && UnaryOpChars.Contains(c))
                return  OpType.Both;
            if (BinaryOpChars.Contains(c))
                return OpType.Binary;
            if (UnaryOpChars.Contains(c))
                return OpType.Unary;
            return OpType.None;
        }


        public  string[] ExtractOperators(string expression )
        {
          return NotDigitsOrParens.Matches(expression).Cast<Match>().Select(t => t.Value).ToArray();
        }

        public  string[] ExtractNumerands(string expression)
        {
            return Digits.Matches(expression).Cast<Match>().Select(t => t.Value).ToArray();
        }

        public string AllowedNumberTypes => "Integer";
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
