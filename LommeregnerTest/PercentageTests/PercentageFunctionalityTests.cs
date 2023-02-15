using FluentAssertions;
using LommeregneApp.Features;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace LommeregnerTest
{
  public class PercentageFunctionalityTests
  {
    private readonly ILogger<AdvancedOperations> _logger = Substitute.For<ILogger<AdvancedOperations>>();
    private readonly IAdvancedOperations _sut;

    public PercentageFunctionalityTests()
    {
      _sut = new AdvancedOperations(_logger);
    }

    [Fact]
    public void PercentageFunctionality_ShouldReturnExpectedResult_GivenTwoValidInputs()
    {
      // Arrange
      var num1 = 80;
      var num2 = 25;
      var expected = 20;

      // Act
      var result = _sut.Percent(num1, num2);

      // Assert
      result.Should().Be(expected);
    }

    [Fact]
    public void PercentageFunctionality_ShouldLogPercentage_WhenGivenValidInput()
    {
      // Arrange

      // Act
      var result = _sut.Percent(default, default);

      // Assert
      _logger.Received().LogInformation("Percentage");
    }
  }
}
