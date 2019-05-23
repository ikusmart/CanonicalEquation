using System;
using CanonicalEquation.Extensions;

namespace CanonicalEquation.Parsers
{
    public static class MonomialParse
    {

        /// <summary>
        /// Consists of a collection of expressions in brackets (if there are in the string) and a list of summands (contained between brackets, if any).
        /// If there are no brackets, then the monomial consists of one summand)
        /// </summary>
        /// <param name="monomialString"></param>
        /// <returns></returns>
        public static Monomial Parse(string monomialString)
        {
            if(monomialString.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(monomialString));


            
            return new Monomial(monomialString);
        }

        


    }
}