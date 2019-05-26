using System.Collections.Generic;

namespace CanonicalEquation.Entities
{
    public class Monomial
    {
        public IEnumerable<Summand> Summands { get; }

        public Monomial() : this(null)
        {
        }

        public Monomial(IEnumerable<Summand> summands)
        {
            Summands = summands ?? new List<Summand>();
        }
    }
}