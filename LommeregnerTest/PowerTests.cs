using Bunit;
using LommeregneApp.Pages;
using System.Reflection;

namespace LommeregnerTest
{
  public class PowerTests : TestContext
  {
    [Fact]
    public void PowerHtml_ShouldReturnExpectedResult_WhenGivenTwoValidInpits()
    {
      // Arrange
      var counterComponent = RenderComponent<Lommeregner>();

      counterComponent.Find("#num1").MarkupMatches("<input id=\"num1\" placeholder=\"Enter First Number\" >");
      counterComponent.Find("#num2").MarkupMatches("<input id=\"num2\" placeholder=\"Enter Second Number\" >");
      counterComponent.Find("#finalresult").MarkupMatches("<input id=\"finalresult\" readonly >");

      var num1 = counterComponent.Find("#num1");
      var num2 = counterComponent.Find("#num2");
      var finalresult = counterComponent.Find("#finalresult");
      var addButton = counterComponent.Find("#PowBtn");

      // Act
      num1.Change("2");
      num2.Change("2");
      addButton.Click();

      //Asser

      Assert.Equal("4", finalresult.GetAttribute("value"));
    }

    [Fact]
    public void PowerFunctionality_ShouldReturnExpectedResult_WhenGivenTwoValidInputs()
    {
      // Arrange
      var sut = new Lommeregner();
      sut.GetType().GetField("num1", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)?.SetValue(sut, "2");
      sut.GetType().GetField("num2", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)?.SetValue(sut, "2");
      var methodInfo = sut.GetType().GetMethod("PowNumbers", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
      var expected = "4";

      // Act
      methodInfo?.Invoke(sut, null);
      var result = sut.GetType().GetField("finalresult", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)?.GetValue(sut);

      //Asser
      Assert.Equal(expected, result);
    }

    [Fact]
    public async Task PowerFunctionality_ShouldThrowException_WhenGivenTwoInvalidInputs()
    {
      // Arrange
      var counterComponent = RenderComponent<Lommeregner>();

      counterComponent.Find("#num1").MarkupMatches("<input id=\"num1\" placeholder=\"Enter First Number\" >");
      counterComponent.Find("#num2").MarkupMatches("<input id=\"num2\" placeholder=\"Enter Second Number\" >");
      counterComponent.Find("#finalresult").MarkupMatches("<input id=\"finalresult\" readonly >");

      var num1 = counterComponent.Find("#num1");
      var num2 = counterComponent.Find("#num2");
      var finalresult = counterComponent.Find("#finalresult");
      var addButton = counterComponent.Find("#PowBtn");

      // Act
      num1.Change("InvalidInput");
      num2.Change("InvalidInput");

      //Asser

      Assert.Throws<FormatException>(() => addButton.Click());
    }
  }
}
