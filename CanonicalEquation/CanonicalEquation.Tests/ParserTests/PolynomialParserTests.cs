using System;
using CanonicalEquation.Parsers;
using Shouldly;
using Xunit;

namespace CanonicalEquation.Tests.ParserTests
{
    public class PolynomialParserTests
    {
        [Fact]
        public void ParseMethod_NullString_ThrowNullArgumentException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var parseResult = PolynomialParser.Parse(null);
            });
        }

        [Fact]
        public void ParseMethod_EmptyString_ThrowNullArgumentException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var parseResult = PolynomialParser.Parse("");
            });
        }

        [Fact]
        public void ParseMethod_OnlyWhiteSpaceString_ThrowNullArgumentException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var parseResult = PolynomialParser.Parse("   ");
            });
        }
    }
}