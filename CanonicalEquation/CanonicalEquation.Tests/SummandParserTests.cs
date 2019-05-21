using System;
using System.Threading;
using Shouldly;
using Xunit;

namespace CanonicalEquation.Tests
{
    public class SummandParserTests
    {
        [Fact]
        public void ParseMethod_NullString_ThrowNullArgumentException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var parseResult = SummandParser.Parse(null);
            });
        }
        [Fact]
        public void ParseMethod_EmptyString_ThrowNullArgumentException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var parseResult = SummandParser.Parse("");
            });
        }
        [Fact]
        public void ParseMethod_WhiteSpaceString_ThrowNullArgumentException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var parseResult = SummandParser.Parse("    ");
            });
        }

        [Theory]
        [InlineData("x^")]
        [InlineData("^2")]
        [InlineData("^")]
        [InlineData(",")]
        [InlineData("xx^x")]
        [InlineData("x^2.4")]
        [InlineData("3^2")]
        [InlineData("-3^x")]
        [InlineData("x^s")]
        [InlineData("+^2")]
        [InlineData("=x^2")]
        [InlineData("x^2y^")]
        [InlineData("x^2.4x^2")]
        [InlineData("x^2.x^2")]
        public void ParseMethod_NotValidVariableString_FormatException(string variableString)
        {
            Should.Throw<FormatException>(() =>
            {
                var parseResult = SummandParser.Parse(variableString);
            });
        }

        [Theory]
        [InlineData("x^2y^2", 1, 2)]
        [InlineData("+7.4z^2wy^2", 7.4, 3)]
        [InlineData("-a^5b^2c", -1, 3)]
        [InlineData("-0.001c", -0.001, 1)]
        public void ParseMethod_ValidVariableString_ReturnVariables(string variableString, float multiplier, int countVariables)
        {
            Summand parseResult = null;

            Should.NotThrow(() =>
            {
                 parseResult = SummandParser.Parse(variableString);
            });

            parseResult.ShouldNotBeNull();
            parseResult.Multiplier.ShouldBe(multiplier);
            parseResult.Variables.Count.ShouldBe(countVariables);
        }
    }
}