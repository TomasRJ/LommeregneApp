using FluentAssertions;
using LommeregneApp.Features;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Collections;

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

    [Theory]
    [ClassData(typeof(PowerTestData))]
    public void PowerFunctionality_ShouldReturnExpectedResult_GivenTwoValidInputs(int num1, int num2, int expected)
    {
      // Arrange


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

  public class PowerTestData : IEnumerable<object[]>
  {
    public IEnumerator<object[]> GetEnumerator()
    {
      yield return new object[] { 2, 2, 4 };
      yield return new object[] { 3, 5, 243 };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
