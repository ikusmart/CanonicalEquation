using System.Linq;
using CanonicalEquation.Entities;
using CanonicalEquation.Parsers;

namespace CanonicalEquation.Tests
{
    public static class TestHelper
    {
        public static Summand NewSummand(float multiplier = 1, params (char name, int power)[] variables)
            => new Summand(variables.Length > 0 ? variables.Select(x => new Variable(x.name, x.power)) : null, multiplier);
    }

}