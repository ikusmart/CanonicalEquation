using System;
using CanonicalEquation.Exceptions;
using CanonicalEquation.Parsers;
using Shouldly;
using Xunit;

namespace CanonicalEquation.Tests.ParserTests
{
    public class BracketsParserTests
    {
        [Fact]
        public void ParseMethod_NullString_ThrowNullArgumentException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var parseResult = BracketsParser.Parse(null);
            });
        }

        [Fact]
        public void ParseMethod_EmptyString_ThrowNullArgumentException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var parseResult = BracketsParser.Parse("");
            });
        }

        [Fact]
        public void ParseMethod_OnlyWhiteSpaceString_ThrowNullArgumentException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var parseResult = BracketsParser.Parse("   ");
            });
        }

        [Theory]
        [InlineData("(yx^2)", "x^2y")]
        [InlineData("((x+2)x+23)", "x(x+2)+23")]
        [InlineData("(yxz)", "xyz")]
        [InlineData("(1-2-3-4-5)", "1-2-3-4-5")]
        public void Parse_ValidData_ReturnBracketsObject(string initialBracketsString, string expectedBracketsString)
        {
            Brackets parseResult = null;
            Should.NotThrow(() =>
            {
                parseResult = BracketsParser.Parse(initialBracketsString);
            });

            parseResult.ShouldNotBeNull();
            parseResult.Polynomial.ToString().ShouldBe(expectedBracketsString);
        }

        [Theory]
        [InlineData("(yx^2")]
        [InlineData("(yx^2-as))")]
        [InlineData("yx^2-as")]
        [InlineData("y(x^2)-as")]
        [InlineData("-1")]
        [InlineData("x")]
        [InlineData("1(1+s)")]
        public void Parse_NotValidBracketsString_ThrowNotValidBracketArgumentException(string initialBracketsString)
        {
            Brackets parseResult = null;
            Should.Throw<NotValidBracketArgumentException>(() =>
            {
                parseResult = BracketsParser.Parse(initialBracketsString);
            });

        }
    }
}