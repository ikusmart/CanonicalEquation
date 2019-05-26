using CanonicalEquation.Parsers;
using Shouldly;
using Xunit;

namespace CanonicalEquation.Tests
{
    public class PolynomialTests
    {
        [Theory]
        [InlineData("x^2+ 83 + zxy", "x^2+83+xyz")]
        [InlineData("(ab-cd)(ab-1) - 1", "a^2b^2-ab-abcd+cd-1")]
        [InlineData("x + (x+1)", "2x+1")]
        [InlineData("(((1-2x^2y)))xy", "xy-2x^3y^2")]
        [InlineData("()()xy", "xy")]
        [InlineData("()", "0")]
        [InlineData("(0)(x-1)(x-1)-1", "-1")]
        public void ParseMethod_ValidInitialString_NotValidEquationArgumentException(
            string initialPolymial, string expectedPolymial)
        {
            var polymial = PolynomialParser.Parse(initialPolymial);

            polymial.ToString().ShouldBe(expectedPolymial);
        }
    }
}