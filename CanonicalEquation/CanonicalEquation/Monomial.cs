using System;
using System.Collections.Generic;
using System.Linq;
using CanonicalEquation.Extensions;

namespace CanonicalEquation
{
    /// <summary>
    /// Part of polynomial.
    /// Single part can be summand or equation inside brackets
    /// </summary>
    public class Monomial
    {
        public IEnumerable<MonomialItem> MonomialItems { get; } = new List<MonomialItem>();

        public IEnumerable<Summand> SummandItems => MonomialItems?.OfType<Summand>();
        public IEnumerable<Brackets> BracketsItems =>  MonomialItems?.OfType<Brackets>();

        public Monomial(IEnumerable<MonomialItem> monomialItems, int multiplier = 1)
        {
            MonomialItems = monomialItems ?? throw new ArgumentNullException(nameof(monomialItems));
        }

        public override string ToString()
        {
            return MonomialItems?.Any() == true
                ? $"{SummandItems.CollectionToStringWithSeparator(String.Empty)}{BracketsItems.CollectionToStringWithSeparator(String.Empty)}"
                : String.Empty;
        }

        public Monomial Negate()
        {
            var summands = SummandItems.ToArray();
            if (summands.Length > 0)
            {
                summands[0].Multiplier = -summands[0].Multiplier;
            }
            else
            {
                summands = new[] { new Summand(-1) };
            }
            var brackets = BracketsItems;

            return new Monomial(summands.OfType<MonomialItem>().Union(brackets));
        }
    }
}