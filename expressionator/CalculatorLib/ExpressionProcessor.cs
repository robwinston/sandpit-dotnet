using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Rtwsq.Thom.Calculator.Config;
using Com.Rtwsq.Thom.Calculator.Validation;

namespace Com.Rtwsq.Thom.Calculator
{
    /// <summary>
    /// Processes a validated expression to compute its value
    /// Uses the same IExpressionConfig implementation used by the IExpressionValidator
    /// (and this is what determines the kind of expressions it is capable of processing) 
    /// </summary>
    public class ExpressionProcessor : IExpressionProcessor
    {
        public ExpressionProcessor(IExpressionConfig expressionConfig)
        {
            _expressionConfig = expressionConfig;
            _expressionValidator = new ExpressionValidator(expressionConfig);
        }

        private readonly IExpressionConfig _expressionConfig;
        private  readonly IExpressionValidator _expressionValidator;
        /// <summary>
        /// Produce a processed \ExpressionElement with either its value (if its valid) or with relevant errors if it is not 
        /// </summary>
        /// <param name="minifiedExpression">A validated, compressed expression produced by the IExpressionValidator</param>
        /// <returns>A processed ExpressionElement</returns>
        public ExpressionElement Process(string minifiedExpression)
        {
            ExpressionElement expressionElement = new ExpressionElement(minifiedExpression, 0, 0);

            // catch exceptions here after potentially recursive calls have been allowed to unwind ...
            try
            {
                expressionElement = Decompose(expressionElement);
            }
            catch (Exception e)
            {              
                throw new Exception("Processing exception [Decompose]", e);
            }
            try
            {
                expressionElement = Reduce(expressionElement);
            }
            catch (Exception e)
            {
                throw new Exception("Processing exception [Reduce]", e);
            }

            return expressionElement;
        }

        /// <summary>
        /// Recusively decompose an expression into a tree of ExpressionElements
        /// </summary>
        /// <param name="expressionElement"></param>
        /// <param name="level"></param>
        /// <returns>The ExpressionElement with the SubExpressionElements property populated</returns>
        private  ExpressionElement Decompose(ExpressionElement expressionElement, int level = 0)
        {
                IEnumerable<ExpressionElement> expressionElementChildren
                    = FindExpressionsAtSameLevel(expressionElement.Expression, level);

                expressionElement.PopulateSubExpressionElements(expressionElementChildren.Select(e => Decompose(e, level + 1)));

                return expressionElement;
        }


        /// <summary>
        /// Recusively process an ExpressionElement to compute its "reduced" value
        /// i.e. an expression with all parenthetical expressions computed
        /// </summary>
        /// <param name="expressionElement"></param>
        /// <returns></returns>
        private  ExpressionElement Reduce(ExpressionElement expressionElement)
        {
                if (!expressionElement.HasChildren())
                {
                    expressionElement.ReducedExpression = expressionElement.Expression;
                }
                else
                {
                    ExpressionElement[] itsReducedChildren =
                        expressionElement.SubExpressionElements.Select(Reduce).ToArray();

                    StringBuilder reducedExpression = new StringBuilder();
                    foreach (ExpressionElement reducedChild in itsReducedChildren)
                    {
                        reducedExpression.Append(Evaluate(reducedChild) + reducedChild.JoinOp);
                    }
                    expressionElement.ReducedExpression = reducedExpression.ToString();
                }
                return expressionElement;
        }


        // Decompose Helpers

        // This method uses two abbreviated variable names for convenience:
        // flp -> the index of the first left-hand parenthesis in the expression (or -1 if there is none)
        // rlp -> the index of its matching right-hand parenthesis (doesn't get used if there's no flp)
        /// <summary>
        /// Breaks up an expression into a set of elements at the same level
        /// These represent either "top-level" parenthetical expressions or expressions outside of parentheses
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private  IEnumerable<ExpressionElement> FindExpressionsAtSameLevel(string expression, int level)
        {
            CheckParens(expression, level, "entry");
            List<ExpressionElement> expressionElements = new List<ExpressionElement>();

            string whatsLeft = (expression.IndexOf(')') == -1) ? String.Empty : expression;
            int index = 0;

            while (whatsLeft.Length > 0)
            {
                expression = whatsLeft;
                int flp = expression.IndexOf('(');

                // it's an expression with no parens, no further decompose required
                if (flp == -1)
                {
                    expressionElements.Add(new ExpressionElement(expression, level, index));
                    whatsLeft = string.Empty;
                }
                // it's an expression starting with, or fully enclosed in parens
                else
                {
                    string joinOp;
                    if (flp == 0 || flp == 1)
                    {
                        char? nestedUnary = null;
                        {
                            // if there's a unary -, take note and discard it
                            if (flp == 1)
                            {
                                nestedUnary = expression[0];
                                expression = expression.Substring(1);
                            }
                        }

                        // figure out what's left, if anything
                        int rlp = FindMatchingParen(expression);
                        if (rlp < expression.Length - 1)
                        {
                            joinOp = expression.Substring(rlp + 1, 1);
                            whatsLeft = expression.Substring(rlp + 2);
                            CheckParens(whatsLeft, level, "starting with paren");
                        }
                        else
                        {
                            joinOp = String.Empty;
                            whatsLeft = String.Empty;
                        }

                        expressionElements.Add(new ExpressionElement(expression.Substring(1, rlp-1), level, index, joinOp, true, nestedUnary));
                    }
                    // it's an expression starting with stuff outside parens
                    else
                    {
                        // Do this to deal with trailing parenthetical expression possibly having a leading unary
                        int lastDigitIndex = expression.Substring(0, flp - 1)
                            .LastIndexOfAny(_expressionConfig.DigitChars);
                        string leftExpression = expression.Substring(0, lastDigitIndex+1);
                        joinOp = expression.Substring(lastDigitIndex+1, 1);
                        whatsLeft = expression.Substring(lastDigitIndex+2);
                        CheckParens(whatsLeft, level, "ending with paren");
                        expressionElements.Add(new ExpressionElement(leftExpression, level, index, joinOp));
                    }
                }
                index++;
            }
            return expressionElements;
        }

