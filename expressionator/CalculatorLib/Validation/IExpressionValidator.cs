using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Rtwsq.Thom.Calculator.Validation
{
    public interface IExpressionValidator
    {
        ExpressionValidationResult Validate(string expression);
        IValidatonCheck CheckForBalancedParentheses { get; }
    }
}
