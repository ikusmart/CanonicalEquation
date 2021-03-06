using System;
using System.Collections.Generic;
using System.Linq;
using CanonicalEquation.Lib.Exceptions;
using CanonicalEquation.Lib.Entities;
using CanonicalEquation.Lib.Parsers;
using Shouldly;
using Xunit;

namespace CanonicalEquation.Lib.Tests
{
    public class SummandTests
    {

        public static IEnumerable<object[]> ValidVariableTestCases =>
            new[]
            {
                new object[] { "yyxyzxxyzxyz", TestHelper.NewSummand(1, ('x', 4), ('y', 5), ('z', 3))},
                new object[] { "-2a^2b^1c^3cca", TestHelper.NewSummand(-2, ('a', 3), ('b', 1), ('c', 5))},
                new object[] { "546", TestHelper.NewSummand(546)},
                new object[] { "1.0002", TestHelper.NewSummand(1.0002f)},
                new object[] { "-121", TestHelper.NewSummand(-121)},
                new object[] { "-abcd", TestHelper.NewSummand(-1, ('a', 1), ('b', 1), ('c', 1), ('d', 1)) },
                new object[] { "+a^2", TestHelper.NewSummand(1, ('a', 2)) },
                new object[] { "1.1zzc", TestHelper.NewSummand(1.1f, ('z', 2), ('c', 1)) },
                new object[] { "-0abc", TestHelper.NewSummand(0) },
                new object[] { "-0", TestHelper.NewSummand(0) },
                new object[] { "-1.2xyza^2b^1c^7", TestHelper.NewSummand(-1.2f, ('c', 7), ('a', 2), ('b', 1), ('x', 1), ('y', 1), ('z', 1)) },
            };

        [Theory, MemberData(nameof(ValidVariableTestCases))]
        public void Normalize_CorrectSummand_ReturnNormalizedSummad(string initialSummandString, Summand expectedNormalizedSummand)
        {
            Summand actualSummand = null;

            Should.NotThrow(() => { actualSummand = SummandParser.Parse(initialSummandString).Normalize(); });

            actualSummand.ShouldNotBeNull();
            actualSummand.Multiplier.ShouldBe(expectedNormalizedSummand.Multiplier);

            actualSummand.Variables.Count().ShouldBe(expectedNormalizedSummand.Variables.Count());

            var sortedActualResult = actualSummand.Variables.OrderBy(x => x.Name).ThenBy(x => x.Power).ToArray();
            var sortedExpectedResult = expectedNormalizedSummand.Variables.OrderBy(x => x.Name).ThenBy(x => x.Power).ToArray();

            for (int i = 0; i < sortedActualResult.Length; i++)
            {
                var expectedValue = sortedExpectedResult[i];
                sortedActualResult[i].Name.ShouldBe(expectedValue.Name);
                sortedActualResult[i].Power.ShouldBe(expectedValue.Power);
            }
        }

        [Theory]
        [InlineData("yyxyzxxyzxyz", "y^5x^4z^3")]
        [InlineData("-2a^2b^1c^3cca", "-2c^5a^3b")]
        [InlineData("-1.1", "-1.1")]
        [InlineData("+23xxxx", "23x^4")]
        [InlineData("-0abx", "0")]
        [InlineData("-1.2xyza^2b^1c^7", "-1.2c^7a^2bxyz")]
        [InlineData("1bab", "b^2a")]
        [InlineData("-1abababb", "-b^4a^3")]
        [InlineData("1.00001ba", "1.00001ab")]
        [InlineData("0", "0")]
        [InlineData("a", "a")]
        [InlineData("-0", "0")]
        public void ToString_NormalizeSummand_ReturnToStringWithSortedVariables(string initialSummandString, string expectedSummandString)
        {
            Summand actualSummand = null;

            Should.NotThrow(() => { actualSummand = SummandParser.Parse(initialSummandString)?.Normalize(); });

            actualSummand.ToString().ShouldBe(expectedSummandString);
        }


        [Theory]
        [InlineData("-y^5x^4z^3", "y^5x^4z^3")]
        [InlineData("+2c^5a^3b", "-2c^5a^3b")]
        [InlineData("+1.1", "-1.1")]
        [InlineData("-23x^4", "23x^4")]
        [InlineData("0", "0")]
        [InlineData("+1.2c^7a^2bxyz", "-1.2c^7a^2bxyz")]
        [InlineData("-b^2a", "b^2a")]
        [InlineData("+b^4a^3", "-b^4a^3")]
        [InlineData("-1.00001ab", "1.00001ab")]
        [InlineData("-a", "a")]
        public void Negate_ReturnNegateVersionOfSummand(string initialSummandString, string expectedSummandString)
        {
            Summand actualSummand = null;

            Should.NotThrow(() => { actualSummand = SummandParser.Parse(initialSummandString)?.Normalize(); });

            var negateSummand = actualSummand.Negate();

            negateSummand.Multiplier.ShouldBe(-actualSummand.Multiplier);
            negateSummand.ToString().ShouldBe(expectedSummandString);
        }
    }
}