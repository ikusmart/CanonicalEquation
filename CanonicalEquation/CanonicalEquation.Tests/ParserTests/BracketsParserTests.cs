using System;
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
    }
}