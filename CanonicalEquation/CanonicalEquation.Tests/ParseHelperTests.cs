using System;
using System.Collections.Generic;
using System.Linq;
using CanonicalEquation.Lib.Exceptions;
using CanonicalEquation.Lib.Helpers;
using Shouldly;
using Xunit;

namespace CanonicalEquation.Lib.Tests
{
    public class ParseHelperTests
    {
        [Theory]
        [InlineData("x-1", "x-1")]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("z+9)", "z+9)")]
        [InlineData("(c+9", "(c+9")]
        public void GetContentForBrackets_StringWithoutBrackets_ReturnStringItself(string inputString, string outputString)
        {
            var bracketsContent = ParseHelper.GetContentForBrackets(inputString);

            bracketsContent.ShouldBe(outputString);
        }

        [Theory]
        [InlineData("(c+9)", "c+9")]
        [InlineData("()", "")]
        [InlineData("( )", " ")]
        [InlineData("(())", "()")]
        public void GetContentForBrackets_StringWithBrackets_ReturnValidContent(string inputString, string outputString)
        {
            var bracketsContent = ParseHelper.GetContentForBrackets(inputString);

            bracketsContent.ShouldBe(outputString);
        }



        public static IEnumerable<object[]> ValidPolynomialStringWithExpectedMonomialParts =>
            new[]
            {
                new object[] { "x^2y^2",  new[]{ "x^2y^2" } },
                new object[] { "x^2y^2- yx",  new[]{ "x^2y^2", "-yx" } },
                new object[] { " + x^2yz- x(y- z ^2)",  new[]{ "+x^2yz", "-x(y-z^2)" } },
                new object[] { "x + 2 - ((a))",  new[]{ "x", "+2", "-((a))" } },
                new object[] { "a + b -",  new[]{ "a", "+b"} },
            };

        [Theory, MemberData(nameof(ValidPolynomialStringWithExpectedMonomialParts))]
        public void GetMonomialParts_ValidPolynomialInputString_ReturnValidParts(string inputString, string[] expectedOutputStrings)
        {
            var monomialParts = ParseHelper.GetMonomialssForPolynomial(inputString).ToList();

            monomialParts.ShouldNotBeNull();
            monomialParts.Count().ShouldBe(expectedOutputStrings.Length);

            var sortedActualResult = monomialParts.OrderBy(x => x).ToArray();
            var sortedExpectedOutputStrings = expectedOutputStrings.OrderBy(x => x).ToArray();

            for (int i = 0; i < sortedExpectedOutputStrings.Length; i++)
            {
                sortedActualResult[i].ShouldBe(sortedExpectedOutputStrings[i]);
            }
        }
        public static IEnumerable<object[]> ValidMonomialStringWithExpectedMonomialParts =>
            new[]
            {
                new object[] { "x^2y^2",  new[]{ "x^2y^2" }, new string[] { }},
                new object[] { "x^2y^2(x+2)",  new[]{ "x^2y^2"}, new[]{ "x+2" } },
                new object[] { "- (a- b)",  new[]{ "-1" }, new[] { "a-b" } },
                new object[] { "-x (a- b) y (b-   c^ 2b) z (( -a))",  new[]{ "-x", "y", "z" }, new[] { "a-b", "b-c^2b", "(-a)" } },
                new object[] { "+10",  new[]{ "+10" }, new string[] { }},
                new object[] { "abc",  new[]{ "abc" }, new string[] { }},
                new object[] { "-",  new[]{ "-1" }, new string[] { }},

            };

        [Theory, MemberData(nameof(ValidMonomialStringWithExpectedMonomialParts))]
        public void GetMonomialParts_ValidMonomialInputString_ReturnValidParts(string inputString, string[] expectedSummandsStrings, string[] expectedBracketsStrings)
        {
            var result = ParseHelper.GetSummandsForMonomial(inputString);

            result.summandItems.ShouldNotBeNull();
            result.bracketsParts.ShouldNotBeNull();

            result.summandItems.Count().ShouldBe(expectedSummandsStrings.Length);
            result.bracketsParts.Count().ShouldBe(expectedBracketsStrings.Length);


            var sortedActualSummandResult = result.summandItems.OrderBy(x => x).ToArray();
            var sortedExpectedSummandOutputStrings = expectedSummandsStrings.OrderBy(x => x).ToArray();

            for (int i = 0; i < sortedExpectedSummandOutputStrings.Length; i++)
            {
                sortedActualSummandResult[i].ShouldBe(sortedExpectedSummandOutputStrings[i], $"Expected summand item {sortedExpectedSummandOutputStrings[i]} not equals actual {sortedActualSummandResult[i]}");
            }

            var sortedActualBracketsResult = result.bracketsParts.OrderBy(x => x).ToArray();
            var sortedExpectedBracketsOutputStrings = expectedBracketsStrings.OrderBy(x => x).ToArray();

            for (int i = 0; i < sortedExpectedBracketsOutputStrings.Length; i++)
            {
                sortedActualBracketsResult[i].ShouldBe(sortedExpectedBracketsOutputStrings[i], $"Expected brackets item {sortedExpectedBracketsOutputStrings[i]} not equals actual {sortedActualBracketsResult[i]}");
            }
        }

        [Theory]
        [InlineData("((c+9)")]
        [InlineData("(()))")]
        [InlineData("( ")]
        [InlineData(")(as + asd ))")]
        public void ValidateStringWithBrackets_NotValidBrackets_ThrowNotValidBracketArgumentException(string inputString)
        {
            Should.Throw<NotValidBracketArgumentException>(() =>
            {
                ParseHelper.ValidateBrackets(inputString);
            });
        }

        [Theory]
        [InlineData("(as + asd)")]
        [InlineData("(6 - (2))")]
        [InlineData("( )")]
        [InlineData("(((as as + asd)))")]
        public void ValidateStringWithBrackets_ValidBrackets_DoNotThrowExceptions(string inputString)
        {
            Should.NotThrow(() =>
            {
                ParseHelper.ValidateBrackets(inputString);
            });
        }

    }


}