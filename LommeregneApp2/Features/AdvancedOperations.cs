namespace LommeregneApp.Features
{

  public class AdvancedOperations : IAdvancedOperations
  {
    private readonly ILogger<AdvancedOperations> _logger;

    public AdvancedOperations(ILogger<AdvancedOperations> logger)
    {
      _logger = logger;
    }

    public double Percent(double num1, double num2)
    {
      _logger.LogInformation("Percentage");
      return (num1 / 100) * num2;
    }

    public double Pow(double num1, double num2)
    {
      _logger.LogInformation("Power");
      return Math.Pow(num1, num2);
    }
    public double SquareRoot(double num1)
    {
      _logger.LogInformation("Square Root");
      return Math.Sqrt(num1);
    }
  }
}
