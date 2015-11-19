using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Rtwsq.Thom.Calculator.Config;
using Com.Rtwsq.Thom.Calculator.Validation;

namespace Com.Rtwsq.Thom.Calculator
{
    public class ArithmeticExpression : ExpressionBase
    {
        // Captures and saves any exceptions during processing
        // This lets the caller decide if & when it wants to know
        // If anything went wrong, ItsValue will be null
        // In effect, this is a somewhat clumsy "Maybe" construct 
        //  ... possibly refactor to make it a proper one?
        //

        // The 'IExpressionConfig' is what controls what can be done ... so just a few steps away from it being truly pluggable 
        public static void SetExpressionType(ExpressionType expressionType)
        {
            ExpressionConfig = ExpressionConfigs.ConfigForType(expressionType);
        }

        public static string AllowedNumberTypes => ExpressionConfig.AllowedNumberTypes;
        public static string AllowedOps => ExpressionConfig.AllowedOperators;

        private static IExpressionConfig _expressionConfig;
        public static IExpressionConfig ExpressionConfig
        {
            get
            {
                if (_expressionConfig == null)
                    throw new InvalidOperationException("Unitialised ExpressionResources");
                return _expressionConfig;
            }
            set
            {
                _expressionConfig = value;
                _expressionValidator = new ExpressionValidator(_expressionConfig);
                _expressionProcessor = new ExpressionProcessor(_expressionConfig);
                
            }
        }

        private static IExpressionValidator _expressionValidator;
        public static IExpressionValidator ExpressionValidator
        {
            get
            {
                if (_expressionValidator == null)
                    throw new InvalidOperationException("Uninitialised ExpressionValidator");
                return _expressionValidator;
            }
        }
        //pri static readonly IExpressionProcessor ExpressionProcessor = new ExpressionProcessor(ExpressionResources);
        private static IExpressionProcessor _expressionProcessor;
        public static IExpressionProcessor ExpressionProcessor
        {
            get
            {
                if (_expressionProcessor == null)
                    throw new InvalidOperationException("Uninitialised ExpressionProcessor");
                return _expressionProcessor;
            }
        }

        public ArithmeticExpression(string expression) 
        {
            try
            {
                RawExpression = expression;
                ExpressionValidationResult = ExpressionValidator.Validate(expression);
                if (IsValid)
                {
                    ExpressionElement = ExpressionProcessor.Process(MinifiedExpression);
                    ItsValue = ExpressionProcessor.Compute(ExpressionElement);
                }
            }
            catch (Exception e)
            {
                ProcessingException = e;
            }
        }
    }
}
