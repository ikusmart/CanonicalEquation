using System;
using System.Collections.Generic;
using CanonicalEquation.Parsers;
using Shouldly;
using Xunit;

namespace CanonicalEquation.Tests
{
    public class MonomialTests
    {

        public static IEnumerable<object[]> MonomialForNegateTestData =>
            new[]
            {
                new object[] { "x^2y^2", "-x^2y^2"  },
                new object[] { "x^2y^3(x+2)", "-y^3x^2(x+2)"  },
                new object[] { "- (a- b)", "(a-b)" },
                new object[] { "-x (a- b) y (b-   c^ 2b) z (( -a))", "+xyz(a-b)(b-c^2b)((-a))"  },
                new object[] { "+10", "-10" },
                new object[] { "abc", "-abc" },
                new object[] { "ab(a+b+)", "-ab(a+b)" },
                new object[] { "-", "+1" },
                new object[] { "0", "0" },

            };
        [Theory, MemberData(nameof(MonomialForNegateTestData))]

        public void NegateMonomial_ReturnNegatedObject(string initialMonomialString, string expectedMonomial)
        {
            Monomial parseResult = null;
            Should.NotThrow(() =>
            {
                parseResult = MonomialParse.Parse(initialMonomialString).Negate();
            });

            parseResult.ToString().ShouldBe(expectedMonomial);
        }


    }
}