using System;
using System.Collections.Generic;
using System.Linq;
using CanonicalEquation.Extensions;

namespace CanonicalEquation
{
    public class Polynomial
    {
        public IEnumerable<Monomial> Monomials { get; } = new List<Monomial>();

        public Polynomial(IEnumerable<Monomial> monomials)
        {
            if (monomials == null) throw new ArgumentNullException(nameof(monomials));

            Monomials = new List<Monomial>(monomials);
        }

        public Polynomial Negate()
        {
            return new Polynomial(Monomials.Select(s => s.Negate()));
        }

        public override string ToString()
        {
            var monomialItemsString =
                Monomials
                    .Select(x => x.ToString())
                    .Where(x => !x.IsNullOrWhiteSpace())
                    .Select((item, index) =>
                    {
                        if (index == 0 && item[0] == SymbolsConsts.Plus)
                        {
                                return item.Substring(1);
                        }
                        return item;
                    }).ToArray();
            

            return monomialItemsString.Any() ? monomialItemsString.CollectionToStringWithSeparator(String.Empty) : "0";
        }
    }
}