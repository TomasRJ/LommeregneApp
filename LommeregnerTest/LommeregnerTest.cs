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

            calcComponent.Find("#num1").MarkupMatches("<input id=\"num1\" placeholder=\"Enter First Number\" >");
            calcComponent.Find("#num2").MarkupMatches("<input id=\"num2\" placeholder=\"Enter Second Number\" >");
            calcComponent.Find("#finalresult").MarkupMatches("<input id=\"finalresult\" readonly >");

            var num1 = calcComponent.Find("#num1");
            var num2 = calcComponent.Find("#num2");
            var finalresult = calcComponent.Find("#finalresult");
            var addButton = calcComponent.Find("#AddBtn");


            // Act
            num1.Change("2");
            num2.Change("3");
            addButton.Click();

            // Assert
            Assert.Equal("5", finalresult.GetAttribute("value"));
        }
    }
}
