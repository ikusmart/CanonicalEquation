using System.Collections.Generic;
using System.Linq;
using CanonicalEquation.Lib.Entities;
using Shouldly;
using Xunit;

namespace CanonicalEquation.Lib.Tests
{
    public class EquationOperationTests
    {
        public static IEnumerable<object[]> MultiplicationTwoSummandsTestData =>
            new[]
            {
                new object[]
                {
                    TestHelper.NewSummand(1, ('x', 4), ('y', 8), ('z', 3)),
                    TestHelper.NewSummand(-2, ('a', 1), ('y', 5), ('x', 4)),
                    TestHelper.NewSummand(-2, ('x', 8), ('y', 13), ('z', 3), ('a', 1))

                }
            };

        [Theory, MemberData(nameof(MultiplicationTwoSummandsTestData))]

        public void SummandMultiplication_TwoNotEmptySummands_ReturnValidSummandEntity(Summand left, Summand right, Summand expectedSummand)
        {
            Summand actualSummand = new Summand();
            Should.NotThrow(() => { actualSummand = left * right; });

            actualSummand.Multiplier.ShouldBe(expectedSummand.Multiplier);
            actualSummand.Variables.Count().ShouldBe(expectedSummand.Variables.Count());


            var sortedActualResult = actualSummand.Variables.OrderBy(x => x.Name).ThenBy(x => x.Power).ToArray();
            var sortedExpectedResult = expectedSummand.Variables.OrderBy(x => x.Name).ThenBy(x => x.Power).ToArray();

            for (int i = 0; i < sortedActualResult.Length; i++)
            {
                var expectedValue = sortedExpectedResult[i];
                sortedActualResult[i].Name.ShouldBe(expectedValue.Name);
                sortedActualResult[i].Power.ShouldBe(expectedValue.Power);
            }

            actualSummand.ToString().ShouldBe(expectedSummand.ToString());
        }

        public static IEnumerable<object[]> ValidVariableTestCases =>
            new[]
            {
                new object[]
                {
                    //(-6a-b^2)(b^3-a^2)
                    new[]
                    {
                        TestHelper.NewSummand(-6, ('a', 1)),
                        TestHelper.NewSummand(-1, ('b', 2)),
                    },
                    new[]
                    {
                        TestHelper.NewSummand(1, ('b', 3)),
                        TestHelper.NewSummand(-1, ('a', 2)),
                    },
                    //
                    new[]
                    {
                        TestHelper.NewSummand(-6, ('a', 1), ('b', 3)),
                        TestHelper.NewSummand(6, ('a', 3)),
                        TestHelper.NewSummand(-1, ('b', 5)),
                        TestHelper.NewSummand(1, ('a', 2),('b', 2)),
                    },
                },
            };

        [Theory, MemberData(nameof(ValidVariableTestCases))]
        public void MultiplicationOperation_CorrectSummand_ReturnMultiplicatedSummad(IEnumerable<Summand> left, IEnumerable<Summand> right, IEnumerable<Summand> expectedResultSummands)
        {
            IEnumerable<Summand> actualMultiplicatedSummands = null;
            Should.NotThrow(() => { actualMultiplicatedSummands = EquationOperation.Multiply(left, right);});

            actualMultiplicatedSummands.ShouldNotBeNull();
            
            var sortedActualResult = actualMultiplicatedSummands.OrderBy(x => x.UniqueId).ToArray();
            var sortedExpectedResult = expectedResultSummands.OrderBy(x => x.UniqueId).ToArray();

            for (int i = 0; i < sortedActualResult.Length; i++)
            {
                var expectedValue = sortedExpectedResult[i];
                sortedActualResult[i].ShouldBe(sortedExpectedResult[i]);
            }
        }

    }
}