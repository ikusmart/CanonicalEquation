using System;
using System.Collections.Generic;
using System.Threading;
using CanonicalEquation.Parsers;
using Shouldly;
using Xunit;
using Xunit.Extensions;

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

        public static IEnumerable<object[]> ValidVariableTestCases =>
            new[]
            {
                new object[] { "x^2y^2", 1, 2, new List<Variable>{ new Variable('x',2), new Variable('y', 2) }},
                new object[] { "+7.4z^4wy^6", 7.4, 3, new List<Variable>{ new Variable('z',4), new Variable('w'), new Variable('y', 6) }},
                new object[] { "-a^5b^2c", -1, 3, new List<Variable>{ new Variable('a',5), new Variable('b', 2), new Variable('c') }},
                new object[] { "-0.001c", -0.001, 1, new List<Variable>{ new Variable('c') }}
            };

        [Theory, MemberData(nameof(ValidVariableTestCases))]
        public void ParseMethod_ValidVariableString_ReturnVariables(string variableString, float multiplier, int countVariables, IList<Variable> expectedVariables)
        {
            Summand parseResult = null;

            Should.NotThrow(() =>
            {
                 parseResult = SummandParser.Parse(variableString);
            });

            parseResult.ShouldNotBeNull();
            parseResult.Multiplier.ShouldBe(multiplier);
            parseResult.Variables.Count.ShouldBe(countVariables);

            foreach (var variable in parseResult.Variables)
            {
                expectedVariables.ShouldContain(x => x.Name == variable.Name && x.Power == variable.Power);
            }
        }
    }
}