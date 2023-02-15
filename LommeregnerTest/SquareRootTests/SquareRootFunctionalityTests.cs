using FluentAssertions;
using LommeregneApp.Features;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace LommeregnerTest
{
  public class SquareRootFunctionalityTests
  {
    private readonly ILogger<AdvancedOperations> _logger = Substitute.For<ILogger<AdvancedOperations>>();
    private readonly IAdvancedOperations _sut;

    public SquareRootFunctionalityTests()
    {
      _sut = new AdvancedOperations(_logger);
    }

    [Fact]
    public void SquareRootFunctionality_ShouldReturnExpectedResult_GivenTwoValidInputs()
    {
      // Arrange
      var num1 = 4;
      var expected = 2;

      // Act
      var result = _sut.SquareRoot(num1);

      // Assert
      result.Should().Be(expected);
    }

    [Fact]
    public void SquareRootFunctionality_ShouldLogPower_WhenGivenValidInput()
    {
      // Arrange

      // Act
      var result = _sut.SquareRoot(default);

      // Assert
      _logger.Received().LogInformation("Square Root");
    }
  }
}
