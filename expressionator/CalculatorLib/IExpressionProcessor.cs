namespace Com.Rtwsq.Thom.Calculator
{
    public interface IExpressionProcessor
    {
        ExpressionElement Process(string minifiedExpression);
        string Evaluate(ExpressionElement reducedExpressionElement);
        string Compute(ExpressionElement reducedExpressionElement);
    }
}