using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Rtwsq.Thom.Calculator.Validation;

namespace Com.Rtwsq.Thom.Calculator
{
    public abstract class ExpressionBase : IExpression
    {
        public string RawExpression { get; protected set; }
        public string MinifiedExpression => ExpressionValidationResult.MinifiedExpression;

        protected ExpressionElement ExpressionElement;
        public ExpressionElement ProcessedExpression => ExpressionElement;

        protected ExpressionValidationResult ExpressionValidationResult;

        public IEnumerable<string> ValidationErrors => ExpressionValidationResult.ValidationErrors;

        public Exception ProcessingException { get; protected set; }

        public bool IsValid => !ValidationErrors.Any();

        public bool HasValue => !string.IsNullOrEmpty(ItsValue);
        public string ItsValue { get; protected set; }

        public bool HasErrors => ProcessingException != null || ValidationErrors.Any();
        private string _errorMessages;
        public string ErrorMessages
        {
            get
            {
                if (!HasErrors)
                    return string.Empty;

                if (string.IsNullOrEmpty(_errorMessages))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    if (ProcessingException != null)
                    {
                        stringBuilder.AppendLine(ProcessingException.Message);
                        if (ProcessingException.InnerException != null)
                        {
                            stringBuilder.AppendLine(ProcessingException.InnerException.Message);
                        }
                    }
                    if (ValidationErrors.Any())
                    {
                        foreach (string validationError in ValidationErrors)
                        {
                            stringBuilder.AppendLine(validationError);
                        }
                    }
                    _errorMessages = stringBuilder.ToString();

                }
                return _errorMessages;
            }
        }

        public string Result => IsValid && HasValue ? ItsValue : ErrorMessages;

    }
}
