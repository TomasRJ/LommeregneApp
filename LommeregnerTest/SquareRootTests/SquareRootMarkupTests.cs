using Bunit;
using FluentAssertions;
using LommeregneApp.Features;
using LommeregneApp.Pages;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace LommeregnerTest.SquareRootTests
{
  public class SquareRootMarkupTests : TestContext
  {
    private readonly IAdvancedOperations _advancedOperations = Substitute.For<IAdvancedOperations>();

    [Fact]
    public void SqrtNumber_ShouldReturnExpectedResult_WhenGivenValidInput()
    {
      // Arrange
      Services.AddSingleton(typeof(IAdvancedOperations), _advancedOperations);
      var counterComponent = RenderComponent<Lommeregner>();

      counterComponent.Find("#num1").MarkupMatches("<input id=\"num1\" placeholder=\"Enter First Number\" >");
      counterComponent.Find("#finalresult").MarkupMatches("<input id=\"finalresult\" readonly >");

      var num1 = counterComponent.Find("#num1");
      var finalresult = counterComponent.Find("#finalresult");
      var sqrtBtn = counterComponent.Find("#SqrtBtn");

      _advancedOperations.SquareRoot(default).ReturnsForAnyArgs(2);
      var expected = "2";

      // Act
      num1.Change("2");
      sqrtBtn.Click();

      //Assert
      var result = finalresult.GetAttribute("value");
      result.Should().Be(expected);
    }

    [Fact]
    public async Task SqrtNumber_ShouldThrowException_WhenGivenInvalidInput()
    {
      // Arrange
      Services.AddSingleton(typeof(IAdvancedOperations), _advancedOperations);
      var counterComponent = RenderComponent<Lommeregner>();

      counterComponent.Find("#num1").MarkupMatches("<input id=\"num1\" placeholder=\"Enter First Number\" >");

      var num1 = counterComponent.Find("#num1");
      var sqrtBtn = counterComponent.Find("#SqrtBtn");

      // Act
      num1.Change("InvalidInput");

      //Assert
      var result = () => sqrtBtn.Click();
      result.Should().Throw<FormatException>();
    }


    [Fact]
    public void SqrtNumber_ShouldCallAdvancedOperationsPow_WhenGivenTwoValidInpits()
    {
      // Arrange
      Services.AddSingleton(typeof(IAdvancedOperations), _advancedOperations);
      var counterComponent = RenderComponent<Lommeregner>();

      var sqrtBtn = counterComponent.Find("#SqrtBtn");

      _advancedOperations.SquareRoot(default).ReturnsForAnyArgs(4);

      // Act
      sqrtBtn.Click();

      //Assert
      _advancedOperations.Received().SquareRoot(Arg.Any<double>());
    }

    [Fact]
    public async Task MarkupForNum2_ShouldBeChangedBySqrtNumber_WhenSqrtNumberIsCalled()
    {
      // Arrange
      Services.AddSingleton(typeof(IAdvancedOperations), _advancedOperations);
      
      var counterComponent = RenderComponent<Lommeregner>();
      counterComponent.Find("#num2").MarkupMatches("<input id=\"num2\" placeholder=\"Enter Second Number\" >");
      
      var sqrtBtn = counterComponent.Find("#SqrtBtn");
      
      var expected = "2√";

      // Act
      sqrtBtn.Click();
      var num2 = counterComponent.Find("#num2").GetAttribute("value");

      //Assert
      num2.Should().Be(expected);
    }
  }
}
