using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Com.Rtwsq.Thom.Calculator.Config;

namespace Com.Rtwsq.Thom.Calculator.Validation
{
    public  class ExpressionValidator : IExpressionValidator
    {
        public ExpressionValidator(IExpressionConfig expressionConfig)
        {
            _expressionConfig = expressionConfig;

            _checkForValidCharacters = new ValidationCheck(
            expression =>
            {
                var matches = _expressionConfig.ValidCharacters.Matches(expression);
                return matches.Count == 1 && matches[0].ToString() == expression;
            },
            "Expression contains one or more invalid characters");

            _checkForValidNumbers = new ValidationCheck(
            expression => _expressionConfig.BadWhiteSpace.Matches(expression).Count == 0,
            "Expression has numbers with invalid embedded white space");

            _checkForValidOpSeq = new ValidationCheck(
            expression => _expressionConfig.InvalidOperatorSequence.Matches(expression).Count == 0,
            "Expression has invalid contiguous operators");

            _checkForValidOpSequences = new ValidationCheck(
            HasValidOperatorSequences,
            "Expression has invalid operator sequences"
            );

            _checkForValidNumOpMix = new ValidationCheck(
            HasValidNumOpMix,
            "Expression has invalid combinations");
        }

        private readonly IExpressionConfig _expressionConfig;
        public  ExpressionValidationResult Validate(string expression)
        {
            try
            {
                var expressionValidationResult = new ExpressionValidationResult(expression);

                // Validation phase 1
                if (!_checkForValidCharacters.IsValid(expression))
                    expressionValidationResult.AddValidationError(_checkForValidCharacters.ErrorMessage);

                if (!CheckForBalancedParentheses.IsValid(expression))
                    expressionValidationResult.AddValidationError(CheckForBalancedParentheses.ErrorMessage);

                if (!_checkForValidNumbers.IsValid(expression))
                    expressionValidationResult.AddValidationError(_checkForValidNumbers.ErrorMessage);

                if (expressionValidationResult.HasValidationErrors)
                    return expressionValidationResult;


                // NOTE: this must be done here 
                //  - earlier checks rely on it not being done yet
                //  - subsequent checks rely on it having being done
                expression = _expressionConfig.RemoveWhiteSpace(expression);
                expressionValidationResult.MinifiedExpression = expression;

                // Validation phase 2
                if (!_checkForValidOpSeq.IsValid(expression))
                    expressionValidationResult.AddValidationError(_checkForValidOpSeq.ErrorMessage);

                if (!_checkForMatchedParentheses.IsValid(expression))
                    expressionValidationResult.AddValidationError(_checkForMatchedParentheses.ErrorMessage);

                if (!_checkForValidOpSequences.IsValid(expression))
                    expressionValidationResult.AddValidationError(_checkForValidOpSequences.ErrorMessage);

                // only do this one if no-one has caught anything
                if (!expressionValidationResult.HasValidationErrors)
                {
                    if (!_checkForValidNumOpMix.IsValid(expression))
                        expressionValidationResult.AddValidationError(_checkForValidNumOpMix.ErrorMessage);
                }

                return expressionValidationResult;

            }
            catch (Exception exception)
            {                
                throw new Exception("Exception during validation", exception);
            }
        }


        public  IValidatonCheck CheckForBalancedParentheses { get; } = new ValidationCheck(
            expression =>
            {
                return expression != null && (expression.IndexOfAny(new[] {')', '('}) == 0 ||
                                              expression.Count(c => c == '(') == expression.Count(c => c == ')'));
            },
            "Expression does not have an equal number of left/right parenetheses"
            );

        private readonly IValidatonCheck _checkForValidCharacters;

        private readonly IValidatonCheck _checkForValidNumbers;

        private readonly IValidatonCheck _checkForValidOpSeq;

        private  readonly IValidatonCheck _checkForMatchedParentheses = new ValidationCheck(
            HasMatchedParens,
            "Expression has mis-matched parentheses");
        private  static bool HasMatchedParens(string expression)
        {
            var parenStack = new Stack<char>();
            foreach (char c in expression)
            {
                if (c == '(' || c == ')')
                {
                    // starting  a new group
                    if (c == '(' && parenStack.Count == 0)
                        parenStack.Push(c);
                    // nesting - (didn't 'or' with above for readability) 
                    else if (c == parenStack.Peek())
                        parenStack.Push(c);
                    // presume matched-up paren - pop or die if there isn't one
                    else if (parenStack.Count == 0)
                        return false;
                    else
                        parenStack.Pop();
                }
            }
            return parenStack.Count == 0;
        }

        private readonly IValidatonCheck _checkForValidNumOpMix;
        private  bool HasValidNumOpMix(string expression)
        {
            // This won't be foolproof, and is a bit brute-force-ish, but should catch misc errors without great expense
            // More subtle ones will cause decompose and/or reduce to fail which will generate a processing error message

            CharType lastCharType = CharType.None;
            CharType lastButOneCharType = CharType.None;
            foreach (char c in expression)
            {
                CharType thisCharType = _expressionConfig.GetCharType(c);
                if (lastCharType != CharType.None)
                {
                    // No attempt made to be clever with logical constructs - simple is easy!
                    if (thisCharType == CharType.LeftParen &&
                        (lastCharType == CharType.RightParen || lastCharType == CharType.Number))
                        return false;
                    if (thisCharType == CharType.RightParen &&
                        (lastCharType == CharType.LeftParen || lastCharType == CharType.Operator))
                        return false;
                    if (thisCharType == CharType.Operator)
                    {
                        // three ops in a row is always illegal (i.e. don't allow compound unary ops)
                        if (lastButOneCharType == CharType.Operator && lastCharType == CharType.Operator)
                            return false;

                        var thisOpType = _expressionConfig.GetOpType(c);
                        if (thisOpType == OpType.Unary && (lastCharType == CharType.RightParen || lastCharType == CharType.Number))
                        {
                            return false;
                        }
                        if (thisOpType == OpType.Binary && (lastCharType == CharType.LeftParen || lastCharType == CharType.Operator))
                        {
                           return false;
                        }
                    }
                }
                lastButOneCharType = lastCharType;
                lastCharType = thisCharType;
            }

            return true;
        }


        // TODO not happy with this one ... seems klunky & potentially incomplete
        private readonly IValidatonCheck _checkForValidOpSequences;

        // If there is a leading operator, it must be a unary -
        // If there are contiguous operators, they must be no longer that two char and the first one must be the a unary -
        private  bool HasValidOperatorSequences(string expression)
        {
            // Check for a leading op ... if there is one, it must be a unary -
            var leadingOps = _expressionConfig.LeadingOperator.Matches(expression);
            if (leadingOps.Count != 0)
            {
                // ... if there is one, it must be a unary -
                if (leadingOps[0].ToString() != "-")
                    return false;
                // and remove it to simplify the remaining checks ...
                expression = expression.Substring(1);
            }

            var hasBadOpsStrings = _expressionConfig.NotDigitsOrParens.Matches(expression).Cast<Match>()
                    .Select(m => m.Value)
                    .ToArray()
                    .Any(m => m.Length > 2 || (m.Length == 2 && m[1] != '-'));

            return !hasBadOpsStrings;
        }
    }    
}
