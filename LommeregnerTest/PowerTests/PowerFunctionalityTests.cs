using FluentAssertions;
using LommeregneApp.Features;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace LommeregnerTest
{
  public class PowerFunctionalityTests
  {
    private readonly ILogger<AdvancedOperations> _logger = Substitute.For<ILogger<AdvancedOperations>>();
    private readonly IAdvancedOperations _sut;

    public PowerFunctionalityTests()
    {
      _sut = new AdvancedOperations(_logger);
    }

    [Fact]
    public void PowerFunctionality_ShouldReturnExpectedResult_GivenTwoValidInputs()
    {
      // Arrange
      var num1 = 2;
      var num2 = 2;
      var expected = 4;

      // Act
      var result = _sut.Pow(num1, num2);

      // Assert
      result.Should().Be(expected);
    }

    [Fact]
    public void PowerFunctionality_ShouldLogPower_WhenGivenValidInput()
    {
      // Arrange

      // Act
      var result = _sut.Pow(default, default);

      // Assert
      _logger.Received().LogInformation("Power");
    }
  }
}
