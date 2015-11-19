namespace Com.Rtwsq.Thom.Calculator.Validation
{
    public interface IValidatonCheck
    {
        string ErrorMessage { get; }
        bool IsValid(string expression);
    }
}
