using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Rtwsq.Thom.Calculator
{
    public class ExpressionElement
    {
        /// <summary>
        /// Hold a piece of the expression at a given level of decomposition
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="level"></param>
        /// <param name="index"></param>
        /// <param name="joinOp"></param>
        /// <param name="hadParens"></param>
        /// <param name="unary"></param>
        public ExpressionElement(string expression, int level, int index, string joinOp = "", bool hadParens = false, char? unary = null)
        {
            if (unary <= 0) throw new ArgumentOutOfRangeException(nameof(unary));
            Expression = expression;
            Level = level;
            Index = index;
            JoinOp = joinOp;
            HadParens = hadParens;
            Unary = unary;
            ReducedExpression = string.Empty;
        }

        /// <summary>
        /// Its parenthetical nesting level
        /// </summary>
        public int Level { get; }
        
        /// <summary>
        /// Its relative position in the larger, same-level expression it came from
        /// </summary>
        public int Index { get;  }                 

        /// <summary>
        /// The operator that joins it to the next expression at this level (or an empty string if there is none) 
        /// </summary>
        public string JoinOp { get;  }

        /// <summary>
        /// Whether or not the whole expression was in parentheses before it was processed
        /// (if it was, during reduce processing, it will be computed before it is combined
        /// </summary>
        public bool HadParens { get; }

        /// <summary>
        /// The whole expression with its outer parentheses (if any), removed
        /// </summary>
        public string Expression { get; }

        /// <summary>
        /// The reduced form of the expression derived during reduce processing
        /// </summary>
        public string ReducedExpression { get; set; }
        /// <summary>
        /// The unary operator (if any) to apply to the whole thing if it was in a parentheses
        /// </summary>
        public char? Unary { get;  }               

        private ExpressionElement[] _subExpressionElements;

        public void PopulateSubExpressionElements(IEnumerable<ExpressionElement> childElements)
        {
            _subExpressionElements = childElements.ToArray();
        }
        public IEnumerable<ExpressionElement> SubExpressionElements => _subExpressionElements;
        //or empty if expression has no inner parens)

        public bool HasChildren()
        {
            return _subExpressionElements != null && _subExpressionElements.Length > 0;
        }

    }
}
