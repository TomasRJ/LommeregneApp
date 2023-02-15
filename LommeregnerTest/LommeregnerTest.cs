using Bunit;
using LommeregneApp.Features;
using LommeregneApp.Pages;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace LommeregnerTest
{
  public class LommeregnerTest : TestContext
  {
    private readonly IAdvancedOperations _advancedOperations = Substitute.For<IAdvancedOperations>();

    [Fact]
    public void TestCounter()
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
      var addButton = counterComponent.Find("#AddBtn");


      // Act
      num1.Change("2");
      num2.Change("3");
      addButton.Click();

      //Asser

      Assert.Equal("5", finalresult.GetAttribute("value"));
    }
  }
}
