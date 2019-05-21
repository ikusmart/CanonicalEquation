using System;
using CanonicalEquation.Exceptions;
using Shouldly;
using Xunit;

namespace CanonicalEquation.Tests
{
    public class VariableParserTests
    {
        [Fact]
        public void ParseMethod_NullString_ThrowNullArgumentException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var parseResult = VariableParser.Parse(null);
            });
        }
        [Fact]
        public void ParseMethod_EmptyString_ThrowNullArgumentException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var parseResult = VariableParser.Parse("");
            });
        }
        [Fact]
        public void ParseMethod_WhiteSpaceString_ThrowNullArgumentException()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var parseResult = VariableParser.Parse("    ");
            });
        }

        [Theory]
        [InlineData("zz")]
        [InlineData("x^")]
        [InlineData("^2")]
        [InlineData("^")]
        [InlineData(",")]
        [InlineData("xx^x")]
        [InlineData("x^2.4")]
        [InlineData("3^2")]
        [InlineData("3^x")]
        [InlineData("x^s")]
        public void ParseMethod_NotValidVariablestring_FormatException(string variableString)
        {
            Should.Throw<FormatException>(() =>
            {
                var parseResult = VariableParser.Parse(variableString);
            });
        }


        [Theory]
        [InlineData(',', 2)]
        [InlineData(' ', 1)]
        [InlineData('!', 12)]
        [InlineData('4', 12)]
        public void Constructor_NotValidName_NotValidVariableArgumentException(char name, int power)
        {
            Variable parsedVariable = null;

            Should.Throw<NotValidVariableArgumentException>(() =>
            {
                parsedVariable = new Variable(name, power);
            });
        }

        [Theory]
        [InlineData("z^2", 'z', 2)]
        [InlineData("x", 'x', 1)]
        [InlineData("w^12", 'w', 12)]
        public void ParseMethod_ValidResult_ReturnProperties(string variableString, char name, int power)
        {
            Variable parsedVariable = null;

            Should.NotThrow(() =>
            {
                parsedVariable = VariableParser.Parse(variableString);
            });

            parsedVariable?.Name.ShouldBe(name);
            parsedVariable?.Power.ShouldBe(power);
        }

        [Theory]
        [InlineData('a', 2, "a^2")]
        [InlineData('b', 1, "b")]
        [InlineData('c', 12, "c^12")]
        public void ToString_ReturnExpectedString(char name, int power, string variableString)
        {
            Variable parsedVariable = null;

            Should.NotThrow(() =>
            {
                parsedVariable = new Variable(name, power);
            });

            parsedVariable?.ToString().ShouldBe(variableString);
        }
    }
}