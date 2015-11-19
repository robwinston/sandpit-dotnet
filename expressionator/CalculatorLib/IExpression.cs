using System;
using System.Collections.Generic;

namespace Com.Rtwsq.Thom.Calculator
{
    public interface IExpression
    {
        string RawExpression { get; }
        string MinifiedExpression { get; }
        ExpressionElement ProcessedExpression { get; }
        IEnumerable<string> ValidationErrors { get; }
        Exception ProcessingException { get; }
        bool IsValid { get; }
        bool HasValue { get; }
        string ItsValue { get; }
        bool HasErrors { get; }
        string ErrorMessages { get; }
        string Result { get; }
    }
}