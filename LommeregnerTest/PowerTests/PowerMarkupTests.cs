using Bunit;
using FluentAssertions;
using LommeregneApp.Features;
using LommeregneApp.Pages;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace LommeregnerTest
{
  public class PowerMarkupTests : TestContext
  {
    private readonly IAdvancedOperations _advancedOperations = Substitute.For<IAdvancedOperations>();

    [Fact]
    public void PowNumbers_ShouldReturnExpectedResult_WhenGivenTwoValidInpits()
    {
      // Arrange
      Services.AddSingleton(typeof(IAdvancedOperations), _advancedOperations);
      var counterComponent = RenderComponent<Lommeregner>();

      counterComponent.Find("#num1").MarkupMatches("<input id=\"num1\" placeholder=\"Enter First Number\" >");
      counterComponent.Find("#num2").MarkupMatches("<input id=\"num2\" placeholder=\"Enter Second Number\" >");
      counterComponent.Find("#finalresult").MarkupMatches("<input id=\"finalresult\" readonly >");

      var num1 = counterComponent.Find("#num1");
      var num2 = counterComponent.Find("#num2");
      var finalresult = counterComponent.Find("#finalresult");
      var powBtn = counterComponent.Find("#PowBtn");

      _advancedOperations.Pow(default, default).ReturnsForAnyArgs(4);
      var expected = "4";

      // Act
      num1.Change("2");
      num2.Change("2");
      powBtn.Click();

      //Assert
      var result = finalresult.GetAttribute("value");
      result.Should().Be(expected);
    }

    [Fact]
    public async Task PowNumbers_ShouldThrowException_WhenGivenTwoInvalidInputs()
    {
      // Arrange
      Services.AddSingleton(typeof(IAdvancedOperations), _advancedOperations);
      var counterComponent = RenderComponent<Lommeregner>();

      counterComponent.Find("#num1").MarkupMatches("<input id=\"num1\" placeholder=\"Enter First Number\" >");
      counterComponent.Find("#num2").MarkupMatches("<input id=\"num2\" placeholder=\"Enter Second Number\" >");

      var num1 = counterComponent.Find("#num1");
      var num2 = counterComponent.Find("#num2");
      var powBtn = counterComponent.Find("#PowBtn");
      
      // Act
      num1.Change("InvalidInput");
      num2.Change("InvalidInput");

      //Assert
      var result = () => powBtn.Click();
      result.Should().Throw<FormatException>();
    }


    [Fact]
    public void PowNumbers_ShouldCallAdvancedOperationsPow_WhenGivenTwoValidInpits()
    {
      // Arrange
      Services.AddSingleton(typeof(IAdvancedOperations), _advancedOperations);
      var counterComponent = RenderComponent<Lommeregner>();
      counterComponent.Find("#finalresult").MarkupMatches("<input id=\"finalresult\" readonly >");

      var powBtn = counterComponent.Find("#PowBtn");

      // Act
      powBtn.Click();

      //Assert
      _advancedOperations.Received().Pow(Arg.Any<double>(), Arg.Any<double>());
    }
  }
}
