namespace WebApplication2.Models;

public abstract class MyResult { }

public class MyOkResult : MyResult
{
    public string Value { get; }
    
    public MyOkResult(string resultValue)
    {
        Value = resultValue;
    }
}

public class MyBadResult : MyResult
{
    public string ErrorMessage { get; }
    
    public MyBadResult(string message)
    {
        ErrorMessage = message;
    }
}
