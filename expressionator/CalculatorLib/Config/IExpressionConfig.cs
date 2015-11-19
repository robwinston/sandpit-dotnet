using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Com.Rtwsq.Thom.Calculator.Validation;

namespace Com.Rtwsq.Thom.Calculator.Config
{
    /// <summary>
    /// Describes the configuration for what constitiutes a valid expression including utility methods used by parse and validate
    /// </summary>
    public interface IExpressionConfig
    {
        Regex ValidCharacters { get; }
        Regex BadWhiteSpace { get; }
        Regex InvalidOperatorSequence { get; }
        Regex Digits { get; }
        Regex NotDigitsOrParens { get; }
        Regex LeadingOperator { get; }
        char[] DigitChars { get; }
        char[] OpChars { get; }
        char[] BinaryOpChars { get; }
        char[] UnaryOpChars { get; }
        char[][] OperatorsByPrecedence { get; }
        Dictionary<string, Func<string, string, string>> BinaryOperations { get; }
        Dictionary<string, Func<string, string>> UnaryOperations { get; }
        string RemoveWhiteSpace(string expression);
        CharType GetCharType(char c);
        OpType GetOpType(char c);
        string[] ExtractOperators(string expression );
        string[] ExtractNumerands(string expression);
        string AllowedOperators { get; }
    }
}