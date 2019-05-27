using System.Linq;
using CanonicalEquation.Lib.Entities;
using CanonicalEquation.Lib.Parsers;

namespace CanonicalEquation.Lib.Tests
{
    public static class TestHelper
    {
        public static Summand NewSummand(float multiplier = 1, params (char name, int power)[] variables)
            => new Summand(variables.Length > 0 ? variables.Select(x => new Variable(x.name, x.power)) : null, multiplier);
    }

}