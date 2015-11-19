using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Rtwsq.Thom.Calculator.Config
{
    public static class ExpressionConfigs
    {
        public static readonly IDictionary<ExpressionType, IExpressionConfig> ConfigsByType = new Dictionary<ExpressionType, IExpressionConfig>()
        {
            {ExpressionType.IntegerBasic, new IntegerArithmeticBasic()},
            {ExpressionType.IntegerExtended, new IntegerAritmeticExtended()},
            {ExpressionType.DecimalBasic, new DecimalArithmeticBasic()},
            {ExpressionType.DecimalExtended, new DecimalArithmeticExtended()}
        };

        public static IExpressionConfig ConfigForType(ExpressionType expressionType)
        {
            if (ConfigsByType.ContainsKey(expressionType)) return ConfigsByType[expressionType];
            throw new InvalidOperationException($"Unsupported expression type: {expressionType}");
        }
    }
}
