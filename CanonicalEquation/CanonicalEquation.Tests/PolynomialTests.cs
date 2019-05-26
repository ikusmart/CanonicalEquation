using CanonicalEquation.Parsers;
using Shouldly;
using Xunit;

namespace CanonicalEquation.Tests
{
    public class PolynomialTests
    {
        [Theory]
        [InlineData("x^2+ 83 + zxy", "x^2+xyz+83")]
        [InlineData("(ab-cd)(ab-1) - 1", "a^2b^2-abcd-ab+cd-1")]
        [InlineData("x + (x+1)", "2x+1")]
        [InlineData("(((1-2x^2y)))xy", "-2x^3y^2+xy")]
        [InlineData("()()xy", "xy")]
        [InlineData("()", "0")]
        [InlineData("-(-(-(1)))", "-1")]
        [InlineData("(0)(x-1)(x-1)-1", "-1")]
        [InlineData("-8.7a- 5xy(4.1x-3z(x-3)(x-3)  (x   - 3) ) + 3.2x^0- a^1", "15x^4yz-135x^3yz+405x^2yz-20.5x^2y-405xyz-9.7a+3.2")]
        public void ParseMethod_ValidInitialString_NotValidEquationArgumentException(
            string initialPolymial, string expectedPolymial)
        {
            var polymial = PolynomialParser.Parse(initialPolymial);

            polymial.ToString().ShouldBe(expectedPolymial);
        }
    }
}