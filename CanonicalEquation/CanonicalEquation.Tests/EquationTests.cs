using System;
using CanonicalEquation.Exceptions;
using CanonicalEquation.Interfaces;
using CanonicalEquation.Parsers;
using Shouldly;
using Xunit;

namespace CanonicalEquation.Tests
{
    public class EquationTests
    {
        [Fact]
        public void ParseMethod_NullString_ThrowNullArgumentException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var parseResult = EquationParser.Parse(null);
            });
        }

        [Fact]
        public void ParseMethod_EmptyString_ThrowNullArgumentException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var parseResult = EquationParser.Parse("");
            });
        }
        
        [Fact]
        public void ParseMethod_OnlyWhiteSpaceString_ThrowNullArgumentException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var parseResult = EquationParser.Parse("   ");
            });
        }

        [Theory]
        [InlineData("qwe")]
        [InlineData("123=")]
        [InlineData("=")]
        [InlineData("=0")]
        [InlineData("==0")]
        public void ParseMethod_NotValidEqualsSymbol_NotValidEquationArgumentException(string equationString)
        {
            Should.Throw<NotValidEquationArgumentException>(() =>
            {
                var parseResult = EquationParser.Parse(equationString);
            });
        }

        [Theory]
        [InlineData("x^2=x", "x^2", "x")]
        [InlineData("x=y", "x", "y")]
        [InlineData("x^2+ 83 + zxy = 4 - ds+2", "x^2+xyz+83", "-ds+6")]
        [InlineData("x^2+ x(a+b)- x^1 = (ab-cd)(ab-1) - 1", "x^2+ax+bx-x", "a^2b^2-abcd-ab+cd-1")]
        public void ParseMethod_ValidInitialString_NotValidEquationArgumentException(
            string equationString, string leftPolynomial, string rightPolynomial)
        {
            var equation = EquationParser.Parse(equationString);

            equation.Left.ToString().ShouldBe(leftPolynomial);
            equation.Right.ToString().ShouldBe(rightPolynomial);
        }
    }
}
