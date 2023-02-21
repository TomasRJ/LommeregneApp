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

    public static IEnumerable<object[]> TestsData => new List<object[]>
    {
      new object[] {4, 2},
      new object[] {81, 9},
      new object[] {36, 6},
      new object[] {100, 10},
      new object[] {25, 5},
    };

    public SquareRootFunctionalityTests()
    {
      _sut = new AdvancedOperations(_logger);
    }

    [Theory]
    [MemberData(nameof(TestsData))]
    public void SquareRootFunctionality_ShouldReturnExpectedResult_GivenTwoValidInputs(int num1, int expected)
    {
      // Arrange

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
