using System;

namespace CanonicalEquation
{
    /// <summary>
    /// Part of polynomial. 
    /// </summary>
    public class Monomial
    {
        private readonly string _initialMonomialString;

        public Monomial(string initialMonomialString)
        {
            _initialMonomialString = initialMonomialString;
        }

        public override string ToString()
        {
            return _initialMonomialString ?? String.Empty;
        }
    }
}