using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Com.Rtwsq.Thom.Calculator.Validation
{
    public class ExpressionValidationResult
    {
        /// <summary>
        /// The output of the IExpressionValidator Validate method
        /// 
        /// </summary>
        /// <param name="expression"></param>
        public ExpressionValidationResult(string expression)
        {
            _originalExpression = expression;
        }

        readonly string _originalExpression;
        /// <summary>
        /// Validated expression with all white space removed
        /// </summary>
        public string MinifiedExpression { get; set; }

        readonly List<string> _validationErrors = new List<string>();       
        /// <summary>
        /// Error message (if any) produced during validation0
        /// </summary>
        public IEnumerable<string> ValidationErrors => _validationErrors;

        /// <summary>
        /// Whether or not there are any ValidationErrors
        /// </summary>
        public bool HasValidationErrors => _validationErrors.Count > 0;
        
        /// <summary>
        /// Add a Validation error message to ValidationErrors
        /// </summary>
        /// <param name="message"></param>
        public void AddValidationError(string message)
        {
            _validationErrors.Add(message);
        }
    }
}