        /// <summary>
        /// For a given expression, enusre it still has balanced parentheses
        /// This is used to short-circuit recursive processing should things go wrong
        /// If test fails, the level and tag params are added the exception message to aid debugging
        /// </summary>
        /// <param name="expression">The expresion to check</param>
        /// <param name="level">Its nesting level</param>
        /// <param name="tag">The current processing step</param>
        private  void CheckParens(string expression, int level, string tag)
        {
            // Belt & braces reality check in case processing goes wrong ...
            // (To ensure short-circuit because we're in recursive processing)
            if (!_expressionValidator.CheckForBalancedParentheses.IsValid(expression))
            {
                throw new Exception($"FindExpressionsAtSameLevel [{tag}] exception: {expression}, level = {level}");
            }
        }

        private  int FindMatchingParen(string expression)
        {
            int nesting = 0;
            var rlpIndex = 0;

            while (rlpIndex < expression.Length)
            {
                if (expression[rlpIndex] == '(')
                {
                    nesting++;
                }
                else if (expression[rlpIndex] == ')')
                {
                    nesting--;
                    if (nesting == 0)
                        return rlpIndex;
                }
                rlpIndex++;
            }
            // This will only happen if validation is broken!
            throw new  Exception("Error finding matching paren in expression: " + expression);
        }

        // Reduce helpers ...
        public  string Evaluate(ExpressionElement reducedExpressionElement)
        {
            return reducedExpressionElement.HadParens ? Compute(reducedExpressionElement) : reducedExpressionElement.ReducedExpression;
        }

        /// <summary>
        /// Compute the numeric value of the expression
        /// </summary>
        /// <param name="reducedExpressionElement"></param>
        /// <returns>The value of the expression element</returns>
        public  string Compute(ExpressionElement reducedExpressionElement)
        {
            string expression = reducedExpressionElement.ReducedExpression;
            
            // break up into numbers and operators
            // (a naive alternative to using something like Polish notation
            string[] operators = _expressionConfig.ExtractOperators(expression);
            string[] numbers = _expressionConfig.ExtractNumerands(expression);

            // if 1st op is a leading unary, apply it and discard
            if (_expressionConfig.UnaryOpChars.Contains(expression[0]))
            {
                numbers[0] = _expressionConfig.UnaryOperations[expression[0].ToString()](numbers[0]);
                operators = operators.Skip(1).ToArray();
            }
            
            // This handles case where input is a simple number  
            if (numbers.Length == 1)
                return numbers[0];

            foreach (char[] opersToProcess in _expressionConfig.OperatorsByPrecedence)
            {
                while (operators.Any(t => t.IndexOfAny(opersToProcess) == 0))
                {
             
                    List<string> remainingOperators = new List<string>();
                    List<string> remainingNums= new List<string>();

                    for (int operIdx = 0; operIdx < operators.Length; operIdx++)
                    {   
                        // when we find something to process, mutate arrays and start over ....
                        // TODO Operator processing feels klunky                     
                        if (operators[operIdx].IndexOfAny(opersToProcess) == 0)
                        {
                            var computedValue = Compute(numbers[operIdx], numbers[operIdx + 1], operators[operIdx]);
                            remainingOperators.AddRange(operators.Skip(operIdx+1));
                            remainingNums.Add(computedValue);
                            remainingNums.AddRange(numbers.Skip(operIdx+2));
                            break;
                        }
                        remainingOperators.Add(operators[operIdx]);
                        remainingNums.Add(numbers[operIdx]);
                    }

                    operators = remainingOperators.ToArray();
                    numbers = remainingNums.ToArray();
                }
            }

            if (numbers.Length != 1 && operators.Length != 0)
                throw new Exception($"Error during compute: {expression}, numbers: {numbers.Length}; operators: {operators.Length}");

            return reducedExpressionElement.Unary.HasValue ?  ComputeUnary(reducedExpressionElement.Unary.ToString(), numbers[0]) : numbers[0];
        }

        private  string Compute(string leftHandNumber, string rightHandNumber, string operand)
        {
            // assume trailing unary -  [prior processing should assure this!]
            if (operand.Length == 2)
            {
                rightHandNumber = ComputeUnary(operand.Substring(1,1), rightHandNumber);
                operand = operand.Substring(0,1);
            }

            return _expressionConfig.BinaryOperations[operand](leftHandNumber, rightHandNumber);
        }

        private string ComputeUnary(string op, string number)
        {
            return _expressionConfig.UnaryOperations[op](number);
        }

    }
}
