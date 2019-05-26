using System;
using System.Collections.Generic;
using System.Linq;
using CanonicalEquation.Extensions;

namespace CanonicalEquation.Entities
{
    public class Polynomial
    {
        public IEnumerable<Summand> Summands { get; set; }

        public Polynomial(IEnumerable<Summand> summands)
        {
            Summands = summands ?? new List<Summand>();
        }

        public Polynomial Normalize()
        {
            return new Polynomial(Summands.Select(x => x.Normalize()).Sum());
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
            //return Summands.CollectionToStringWithSeparator(String.Empty);

            var nonZeroSummands = Summands.Where(s => Math.Abs(s.Multiplier) > float.Epsilon).ToList();

            if (nonZeroSummands.Count == 0) return "0";

            var summands = nonZeroSummands.Select(
                (summand, index) =>
                    index == 0
                        ? summand.ToString() /* as is, plus won't show, minus will */
                        : Math.Sign(summand.Multiplier) > 0
                            ? "+" + summand.ToString() /* append plus */
                            : summand.ToString()) /* the minus will appear anyways */;

            return string.Join(string.Empty, summands);

            return Summands.CollectionToStringWithSeparator(String.Empty);
        }
    }
}