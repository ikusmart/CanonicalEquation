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
                },
                new object[]
                {
                    TestHelper.NewSummand(-5, ('x', 2), ('x', 2), ('z', 4)),
                    TestHelper.NewSummand(-5, ('x', 2), ('x', 2), ('z', 4)),
                    TestHelper.NewSummand(25, ('x', 8), ('z', 8))
                },
                new object[]
                {
                    TestHelper.NewSummand(3.5f, ('x', 1), ('z', 0), ('y', 2)),
                    TestHelper.NewSummand(3.5f, ('x', 2), ('y', 1), ('a', 0)),
                    TestHelper.NewSummand(12.25f, ('x', 3), ('y', 3))
                },
                new object[]
                {
                    TestHelper.NewSummand(0), TestHelper.NewSummand(-2, ('a', 1), ('y', 5), ('x', 4)), TestHelper.NewSummand(0)
                },
                new object[]
                {
                    TestHelper.NewSummand(), TestHelper.NewSummand(-2), TestHelper.NewSummand(-2)
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

        public static IEnumerable<object[]> NullParametersForMultiplicatonSingleSummandTestCases =>
            new[]
            {
                new object[]
                {
                    default(Summand),
                    TestHelper.NewSummand(1, ('a', 1)),
                    default(Summand),
                },
                new object[]
                {
                    TestHelper.NewSummand(-81, ('a', 1),  ('z', 34)),
                    default(Summand),
                    default(Summand),
                },
            };
        [Theory, MemberData(nameof(NullParametersForMultiplicatonSingleSummandTestCases))]

        public void MultiplicationSingleSummandOperation_ParametersIsNullOrEmply_ReturnEmptyList(Summand left, Summand right, Summand expectedSummand)
        {
            Summand actualSummand = new Summand();
            Should.NotThrow(() => { actualSummand = left * right; });
            actualSummand.ShouldBeNull();
        }

        public static IEnumerable<object[]> ValidSummandsForMultiplicationTestCases =>
            new[]
            {
                new object[]
                {
                    
                    new[]
                    {
                        TestHelper.NewSummand(5, ('x', 1)),
                        TestHelper.NewSummand(-4, ('y', 0)),
                    },
                    new[]
                    {
                        TestHelper.NewSummand(3, ('x', 1)),
                        TestHelper.NewSummand(-2),
                    },
                    //
                    new[]
                    {
                        TestHelper.NewSummand(15, ('x', 2)),
                        TestHelper.NewSummand(-10, ('x', 1)),
                        TestHelper.NewSummand(-12, ('x', 1)),
                        TestHelper.NewSummand(8),
                    },
                },
                new object[]
                {
                   
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

        [Theory, MemberData(nameof(ValidSummandsForMultiplicationTestCases))]
        public void MultiplicationOperation_CorrectSummand_ReturnMultiplicatedSummad(IEnumerable<Summand> left, IEnumerable<Summand> right, IEnumerable<Summand> expectedResultSummands)
        {
            IEnumerable<Summand> actualMultiplicatedSummands = null;
            Should.NotThrow(() => { actualMultiplicatedSummands = EquationOperation.Multiply(left, right);});

            actualMultiplicatedSummands.ShouldNotBeNull();
            
            var sortedActualResult = actualMultiplicatedSummands.OrderBy(x => x.UniqueId).ToArray();
            var sortedExpectedResult = expectedResultSummands.OrderBy(x => x.UniqueId).ToArray();

            for (int i = 0; i < sortedActualResult.Length; i++)
            {
                sortedActualResult[i].ToString().ShouldBe(sortedExpectedResult[i].ToString());
            }
        }

        public static IEnumerable<object[]> NullParametersForMultiplicatonTestCases =>
            new[]
            {
                new object[]
                {
                    default(Summand[]),
                    new[]{ TestHelper.NewSummand(1, ('a', 1)), TestHelper.NewSummand(2, ('b', 1)) },
                    new Summand[] {}
                },
                new object[]
                {
                    new[]{ TestHelper.NewSummand(1, ('a', 1)), TestHelper.NewSummand(2, ('b', 1)) },
                    default(Summand[]),
                    new Summand[] {}
                },
                new object[]
                {
                    new[]{ TestHelper.NewSummand(1, ('a', 1)), TestHelper.NewSummand(2, ('b', 1)) },
                    new Summand[] {},
                    new Summand[] {}
                },
                new object[]
                {
                    new Summand[] {},
                    new[]{ TestHelper.NewSummand(1, ('a', 1)), TestHelper.NewSummand(2, ('b', 1)) },
                    new Summand[] {}
                }
            };
        [Theory, MemberData(nameof(NullParametersForMultiplicatonTestCases))]
        public void MultiplicationOperation_ParametersIsNullOrEmply_ReturnEmptyList(IEnumerable<Summand> left, IEnumerable<Summand> right, IEnumerable<Summand> expectedResultSummands)
        {
            IEnumerable<Summand> actualMultiplicatedSummands = null;
            Should.NotThrow(() => { actualMultiplicatedSummands = EquationOperation.Multiply(left, right); });
            actualMultiplicatedSummands.ShouldNotBeNull();
            actualMultiplicatedSummands.Count().ShouldBe(0);
        }




        public static IEnumerable<object[]> ValidSummandsForSumOperationTestCases =>
            new[]
            {
                new object[]
                {
                    
                    new[]
                    {
                        TestHelper.NewSummand(3, ('x', 2)),
                        TestHelper.NewSummand(-5, ('y', 3), ('x', 2)),
                        TestHelper.NewSummand(4, ('y', 4)),
                        TestHelper.NewSummand(-2, ('z', 5)),
                        TestHelper.NewSummand(-2, ('q', 5)),
                        TestHelper.NewSummand(-2),
                        TestHelper.NewSummand(-15, ('x', 2)),
                        TestHelper.NewSummand(9, ('y', 3), ('x', 2)),
                        TestHelper.NewSummand(-10, ('z', 5)),
                        TestHelper.NewSummand(2, ('q', 5)),
                        TestHelper.NewSummand(0, ('c', 9)),
                        TestHelper.NewSummand(8),
                    },
                    new[]
                    {
                        TestHelper.NewSummand(-12, ('x', 2)),
                        TestHelper.NewSummand(4, ('y', 3), ('x', 2)),
                        TestHelper.NewSummand(4, ('y', 4)),
                        TestHelper.NewSummand(-12, ('z', 5)),
                        TestHelper.NewSummand(6),
                    },
                },
                new object[]
                {

                    new[]
                    {
                        TestHelper.NewSummand(3),
                        TestHelper.NewSummand(22),
                        TestHelper.NewSummand(5),
                    },
                    new[]
                    {
                        TestHelper.NewSummand(30),
                    },
                },
                new object[]
                {
                    default(Summand[]),
                    new Summand[] { }
                },
                new object[]
                {
                    new Summand[] { },
                    new Summand[] { }
                },
            };

        [Theory, MemberData(nameof(ValidSummandsForSumOperationTestCases))]
        public void SumOperation_CorrectSummand_ReturnSummedSummad(IEnumerable<Summand> inputSummands, IEnumerable<Summand> expectedSumSummands)
        {
            IEnumerable<Summand> actualMultiplicatedSummands = null;
            Should.NotThrow(() => { actualMultiplicatedSummands = inputSummands.Sum(); });

            actualMultiplicatedSummands.ShouldNotBeNull();

            var sortedActualResult = actualMultiplicatedSummands.OrderBy(x => x.UniqueId).ToArray();
            var sortedExpectedResult = expectedSumSummands.OrderBy(x => x.UniqueId).ToArray();

            for (int i = 0; i < sortedActualResult.Length; i++)
            {
                sortedActualResult[i].ToString().ShouldBe(sortedExpectedResult[i].ToString());
            }
        }
    }
}