using Bunit;
using LommeregneApp.Pages;

namespace LommeregnerTest.MultiplyTest
{
    public class MultiplyTest : TestContext
    {
        [Fact]
        public void Test()
        {
            // Arrange
            var calcComponent = RenderComponent<Lommeregner>();

            calcComponent.Find("#num1").MarkupMatches("<input id=\"num1\" placeholder=\"Enter First Number\" >");
            calcComponent.Find("#num2").MarkupMatches("<input id=\"num2\" placeholder=\"Enter Second Number\" >");
            calcComponent.Find("#finalresult").MarkupMatches("<input id=\"finalresult\" readonly >");

            var num1 = calcComponent.Find("#num1");
            var num2 = calcComponent.Find("#num2");
            var finalresult = calcComponent.Find("#finalresult");
            var mulButton = calcComponent.Find("#MulBtn");

            // Act
            num1.Change("7");
            num2.Change("191");
            mulButton.Click();

            // Assert
            Assert.Equal("1337", finalresult.GetAttribute("value"));
        }
    }
}
