using Bunit;
using FluentAssertions;
using LommeregneApp.Features;
using LommeregneApp.Pages;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace LommeregnerTest.PercentageTests
{
  public class PercentageMarkupTests : TestContext
  {
    private readonly IAdvancedOperations _advancedOperations = Substitute.For<IAdvancedOperations>();

    [Fact]
    public void PctNumbers_ShouldReturnExpectedResult_WhenGivenTwoValidInpits()
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
      var pctBtn = counterComponent.Find("#PctBtn");

      _advancedOperations.Percent(default, default).ReturnsForAnyArgs(20);
      var expected = "20";

      // Act
      num1.Change("80");
      num2.Change("25");
      pctBtn.Click();

      //Assert
      var result = finalresult.GetAttribute("value");
      result.Should().Be(expected);
    }

    [Fact]
    public async Task PctNumbers_ShouldThrowException_WhenGivenTwoInvalidInputs()
    {
      // Arrange
      Services.AddSingleton(typeof(IAdvancedOperations), _advancedOperations);
      var counterComponent = RenderComponent<Lommeregner>();

      counterComponent.Find("#num1").MarkupMatches("<input id=\"num1\" placeholder=\"Enter First Number\" >");
      counterComponent.Find("#num2").MarkupMatches("<input id=\"num2\" placeholder=\"Enter Second Number\" >");

      var num1 = counterComponent.Find("#num1");
      var num2 = counterComponent.Find("#num2");
      var pctBtn = counterComponent.Find("#PctBtn");

      // Act
      num1.Change("InvalidInput");
      num2.Change("InvalidInput");

      //Assert
      var result = () => pctBtn.Click();
      result.Should().Throw<FormatException>();
    }


    [Fact]
    public void PctNumbers_ShouldCallAdvancedOperationsPow_WhenGivenTwoValidInpits()
    {
      // Arrange
      Services.AddSingleton(typeof(IAdvancedOperations), _advancedOperations);
      var counterComponent = RenderComponent<Lommeregner>();

      var pctBtn = counterComponent.Find("#PctBtn");

      // Act
      pctBtn.Click();

      //Assert
      _advancedOperations.Received().Percent(Arg.Any<double>(), Arg.Any<double>());
    }
  }
}
