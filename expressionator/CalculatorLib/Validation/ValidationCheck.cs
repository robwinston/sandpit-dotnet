using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Rtwsq.Thom.Calculator.Validation
{
    public class ValidationCheck : IValidatonCheck
    {
        public ValidationCheck(Predicate<string> validator, string errorMessage)
        {
            _validator = validator;
            ErrorMessage = errorMessage;
        }

        private readonly Predicate<string> _validator;

        public string ErrorMessage { get; }

        public bool IsValid(string expression)
        {
            return _validator(expression);
        }
    }
}
