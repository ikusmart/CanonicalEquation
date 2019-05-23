using System;
using System.Collections.Generic;
using System.Linq;

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
        
        public override string ToString()
        {
            return String.Join(String.Empty, Monomials.Select(x => x.ToString()));
        }
    }
}