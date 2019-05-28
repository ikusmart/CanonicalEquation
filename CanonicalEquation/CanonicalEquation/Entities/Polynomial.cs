using System;
using System.Collections.Generic;
using System.Linq;
using CanonicalEquation.Lib.Extensions;

namespace CanonicalEquation.Lib.Entities
{
    public class Polynomial
    {
        public IEnumerable<Summand> Summands { get; }

        public Polynomial(IEnumerable<Summand> summands = null)
        {
            Summands = summands ?? new List<Summand>();
        }

        public Polynomial Normalize()
        {
            var normalizedSummands = 
                    Summands
                        .Select(x => x.Normalize())
                        .Sum()
                        .OrderByDescending(x => x.MaxPower)
                        .ThenByDescending(x => x.TotalPower)
                        .ThenBy(x => x.VariableNames);
            return new Polynomial(normalizedSummands);
        }

        public Polynomial Negate() => new Polynomial(Summands.Select(x => x.Negate()));

        public static Polynomial operator -(Polynomial polynomial)
        {
            return polynomial.Negate();
        }

        public static Polynomial operator -(Polynomial left, Polynomial right)
        {
            var summandNews = left.Summands.Concat((-right).Summands).Sum();
            return new Polynomial(summandNews);
        }

        public override string ToString()
        {
            var nonZeroSummands = Summands.Where(s => Math.Abs(s.Multiplier) > float.Epsilon).ToList();
            if (nonZeroSummands.Count == 0) return "0";

            var summands = nonZeroSummands.Select(
                (summand, index) =>
                    index == 0
                        ? summand.ToString()
                        : Math.Sign(summand.Multiplier) > 0
                            ? "+" + summand.ToString()
                            : summand.ToString());

            return summands.CollectionToStringWithSeparator(String.Empty);
        }
    }
}